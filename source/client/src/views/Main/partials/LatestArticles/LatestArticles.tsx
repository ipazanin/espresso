import React from 'react';

import { LoadingSpinner } from 'components/Loader';
import { GetLatestArticlesArticle } from 'models';
import Article from 'components/Article';
import Flex from '../../../../components/Flex/Flex';
import Text from '../../../../components/Text/Text';
import useWindowDimensions from '../../../../hooks/useWindowDimensions';

import styles from './latest_articles.module.scss';

interface LatestArticlesProps {
  articles: GetLatestArticlesArticle[];
  onRefetch: () => void;
  loading: boolean;
}

const LIST_ITEM_HEIGHT = 108;
const SCROLL_THRESHOLD = LIST_ITEM_HEIGHT * 5;

const handleMouseEnter = () => {
  document.body.style.setProperty('overflow-y', 'hidden');
};

const handleMouseLeave = () => {
  document.body.style.removeProperty('overflow-y');
};

const LatestArticles: React.FC<LatestArticlesProps> = ({
  articles,
  onRefetch,
  loading,
}) => {
  const hasCrossedThreshold = React.useRef<boolean>(false);

  const { height } = useWindowDimensions();
  const { LIST_HEIGHT, VISIBLE_LIST_ITEM_COUNT } = React.useMemo(() => {
    const listHeight = height - 138;
    const visibleListItemCount = Math.floor(listHeight / LIST_ITEM_HEIGHT);

    return {
      LIST_HEIGHT: listHeight,
      VISIBLE_LIST_ITEM_COUNT: visibleListItemCount,
    };
  }, [height]);

  const handleListScroll = async (
    e: React.UIEvent<HTMLDivElement, UIEvent>
  ) => {
    const {
      currentTarget: { scrollTop, scrollHeight },
    } = e;

    if (
      scrollTop + LIST_ITEM_HEIGHT * VISIBLE_LIST_ITEM_COUNT >=
        scrollHeight - SCROLL_THRESHOLD &&
      !hasCrossedThreshold.current
    ) {
      hasCrossedThreshold.current = true;
      await onRefetch();
    }
  };

  React.useEffect(() => {
    hasCrossedThreshold.current = false;
  }, [articles.length]);

  return (
    <Flex className={styles.container} disableStyles>
      <Text
        weight="bold"
        align="left"
        color="muted"
        size="caption"
        transform="uppercase"
        className={styles.subtitle}
      >
        Najnovije
      </Text>
      {loading ? (
        <Flex flexOut>
          <LoadingSpinner size={24} />
        </Flex>
      ) : (
        <Flex
          flexDirection="column"
          onScroll={handleListScroll}
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeave}
          className={styles.listWrapper}
          style={{ height: LIST_HEIGHT }}
        >
          <Flex
            flexDirection="column"
            className={styles.list}
            style={{ height: articles.length * LIST_ITEM_HEIGHT }}
          >
            {articles.map(article => (
              <Article.Latest
                key={article.id}
                article={article}
                className={styles.article}
              />
            ))}
          </Flex>
        </Flex>
      )}
    </Flex>
  );
};

export default LatestArticles;
