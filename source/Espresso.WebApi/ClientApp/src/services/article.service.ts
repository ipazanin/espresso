import axios from 'axios';
import { GetLatestArticlesResponseModel } from '../models';

class ArticleService {
  private currentSkip = 0;

  private defaultSkipIncrement = 20;

  private defaultTake = 20;

  public async getLatestArticles(): Promise<GetLatestArticlesResponseModel> {
    this.currentSkip += this.defaultSkipIncrement;

    try {
      const res = await axios.get<GetLatestArticlesResponseModel>(
        `/api/articles?take=${this.defaultTake}&skip=${this.currentSkip}`
      );
      return res.data;
    } catch (err) {
      console.log('err while fetching latest articles:', err);
      return { articles: [] };
    }
  }
}

export const articleService = new ArticleService();
