import React, {useContext, useEffect, useState } from "react";
import Home from "./components/Home";
import About from "./components/About";
import Admin from "./components/Admin";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import {  Route, Routes } from "react-router-dom";
import "./global.css";
import Gallery from "./components/Gallery";
import Register from "./components/Register";
import Login from "./components/Login";
import Dashboard from "./components/Dashboard";
import PropTesting from "./components/PropTesting";
import Contact from "./components/Contact";



const App = () => {
  const [darkMode, setDarkMode] = useState(false);

  const toggleMode = () => {
    setDarkMode(!darkMode);
  };


  return (
    // jsx fragament
    <>
  
        <Navbar darkMode={darkMode} toggleMode={toggleMode} />
        <div className={darkMode ? "body-dark" : "body-light"}>
          <Routes>
            {/* Mount and unmouut */}

            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/admin" element={<Admin />} />
            <Route path="/gallery" element={<Gallery />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login/>} />
            <Route path="/dashboard" element={<Dashboard/>} />
            <Route path="*" element={<h1 style={{textAlign : "center"}}> No page Found </h1>} />
            <Route path="/Proptesting" element={   <PropTesting/> } />
          </Routes>
        </div> 

        <Footer />
     
    </>
  );
};

export default App;
