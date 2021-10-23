// DeleteOldArticlesCommandHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Dashboard.Application.DeleteOldArticles
{
    public class DeleteOldArticlesCommandHandler : IRequestHandler<DeleteOldArticlesCommand, DeleteOldArticlesCommandResponse>
    {
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;
        private readonly IRemoveOldArticlesService _removeOldArticlesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteOldArticlesCommandHandler"/> class.
        /// </summary>
        /// <param name="espressoDatabaseContext"></param>
        /// <param name="removeOldArticlesService"></param>
        public DeleteOldArticlesCommandHandler(
            IEspressoDatabaseContext espressoDatabaseContext,
            IRemoveOldArticlesService removeOldArticlesService)
        {
            _espressoDatabaseContext = espressoDatabaseContext;
            _removeOldArticlesService = removeOldArticlesService;
        }

        public async Task<DeleteOldArticlesCommandResponse> Handle(DeleteOldArticlesCommand request, CancellationToken cancellationToken)
        {
            var oldArticles = _removeOldArticlesService.RemoveOldArticlesFromCollection(request.Articles);

            var similarArticlesToDelete = oldArticles
                .SelectMany(oldArticle => oldArticle.SubordinateArticles.Append(oldArticle.MainArticle))
                .Where(similarArticle => similarArticle is not null);

            _espressoDatabaseContext.SimilarArticles.RemoveRange(similarArticlesToDelete!);
            _ = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            _espressoDatabaseContext.Articles.RemoveRange(oldArticles);
            var numberOfDeletedDatabaseArticles = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            var response = new DeleteOldArticlesCommandResponse
            {
                NumberOfDeletedDatabaseArticles = numberOfDeletedDatabaseArticles,
            };

            return response;
        }
    }
}
