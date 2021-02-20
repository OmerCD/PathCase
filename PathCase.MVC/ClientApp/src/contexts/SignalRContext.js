import React from "react";
import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/chatHub", {
    accessTokenFactory: () => localStorage.getItem("path.token"),
  })
  .build();

if (document.URL.indexOf("/chatRoom") > -1) {
  connection.start().catch((x) => console.log(x));
}
const SignalRContext = React.createContext(connection);

const SignalRProvider = ({ children }) => (
  <SignalRContext.Provider value={connection}>
    {children}
  </SignalRContext.Provider>
);

export { SignalRContext, SignalRProvider };
