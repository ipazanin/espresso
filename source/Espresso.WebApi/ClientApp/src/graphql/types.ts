import { DocumentNode } from 'graphql';

export interface MultiApiQuery {
  gql: DocumentNode;
  rest: DocumentNode;
}

export interface PaginationOptions {
  take?: number;
  skip?: number;
}
