import React from 'react';
import { css } from 'aphrodite';
import { featuredArticlesStyle } from './FeaturedArticles.style';

const FeaturedArticles: React.FC = () => {
  return (
    <>
      <div className={css(featuredArticlesStyle.container)}>Featured</div>
    </>
  );
};

export default FeaturedArticles;
