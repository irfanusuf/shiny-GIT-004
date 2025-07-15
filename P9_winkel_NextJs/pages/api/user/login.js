import { connectDb } from "../../../config/connectDb";
import { User } from "../../../models/userModel";
import bcrypt from "bcryptjs";
import jwt from "jsonwebtoken";

const login = async (req, res) => {
  try {
    if (req.method !== "POST") {
      return res.status(400).json({ message: "Only Post method Allowed!" });
    }

    await connectDb();

    const {  email, password } = req.body;

    if ( email === "" || password === "") {
      return res.status(400).json({ message: "All Feilds are required!" });
    }

    const user = await User.findOne({ email });

    if (!user) {
      return res.status(400).json({ message: "User not Found!" });
    }

    const checkPass = await bcrypt.compare(password , user.password)

    if (checkPass) {
      const userId = user._id;

      const token = await jwt.sign({ userId }, "secretKeyanyRandomString", {
        expiresIn: "48h",
      });

      const expires = new Date(Date.now() + 48 * 60 * 60 * 1000).toUTCString();
      res.setHeader(
        "Set-Cookie",
        `token=${token}; HttpOnly; Path=/; SameSite=Strict; Expires=${expires}`
      );

      return res
        .status(201)
        .json({ message: "User Login Succesfully!", token: token });
    } else {
      return res.status(400).json({ message: "Password Incorrect!" });
    }
  } catch (error) {
    console.error(error);
    return res.status(500).json({ message: "Server Error!" });
  }
};

export default login;
