import React from 'react';
import AppRouter from 'routers/AppRouter';
import moment from 'moment';
import apolloClient from 'configuration/apollo-client';
import 'moment/locale/hr';

import { ApolloProvider } from '@apollo/client';
import { Palette } from 'styles/Palette';
import { TextConfigProvider } from '../Text/Text.config';
import useStringHelpers from '../../hooks/useStringHelpers';

moment.locale('hr');

const App: React.FC = () => {
  const { hexToRgba } = useStringHelpers();

  return (
    <ApolloProvider client={apolloClient}>
      <TextConfigProvider
        config={{
          colors: {
            primary: Palette.black,
            secondary: hexToRgba(Palette.gray800, 0.4),
            white: '#FFFFFF',
            black: '#000000',
            muted: hexToRgba(Palette.gray800, 0.4),
            error: Palette.primary,
            success: Palette.white,
          },
        }}
      >
        <AppRouter />
      </TextConfigProvider>
    </ApolloProvider>
  );
};

export default App;
