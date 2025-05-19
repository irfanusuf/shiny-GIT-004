


import React, { useContext } from 'react'
import Button from './atoms/Button'
import { Context } from '../../context/Store'

const Form = () => {


 const {username} = useContext(Context);

  return (
    <div>



    <h1> Welcome {username} </h1>

    <form> 

    <input placeholder=' Enter Something'/>



    </form>


    <Button/>



    </div>
  )
}

export default Form