import React from 'react';
import { FixedSizeList, ListChildComponentProps } from 'react-window';
import { ArticleModel } from '../../models';
import Article from '../Article/Article';

interface FixedSizeListRenderFnProps extends ListChildComponentProps {
  // eslint-disable-next-line react/no-unused-prop-types
  data: ArticleModel[];
}

const articleStyle: React.CSSProperties = {
  display: 'flex',
  justifyContent: 'flex-start',
};

interface ArticleListProps {
  articles: ArticleModel[];

  articleRef: (node?: Element | null | undefined) => void;
}

const ArticleList: React.FC<ArticleListProps> = ({ articles, articleRef }) => {
  return (
    <>
      <div>Najnovije</div>
      <FixedSizeList
        height={800}
        itemData={articles}
        itemCount={articles.length}
        itemSize={120}
        overscanCount={5}
        width="40%"
        style={{ margin: '10px' }}
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
              style={{ ...articleStyle, ...style }}
              ref={articles.length - index === 5 ? articleRef : undefined}
            />
          );
        }}
      </FixedSizeList>
    </>
  );
};

export default ArticleList;
