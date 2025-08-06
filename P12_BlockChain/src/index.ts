
import crypto from "crypto";


class Transaction {

  constructor(public fromAddress: string, public toAddress: string, public amount: number, public signature: string = '') { }


  calculateHash(): string {
    const str = this.fromAddress + this.toAddress + this.amount
    return crypto.createHash("sha256").update(str).digest("hex");   // this is generated hash with nonce  =0 
  }

  signTransaction(privateKey: string)  {

    const sign = crypto.createSign('SHA256');

    sign.update(this.calculateHash()).end()

    const privateKeyBuffer = Buffer.from(privateKey, "base64")

     this.signature =   sign.sign({
      key: privateKeyBuffer,
      format: 'der',
      type: 'pkcs8',
    }, "base64")

  }

  verifyTranscation(): boolean {
    return true
  }

}

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

class Ledger {

  public balanceSheet: Map<string, number> = new Map();
  public genesisWallet: Wallet;
  public TOTAL_SUPPLY: number = 10000000.00   // by rule  

  constructor() {
    this.genesisWallet = new Wallet()
    this.balanceSheet.set(this.genesisWallet.publicKey, this.TOTAL_SUPPLY);
  }

  createWallet(): Wallet {
    const wallet = new Wallet()  // waalte create
    this.balanceSheet.set(wallet.publicKey, 0)
    return wallet
  }

  getAllBalances(): Map<string, number> {
    return this.balanceSheet;
  }

  getBalance(publicKey: string): number {
    return this.balanceSheet.get(publicKey) || 0;
  }


  sendTokens(transaction: Transaction) {



  }




}

class Block {
  index: number;
  timestamp: string;
  data: Transaction;
  previousHash: string;
  nonce: number;
  balanceSheet : Map<string, number>;
  hash: string;

  constructor(index: number,timestamp: string,data: Transaction, previousHash: string = "", balanceSheet : Map<string, number>) 
  {
    this.index = index;
    this.timestamp = timestamp;
    this.data = data;
    this.previousHash = previousHash;
    this.nonce = 0;
    this.balanceSheet = balanceSheet;
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
    const ledger = new Ledger()

    return new Block(0, new Date().toISOString(), "GenesisTx" , "0" ,ledger.balanceSheet);
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
const ironChain = new Blockchain(2)



console.log(JSON.stringify(ironChain , null , 2));

// console.log(ironChain.getLatestBlock())

// const valid = ironChain.isChainValid()



// console.log("Is chain Valid : " + valid)





// const wallet = new Wallet()
// console.log("public Key : "  + wallet.publicKey)
// console.log("private Key : "  + wallet.privateKey)



// const myledger = new Ledger()

// const walletB = myledger.createWallet()

// const walletC = myledger.createWallet()


// console.log(myledger)
// console.log(walletB)
// console.log(walletC)


