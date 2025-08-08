import { Block } from "./Block";
import { Ledger } from "./Ledger";
import { Transaction } from "./Transaction";

// Blockchain classe
export class Blockchain {

  chain: Block[];
  difficulty: number;
 
  constructor(difficulty: number = 2) {
    this.chain = [this.createGenesisBlock()];
    this.difficulty = difficulty;
  }

  private createGenesisBlock(): Block {
      const ledger = new Ledger()
      const GenesisTx = ledger.GenesisTx
     
    return new Block(0, new Date().toISOString(), GenesisTx , "0" ,ledger.balanceSheet);
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
}

 // Instantiate and test
// const ironChain = new Blockchain(2)
// console.log(JSON.stringify(ironChain , null , 2));
// console.log(ironChain.getLatestBlock())
// const valid = ironChain.isChainValid()
// console.log("Is chain Valid : " + valid)