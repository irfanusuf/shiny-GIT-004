import React from 'react'
import { Link } from 'react-router-dom'

const Navbar = ({darkMode , toggleMode}) => {



  return (
    <div className={darkMode ? 'navbar-dark' : 'navbar-light'}>

        <ul>

            <li> <Link to='/'> Home </Link> </li>
            <li> <Link to='/about'> About </Link> </li>
            <li> <Link to='/contact'> Contact </Link> </li>
            <li> <Link to='/admin'> Admin </Link> </li>
        </ul>

        <div>
          <button onClick={toggleMode}>
           {darkMode ? "Enable Light Mode" : " Enable Dark Mode"}
          </button>
        </div>

    </div>
  )
}

export default Navbar