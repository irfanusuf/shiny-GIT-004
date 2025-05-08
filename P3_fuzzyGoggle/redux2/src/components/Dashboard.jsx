import React from "react";
import IsAuthorised from "../utils/IsAuthorised";
import { useSelector } from "react-redux";

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
        <div style={{textAlign: "center"}}> Loading..... </div>
      )}
    </>
  );
};

export default Dashboard;
