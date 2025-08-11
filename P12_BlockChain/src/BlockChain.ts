import { Block } from "./Block";
import { Ledger } from "./Ledger";

import fs from 'fs';
import path from 'path';
import { Transaction } from "./Transaction";



// Blockchain classe
export class Blockchain {

  chain: Block[];
  difficulty: number;
 
  constructor(difficulty: number = 2) {
    this.chain = [this.createGenesisBlock()];
    this.difficulty = difficulty;
     this.saveToFile(path.join('chain.json'))
  }

  private createGenesisBlock(): Block {
      const ledger = new Ledger()
      const GenesisTx = ledger.GenesisTx
      const genesisBlock = new Block(0, new Date().toISOString(), [GenesisTx] , "0" ,ledger.balanceSheet);
      genesisBlock.mineBlock(2)  
      return genesisBlock;
  }


  getLatestBlock(): Block {
    return this.chain[this.chain.length - 1];
  }

  addBlock(newBlock: Block): void {
    newBlock.previousHash = this.getLatestBlock().hash;    // block chain temper proof
    newBlock.mineBlock(this.difficulty);
    this.chain.push(newBlock);
  }

  isChainValid(): boolean {
    for (let i = 1; i < this.chain.length; i++) {



      const current = this.chain[i];

      const previous = this.chain[i - 1];

      if (current.hash !== current.calculateHash()) {
        console.error(`❌ Hash mismatch at block ${i}`);
        return false;
      }

      if (current.previousHash !== previous.hash) {
        console.error(`❌ Previous hash mismatch at block ${i}`);
        return false;
      }
    }
    return true;
  }



  // methoods for persistance     

  saveToFile(filePath: string): void {
    fs.writeFileSync(filePath, JSON.stringify(this.chain, null, 2), "utf-8");
  }

  static loadFromFile(filePath: string): Blockchain {
    if (!fs.existsSync(filePath)) {
      console.log("⛏ No existing chain found, creating new one...");
      return new Blockchain();
    }

    const data = fs.readFileSync(filePath, "utf-8");
    const parsed = JSON.parse(data);

    const blockchain = Object.create(Blockchain.prototype) as Blockchain;
    blockchain.difficulty = parsed.difficulty;
    blockchain.chain = parsed.chain.map((b: any) =>
      Blockchain.rehydrateBlock(b)
    );

    return blockchain;
  }

  private static rehydrateBlock(blockData: any): Block {
    const block = Object.assign(Object.create(Block.prototype), blockData);
    if (block.transactions) {
      block.transactions = block.transactions.map((t: any) =>
        Object.assign(Object.create(Transaction.prototype), t)
      );
    }
    return block;
  }
}

 // Instantiate and test
// const ironChain = new Blockchain(2)
// console.log(JSON.stringify(ironChain , null , 2));
// console.log(ironChain.getLatestBlock())
// const valid = ironChain.isChainValid()
// console.log("Is chain Valid : " + valid)