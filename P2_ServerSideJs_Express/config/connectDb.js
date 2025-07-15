const mongoose = require("mongoose");

// const uri = "mongodb://localhost:27017/myNewDb";  

const connectDb = async () => {
  try {
    const uri =
     "mongodb+srv://irfanusuf33:dotnet123@dotnet.2h159.mongodb.net/ExpressJs";
    const connect = await mongoose.connect(uri);

    if (connect) {
      console.log("Database connected on atlas");
    }
  } catch (error) {
    console.log(error);
  }
};


module.exports = {connectDb}