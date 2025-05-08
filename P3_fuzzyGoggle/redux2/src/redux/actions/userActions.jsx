
import { apiCall } from "../../utils/apiCall";



// post methods 

export const handleRegister = (e,formData) => (apiCall("/user/register",  e , formData ))

export const handleLogin =  (e,formData) => (apiCall("/user/login", e , formData )) 

export const forgotpassword =  (e,formData) => (apiCall("/user/forgot/password", e , formData )) 

// delete method 
export const deleteUser =  (userId) => (apiCall(`/user/delete/${userId}` )) 

// get method 
export const verifyUser =  (token)=>(apiCall(`/user/verify/${token}` ))
