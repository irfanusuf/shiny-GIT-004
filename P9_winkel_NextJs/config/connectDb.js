import mongoose from "mongoose";

export const connectDb = async () => {
    
  if (mongoose.connections[0].readyState) {
    return;
  }

  try {
    await mongoose.connect(
      "mongodb+srv://irfanusuf33:dotnet123@dotnet.2h159.mongodb.net/nextJs"
    );

    console.log("Db Connected!");
  } catch (error) {
    console.error(error);
  }
};
