using System;
using System.Collections.Generic;
using System.Linq;

namespace Espresso.WebApi.Application.Utilities
{
    public class TrendingScoreUtility
    {
        #region Constants

        private const int HalfOfMaxTrendingScoreValue = 500;
        private const decimal AgeWeight = 1;

        #endregion

        #region Fields

        private readonly IEnumerable<int> _numberOfClicksPerArticle;
        private readonly decimal _averageClicks;
        private readonly int _numberOfArticles;
        private readonly decimal _standardClicksDeviation;
        private readonly decimal _maxTrendingScore;

        #endregion

        #region Constructors

        public TrendingScoreUtility(IEnumerable<int> clicksPerArticle)
        {
            _numberOfClicksPerArticle = clicksPerArticle;
            _numberOfArticles = clicksPerArticle.Count();

            _averageClicks = _numberOfArticles == 0 ? 0M : clicksPerArticle.Select(click => (decimal)click).Average();

            _standardClicksDeviation = _numberOfArticles == 0 ? 0M : (decimal)Math.Sqrt(
                (double)(_numberOfClicksPerArticle.Select(clicks => (decimal)clicks).Sum(clicks =>
                {
                    var pow = (decimal)Math.Pow(decimal.ToDouble(clicks - _averageClicks), 2);
                    return pow;
                }) / (_numberOfArticles == 0 ? 1M : _numberOfArticles))
                );

            _maxTrendingScore = clicksPerArticle.Select(clicks => CalculateUnNormalisedTrendingScore(clicks)).Max();
        }

        #endregion

        private decimal CalculateUnNormalisedTrendingScore(int clicks)
        {
            var trendingScore = (clicks - _averageClicks) / (_standardClicksDeviation == 0 ? 1M : _standardClicksDeviation);

            return trendingScore;
        }

        public decimal CalculateTrendingScore(int clicks, DateTime publishDateTime)
        {
            var trendingScore = CalculateUnNormalisedTrendingScore(clicks);

            var articleAge = DateTime.UtcNow - publishDateTime;
            var articleAgeInDays = (decimal)articleAge.TotalDays;
            var dayAgeWeight = _maxTrendingScore * AgeWeight;

            var trendingScoreWithArticleAge = trendingScore - (dayAgeWeight * articleAgeInDays);

            var normalisedTrendingScore = NormaliseTrendingScore(trendingScoreWithArticleAge);

            return normalisedTrendingScore < 0 ? 0 : normalisedTrendingScore >= 1000M ? 999M : normalisedTrendingScore;
        }

        private decimal NormaliseTrendingScore(decimal trendingScore)
        {
            var maxTrendingScore = _maxTrendingScore == 0 ? 1 : _maxTrendingScore;
            var normalisedTrendingScore = (trendingScore / maxTrendingScore * HalfOfMaxTrendingScoreValue) + HalfOfMaxTrendingScoreValue;

            return normalisedTrendingScore;
        }

        //public static long CalculateTrendingScore(int numberOfClicks, DateTime publishDateTime)
        //{
        //    var random = new Random();
        //    var variance = 0.01;

        //    var baseTimeScore = (publishDateTime - DateTimeConstants.TrendingReferentDateTime).TotalMinutes;
        //    var timeScore = baseTimeScore + (baseTimeScore * random.NextDouble() * variance);

        //    var timeScoreFor1DayAge = TimeSpan.FromDays(1).TotalMinutes;
        //    var clickWeight = 100;
        //    var baseClickScore = numberOfClicks * (timeScoreFor1DayAge / clickWeight);
        //    var clickScore = baseClickScore + (baseClickScore * random.NextDouble() * variance);

        //    var trendingScore = clickScore + timeScore;
        //    return (long)trendingScore;
        //}
    }
}
