import { gql } from '@apollo/client';
import { MultiApiQuery } from 'graphql/types';

const writeQuery = (useRest: boolean) => gql`
  query GET_CONFIGURATION {
    webConfiguration ${
      useRest
        ? '@rest(type: "GetWebConfigurationQueryResponse", path: "/web-configuration")'
        : ''
    } {
      categories {
        id
        name
        color
        url
      }
      newsPortalIds
    }
  }
`;

export const GET_CONFIGURATION: MultiApiQuery = {
  gql: writeQuery(false),
  rest: writeQuery(true),
};
