import React from 'react';
import AppRouter from 'routers/AppRouter';
import moment from 'moment';
import apolloClient from 'configuration/apollo-client';
import { ApolloProvider } from '@apollo/client';
import { TextConfigProvider } from '@profico/react-ui-components';
import { Palette } from 'styles/Palette';
import { useStringHelpers } from '@profico/react-utils';

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
