import React, { useState } from 'react';
import Body from 'components/Layout/Body';

import { Flex } from '@profico/react-ui-components';
import {
  GetWebConfigurationCategory,
  GetFeaturedArticlesQueryResponse,
  GetLatestArticlesQueryResponse,
  GetLatestArticlesArticle,
} from 'models';
import { useLazyQuery } from '@apollo/client';
import {
  GET_FEATURED_ARTICLES,
  GetFeaturedArticlesQueryArgs,
} from 'graphql/queries/get_featured_articles';
import {
  GET_LATEST_ARTICLES,
  GetLatestArticlesQueryArgs,
} from 'graphql/queries/get_latest_articles';
import { useDidMount } from '@profico/react-utils';

import FeaturedArticles from './partials/FeaturedArticles';
import LatestArticles from './partials/LatestArticles';

import styles from './main.module.scss';

interface MainProps {
  category: GetWebConfigurationCategory;
  newsPortalIds: number[];
}

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
  const [runFaQuery, faResult] = useLazyQuery<
    { featuredArticles: GetFeaturedArticlesQueryResponse },
    GetFeaturedArticlesQueryArgs
  >(GET_FEATURED_ARTICLES.gql, {});
  const [runLaQuery, laResult] = useLazyQuery<
    { latestArticles: GetLatestArticlesQueryResponse },
    GetLatestArticlesQueryArgs
  >(GET_LATEST_ARTICLES.rest, {
    onCompleted: result => {
      setLatestArticles(result.latestArticles.articles);
    },
  });

  useDidMount(() => {
    runFaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        newsPortalIds: newsPortalIds.join(','),
      },
    });
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
    if (laResult.refetch) {
      skipLatestArticles.current += 20;

      const refetchResult = await laResult.refetch({
        skip: skipLatestArticles.current,
      });

      if (refetchResult.data) {
        const {
          data: {
            latestArticles: { articles: newArticles },
          },
        } = refetchResult;

        setLatestArticles(prevArticles => [...prevArticles, ...newArticles]);
      }
    }
  }, [laResult]);

  React.useEffect(() => {
    skipLatestArticles.current = 0;
    setLatestArticles([]);
    runFaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        newsPortalIds: newsPortalIds.join(','),
      },
    });
    runLaQuery({
      variables: {
        ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
        take: 20,
        skip: skipLatestArticles.current,
        newsPortalIds: newsPortalIds.join(','),
      },
    });
  }, [categoryId, newsPortalIds, runLaQuery, runFaQuery]);

  return (
    <Body>
      <Flex className={styles.container}>
        <FeaturedArticles
          articles={
            faResult.data ? faResult.data.featuredArticles.articles : []
          }
          loading={faResult.loading}
        />
        <LatestArticles
          articles={latestArticles}
          onRefetch={handleRefetch}
          loading={laResult.loading}
        />
      </Flex>
    </Body>
  );
};

export default Main;
