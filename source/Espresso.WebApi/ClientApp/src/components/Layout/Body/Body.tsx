import React from 'react';

import { Flex } from '@profico/react-ui-components';

import styles from './body.module.scss';

const Body: React.FC = ({ children }) => (
  <Flex flexDirection="column" className={styles.container}>
    {children}
  </Flex>
);

export default Body;
