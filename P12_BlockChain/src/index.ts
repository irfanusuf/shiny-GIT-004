
import crypto from "crypto";


type Transaction = string | object;


class Block {

  index: number;
  timestamp: string;
  data: Transaction;
  previousHash: string;
  nonce: number;
  hash: string;

  constructor(
    index: number,
    timestamp: string,
    data: Transaction,
    previousHash: string = ""
  ) {
    this.index = index;
    this.timestamp = timestamp;
    this.data = data;
    this.previousHash = previousHash;
    this.nonce = 0;
    this.hash = this.calculateHash();
  }

  calculateHash(): string {
    const str =
      this.index +
      this.previousHash +
      this.timestamp +
      JSON.stringify(this.data) +
      this.nonce;
    return crypto.createHash("sha256").update(str).digest("hex");   // this is generated hash with nonce  =0 
  }





  mineBlock(difficulty: number = 2): void {


    const targetPrefix = Array(difficulty + 1).join("0");

    const startTime = Date.now();

    while (!this.hash.startsWith(targetPrefix)) {
      this.nonce++;
      this.hash = this.calculateHash();
      console.log("⛏️ Mining... Nonce: " + this.nonce);
    }

    const endTime = Date.now();

    const timeTaken = endTime - startTime;

    console.log(`✅ Mined Block Hash: ${this.hash}`);

    console.log(`⏱️  Mining took ${timeTaken} ms`);
  }
}

//unit testing 

// const block = new Block(1 , Date.now().toString(), "Genesis Block" , "12345")
//  block.mineBlock()
// console.log(block)


// Blockchain class
class Blockchain {

  chain: Block[];
  difficulty: number;

  constructor(difficulty: number = 2) {
    this.chain = [this.createGenesisBlock()];
    this.difficulty = difficulty;
  }

  private createGenesisBlock(): Block {
    return new Block(0, new Date().toISOString(), "Genesis Block", "0");
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

// // Instantiate and test
// const ironChain = new Blockchain(2)
// ironChain.addBlock(new Block(1 , Date.now().toString() , "2nd Block"))
// ironChain.addBlock(new Block(2 , Date.now().toString() , "3nd Block"))
// ironChain.addBlock(new Block(3 , Date.now().toString() , "4nd Block"))
// ironChain.addBlock(new Block(4 , Date.now().toString() , "5nd Block"))

// const valid = ironChain.isChainValid()

// console.log(JSON.stringify(ironChain, null, 2));

// console.log("Is chain Valid : " + valid)


// wallet

class Wallet {

  public publicKey: string;
  public privateKey: string;

  constructor() {

    const { publicKey, privateKey } = crypto.generateKeyPairSync("ec", {

      namedCurve: "secp256k1",
      publicKeyEncoding: { type: "spki", format: "der" },
      privateKeyEncoding: { type: "pkcs8", format: "der" },
    })

    this.publicKey = publicKey.toString("base64")
    this.privateKey = privateKey.toString("base64")
  }
}


// const wallet = new Wallet()


// console.log("public Key : "  + wallet.publicKey)
// console.log("private Key : "  + wallet.privateKey)






class Ledger {

  public balanceSheet: Map<string, number> = new Map();
  public genesisWallet: Wallet;
  public TOTAL_SUPPLY: number = 10000000.00   // by rule  


  constructor() {
    this.genesisWallet = new Wallet()
    this.balanceSheet.set(this.genesisWallet.publicKey, this.TOTAL_SUPPLY);

  }

   getAllBalances(): Map<string, number> {
   return this.balanceSheet;
  }

}


const myledger = new Ledger() 


console.log(myledger)
