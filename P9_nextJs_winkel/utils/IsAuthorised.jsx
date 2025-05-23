"use client";

import { useRouter } from "next/navigation";
import React, { useEffect } from "react";
import { toast } from "react-toastify";

const IsAuthorised = () => {
  const router = useRouter();

  const verifyToken = async () => {
    try {
      const url = "/api/user/verifyToken";

      const res = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      const data = await res.json();
      sessionStorage.setItem("userId" , data.verify.userId)

      if (res.ok) {
        return true;
      } else {
        return false;
      }
    } catch (error) {
      console.error(error);
      return false;
    }
  };


  useEffect(() => {
    (async () => {
  
      let verify  = await verifyToken();

      if (!verify) {
        router.push("/user/login");
        toast.error("Bad Authentication! Kindly Login Again");
      }
    })();
  }, []);


};

export default IsAuthorised;
