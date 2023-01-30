import React, { useState } from 'react'
import axios from 'axios'
import Constants from '../utilities/Constants'

export default function RegisterForm(props) {
  
  const initialFormData = Object.freeze({
    userName: "Name",
    email: "Email",
    password : "Password",
    confirmPassword : "Confirm Password"
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

    const user = {
      name: formData.userName,
      email: formData.email,
      password: formData.password,
      confirmPassword: formData.confirmPassword
    };

    if(user.password == user.confirmPassword)
    {
        const createUserUrl = Constants.API_URL_CREATE_USER
        axios.post(createUserUrl, user)
        .then(() => {            
        const postLoginUrl = Constants.API_URL_POST_LOGIN

        let tokenType = 'Bearer';

        axios.post(postLoginUrl, user)
          .then((response) => {
            sessionStorage.setItem('AccessToken', `${tokenType} ${response.data.accessToken}`);
            sessionStorage.setItem('RefreshToken', response.data.refreshToken);
            sessionStorage.setItem('ExpiryTime', response.data.expiryTime);
            props.onRegister(user.name);
          })
          .catch((error) => {
                console.log(error);
                alert("Wrong Login or Password!");
              });
    })
      .catch((error) => {
            console.log(error);
            alert(error);
          });
    }
  }

  return (
    <div>
      <form className="w-100 px-5">
        <h1 className="mt-5"> RegisterForm Component</h1>

        <div className="mt-5">

          <label className="h3 form-label"> Name</label>
          <input
            value={FormData.userName}
            name="userName"
            type="text"
            className="form-control"
            onChange={handleChange} />  

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

          <label className="h3 form-label"> Confirm Password</label>
          <input
            value={FormData.confirmPassword}
            name="confirmPassword"
            type="text"
            className="form-control"
            onChange={handleChange} 
            />
         

          <button onClick={handleSubmit} className="btn btn-dark brn-lg w-100 mt-5">
            Register
          </button>
          <button onClick={() => props.onRegister(null)} className="btn btn-secondary brn-lg w-100 mt-5">
            Cancel
          </button>
        </div>
      </form>
    </div>
  )
}
