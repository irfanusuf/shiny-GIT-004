import { Transaction } from "./Transaction";
import { Wallet } from "./Wallet";

export  class Ledger {

  public balanceSheet: Map<string, number> = new Map();
  public genesisWallet: Wallet;
  public TOTAL_SUPPLY: number = 10000000.00   // by rule   by developer decison
  public GenesisTx : Transaction

  constructor() {
    this.genesisWallet = new Wallet()

    console.log(this.genesisWallet)

    this.GenesisTx = new Transaction(this.genesisWallet.publicKey , this.genesisWallet.publicKey , this.TOTAL_SUPPLY)  // rule 
    this.GenesisTx.signTransaction(this.genesisWallet.privateKey)
    
    this.balanceSheet.set(this.genesisWallet.publicKey, this.GenesisTx.amount);
  }

  createWallet(): Wallet {
    const wallet = new Wallet()  // wallet create
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
