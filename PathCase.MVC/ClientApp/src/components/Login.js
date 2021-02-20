import React, { useEffect, useState } from 'react';
import "./Login.css"

function Login({ onValidSubmit }) {
    const handleLogin = (e) => {
        e.preventDefault();
        const element = document.getElementById("userName");
        const userName = element.value;
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