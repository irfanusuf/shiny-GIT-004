import React from "react";

const Button = ({btnName , clickHandler}) => {



  return <button style={{backgroundColor:"green" , color: "black" , border : "none"}} onClick={clickHandler}>{btnName}</button>;
};

export default Button;
