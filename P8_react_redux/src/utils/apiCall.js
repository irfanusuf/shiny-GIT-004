import { toast } from "react-toastify";
import { axiosInstance } from "./axiosInstance";

export const apiCall =
  (requsetType, apiPrefix, url, paramId, e, formData) => async (action) => {
    try {
      if (e !== undefined) {
        e.preventDefault();
      }

      let res;

      action({ type: `${apiPrefix}_API_REQUEST` }); // state.loading :true

      if (requsetType === "POST") {
        res = await axiosInstance.post(`${apiPrefix}${url}`, formData);
      } else if (requsetType === "PUT" && paramId !== undefined) {
        res = await axiosInstance.put(
          `${apiPrefix}${url}/${paramId}`,
          formData
        );
      } else if (requsetType === "GET") {
        if (paramId !== undefined) {
          res = await axiosInstance.get(`${apiPrefix}${url}/${paramId}`);
        } else {
          res = await axiosInstance.get(`${apiPrefix}${url}`);
        }
      } else if (requsetType === "DELETE") {
        if (paramId !== undefined) {
          res = await axiosInstance.delete(`${apiPrefix}${url}/${paramId}`);
        } else {
          res = await axiosInstance.delete(`${apiPrefix}${url}`);
        }
      }

      if (res.status === 200 && apiPrefix === "USER") {
        action({
          type: `${apiPrefix}_API_SUCCESS`,
          message: res.data.message,
          username: res.data.username,
          email: res.data.email,
        });

        toast.success(res.data.message);
        return true;
      }

      if (res.status === 200 && apiPrefix === "POST") {
        action({
          type: `${apiPrefix}_API_SUCCESS`,
          postArr: res.data.payload,
          message: res.data.message,
        });

        toast.success(res.data.message);

        return true;
      }

    } catch (err) {
      console.error(err);

      if (err.message === "Network Error") {
        toast.error("Network error. Please check your connection.");
        return;
      } else {
        const errorMessage =
          err.response?.data?.message || "Something went wrong";

        action({
          type: `${apiPrefix}_API_FAILURE`,
          error: err,
          message: errorMessage,
        });

        toast.error(errorMessage); 
      }

      return false;
    }
  };
