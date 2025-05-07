import React, { useEffect } from "react";
// import IsAuthorised from '../utils/IsAuthorised'
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { verifyUser } from "../redux/actions/userActions";

const Dashboard = () => {
  //  IsAuthorised();

  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token === null) {
      navigate("/login");
    } else {
      dispatch(verifyUser(token));
    }
  }, [dispatch, navigate]);

  return (
    <div className="container mt-5 ">
      <h1> Welcome to the Dashboard </h1>
    </div>
  );
};

export default Dashboard;
