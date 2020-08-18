import React from 'react';
import { ArticleModel } from 'models';
import moment from 'moment';

interface ArticleProps extends React.HTMLAttributes<HTMLDivElement> {
  key: string;
  article: ArticleModel;
}

const imgStyle: React.CSSProperties = {
  height: '100px',
  width: '100px',
};

const textContainerStyle: React.CSSProperties = {
  marginLeft: '5px',
};

const titleContainerStyle: React.CSSProperties = {
  fontSize: '12px',
};

const newsPortalNameContainerStyle: React.CSSProperties = {
  display: 'flex',
  fontSize: '12px',
};

const smallDotStyle: React.CSSProperties = {
  backgroundColor: '#000',
  borderRadius: '50%',
  display: 'inline-block',
  height: '5px',
  margin: '5px',
  width: '5px',
};

const getArticleAgeDisplayString = (publishDateTimeString: string): string => {
  const publishDateTime = moment(publishDateTimeString);
  const utcNow = moment(Date.now());

  const diffInHours = utcNow.diff(publishDateTime, 'hours');

  if (diffInHours < 24) {
    return `prije ${diffInHours}h`;
  }

  const diffInDays = utcNow.diff(publishDateTime, 'days');

  return `prije ${diffInDays}d`;
};

const Article = React.forwardRef<HTMLDivElement, ArticleProps>(
  ({ article, style, ...props }, ref) => {
    return (
      <article ref={ref} style={{ ...style }} {...props}>
        <img alt={article.title} src={article.imageUrl} style={imgStyle} />
        <div style={textContainerStyle}>
          <div style={titleContainerStyle}>{article.title}</div>
          <div style={newsPortalNameContainerStyle}>
            <div>{article.newsPortal.name}</div>
            <div style={smallDotStyle} />
            <div>{getArticleAgeDisplayString(article.publishDateTime)}</div>
          </div>
        </div>
      </article>
    );
  }
);

export default Article;
