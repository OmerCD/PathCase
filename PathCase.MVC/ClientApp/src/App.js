import React, { Component } from "react";
import { Route } from "react-router";
import { withRouter } from "react-router-dom";

import Login from "./components/Login";
import ChatRooms from "./components/ChatRooms";
import { AxiosProvider } from "./contexts/AxiosContext";
import { SignalRProvider } from "./contexts/SignalRContext";

class App extends Component {
  constructor(props){
    super(props);
    this.state = {userName:localStorage.getItem('path.userName')}
  }
  static displayName = App.name;
  handleValidSubmit = (userName) => {
    this.setState({...this.state, userName:userName});
    localStorage.setItem('path.userName', userName);
    this.props.history.push({
      pathname: "chatRooms",
      state: { userName },
    });
  }
  render() {
    return (
      <AxiosProvider>
        <Route exact path="/">
          <Login onValidSubmit={this.handleValidSubmit} />
        </Route>
        <SignalRProvider>
          <Route path="/chatRooms">
            <ChatRooms userName={this.state.userName}/>
          </Route>
        </SignalRProvider>
      </AxiosProvider>
    );
  }
}

export default withRouter(App);
