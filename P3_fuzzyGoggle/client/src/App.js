import React, { useState } from "react";
import Home from "./components/Home";
import About from "./components/About";
import Admin from "./components/Admin";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./global.css";
import Gallery from "./components/Gallery";
import Register from "./components/Register";
import { ToastContainer } from "react-toastify";
import Login from "./components/Login";
import Dashboard from "./components/Dashboard";

const App = () => {
  const [darkMode, setDarkMode] = useState(false);

  const toggleMode = () => {
    setDarkMode(!darkMode);
  };

  return (
    // jsx fragament
    <>
      <BrowserRouter>
        <ToastContainer
          position="top-center"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick={false}
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="dark"
        />

        <Navbar darkMode={darkMode} toggleMode={toggleMode} />

        <div className={darkMode ? "body-dark" : "body-light"}>
          <Routes>
            {/* Mount and unmouut */}

            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/admin" element={<Admin />} />
            <Route path="/gallery" element={<Gallery />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login/>} />
            <Route path="/dashboard" element={<Dashboard/>} />
            <Route path="*" element={<h1 style={{textAlign : "center"}}> No page Found </h1>} />
          </Routes>
        </div> 

        <Footer />
      </BrowserRouter>
    </>
  );
};

export default App;
