import React from "react";
import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/chatHub", {
    accessTokenFactory: () => localStorage.getItem("path.token"),
  })
  .withAutomaticReconnect()
  .build();

const SignalRContext = React.createContext(connection);

const SignalRProvider = ({ children }) => (
  <SignalRContext.Provider value={connection}>
    {children}
  </SignalRContext.Provider>
);

export { SignalRContext, SignalRProvider };
