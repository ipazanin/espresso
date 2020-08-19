import React, { useState, useEffect } from 'react';
import ArticleList from 'components/ArticleList';
import FeaturedArticles from 'components/FeaturedArticles';
import { GetLatestArticlesResponseModel } from 'models';
import { articleService } from 'services';
import { useInView } from 'react-intersection-observer';
import { css } from 'aphrodite';
import { homeStyle } from './Home.style';

const Home: React.FC = () => {
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
    <div className={css(homeStyle.container)}>
      <FeaturedArticles />
      <ArticleList articles={state.articles} articleRef={articleRef} />
    </div>
  );
};

export default Home;
