const authorize = (req,res , next) => {
  try {

// simultaion 

    const {token} = req.query

    console.log(token)

    if(!token || token === ""  || token === undefined || token === null ){
      return  res.status(403).json({message : "Forbidden , Token is missing!"})
    }else{
        next()
    }

    ///


  } catch (error) {
    console.error(error);
  }
};

module.exports = { authorize };
