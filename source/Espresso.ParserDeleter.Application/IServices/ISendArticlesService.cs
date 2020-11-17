using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.Entities;

namespace Espresso.ParserDeleter.Application.IServices
{
    public interface ISendArticlesService
    {
        public Task SendArticlesMessage(
            IEnumerable<Article> createArticles,
            IEnumerable<Article> updateArticles,
            CancellationToken cancellationToken
        );
    }
}