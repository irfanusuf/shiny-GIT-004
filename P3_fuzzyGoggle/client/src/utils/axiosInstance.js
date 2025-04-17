import axios from "axios";



 export const axiosInstance = axios.create({
 baseURL: "http://localhost:5157/api",
});
//   // Add a request interceptor