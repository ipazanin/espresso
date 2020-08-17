import { NewsPortalModel } from './newsPortal.model';
import { CategoryModel } from './category.model';

export interface ArticleModel {
  id: string;
  title: string;
  utl: string;
  imageUrl: string;
  publishDateTime: string;
  newsPortal: NewsPortalModel;
  categories: CategoryModel[];
}
