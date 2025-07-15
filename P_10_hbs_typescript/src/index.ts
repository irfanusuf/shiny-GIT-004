
import express from "express";
import { Request, Response } from "express";
import path from "path"
import { fileURLToPath } from "url";


const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);


const port :number = 4000



const app = express()



app.get("/", (req: Request, res: Response) => {
    res.sendFile(path.join(__dirname, "views", "index.html"));
});


app.get("/login", (req: Request, res: Response) => {
    res.sendFile(path.join(__dirname, "views", "login.html"));
});


app.get("/register", (req: Request, res: Response) => {
    res.sendFile(path.join(__dirname, "views", "register.html"));
});





app.listen(port, () => { console.log(`Server listening on port ${port}`) })

