import React, { useState } from 'react'
import axios from 'axios'
import Constants from '../utilities/Constants'

export default function LoginForm(props) {
  
  const initialFormData = Object.freeze({
    email: "Email",
    password : ""
  }); 

  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    const login = {
      email: formData.email,
      password: formData.password
    };

    const url = Constants.API_URL_POST_LOGIN
    let tokenType = 'Bearer';

    axios.post(url, login)
      .then((response) => {
        setFormData(response.data);
        sessionStorage.setItem('Authorization', `${tokenType} ${response.data}`);
        props.onLogin(true);
      })
      .catch((error) => {
            console.log(error);
            alert("Wrong Login or Password!");
          });
  }

  return (
    <div>
      <form className="w-100 px-5">
        <h1 className="mt-5"> LoginForm Component</h1>

        <div className="mt-5">

          <label className="h3 form-label"> Email</label>
          <input
            value={FormData.email}
            name="email"
            type="text"
            className="form-control"
            onChange={handleChange} />

          <label className="h3 form-label"> Password</label>
          <input
            value={FormData.password}
            name="password"
            type="text"
            className="form-control"
            onChange={handleChange} 
            />

          <button onClick={handleSubmit} className="btn btn-dark brn-lg w-100 mt-5">
            LogIn
          </button>
          <button onClick={() => props.onLogin(null)} className="btn btn-secondary brn-lg w-100 mt-5">
            Cancel
          </button>
        </div>
      </form>
    </div>
  )
}
