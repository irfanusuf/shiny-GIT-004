/// rapid api 

// mountain details 

const axios = require('axios');

const options = {
  method: 'GET',
  url: 'https://mountain-api1.p.rapidapi.com/api/mountains',
  params: {
    name: 'mountain name '
  },
  headers: {
    'x-rapidapi-key': '79bd850338msh2791f9644fac656p197751jsnac6970a86c03',
    'x-rapidapi-host': 'mountain-api1.p.rapidapi.com'
  }
};

try {
	const response = await axios.request(options);
	console.log(response.data);
} catch (error) {
	console.error(error);
}



// 