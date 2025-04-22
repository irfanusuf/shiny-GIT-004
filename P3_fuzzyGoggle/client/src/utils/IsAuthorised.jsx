import { useEffect } from "react";
import { axiosInstance } from "./axiosInstance";
import { useNavigate } from "react-router-dom";

const IsAuthorised = () => {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      if (!token || token === "undefined" || token === "null") {
        navigate("/login");
        return;
      }

      const verifyToken = async () => {
        try {
          const res = await axiosInstance.get(`user/verify/${token}`);

          if (res.status === 200) {
            return true;
          } else {
            return false;
          }
        } catch (error) {
          console.error("Error verifying token:", error);
          return false;
        }
      };

      const verify = await verifyToken();

      if (!verify) {
        navigate("/login");
      }
    })();
  }, [token, navigate]);
};

export default IsAuthorised;
