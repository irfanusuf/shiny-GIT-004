const axios = require('axios');

const options = {
  method: 'POST',
  url: 'https://recipe-food-nutrition15.p.rapidapi.com/recipee-search',
  headers: {
    'x-rapidapi-key': '79bd850338msh2791f9644fac656p197751jsnac6970a86c03',
    'x-rapidapi-host': 'recipe-food-nutrition15.p.rapidapi.com',
    'Content-Type': 'application/json'
  },
  data: {
    query: 'coffee and croissant'
  }
};

const fetchData = async(req,res) =>{

    try {
        const response = await axios.request(options);

        res.json(response.data)

        // console.log(response.data);  
    } catch (error) {
        console.error(error);
    }


}


module.exports = {fetchData}


