
import express from "express";
import { Request, Response } from "express";
import path from "path"
import { fileURLToPath } from "url";

import bodyParser from "body-parser";
import xhbs from "express-handlebars"
import userRoutes from "./routes/userRoutes";
import adminRoutes from "./routes/adminRoutes";


const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const port :number = 4000
const app = express()

//middlewares
// app.use(express.json())

app.use(bodyParser.json())
app.use(bodyParser.urlencoded({extended :true}))
app.use(express.static(path.join("public")))
/// by default view engine is  HTML   
app.set("view engine" , "hbs")  
app.set("views", path.join(__dirname, "views", "pages"));   

// now we can render hbs files 
app.engine("hbs" , xhbs.engine({
    extname: "hbs",
    defaultLayout: "layout",
    layoutsDir : path.join( __dirname , "views" , "layout"),
    partialsDir : path.join(  __dirname ,"views" , "partials"),
    helpers : {
        // functions which can be used inside html / hbs
    }
}))



// routing 

app.get("/", (req: Request, res: Response) => {res.render("index" , {pageTitle : "Home" , username : "javeed"})});

app.use("/user" , userRoutes)
app.use("/admin" , adminRoutes)





app.listen(port, () => { console.log(`Server listening on port ${port}`) })

