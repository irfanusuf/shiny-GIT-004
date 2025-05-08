import { axiosInstance } from "./axiosInstance";

export const apiCall =
  (requsetType, url, e, formData, Id) => async (action) => {
    try {
      if (e !== undefined) {
        e.preventDefault();
      }

      let res;

      action({ type: "API_REQUEST" }); // state.loading :true

      if (formData !== undefined && e !== undefined) {
        if (requsetType === "post") {
          res = await axiosInstance.post(`${url}`, formData);
        } else if (requsetType === "put" && Id !== undefined) {
          res = await axiosInstance.put(`${url}/${Id}`, formData);
        }
      } else {
        if (requsetType === "get") {
          if (Id !== undefined) {
            res = await axiosInstance.get(`url/${Id}`);
          } else {
            res = await axiosInstance.get(url);
          }
        }
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
        message: err.response.data.message,
      });

      return false;
    }
  };
