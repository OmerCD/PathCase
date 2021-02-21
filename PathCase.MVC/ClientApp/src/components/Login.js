import React, { useEffect, useState } from 'react';
import { AxiosContext } from '../contexts/AxiosContext';
import "./Login.css"

function Login({ onValidSubmit }) {
    const axios = React.useContext(AxiosContext);
    useEffect(()=>localStorage.removeItem('path.token'), []);
    const handleLogin = async (e) => {
        e.preventDefault();
        const element = document.getElementById("userName");
        const userName = element.value;
        const response = await axios.post("authentication",{userName})
        const {token} = response.data;
        localStorage.setItem("path.token", token);
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
        onValidSubmit(userName);
    }
    return (
        <>
            <div className="form-container">
                <form className="login-container align-middle" onSubmit={handleLogin}>
                    <input id="userName" type="text" placeholder={"User Name"} />
                    <button type="submit" className="btn btn-success ">Login</button>
                </form>
            </div>

        </>
    )
}
export default Login;