import { Transaction } from "./Transaction";
import crypto from "crypto";

export class Block {
  index: number;
  timestamp: string;
  data: Transaction;
  previousHash: string;
  nonce: number;
  balanceSheet: Map<string, number>; // developer decision
  hash: string;

  constructor(
    index: number,
    timestamp: string,
    data: Transaction,
    previousHash: string = "",
    balanceSheet: Map<string, number>
  ) {
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
    return crypto.createHash("sha256").update(str).digest("hex"); // this is generated hash with nonce  =0
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
