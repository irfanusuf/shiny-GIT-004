import { createReducer } from "@reduxjs/toolkit";




const intialState ={
 loading : false,
 error : null,
 message : "",
 postArr : []

}


export const postReducer = createReducer(intialState , (builder)=>{

builder.addCase("POST_API_REQUEST" , (state) =>{
state.loading = true
})


builder.addCase("POST_API_SUCCESS" , (state , action)=>{

state.error = null
state.loading = false
state.message = action.message
state.postArr = action.postArr

})


builder.addCase("POST_API_FAILURE" , (state , action)=>{

state.loading = false
state.error = action.error
state.message = action.message

})


})