
const express = require("express")
const cors = require("cors")
const { fetchData } = require("./controllers/dataController")
const { connectDb } = require("./config/connectDb")
const { createUser, loginhandler } = require("./controllers/userController")
const { authorize } = require("./controllers/authController")
const { uploadSingle } = require("./utils/multer")
const { addPost } = require("./controllers/postController")


const app = express()

connectDb()   // async // once for all 

const PORT = 4003

// middle wares

app.use(cors())

app.use(express.json())   // json parsing // 1mb 



app.get("/" , (req,res)=>{res.send("Hello from the server. this server is made in express")})


app.post("/user/register" , createUser)
app.post("/user/login" , loginhandler)


app.post("/fetch-data" , authorize,  fetchData )

// app.get("/get/post" , authorize , getPost)

app.post("/add/post" ,authorize, uploadSingle , addPost)




app.listen(PORT , ()=>{console.log(`server listening on PORT ${PORT}`)})