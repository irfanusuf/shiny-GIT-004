

function messageHandler (res , statusCode ,success, message , payload){

return res.status(statusCode).json({ success : Boolean(success) , message : String(message) , payload:payload})


}


module.exports = {messageHandler}