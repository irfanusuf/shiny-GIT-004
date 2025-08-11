import crypto from "crypto";


export class Transaction {

  public fromAddress: string
  public toAddress: string
  public amount: number
  public timeStamp : string 
  public signature: string 

  constructor(fromAddress: string, toAddress: string, amount: number) {
    this.fromAddress = fromAddress
    this.toAddress = toAddress
    this.amount = amount
    this.timeStamp = new Date().toISOString()
    this.signature = ""
  }

  calculateHash(): string {
    const str = this.fromAddress + this.toAddress + this.amount
    return crypto.createHash("sha256").update(str).digest("hex");
  }

  signTransaction(privateKey: string) {

    const sign = crypto.createSign('SHA256');

    sign.update(this.calculateHash()).end()

    const privateKeyBuffer = Buffer.from(privateKey, "base64")

    this.signature = sign.sign({
      key: privateKeyBuffer,
      format: 'der',
      type: 'pkcs8',
    }, "base64")

  }

  verifyTranscation(): boolean {
    return true
  }

}
