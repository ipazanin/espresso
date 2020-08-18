import React, { useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';
import { ArticleModel, GetLatestArticlesResponseModel } from 'models';
import { FixedSizeList, ListChildComponentProps } from 'react-window';
import { articleService } from 'services';
import Article from './Article/Article';

interface FixedSizeListRenderFnProps extends ListChildComponentProps {
  // eslint-disable-next-line react/no-unused-prop-types
  data: ArticleModel[];
}

const ArticleList: React.FC = () => {
  const [state, setState] = useState<GetLatestArticlesResponseModel>({
    articles: [],
    newNewsPortals: [],
    newNewsPortalsPosition: 2,
  });
  const [articleRef, inView] = useInView();

  const fetchArticles = React.useCallback(async () => {
    const response = await articleService.getLatestArticles();

    setState(prevState => ({
      articles: [...prevState.articles, ...response.articles],
      newNewsPortals: response.newNewsPortals,
      newNewsPortalsPosition: response.newNewsPortalsPosition,
    }));
  }, []);

  useEffect(() => {
    fetchArticles();
  }, [fetchArticles]);

  useEffect(() => {
    if (inView) {
      fetchArticles();
    }
  }, [inView, fetchArticles]);

  return (
    <>
      <div>Najnovije</div>
      <FixedSizeList
        height={800}
        itemData={state.articles}
        itemCount={state.articles.length}
        itemSize={120}
        overscanCount={15}
        width="40%"
        style={{ margin: '10px' }}
      >
        {({ data, index, style }: FixedSizeListRenderFnProps) => {
          const article = data[index];

          if (!article) {
            return null;
          }

          const articleStyle: React.CSSProperties = {
            display: 'flex',
            justifyContent: 'flex-start',
          };

          return (
            <Article
              key={article.id}
              article={article}
              style={{ ...articleStyle, ...style }}
              ref={
                state.articles.length - index === 15 ? articleRef : undefined
              }
            />
          );
        }}
      </FixedSizeList>
    </>
  );
};

export default ArticleList;
