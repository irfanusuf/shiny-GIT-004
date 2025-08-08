import crypto from "crypto";


export class Transaction {

  constructor(public fromAddress: string, public toAddress: string, public amount: number, public signature: string = '') { }


  calculateHash(): string {
    const str = this.fromAddress + this.toAddress + this.amount
    return crypto.createHash("sha256").update(str).digest("hex");   
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
