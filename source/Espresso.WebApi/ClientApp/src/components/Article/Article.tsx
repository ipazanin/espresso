import React from 'react';
import moment from 'moment';
import { css } from 'aphrodite';
import { ArticleModel } from 'models/article.model';
import { articleStyle } from './Article.style';

interface ArticleProps extends React.HTMLAttributes<HTMLDivElement> {
  key: string;
  article: ArticleModel;
}

const getArticleAgeDisplayString = (publishDateTimeString: string): string => {
  const publishDateTime = moment.utc(publishDateTimeString);

  const utcNow = moment(new Date().toUTCString()).utc();

  const diffInMinutes = utcNow.diff(publishDateTime, 'minutes');

  if (diffInMinutes < 60) {
    return `prije ${diffInMinutes}m`;
  }

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
        <img
          alt={article.title}
          src={article.imageUrl}
          className={css(articleStyle.img)}
        />
        <div className={css(articleStyle.textContainer)}>
          <div className={css(articleStyle.titleContainer)}>
            {article.title}
          </div>
          <div className={css(articleStyle.newsPortalName)}>
            <div>{article.newsPortal.name}</div>
            <div className={css(articleStyle.smallDot)} />
            <div>{getArticleAgeDisplayString(article.publishDateTime)}</div>
          </div>
        </div>
      </article>
    );
  }
);

export default Article;
