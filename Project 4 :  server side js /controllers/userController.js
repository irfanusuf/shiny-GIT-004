const { User } = require("../models/userModel");

const createUser = async (req, res) => {
  try {
    const { username, email, password } = req.body;

    const newuser = await User.create({ username, email, password });

    if(newuser){
        res.status(201).json({message : "User Created Succesfully!"})
    }





  } catch (error) {
    console.log(error);
  }
};


module.exports ={createUser}
