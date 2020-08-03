import React from 'react';

interface ArticleProps {
  title: string;
  url: string;
  imageUrl: string;
}

const Article: React.FC<ArticleProps> = ({
  title,
  url,
  imageUrl,
}: ArticleProps) => (
  <>
    <a href={url} style={{ display: 'block' }}>
      {title}
    </a>
    <img src={imageUrl} alt="" />
    <hr />
  </>
);

export default Article;
