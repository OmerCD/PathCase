import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { withRouter } from 'react-router-dom';

import Login from "./components/Login";
import ChatRooms from './components/ChatRooms';

class App extends Component {
  static displayName = App.name;

  render() {
    const handleValidSubmit = (userName) => {
      this.props.history.push({
        pathname: 'chatRooms',
        state: { userName }
      });
    }
    return (
      <>
        <Route exact path='/' component={Login}>
          <Login onValidSubmit={handleValidSubmit} />
        </Route>
        <Route path='/chatRooms' component={ChatRooms} />
      </>
    );
  }
}

export default withRouter(App);
