import React from "react";
import { axiosInstance } from "../utils/axiosInstance";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");

  const formData = {
    email,
    password,
  };

  const navigate = useNavigate();

  const handleClick = async (e) => {
    e.preventDefault();
    try {
      const res = await axiosInstance.post("/user/login", formData);
      console.log(res.data);
      if (res.status === 200) {
        toast.success(res.data.message);

        // api result save
        localStorage.setItem("token", res.data.payload);
        localStorage.setItem("userId", res.data.userId);
        // form sanitization
        setEmail("");
        setPassword("");

        // redirect to home page
        navigate("/dashboard");
      }
    } catch (error) {
      if ([400, 401, 403, 404, 500].includes(error.response.status)) {
        
        toast.error(error.response.data.message );
        // toast.error( error.response.data.errors.Email[0]);
        // toast.error( error.response.data.errors.Password[0]);
      }
      console.log(error);
    }
  };

  return (
    <div className="container mt-5 w-50">
      <h2 className="mb-3">Login</h2>

      <form>
        <div className="mb-3">
          <label for="exampleInputEmail1" className="form-label">
            Email address
          </label>
          <input
            type="email"
            className="form-control"
            id="exampleInputEmail1"
            aria-describedby="emailHelp"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
            }}
          />
          <div id="emailHelp" className="form-text">
            We'll never share your email with anyone else.
          </div>
        </div>

        <div className="mb-3">
          <label for="exampleInputPassword1" className="form-label">
            Password
          </label>
          <input
            type="password"
            className="form-control"
            id="exampleInputPassword1"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
        </div>

        <div className="mb-3 form-check">
          <input
            type="checkbox"
            className="form-check-input"
            id="exampleCheck1"
          />
          <label className="form-check-label" for="exampleCheck1">
            Check me out
          </label>
        </div>

        <button onClick={handleClick} type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};

export default Login;
