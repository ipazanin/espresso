import axios from 'axios';
import { GetLatestArticlesResponseModel } from '../models';

class ArticleService {
  private defaultTake = 20;

  private currentSkip = 0;

  private defaultSkipIncrement = this.defaultTake;

  public async getLatestArticles(): Promise<GetLatestArticlesResponseModel> {
    this.currentSkip += this.defaultSkipIncrement;

    try {
      const res = await axios.get<GetLatestArticlesResponseModel>(
        `/api/articles?take=${this.defaultTake}&skip=${this.currentSkip}`
      );
      return res.data;
    } catch (err) {
      console.log('err while fetching latest articles:', err);
      return { articles: [], newNewsPortals: [], newNewsPortalsPosition: 0 };
    }
  }
}

export const articleService = new ArticleService();
