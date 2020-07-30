import React from 'react';

import UpcomingEvents from './partials/UpcomingEvents';
import { ConfigurationBuilder } from 'config';

const config = ConfigurationBuilder.getConfiguration();

const Home: React.FC = () => (
  <div>
    <div>{process.env.REACT_APP_TEST}</div>
    <UpcomingEvents events={[{ name: 'First event' }, { name: 'Second event' }]} />
  </div>
);

export default Home;
