import React, { useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';
import { ArticleModel } from 'models';
import { FixedSizeList, ListChildComponentProps } from 'react-window';
import { articleService } from 'services';
import Article from './Article/Article';

interface FixedSizeListRenderFnProps extends ListChildComponentProps {
  data: ArticleModel[];
}

const ArticleList: React.FC = () => {
  const [articles, setArticles] = useState<ArticleModel[]>([]);
  const [articleRef, inView] = useInView();

  const loadMoreArticles = React.useCallback(async () => {
    const response = await articleService.getLatestArticles();

    setArticles(prevArticles => [...prevArticles, ...response.articles]);
  }, []);

  useEffect(() => {
    loadMoreArticles();
  }, [loadMoreArticles]);

  useEffect(() => {
    if (inView) {
      loadMoreArticles();
    }
  }, [inView, loadMoreArticles]);

  return (
    <FixedSizeList
      height={800}
      itemData={articles}
      itemCount={articles.length}
      itemSize={80}
      overscanCount={5}
      width="100%"
    >
      {({ data, index, style }: FixedSizeListRenderFnProps) => {
        const item = data[index];

        if (!item) {
          return null;
        }

        const { id } = item;

        return (
          <Article
            key={id}
            id={id}
            style={style}
            ref={articles.length - index === 5 ? articleRef : undefined}
          />
        );
      }}
    </FixedSizeList>
  );
};

export default ArticleList;
