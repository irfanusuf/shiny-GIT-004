import React from "react";
import Home from "./components/Home";
import About from "./components/About";
import Admin from "./components/Admin";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./global.css";

const App = () => {
  return (
    // jsx fragament
    <>
      <BrowserRouter>
        <Navbar />

        <div className="body">
          <Routes>
            {/* Mount and unmouut */}
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/admin" element={<Admin />} />
          </Routes>
        </div>

        <Footer />
      </BrowserRouter>
    </>
  );
};

export default App;
