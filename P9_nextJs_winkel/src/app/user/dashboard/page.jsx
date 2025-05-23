"use client";

import React, { useEffect, useState } from "react";
import IsAuthorised from "../../../../utils/IsAuthorised";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import loadingGIF from "./loading.gif"



const Dashboard = () => {
  const [Loading, setLaoding] = useState(true);
  const [username, setUsername] = useState("");
  const router = useRouter();

  // IsAuthorised();   // verify token

  useEffect(() => {
    (async () => {
      try {
        setLaoding(true);
        const url = "/api/user/getUser";
        const res = await fetch(url, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
        });

        const data = await res.json();
        if (res.ok) {
          setUsername(data.payload.username);
          setLaoding(false);
        } else {
          toast.error("UnAuthorised!");
          router.push("/user/login");
        }
      } catch (error) {
        console.error(error);
        toast.error(error.message);
        router.push("/user/login");
      }
    })();
  }, [router]);

  return (
    <>
      {Loading ? (
        <div className="loading_container">

        <h3> Loading...... </h3>

        </div>
      ) : (
        <div>
          <h1> Welcome to the dashboard</h1>
          <h1> Welcome {username}</h1>
        </div>
      )}
    </>
  );
};

export default Dashboard;
