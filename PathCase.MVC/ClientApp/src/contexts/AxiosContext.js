import React from "react";
import axios from "axios";

const token = localStorage.getItem("path.token");
const headers = {};
if(token){
  headers["Authorization"] = `Bearer ${token}`
}
const axiosInstance = axios.create({
    timeout: 10000,
    headers:headers
});

axiosInstance.interceptors.response.use(response => {
    return response;
 }, error => {
   if (error.response.status === 401) {
     localStorage.removeItem("path.token");
    window.location ="/";
   }
   return error;
 });

const AxiosContext = React.createContext(axiosInstance);
const AxiosProvider = ({ children }) => <AxiosContext.Provider value={axiosInstance}>{children}</AxiosContext.Provider>

export {AxiosContext, AxiosProvider};