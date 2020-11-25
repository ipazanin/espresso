import { RestLink } from 'apollo-link-rest';
import {
  HttpLink,
  ApolloClient,
  InMemoryCache,
  ApolloLink,
} from '@apollo/client';
import { GetLatestArticlesQueryResponse } from 'models';

import { ConfigurationBuilder } from './configuration.builder';

const config = ConfigurationBuilder.getConfiguration();
const headers = config.getProperty<Record<string, string>>('headers');
const apiBaseUrl = config.getProperty<string>('serverUrl');

interface LatestArticlesResponse {
  latestArticles: GetLatestArticlesQueryResponse;
}

const restLink = new RestLink({
  headers,
  credentials: 'omit',
  uri: `${apiBaseUrl}/api`,
});
const httpLink = new HttpLink({
  uri: `${apiBaseUrl}/graphql`,
  headers,
  credentials: 'omit',
});
const cache = new InMemoryCache();

const client = new ApolloClient({
  cache,
  headers,
  link: ApolloLink.from([restLink, httpLink]),
});

export default client;
