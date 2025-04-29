


import React from 'react'
import Button from './atoms/Button'

const Form = ({btnName , clickHandler}) => {
  return (
    <div>

    <form> 

    <input placeholder=' Enter Something'/>

  




    </form>


    <Button btnName={btnName} clickHandler={clickHandler}/>



    </div>
  )
}

export default Form