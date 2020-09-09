import { gql } from '@apollo/client';
import { MultiApiQuery, PaginationOptions } from 'graphql/types';

const writeQuery = (useRest: boolean) => gql`
  query GET_FEATURED_ARTICLES($take: Int = 10, $skip: Int = 0, $minTimestamp: String = "", $newsPortalIds: String = "", $categoryIds: String = "") {
    featuredArticles(
        take: $take,
        skip: $skip,
        minTimestamp: $minTimestamp,
        newsPortalIds: $newsPortalIds,
        categoryIds: $categoryIds,
    ) ${
      useRest
        ? '@rest(type: "GetFeaturedArticlesQueryResponse", path: "/articles/featured?take={args.take}&skip={args.skip}&minTimestamp={args.minTimestamp}&newsPortalIds={args.newsPortalIds}&categoryIds={args.categoryIds}")'
        : ''
    } {
      articles {
        id
        webUrl
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

export interface GetFeaturedArticlesQueryArgs extends PaginationOptions {
  minTimestamp?: string;
  newsPortalIds?: string;
  categoryIds?: string;
}

export const GET_FEATURED_ARTICLES: MultiApiQuery = {
  gql: writeQuery(false),
  rest: writeQuery(true),
};
