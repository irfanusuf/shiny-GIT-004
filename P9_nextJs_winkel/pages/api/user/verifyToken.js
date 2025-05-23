import jwt from "jsonwebtoken";

const verifyToken = async (req, res) => {

   if (req.method !== "GET") {
    return res.status(400).json({ message: "Only Get method Allowed!" });
  }

  try {
    const { token } = req.cookies;

    if (token === undefined || !token) {
      return res.status(401).json({ message: "Token not Found!" });
    }

    const verify = await jwt.verify(token, "secretKeyanyRandomString");

    if (verify) {
      res.status(200).json({ message: "Token verified!" , verify });
    } else {
      res.status(403).json({ message: "Unauthorised!" });
    }
  } catch (error) {
    console.log(error);
  }
};
 export default verifyToken