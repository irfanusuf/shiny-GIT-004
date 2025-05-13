import { createReducer } from "@reduxjs/toolkit";




const intialState ={
 username : "",
 email : "",
 loading : false,
 error : null,
 message : "",
 payload : []

}


export const userReducer = createReducer(intialState , (builder)=>{

builder.addCase("USER_API_REQUEST" , (state) =>{
state.loading = true
})


builder.addCase("USER_API_SUCCESS" , (state , action)=>{

state.error = null
state.loading = false
state.message = action.message
state.payload = action.payload
state.username = action.username
state.email = action.email
})


builder.addCase("USER_API_FAILURE" , (state , action)=>{

state.loading = false
state.error = action.error
state.message = action.message

})


})