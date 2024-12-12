const express = require("express")     // import express from node modules    

const { fetchData } = require("./controller")



const PORT = 4003


const server  = express()    // inheritance

server.use(express.json())    //middleware


server.get("/" , (req, res)=>{res.send("hello from  the server ..")})


server.post("/fetch-recipes" , fetchData)


server.listen(PORT , ()=>{console.log("server listenting on the port 4003")})
