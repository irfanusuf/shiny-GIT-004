import React from "react";
import IsAuthorised from "../utils/IsAuthorised";
import { useSelector } from "react-redux";
import loadingGif from "../assets/loading.gif";

const Dashboard = () => {
  IsAuthorised();

  // const username = "irfan usuf"

  const { username, loading } = useSelector((state) => state.user);

  return (
    <>
      {!loading ? (
        <div className="container mt-5 ">
          <h1> Welcome to the Dashboard </h1>
          how are u Admin {username}
        </div>
      ) : (
        <div className="loading">
        <img src={loadingGif} alt="not available" />
      </div>
      )}
    </>
  );
};

export default Dashboard;
