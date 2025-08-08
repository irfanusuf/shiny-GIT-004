import crypto from "crypto"




export  class Wallet {

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
