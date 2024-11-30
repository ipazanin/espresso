import Body from 'components/Layout/Body';
import React, { useState } from 'react';

import { ApolloQueryResult, useLazyQuery } from '@apollo/client';
import {
  GET_FEATURED_ARTICLES,
  GetFeaturedArticlesQueryArgs,
} from 'graphql/queries/get_featured_articles';
import {
  GET_LATEST_ARTICLES,
  GetLatestArticlesQueryArgs,
} from 'graphql/queries/get_latest_articles';
import {
  GetFeaturedArticlesQueryResponse,
  GetLatestArticlesArticle,
  GetLatestArticlesQueryResponse,
  GetWebConfigurationCategory,
} from 'models';
import Flex from '../../components/Flex/Flex';
import useDidMount from '../../hooks/useDidMount';

import FeaturedArticles from './partials/FeaturedArticles';
import LatestArticles from './partials/LatestArticles';

import styles from './main.module.scss';

interface MainProps {
  category: GetWebConfigurationCategory;
  newsPortalIds: number[];
}

type FeaturedArticlesData = {
  featuredArticles: GetFeaturedArticlesQueryResponse;
};

type LatestArticlesData = {
  latestArticles: GetLatestArticlesQueryResponse;
};

const Main: React.FC<MainProps> = ({ category, newsPortalIds }) => {
  const skipLatestArticles = React.useRef<number>(0);

  const [latestArticles, setLatestArticles] = useState<
    GetLatestArticlesArticle[]
  >([]);

  const categoryId = +category.id === -1 ? undefined : +category.id;

  /*
    In order to switch from REST to GQL or vice versa, we just need to change the query type.

    E.g. `GET_FEATURED_ARTICLES.rest` to `GET_FEATURED_ARTICLES.gql` to switch from REST to GQL.
  */
  const [, faResult] = useLazyQuery<
    FeaturedArticlesData,
    GetFeaturedArticlesQueryArgs
  >(GET_FEATURED_ARTICLES.rest);
  const { data, loading } = faResult as unknown as ApolloQueryResult<FeaturedArticlesData>;
  const [runLaQuery, laResult] = useLazyQuery<
    LatestArticlesData,
    GetLatestArticlesQueryArgs
  >(GET_LATEST_ARTICLES.rest, {
    onCompleted: resultData => {
      setLatestArticles(resultData.latestArticles.articles);
    },
  });
  const { loading: laLoading } = laResult as unknown as ApolloQueryResult<LatestArticlesData>;

  useDidMount(() => {
    runLaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        take: 20,
        skip: skipLatestArticles.current,
        newsPortalIds: newsPortalIds.join(','),
      },
    });
  });

  const handleRefetch = React.useCallback(async () => {
    skipLatestArticles.current += 20;

    runLaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        take: 20,
        skip: skipLatestArticles.current,
        newsPortalIds: newsPortalIds.join(','),
      },
    });
  }, [categoryId, newsPortalIds, runLaQuery]);

  React.useEffect(() => {
    skipLatestArticles.current = 0;
    setLatestArticles([]);
    runLaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        take: 20,
        skip: skipLatestArticles.current,
        newsPortalIds: newsPortalIds.join(','),
      },
    });
  }, [categoryId, newsPortalIds, runLaQuery]);

  return (
    <Body>
      <Flex className={styles.container}>
        <FeaturedArticles
          articles={data?.featuredArticles?.articles ?? []}
          loading={loading}
        />
        <LatestArticles
          articles={latestArticles}
          onRefetch={handleRefetch}
          loading={laLoading}
        />
      </Flex>
    </Body>
  );
};

export default Main;
