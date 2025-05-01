import React, { createContext, useCallback, useEffect } from "react";
import App from "../App";
import axios from "axios";
import { toast } from "react-toastify";
import { axiosInstance } from "../utils/axiosInstance";
import { useNavigate } from "react-router-dom";

export const Context = createContext();

const Store = () => {

  const navigate = useNavigate();

  const [store, setStore] = React.useState({
    btnName: "Something",
    darkMode: false,
    username: "",
    loading: false,
    posts: [],
  });

  const handleRegister = async (e, form) => {
    e.preventDefault();

    try {
      const res = await axios.post(
        `http://localhost:5157/api/user/register`,
        form
      );

      console.log(res.data);

      if (res.status === 200) {
        toast.success("User registered successfully");
      }

    
    } catch (error) {
      if (error.response && [400, 401, 403, 404, 500].includes(error.response.status)) {
        toast.error(error.response.data.message);
      }else{
        toast.error("Some Network Error!");
      }

      console.log(error);
    }
  };

  const handleLogin = async (e, formData) => {
    e.preventDefault();
    try {
      const res = await axiosInstance.post("/user/login", formData);
      
      if (res.status === 200) {
        toast.success(res.data.message);

        // api result save
        localStorage.setItem("token", res.data.payload);
        localStorage.setItem("userId", res.data.userId);
    
        navigate("/dashboard");
      }
    } catch (error) {
      if (
        error.response &&
        [400, 401, 403, 404, 500].includes(error.response.status)
      ) {
        toast.error(error.response.data.message);
      
      } else {
        toast.error("Some Network Error!");
      }
      console.log(error);
    }
  };

  const fetchData = useCallback(async () => {
    try {
      setStore((prevState) => ({...prevState,  loading: true, }));

      const response = await axios.get(
        "https://jsonplaceholder.typicode.com/posts"
      );
      //setStore(response.data);   // wrong way to set data
      setStore((prevState) => ({...prevState,  posts: response.data, loading: false, }));

    } catch (error) {
      console.log(error);
    }
  }, []);





useEffect(()=>{
  fetchData()

}, [fetchData])


  return (
    <Context.Provider value={{ ...store, handleRegister, handleLogin , fetchData }}>
      <App />
    </Context.Provider>
  );
};

export default Store;
