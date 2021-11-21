// ArticleData.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.Entities;

namespace Espresso.Domain.Records
{
    public class ArticleData
    {
        public Guid Id { get; set; }

        public string? Url { get; set; }

        public string? WebUrl { get; set; }

        public string? Summary { get; set; }

        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset UpdateDateTime { get; set; }

        public DateTimeOffset? PublishDateTime { get; set; }

        public int NumberOfClicks { get; set; }

        public decimal TrendingScore { get; set; }

        public IEnumerable<ArticleCategory>? ArticleCategories { get; set; }
    }
}
