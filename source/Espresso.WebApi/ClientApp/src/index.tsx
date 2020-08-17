import React from 'react';
import ReactDOM from 'react-dom';

import App from 'components/App';
import { ConfigurationBuilder } from 'configuration';
import Axios from 'axios';
import * as serviceWorker from './serviceWorker';

const config = ConfigurationBuilder.getConfiguration();
Axios.defaults.baseURL = config.getProperty('serverUrl');
Axios.defaults.headers = config.getProperty('headers');

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
