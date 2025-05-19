import React, { useContext } from "react";
import { Context } from "../../../context/Store";


const Button = () => {


const {btnName , darkMode} = useContext(Context);




  return <button style={{backgroundColor:"green" , color: "black" , border : "none"}} >  {btnName}  </button>;
};

export default Button;

