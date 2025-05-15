"use client"


// by default all the components are server components 

// in server components we cant use hooks // user event handling // interactivty

import React from 'react'
import "./styles.css"

const Register = () => {

// const [username , setUsername] = useState("")




  return (
    <div className='register_container' ><h1> This is register</h1>
    
      <input placeholder='Enter Your Username ' onChange={(e)=>{console.log(e.target.value)}}   />
    
    
    </div>
  )
}

export default Register