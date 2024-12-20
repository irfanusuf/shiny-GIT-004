const { User } = require("../models/userModel");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken")
const { messageHandler } = require("../utils/messagehandler");

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

const loginhandler = async (req, res) => {
  try {
    const { email, password } = req.body;

    if (!email || !password) {
      return messageHandler(res , 400 , false , "Both feilds Required!");
    }

    const findUser = await User.findOne({ email }); // db query  // return user object

    if (!findUser) {
      return messageHandler(res, 404, false , "User not Found");
    }

    const comparePass = await bcrypt.compare(password, findUser.password);

    if (comparePass) {

      const userId = findUser._id
      const secretKey = "randomString"

      const token = await jwt.sign({userId} , secretKey )

      res.cookie('token', token, { httpOnly: true, maxAge: 3600000 });    // i m sending cookie directly no need of sending payload

      return  messageHandler(res, 200, true , "Logged In succesfully!" , token);     // u can just remove the token from the payload
    } else {
     return messageHandler(res, 400, false ,  "Incorrect PassWord");
    }
  } catch (error) {
    console.log(error);
  }
};






module.exports = { createUser, loginhandler };
