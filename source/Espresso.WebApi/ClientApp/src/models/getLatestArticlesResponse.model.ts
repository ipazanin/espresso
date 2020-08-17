import { ArticleModel } from './article.model';
import { NewsPortalModel } from './newsPortal.model';

export interface GetLatestArticlesResponseModel {
  articles: ArticleModel[];

  newNewsPortals: NewsPortalModel[];

  newNewsPortalsPosition: number;
}
