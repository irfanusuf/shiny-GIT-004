
const express = require("express")
const cors = require("cors")
const { fetchData } = require("./controllers/dataController")


const server = express()
const PORT = 4003


server.use(cors())



server.get("/" , (req,res)=>{res.send("Hello from the server. this server is made in express")})

server.post("/fetch-data" , fetchData )




server.listen(PORT , ()=>{console.log(`server listening on PORT ${PORT}`)})