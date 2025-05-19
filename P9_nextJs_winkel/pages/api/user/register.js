import { connectDb } from "../../../config/connectDb";
import { User } from "../../../models/userModel";


const register = async (req, res) => {
  try {
    if (req.method !== "POST") {
      return res.status(400).json({ message: "Only Post method Allowed!" });
    }

    await connectDb();

    const { username, email, password } = req.body;

    const user = await User.findOne({ email });

    if (user) {
      res.json.status(400).json({ message: "User already Exists!" });
    }

    console.log("testingggg");
  } catch (error) {
    console.error(error);
  }
};

export default register;
