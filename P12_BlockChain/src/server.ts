import express, { Request, Response } from "express";
import { Wallet } from "./Wallet";
import { Blockchain } from "./BlockChain";
import { Transaction } from "./Transaction";

const app = express()
const port = 4000


const chain = new Blockchain()


app.use(express.json())


app.get("/", (req: Request, res: Response) => {
    res.json({ chain: chain.chain })
})


app.post("/create/wallet", (req: Request, res: Response) => {
    const wallet = new Wallet()
    res.status(200).json(wallet)
})


app.get("/getBalances/:block", (req: Request, res: Response) => {


    const block: number = parseInt(req.params.block)


    res.status(200).json({ balanceSheet: Object.fromEntries(chain.chain[block].balanceSheet) })
})

app.post("/new/transaction" , (req: Request, res: Response)=>{

    const {fromAddress , toAddress , amount , privateKey} = req.body
    const newTx = new Transaction(fromAddress , toAddress , amount)
    newTx.signTransaction(privateKey)
    chain.chain[0].balanceSheet.set(toAddress , amount)
    res.status(200).json({message : "transaction succesful !"})


})



app.listen(port, () => { console.log("blockchain server listening!") })