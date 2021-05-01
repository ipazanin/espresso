using System.Collections.Generic;
using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices
{
    public interface ITrendingScoreService
    {
        public IEnumerable<Article> CalculateTrendingScore(IEnumerable<Article> articles);
    }
}