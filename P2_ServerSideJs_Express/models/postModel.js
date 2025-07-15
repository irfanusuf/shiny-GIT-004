
const mongoose = require("mongoose")  


const Post =  mongoose.model("Post" , {

title : String,
imageUrl : String,
desc : String


})



module.exports = {Post}

