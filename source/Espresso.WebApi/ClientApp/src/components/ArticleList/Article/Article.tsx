import React from 'react';
import { ArticleModel } from 'models';

interface ArticleProps extends React.HTMLAttributes<HTMLDivElement> {
  article: ArticleModel;
}

const Article = React.forwardRef<HTMLDivElement, ArticleProps>(
  ({ article, ...props }, ref) => {
    return (
      <div ref={ref} {...props}>
        {article.id}
      </div>
    );
  }
);

export default Article;
