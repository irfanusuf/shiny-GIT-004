
import React from "react";
import { Context } from "../context/Store";

const Register = () => {

  const [form, setForm] = React.useState({
    username : "",
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

 const { handleRegister } = React.useContext(Context);



  return (
    <div className="container mt-5 w-50">
      <h2 className="mb-3"> Register</h2>

      <form onSubmit={(e)=>{handleRegister(e,form)}}>

      <div className="mb-3">
          <label for="exampleInputPassword1" className="form-label">
            Username
          </label>
          <input
            type="text"
            className="form-control"
            name="username"
            value={form.username}
            onChange={handleChange}
          />
        </div>


        <div className="mb-3">
          <label className="form-label">Email address</label>
          <input
            type="email"
            className="form-control"
            aria-describedby="emailHelp"
            name="email"
            value={form.email}
            onChange={handleChange}
          />

          <div className="form-text">
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
            name="password"
            value={form.password}
            onChange={handleChange}
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

        <button type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};

export default Register;
