"use client";

// by default all the components are server components

// in server components we cant use hooks // user event handling // interactivty

import React, { useState } from "react";
import "./styles.css";

const Register = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const formData = { username, email, password };

  const handleRegister = async () => {
    try {

      const url = "/api/user/register"
      const api = await fetch(url, {
        method: "POST",
        body: JSON.stringify(formData),
      });

      const { res } = await api.json();

      // if(res.status == 200 ){

      console.log(res);
      // }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="register_container">
      <h1> Register</h1>

      <form>
        <input
          placeholder="Enter Your username "
          value={username}
          onChange={(e) => {
            setUsername(e.target.value);
          }}
        />
        <input
          placeholder="Enter Your Email "
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />
        <input
          placeholder="Enter Your Password "
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        />

        <button onClick={handleRegister}> Register </button>
      </form>
    </div>
  );
};

export default Register;
