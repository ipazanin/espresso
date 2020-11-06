using Espresso.Application.Infrastructure.MediatorInfrastructure;

namespace Espresso.ParserDeleter.Application.GroupSimilarArticles
{
    public record GroupSimilarArticlesCommand : Request<GroupSimilarArticlesCommandResponse>
    {
        public string ParserApiKey { get; init; } = "";
        public string ServerUrl { get; init; } = "";
    }
}