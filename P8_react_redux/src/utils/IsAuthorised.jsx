import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { verifyUser } from "../redux/actions/userActions";

const IsAuthorised = () => {
  // const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    (async () => {
      // if (!token || token === "undefined" || token === "null") {
      //   navigate("/login");
      //   return;
      // }

      const verify = await dispatch(verifyUser());

      if (!verify) {
        navigate("/login");
      }
    })();
  }, [ navigate , dispatch]);
};

export default IsAuthorised;
