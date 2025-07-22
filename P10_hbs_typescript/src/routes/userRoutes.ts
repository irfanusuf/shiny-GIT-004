
import express from "express"
import { Request, Response } from "express";
import * as userController from "../controllers/userController"


const router = express.Router()



router.get("/register" , (req: Request, res: Response) => {res.render("register" , {pageTitle : "Register"}) })
router.get("/login" , (req: Request, res: Response) => {res.render("login" , {pageTitle : "Login"})})

router.post("/register" , userController.handleRegister )   
router.post("/login" , userController.handleLogin )   
router.post("/forgot-pass" , userController.forgotPass )   



export default router