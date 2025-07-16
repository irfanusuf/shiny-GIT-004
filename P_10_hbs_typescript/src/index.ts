
import express from "express";
import { Request, Response } from "express";
import path from "path"
import { fileURLToPath } from "url";
import handleLogin from "./controllers/userController"
import bodyParser from "body-parser";


const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);


const port :number = 4000



const app = express()



/// by default iska html rendering engine HTML   

app.set("view engine" , "hbs")     

// now we can render hbs files 



//middlewares
// app.use(express.json())

app.use(bodyParser.json())
app.use(bodyParser.urlencoded({extended :true}))


app.get("/", (req: Request, res: Response) => {res.render("index" , {pageTitle : "Home" , username : "javeed"})});





app.get("/login", (req: Request, res: Response) => {res.render("login") , {pageTitle : "Login"}});
app.post("/login" , handleLogin )   






app.get("/register", (req: Request, res: Response) => {res.render("register")});





app.listen(port, () => { console.log(`Server listening on port ${port}`) })

