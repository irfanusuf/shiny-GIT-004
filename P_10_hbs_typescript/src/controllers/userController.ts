
import { Request, Response } from "express";



export async function handleLogin  (req : Request,res : Response){


try {
    
console.log("request incoming")

const {email , password} = req.body
console.log(email)
console.log(password)

// Send a response to the client to use 'res'
// res.status(200).json({ message: "Login request received", email, password });

} catch (error) {
    console.error(error)
    res.status(500).json({ error: "Internal server error" });
}

}



export  async function handleRegister (req :Request , res : Response){





}