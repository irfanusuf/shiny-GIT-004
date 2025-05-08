import { axiosInstance } from "./axiosInstance";

export const apiCall = (url, e , formData) => async (action) => {
  try {
    if (e !== undefined) {
      e.preventDefault();
    }

    let res;

    action({ type: "API_REQUEST" }); // state.loading :true

    console.log(formData);

    if (formData !== undefined) {
      res = await axiosInstance.post(`${url}`, formData);
    } else {
      res = await axiosInstance.get(url);
    }



    if (res.status === 200) {
      action({
        type: "API_SUCCESS",
        payload: res.data.payload,
        message: res.data.message,
        username: res.data.username,
        email: res.data.email,
      });

      return true;
    } else {
      return false;
    }
  } catch (err) {
    console.log(err);
    action({
      type: "API_FAILURE",
      error: err,
      message: err.message,
    });

    return false;
  }
};
