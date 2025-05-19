
import { apiCall } from "../../utils/apiCall"

// post methods 

export const handleRegister = (e,formData) => ( apiCall("POST","USER" ,  "/register", undefined,  e , formData ))

export const handleLogin =  (e,formData) => ( apiCall("POST","USER" ,"/login",undefined, e , formData )) 

export const forgotpassword =  (e,formData) => (apiCall("POST", "USER" , "/forgot/password",undefined, e , formData )) 

export const OtpVerify = (e , formData ,otpToken)=>(apiCall("POST" ,"USER" , `/otp/verify/${otpToken}` , undefined , e , formData))


//put
export const editUser = (e , formData ,userId) =>(apiCall("PUT" , "USER" , "/edit" , userId ,e , formData ))

export const uploadProfile = (e , formData , userId) =>(apiCall("PUT" , "USER" , "/upload/profile" , userId , e , formData))


// delete method 
export const deleteUser =  (userId) => (apiCall( "DELETE" , "USER" , `/delete` , userId)) 

// get method 
export const verifyUser =  ()=>(apiCall( "GET" , "USER" , `/token/verify` ))

export const verifyAdmin = ()=>(apiCall("GET" , "USER" , "/admin/verify"))
