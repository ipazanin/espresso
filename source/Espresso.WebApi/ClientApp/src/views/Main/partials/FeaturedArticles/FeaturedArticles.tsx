import React from 'react';
import Article from 'components/Article';

import { GetFeaturedArticlesArticle } from 'models';
import { LoadingSpinner } from 'components/Loader';
import Text from '../../../../components/Text/Text';
import Flex from '../../../../components/Flex/Flex';

import styles from './featured_articles.module.scss';

interface FeaturedArticlesProps {
  articles: GetFeaturedArticlesArticle[];
  loading: boolean;
}

const FeaturedArticles: React.FC<FeaturedArticlesProps> = ({
  articles,
  loading,
}) => {
  const [mainArticle, ...restOfArticles] = articles;

  return (
    <Flex className={styles.container} flexDirection="column" fluid>
      <Text
        weight="bold"
        align="left"
        color="muted"
        size="caption"
        transform="uppercase"
        className={styles.subtitle}
      >
        Izdvojeno
      </Text>
      {loading ? (
        <Flex flexOut>
          <LoadingSpinner size={24} />
        </Flex>
      ) : (
        <Flex flexDirection="column">
          {mainArticle && (
            <Article.Featured
              className={styles.mainArticle}
              article={mainArticle}
              variant="main"
            />
          )}
          <Flex
            className={styles.list}
            justifyContent="space-between"
            flexWrap="wrap"
          >
            {restOfArticles.map(article => (
              <Article.Featured
                key={article.id}
                article={article}
                variant="regular"
              />
            ))}
          </Flex>
        </Flex>
      )}
    </Flex>
  );
};

export default FeaturedArticles;
