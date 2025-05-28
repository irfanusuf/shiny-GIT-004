"use client";

// by default all the components are server components

// in server components we cant use hooks // user event handling // interactivty

import React, { useState } from "react";
import "./styles.css";
import { toast } from "react-toastify";

// // import { useRouter } from "next/router";  // page router
import { useRouter } from "next/navigation"; // new one

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
    <div className="max-w-md mx-auto mt-10 p-6 bg-white shadow-lg rounded-xl">
      <h4 className="text-2xl font-semibold text-center mb-6">Register</h4>
  
      <form className="space-y-4">
        <input
          type="text"
          placeholder="Enter your username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="email"
          placeholder="Enter your email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="password"
          placeholder="Enter your password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
  
        <button
          onClick={handleRegister}
          className="w-full py-2 px-4 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition duration-200"
        >
          Register
        </button>
      </form>
    </div>
  );
  
};

export default Register;
