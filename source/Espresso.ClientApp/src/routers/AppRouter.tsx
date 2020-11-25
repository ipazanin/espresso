import React from 'react';
import AppContainer from 'containers/AppContainer';

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from 'react-router-dom';

const AppRouter: React.FC = () => (
  <Router>
    <Switch>
      <Route path="/home" exact component={() => <Redirect to="/" />} />
      <Route path="/:path?" exact component={AppContainer} />
      <Route component={() => <div>Page not found.</div>} />
    </Switch>
  </Router>
);

export default AppRouter;
