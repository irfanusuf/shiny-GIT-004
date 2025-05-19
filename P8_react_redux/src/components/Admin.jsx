import React, { useEffect } from "react";
import { apiCall } from "../utils/apiCall";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

const Admin = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    (async () => {

      // crud operation 
      const success = await dispatch(apiCall("GET", "USER", "/admin/verify"));

      if (!success) {
        navigate("/");
      }
    }) ()
  });

  return (
    <div>
      <h1> Admin</h1>
    </div>
  );
};

export default Admin;
