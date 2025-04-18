import React from 'react'
import IsAuthorised from '../utils/IsAuthorised'

const Dashboard = () => {


 IsAuthorised();



  return (
    <div className='container mt-5 '>

    <h1> Welcome to the Dashboard </h1>


    </div>
  )
}

export default Dashboard