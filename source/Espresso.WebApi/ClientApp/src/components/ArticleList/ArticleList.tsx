import React, { useEffect, useState } from 'react';
import axios from 'axios';
import InfiniteScroll from 'react-infinite-scroll-component';
import { ConfigurationBuilder } from '../../config/index';
import Article from '../Article/Article';

interface ArticleModel {
  id: string;
  title: string;
  url: string;
  imageUrl: string;
}

const ArticleList: React.FC = () => {
  const [state, setState] = useState({
    articles: [] as ArticleModel[],
    skip: 0,
  });
  const configuration = ConfigurationBuilder.getConfiguration();

  const fetchData = () => {
    axios
      .get(
        `${configuration.getServerUrl()}/api/articles?take=20&skip=${
          state.skip
        }`,
        {
          headers: configuration.getHeaders(),
        }
      )
      .then((res) => {
        const articles = state.articles.concat(res.data.articles);
        setState({ ...res.data, articles, skip: state.skip + 20 });
        console.log('state:', state);
      })
      .catch((error) => console.log(error));
    console.log('fetch data');
  };

  const refresh = () => {
    setState({ ...state, skip: 0 });
    fetchData();
    console.log('refresh');
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <InfiniteScroll
        dataLength={state.articles.length} // This is important field to render the next data
        next={fetchData}
        hasMore
        loader={<h4>Loading...</h4>}
        refreshFunction={refresh}
        pullDownToRefresh
        pullDownToRefreshContent={
          <h3 style={{ textAlign: 'center' }}>&#8595; Pull down to refresh</h3>
        }
        releaseToRefreshContent={
          <h3 style={{ textAlign: 'center' }}>&#8593; Release to refresh</h3>
        }
        pullDownToRefreshThreshold={20}
      >
        {state.articles.map((article) => (
          <Article
            key={article.id}
            title={article.title}
            url={article.url}
            imageUrl={article.imageUrl}
          />
        ))}
      </InfiniteScroll>
    </>
  );
};

export default ArticleList;
