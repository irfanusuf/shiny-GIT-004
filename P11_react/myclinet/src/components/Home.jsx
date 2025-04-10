import React, { useEffect, useState } from 'react'

const Home = () => {


  const [count , setCount] = useState(0)
  const [name  , setName] = useState("madhat")


  const clickHandler = () =>{
   setCount(count => count +1)
  }


  useEffect(()=>{


    setCount(count => count +1)

  },)



  return (
    <div className='home'>


      <h1>This is the Home page  </h1>

      <h2> Welcome {name}</h2>

    

      <h2> Counter : {count}</h2>


      <button onClick={clickHandler}> Click me  </button>


    </div>
  )
}

export default Home