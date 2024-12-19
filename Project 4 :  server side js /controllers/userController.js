const { User } = require("../models/userModel");
const bcrypt = require("bcrypt");

const createUser = async (req, res) => {
  try {
    const { username, email, password } = req.body;

    if (!username || !email || !password) {
      return res.status(400).json({ message: "All details of user Required!" });
    }

    const existingUser = await User.findOne({ email });

    // console.log(existingUser)

    if (existingUser) {
      return res.status(400).json({ message: "User Already Exists!" });
    }

    const passHash = await bcrypt.hash(password, 10);

    if (passHash) {
      const newuser = await User.create({
        username,
        email,
        password: passHash,
      }); // db query

      if (newuser) {
        res.status(201).json({ message: "User Created Succesfully!" });
      }
    }
  } catch (error) {
    console.log(error);
  }
};

module.exports = { createUser };
