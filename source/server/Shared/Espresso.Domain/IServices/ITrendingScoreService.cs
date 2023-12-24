// ITrendingScoreService.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.IServices;

public interface ITrendingScoreService
{
    public IReadOnlyList<Article> CalculateTrendingScore(IReadOnlyList<Article> articles);
}
