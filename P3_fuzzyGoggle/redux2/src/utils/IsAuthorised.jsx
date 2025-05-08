import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { verifyUser } from "../redux/actions/userActions";

const IsAuthorised = () => {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    (async () => {
      if (!token || token === "undefined" || token === "null") {
        navigate("/login");
        return;
      }

      const verify = dispatch(verifyUser(token));

      if (!verify) {
        navigate("/login");
      }
    })();
  }, [token, navigate , dispatch]);
};

export default IsAuthorised;
