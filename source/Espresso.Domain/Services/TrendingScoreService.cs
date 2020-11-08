using System;
using System.Collections.Generic;
using System.Linq;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;

namespace Espresso.Domain.Services
{
    public class TrendingScoreService : ITrendingScoreService
    {
        #region Fields
        private readonly int _halfOfMaxTrendingScoreValue;
        private readonly decimal _ageWeight;

        #endregion

        #region Constructors
        public TrendingScoreService(
            int halfOfMaxTrendingScoreValue,
            decimal ageWeight
        )
        {
            _halfOfMaxTrendingScoreValue = halfOfMaxTrendingScoreValue;
            _ageWeight = ageWeight;
        }
        #endregion

        #region Methods

        public IEnumerable<Article> CalculateTrendingScore(IEnumerable<Article> articles)
        {
            var clicksPerArticle = articles.Select(articleKey => articleKey.NumberOfClicks);
            var numberOfArticles = articles.Count();

            var averageClicks = numberOfArticles == 0 ? 0M : clicksPerArticle.Select(click => (decimal)click).Average();

            var standardClicksDeviation = CalculateStandardDeviation(
                clicksPerArticle: clicksPerArticle,
                numberOfArticles: numberOfArticles,
                averageClicks: averageClicks
            );

            var maxTrendingScore = clicksPerArticle
                .Select(clicks => CalculateUnNormalisedTrendingScore(
                    clicks: clicks,
                    averageClicks: averageClicks,
                    standardClicksDeviation: standardClicksDeviation
                )).Max();

            foreach (var article in articles)
            {
                article.UpdateTrendingScore(
                    trendingScore: CalculateTrendingScore(
                        clicks: article.NumberOfClicks,
                        averageClicks: averageClicks,
                        standardClicksDeviation: standardClicksDeviation,
                        maxTrendingScore: maxTrendingScore,
                        publishDateTime: article.PublishDateTime
                    )
                );
            }

            return articles;
        }

        private static decimal CalculateStandardDeviation(
            IEnumerable<int> clicksPerArticle,
            int numberOfArticles,
            decimal averageClicks
        )
        {
            var standardClicksDeviation = numberOfArticles == 0 ? 0M : (decimal)Math.Sqrt(
                (double)(clicksPerArticle.Select(clicks => (decimal)clicks).Sum(clicks =>
                {
                    var pow = (decimal)Math.Pow(decimal.ToDouble(clicks - averageClicks), 2);
                    return pow;
                }) / (numberOfArticles == 0 ? 1M : numberOfArticles))
            );

            return standardClicksDeviation;
        }

        private static decimal CalculateUnNormalisedTrendingScore(
            int clicks,
            decimal averageClicks,
            decimal standardClicksDeviation
        )
        {
            var trendingScore = (clicks - averageClicks) / (standardClicksDeviation == 0 ? 1M : standardClicksDeviation);

            return trendingScore;
        }

        public decimal CalculateTrendingScore(
            int clicks,
            decimal averageClicks,
            decimal standardClicksDeviation,
            decimal maxTrendingScore,
            DateTime publishDateTime
        )
        {
            var trendingScore = CalculateUnNormalisedTrendingScore(
                    clicks: clicks,
                    averageClicks: averageClicks,
                    standardClicksDeviation: standardClicksDeviation
                );

            var articleAge = DateTime.UtcNow - publishDateTime;
            var articleAgeInDays = (decimal)articleAge.TotalDays;
            var dayAgeWeight = maxTrendingScore * _ageWeight;

            var trendingScoreWithArticleAge = trendingScore - (dayAgeWeight * articleAgeInDays);

            var normalisedTrendingScore = NormaliseTrendingScore(
                trendingScore: trendingScoreWithArticleAge,
                maxTrendingScore: maxTrendingScore
            );

            return normalisedTrendingScore < 0 ? 0 : normalisedTrendingScore >= 1000M ? 999M : normalisedTrendingScore;
        }

        private decimal NormaliseTrendingScore(decimal trendingScore, decimal maxTrendingScore)
        {
            var normalisedMaxTrendingScore = maxTrendingScore == 0 ? 1 : maxTrendingScore;
            var normalisedTrendingScore = (trendingScore / normalisedMaxTrendingScore * _halfOfMaxTrendingScoreValue) + _halfOfMaxTrendingScoreValue;

            return normalisedTrendingScore;
        }
        #endregion
    }
}