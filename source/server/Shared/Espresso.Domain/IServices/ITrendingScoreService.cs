// ITrendingScoreService.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices;

public interface ITrendingScoreService
{
    public IEnumerable<Article> CalculateTrendingScore(IEnumerable<Article> articles);
}
