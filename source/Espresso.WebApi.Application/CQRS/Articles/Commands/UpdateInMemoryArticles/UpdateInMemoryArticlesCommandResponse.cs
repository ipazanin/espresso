namespace Espresso.WebApi.Application.CQRS.Articles.Commands.UpdateInMemoryArticles
{
    public class UpdateInMemoryArticlesCommandResponse
    {
        public int NumberOfUpdatedArticles { get; set; }
        public int NumberOfCreatedArticles { get; set; }

        public UpdateInMemoryArticlesCommandResponse(int numberOfUpdatedArticles, int numberOfCreatedArticles)
        {
            NumberOfUpdatedArticles = numberOfUpdatedArticles;
            NumberOfCreatedArticles = numberOfCreatedArticles;
        }

        public override string ToString()
        {
            return $"{nameof(NumberOfCreatedArticles)}:{NumberOfCreatedArticles}, {nameof(NumberOfUpdatedArticles)}:{NumberOfUpdatedArticles}";
        }
    }
}
