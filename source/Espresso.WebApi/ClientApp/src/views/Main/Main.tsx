import React, { useState } from 'react';
import Body from 'components/Layout/Body';

import { Flex } from '@profico/react-ui-components';
import {
  GetWebConfigurationCategory,
  GetFeaturedArticlesQueryResponse,
  GetLatestArticlesQueryResponse,
  GetLatestArticlesArticle,
} from 'models';
import { useQuery, NetworkStatus } from '@apollo/client';
import {
  GET_FEATURED_ARTICLES,
  GetFeaturedArticlesQueryArgs,
} from 'graphql/queries/get_featured_articles';
import {
  GET_LATEST_ARTICLES,
  GetLatestArticlesQueryArgs,
} from 'graphql/queries/get_latest_articles';

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
  const faResult = useQuery<
    { featuredArticles: GetFeaturedArticlesQueryResponse },
    GetFeaturedArticlesQueryArgs
  >(GET_FEATURED_ARTICLES.gql, {
    variables: {
      ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
      newsPortalIds: newsPortalIds.join(','),
    },
  });
  const laResult = useQuery<
    { latestArticles: GetLatestArticlesQueryResponse },
    GetLatestArticlesQueryArgs
  >(GET_LATEST_ARTICLES.rest, {
    variables: {
      ...(categoryId ? { categoryIds: `${categoryId}` } : {}),
      take: 20,
      skip: skipLatestArticles.current,
      newsPortalIds: newsPortalIds.join(','),
    },
    onCompleted: result => {
      setLatestArticles(prevArticles => [
        ...prevArticles,
        ...result.latestArticles.articles,
      ]);
    },
  });

  const handleRefetch = React.useCallback(async () => {
    skipLatestArticles.current += 20;

    await laResult.refetch({ skip: skipLatestArticles.current });
  }, [laResult]);

  React.useEffect(() => {
    skipLatestArticles.current = 0;
  }, [category.id]);

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
          refetching={laResult.networkStatus === NetworkStatus.refetch}
        />
      </Flex>
    </Body>
  );
};

export default Main;
