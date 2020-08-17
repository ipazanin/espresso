import React, { useEffect, useState } from 'react';
import { useInView } from 'react-intersection-observer';
import { ArticleModel, GetLatestArticlesResponseModel } from 'models';
import { FixedSizeList, ListChildComponentProps } from 'react-window';
import { articleService } from 'services';
import Article from './Article/Article';

interface FixedSizeListRenderFnProps extends ListChildComponentProps {
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
    <FixedSizeList
      height={800}
      itemData={state.articles}
      itemCount={state.articles.length}
      itemSize={80}
      overscanCount={5}
      width="100%"
    >
      {({ data, index, style }: FixedSizeListRenderFnProps) => {
        const article = data[index];

        if (!article) {
          return null;
        }

        return (
          <Article
            key={article.id}
            article={article}
            style={style}
            ref={state.articles.length - index === 5 ? articleRef : undefined}
          />
        );
      }}
    </FixedSizeList>
  );
};

export default ArticleList;
