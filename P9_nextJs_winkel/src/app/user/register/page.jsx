"use client";

// by default all the components are server components

// in server components we cant use hooks // user event handling // interactivty

import React, { useState } from "react";
import "./styles.css";
import { toast } from "react-toastify";

// // import { useRouter } from "next/router";  // page router
import { useRouter } from "next/navigation"; // new one
import mongoose from "mongoose";

const Register = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const formData = { username, email, password };

  const router = useRouter();

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      const url = "/api/user/register";

      const res = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      const data = await res.json();

      if (res.ok) {
        toast.success(data.message);
        setTimeout(() => {
          router.push("/user/dashboard");
        }, [2000]);
      } else {
        toast.error(data.message);
      }
    } catch (error) {
      console.log("Error during registration:", error);
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
