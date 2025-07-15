import { connectDb } from "../../../config/connectDb";
import { User } from "../../../models/userModel";
import bcrypt from "bcryptjs";
import jwt from "jsonwebtoken";

const register = async (req, res) => {
  try {
    if (req.method !== "POST") {
      return res.status(400).json({ message: "Only Post method Allowed!" });
    }

    await connectDb();

    const { username, email, password } = req.body;

    if (username === "" || email === "" || password === "") {
      return res.status(400).json({ message: "All Feilds are required!" });
    }

    const user = await User.findOne({ email });

    if (user) {
      return res.status(400).json({ message: "User already Exists!" });
    }

    const passEncrypt = await bcrypt.hash(password, 10);

    const newUser = await User.create({
      username,
      email,
      password: passEncrypt,
    });

    if (newUser) {
      const userId = newUser._id;

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
        .json({ message: "User Created Succesfully!", token: token });
    } else {
      return res.status(500).json({ message: "Something Went Wrong" });
    }
  } catch (error) {
    console.error(error);
    return res.status(500).json({ message: "Server Error!" });
  }
};

export default register;
