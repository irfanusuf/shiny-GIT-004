import express, { Request, Response } from "express";
import { Wallet } from "./Wallet";
import { Blockchain } from "./BlockChain";
import { Transaction } from "./Transaction";
import { Block } from "./Block";

import fs from 'fs';
import path from 'path';


const app = express();
const port = 4000;

const BLOCK_CHAIN = path.join('chain.json');



// we  ccan either  Load chain from file or create new

let chain = Blockchain.loadFromFile(BLOCK_CHAIN);


function valid  () {
if (chain.isChainValid())  return "Chain Validation Succesfull ✅ !"
}  

console.log("Vaidating..." + " " + valid() )

let latestBlock = chain.getLatestBlock()




app.use(express.json());

app.get("/", (req: Request, res: Response) => {
  res.json({ chain: chain.chain });
});





app.post("/create/wallet", (req: Request, res: Response) => {
  const wallet = new Wallet();
  res.status(200).json(wallet);
});




app.get("/BalanceSheet/:block", (req: Request, res: Response) => {
  const block: number = parseInt(req.params.block);
  res
    .status(200)
    .json({
      balanceSheet: Object.fromEntries(chain.chain[block].balanceSheet),
    });
});




app.get("/Latest/BalanceSheet", (req: Request, res: Response) => {
  res.status(200).json({ balanceSheet: Object.fromEntries(latestBlock.balanceSheet) })
})




app.get("/balance/:walletAddress", (req: Request, res: Response) => {

  const walletAddress: string = req.params.walletAddress
  res.status(200).json({ balance: latestBlock.balanceSheet.get(walletAddress) })

})




app.post("/new/transaction", (req: Request, res: Response) => {
  const { fromAddress, toAddress, amount, privateKey } = req.body;

  const newTx = new Transaction(fromAddress, toAddress, amount);
  newTx.signTransaction(privateKey);



  const payerBalance = latestBlock.balanceSheet.get(fromAddress) || 0;

  const payeeBalance = latestBlock.balanceSheet.get(toAddress) || 0;

  if (payerBalance >= amount) {


    if (latestBlock.transactionData.length < 10) {

      latestBlock.balanceSheet.set(fromAddress, payerBalance - amount);
      latestBlock.balanceSheet.set(toAddress, payeeBalance + amount);

      latestBlock.transactionData.push(newTx);
      // every time we will do a new tx new preferred hash will be generated  by using mineBlock function
      latestBlock.mineBlock(); // and a block will be mined
      chain.saveToFile(BLOCK_CHAIN)

      
       } else {

  
      const newBlock = new Block(
        latestBlock.index + 1,
        new Date().toISOString(),
        [newTx],
        latestBlock.hash,
        latestBlock.balanceSheet
      );

      chain.addBlock(newBlock);
      chain.saveToFile(BLOCK_CHAIN)
    }

    res
      .status(200)
      .json({ message: "transaction succesful !", TranseferredAmount: amount });
  } else {
    res
      .status(400)
      .json({ message: "transaction Failed !", reason: "Not enough Funds!" });
  }
});

app.listen(port, () => {
  console.log("blockchain server listening!");
});
