export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = {
  [K in keyof T]: T[K];
};
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  Date: unknown;
  DateTime: unknown;
  DateTimeOffset: unknown;
  Seconds: unknown;
  Milliseconds: unknown;
  Decimal: unknown;
  Uri: unknown;
  Guid: unknown;
  Short: unknown;
  UShort: unknown;
  UInt: unknown;
  Long: unknown;
  BigInt: unknown;
  ULong: unknown;
  Byte: unknown;
  SByte: unknown;
};

export type RootQuery = {
  __typename?: 'RootQuery';
  categoryArticles?: Maybe<GetCategoryArticlesQueryResponse>;
  configuration?: Maybe<GetConfigurationQueryResponse>;
  featuredArticles?: Maybe<GetFeaturedArticlesQueryResponse>;
  latestArticles?: Maybe<GetLatestArticlesQueryResponse>;
  trendingArticles?: Maybe<GetTrendingArticlesQueryResponse>;
  webConfiguration?: Maybe<GetWebConfigurationQueryResponse>;
};

export type RootQueryCategoryArticlesArgs = {
  take?: Scalars['Int'];
  skip?: Scalars['Int'];
  minTimestamp?: Maybe<Scalars['String']>;
  newsPortalIds?: Maybe<Scalars['String']>;
  categoryId: Scalars['Int'];
  regionId?: Maybe<Scalars['Int']>;
  titleSearchQuery?: Maybe<Scalars['String']>;
};

export type RootQueryFeaturedArticlesArgs = {
  take?: Scalars['Int'];
  skip?: Scalars['Int'];
  minTimestamp?: Maybe<Scalars['String']>;
  newsPortalIds?: Maybe<Scalars['String']>;
  categoryIds?: Maybe<Scalars['String']>;
};

export type RootQueryLatestArticlesArgs = {
  take?: Scalars['Int'];
  skip?: Scalars['Int'];
  minTimestamp?: Maybe<Scalars['String']>;
  newsPortalIds?: Maybe<Scalars['String']>;
  categoryIds?: Maybe<Scalars['String']>;
  titleSearchQuery?: Maybe<Scalars['String']>;
};

export type RootQueryTrendingArticlesArgs = {
  take?: Scalars['Int'];
  skip?: Scalars['Int'];
  minTimestamp?: Maybe<Scalars['String']>;
};

export type GetLatestArticlesQueryResponse = {
  __typename?: 'GetLatestArticlesQueryResponse';
  articles: Array<GetLatestArticlesArticle>;
  newNewsPortals: Array<GetLatestArticlesNewsPortal>;
  newNewsPortalsPosition: Scalars['Int'];
};

export type GetLatestArticlesArticle = {
  __typename?: 'GetLatestArticlesArticle';
  categories: Array<GetLatestArticlesCategory>;
  id: Scalars['ID'];
  imageUrl?: Maybe<Scalars['String']>;
  newsPortal: GetLatestArticlesNewsPortal;
  publishDateTime: Scalars['String'];
  title: Scalars['String'];
  url: Scalars['String'];
};

