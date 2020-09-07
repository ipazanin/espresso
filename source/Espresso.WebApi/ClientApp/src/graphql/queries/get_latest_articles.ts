import { gql } from '@apollo/client';
import { MultiApiQuery, PaginationOptions } from 'graphql/types';

const writeQuery = (useRest: boolean) => gql`
  query GET_LATEST_ARTICLES($take: Int = 5, $skip: Int = 0, $minTimestamp: String = "", $newsPortalIds: String = "", $categoryIds: String = "", $titleSearchQuery: String = "") {
    latestArticles(
      take: $take,
      skip: $skip,
      minTimestamp: $minTimestamp,
      newsPortalIds: $newsPortalIds,
      categoryIds: $categoryIds,
      titleSearchQuery: $titleSearchQuery
    ) ${
      useRest
        ? '@rest(type: "GetLatestArticlesQueryResponse", path: "/articles?take={args.take}&skip={args.skip}&minTimestamp={args.minTimestamp}&newsPortalIds={args.newsPortalIds}&categoryIds={args.categoryIds}")'
        : ''
    } {
      articles {
        id
        url
        title
        imageUrl
        publishDateTime
        newsPortal {
          name
          iconUrl
        }
        categories {
          id
          name
          color
        }
      }
    }
  }
`;

export interface GetLatestArticlesQueryArgs extends PaginationOptions {
  minTimestamp?: string;
  newsPortalIds?: string;
  categoryIds?: string;
  titleSearchQuery?: string;
}

export const GET_LATEST_ARTICLES: MultiApiQuery = {
  gql: writeQuery(false),
  rest: writeQuery(true),
};
