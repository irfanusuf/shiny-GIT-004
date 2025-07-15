
// by default all the components are server components

// in server components we cant use hooks // user event handling // interactivty
"use client"


import React, { useState } from "react";
import { toast } from "react-toastify";

// // import { useRouter } from "next/router";  // page router
import { useRouter } from "next/navigation"; // new one

const Login = () => {

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const formData = { email, password };

  const router = useRouter();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const url = "/api/user/login";

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
    <div className="min-h-screen flex items-center justify-center ">
      <div className="bg-zinc-900 p-8 rounded-lg shadow-md w-full max-w-sm">
        <h1 className="text-2xl font-bold mb-6 text-center">Login</h1>

        <form onSubmit={handleLogin} className="space-y-4">
          <input
            type="email"
            placeholder="Enter Your Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
          <input
            type="password"
            placeholder="Enter Your Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />

          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition duration-200"
          >
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
