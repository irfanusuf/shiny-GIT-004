import jwt from "jsonwebtoken";
import { resHandler } from "../../../utils/messageHandler";
import { User } from "../../../models/userModel";
import { connectDb } from "../../../config/connectDb";

const getUser = async (req, res) => {
  if (req.method !== "GET") {
    return res.status(400).json({ message: "Only Get method Allowed!" });
  }

  try {
    await connectDb();

    const { token } = req.cookies;

    if (token === undefined || !token) {
      return resHandler(res, 401, "Unauthorised!");
    }

    const verify = await jwt.verify(token, "secretKeyanyRandomString");

    if (verify) {
      const user = await User.findById(verify.userId);

      if (user) {
        resHandler(res, 200, undefined, user);
      }
    } else {
      return resHandler(res, 403, "Forbidden!");
    }
  } catch (error) {
    console.log(error);
  }
};
export default getUser;
