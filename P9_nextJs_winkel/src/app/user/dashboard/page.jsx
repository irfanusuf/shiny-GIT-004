"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import loadingGIF from "./loading.gif";
import Image from "next/image";

const Dashboard = () => {
  const [loading, setLoading] = useState(true);
  const [username, setUsername] = useState("");
  const router = useRouter();

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        setLoading(true);
        const url = "/api/user/getUser";
        const res = await fetch(url, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
        });

        if (!res.ok) {
          throw new Error("Unauthorized");
        }

        const data = await res.json();
        setUsername(data.payload.username);
      } catch (error) {
        console.error("Dashboard error:", error);
        toast.error(error.message || "Unauthorized access");
        router.push("/user/login");
      } finally {
        setLoading(false);
      }
    };

    fetchUserData();
  }, [router]);

  if (loading) {
    return (
      <div className="loading_container ">
        <Image 
          src={loadingGIF} 
          alt="Loading..." 
          width={200}
          height={200}
          unoptimized 
        />
      </div>
    );
  }

  return (
    <div className="loading_container"  >
      <h1 >Welcome to the dashboard</h1>
      <h2 >Welcome {username}</h2>
    </div>
  );
};

export default Dashboard;