const { Post } = require("../models/postModel");

const addPost = async (req, res) => {
  const { title, description } = req.body;

  const { image } = req.file.path; // fetching image from uploads

  /// cloudinary upload  /// return url  // save in mongodb

  const secureUrl = "";

  const newpost = await Post.create({
    title,
    description,
    imageUrl: secureUrl,
  });

  if (newpost) {
    res.status(201).json({ message: "Post created!" });
  }
};

module.exports = {
  addPost,
};
