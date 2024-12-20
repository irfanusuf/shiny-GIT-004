
const express = require("express")
const cors = require("cors")
const { fetchData } = require("./controllers/dataController")
const { connectDb } = require("./config/connectDb")
const { createUser, loginhandler } = require("./controllers/userController")


const server = express()

connectDb()   // async

const PORT = 4003


server.use(cors())
server.use(express.json())



server.get("/" , (req,res)=>{res.send("Hello from the server. this server is made in express")})

server.post("/fetch-data" , fetchData )
server.post("/user/register" , createUser)
server.post("/user/login" , loginhandler)



server.listen(PORT , ()=>{console.log(`server listening on PORT ${PORT}`)})