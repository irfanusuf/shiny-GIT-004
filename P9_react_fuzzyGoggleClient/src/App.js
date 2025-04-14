import React, { useState } from "react";
import Home from "./components/Home";
import About from "./components/About";
import Admin from "./components/Admin";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./global.css";
import Gallery from "./components/Gallery";

const App = () => {


  const [darkMode , setDarkMode] = useState(false)

  const  toggleMode = () =>{
    setDarkMode(!darkMode)
  }


  return (
    // jsx fragament
    <>
      <BrowserRouter>
        <Navbar darkMode ={darkMode} toggleMode ={toggleMode} />

        <div className={darkMode ? "body-dark" : "body-light"}>
          <Routes>
            {/* Mount and unmouut */}
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/admin" element={<Admin />} />
            <Route path="/gallery" element={<Gallery />} />
          </Routes>
        </div>

        <Footer />
      </BrowserRouter>
    </>
  );
};

export default App;
