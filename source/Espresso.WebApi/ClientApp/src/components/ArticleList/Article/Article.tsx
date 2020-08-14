import React from 'react';

interface ArticleProps extends React.HTMLAttributes<HTMLDivElement> {
  id: string;
}

const Article = React.forwardRef<HTMLDivElement, ArticleProps>(
  ({ id, ...props }, ref) => {
    return (
      <div ref={ref} {...props}>
        {id}
      </div>
    );
  }
);

export default Article;
