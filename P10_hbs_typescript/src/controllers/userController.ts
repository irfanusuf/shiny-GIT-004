
import { Request, Response } from "express";



export async function handleLogin(req: Request, res: Response) {


    try {

        console.log("request incoming")

        const { email, password } = req.body
     
        if(email !== "" && password !== ""){

            res.render("login" ,  {pageTitle : "Login" , message : "Login Succesful!"})
        }



    } catch (error) {
        console.error(error)
        res.status(500).json({ error: "Internal server error" });

    }

}



export async function handleRegister(req: Request, res: Response) {





}


export async function forgotPass(req: Request, res: Response) {





}