import React, { Component } from "react";
import { Route } from "react-router";
import { withRouter } from "react-router-dom";

import Login from "./components/Login";
import ChatRooms from "./components/ChatRooms";
import { AxiosProvider } from "./contexts/AxiosContext";
import { SignalRProvider } from "./contexts/SignalRContext";

class App extends Component {
  static displayName = App.name;

  render() {
    const handleValidSubmit = (userName) => {
      this.props.history.push({
        pathname: "chatRooms",
        state: { userName },
      });
    };
    return (
      <AxiosProvider>
        <Route exact path="/" component={Login}>
          <Login onValidSubmit={handleValidSubmit} />
        </Route>
        <SignalRProvider>
          <Route path="/chatRooms" component={ChatRooms} />
        </SignalRProvider>
      </AxiosProvider>
    );
  }
}

export default withRouter(App);
