import { axiosInstance } from "../../utils/axiosInstance";

export const handleRegister = (e, formData) => async (action) => {
  try {
    e.preventDefault();
    action({ type: "API_REQUEST" }); // state.loading :true

    const res = await axiosInstance.post("/user/register", formData);

    if (res.status === 200) {
      action({
        type: "API_SUCCESS",
        payload: res.data.payload,
        message: res.data.message,
        username : res.data.username,
        email : res.data.email
      });
    }
  } catch (err) {
    console.log(err);
    action({
      type: "API_FAILURE",
      error: err,
      message: err.message,
    });
  }
};


export const handleLogin = (e, formData) => async (action) => {
  try {
    e.preventDefault();
    action({ type: "API_REQUEST" }); // state.loading :true

    const res = await axiosInstance.post("/user/login", formData);

    if (res.status === 200) {
      action({
        type: "API_SUCCESS",
        payload: res.data.payload,
        message: res.data.message,
        username : res.data.username,
        email : res.data.email
      });


      localStorage.setItem("token" , res.data.payload)
    }
  } catch (err) {
    console.log(err);
    action({
      type: "API_FAILURE",
      error: err,
      message: err.message,
    });
  }
};


export const verifyUser = (token) => async (action) => {
  try {
   
    action({ type: "API_REQUEST" }); // state.loading :true

    const res = await axiosInstance.get(`/user/verify/${token}`);

    if (res.status === 200) {
      action({
        type: "API_SUCCESS",
        payload: res.data.payload,
        message: res.data.message,
        username : res.data.username,
        email : res.data.email
      });

    }
  } catch (err) {
    console.log(err);
    action({
      type: "API_FAILURE",
      error: err,
      message: err.message,
    });
  }
};