export type GetLatestArticlesNewsPortal = {
  __typename?: 'GetLatestArticlesNewsPortal';
  iconUrl: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetLatestArticlesCategory = {
  __typename?: 'GetLatestArticlesCategory';
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetFeaturedArticlesQueryResponse = {
  __typename?: 'GetFeaturedArticlesQueryResponse';
  articles: Array<GetFeaturedArticlesArticle>;
};

export type GetFeaturedArticlesArticle = {
  __typename?: 'GetFeaturedArticlesArticle';
  categories: Array<GetFeaturedArticlesCategory>;
  id: Scalars['ID'];
  imageUrl?: Maybe<Scalars['String']>;
  newsPortal: GetFeaturedArticlesNewsPortal;
  publishDateTime: Scalars['String'];
  title: Scalars['String'];
  url: Scalars['String'];
};

export type GetFeaturedArticlesNewsPortal = {
  __typename?: 'GetFeaturedArticlesNewsPortal';
  iconUrl: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetFeaturedArticlesCategory = {
  __typename?: 'GetFeaturedArticlesCategory';
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetCategoryArticlesQueryResponse = {
  __typename?: 'GetCategoryArticlesQueryResponse';
  articles: Array<GetCategoryArticlesArticle>;
  newNewsPortals: Array<GetCategoryArticlesNewsPortal>;
  newNewsPortalsPosition: Scalars['Int'];
};

export type GetCategoryArticlesArticle = {
  __typename?: 'GetCategoryArticlesArticle';
  categories: Array<GetCategoryArticlesCategory>;
  id: Scalars['ID'];
  imageUrl?: Maybe<Scalars['String']>;
  newsPortal: GetCategoryArticlesNewsPortal;
  publishDateTime: Scalars['String'];
  title: Scalars['String'];
  url: Scalars['String'];
};

export type GetCategoryArticlesNewsPortal = {
  __typename?: 'GetCategoryArticlesNewsPortal';
  iconUrl: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetCategoryArticlesCategory = {
  __typename?: 'GetCategoryArticlesCategory';
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetTrendingArticlesQueryResponse = {
  __typename?: 'GetTrendingArticlesQueryResponse';
  articles: Array<GetTrendingArticlesArticle>;
};

export type GetTrendingArticlesArticle = {
  __typename?: 'GetTrendingArticlesArticle';
  categories: Array<GetTrendingArticlesCategory>;
  id: Scalars['ID'];
  imageUrl?: Maybe<Scalars['String']>;
  newsPortal: GetTrendingArticlesNewsPortal;
  publishDateTime: Scalars['String'];
  title: Scalars['String'];
  trendingScore: Scalars['Int'];
  url: Scalars['String'];
};

export type GetTrendingArticlesNewsPortal = {
  __typename?: 'GetTrendingArticlesNewsPortal';
  iconUrl: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetTrendingArticlesCategory = {
  __typename?: 'GetTrendingArticlesCategory';
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type GetConfigurationQueryResponse = {
  __typename?: 'GetConfigurationQueryResponse';
  categories: Array<GetConfigurationCategory>;
  categoriesWithNewsPortals: Array<GetConfigurationCategoryWithNewsPortals>;
  regions: Array<GetConfigurationRegion>;
};

export type GetConfigurationCategory = {
  __typename?: 'GetConfigurationCategory';
  categoryType: Scalars['Int'];
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
  position?: Maybe<Scalars['Int']>;
};

export type GetConfigurationCategoryWithNewsPortals = {
  __typename?: 'GetConfigurationCategoryWithNewsPortals';
  categoryType: Scalars['Int'];
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
  newsPortals: Array<GetConfigurationNewsPortal>;
  position?: Maybe<Scalars['Int']>;
};

export type GetConfigurationNewsPortal = {
  __typename?: 'GetConfigurationNewsPortal';
  categoryId: Scalars['Int'];
  iconUrl: Scalars['String'];
  id: Scalars['ID'];
  isNew: Scalars['Boolean'];
  name: Scalars['String'];
  regionId?: Maybe<Scalars['Int']>;
};

export type GetConfigurationRegion = {
  __typename?: 'GetConfigurationRegion';
  id: Scalars['ID'];
  name: Scalars['String'];
  newsPortals: Array<GetConfigurationNewsPortal>;
};

export type GetWebConfigurationQueryResponse = {
  __typename?: 'GetWebConfigurationQueryResponse';
  categories: Array<GetWebConfigurationCategory>;
  newsPortalIds: Array<Scalars['Int']>;
};

export type GetWebConfigurationCategory = {
  __typename?: 'GetWebConfigurationCategory';
  categoryType: Scalars['Int'];
  color: Scalars['String'];
  id: Scalars['ID'];
  name: Scalars['String'];
  position?: Maybe<Scalars['Int']>;
  url: Scalars['String'];
};

export type RootGraphQlMutation = {
  __typename?: 'RootGraphQlMutation';
  incrementNumberOfClicks?: Maybe<Scalars['String']>;
};

export type RootGraphQlMutationIncrementNumberOfClicksArgs = {
  articleId: Scalars['String'];
};
