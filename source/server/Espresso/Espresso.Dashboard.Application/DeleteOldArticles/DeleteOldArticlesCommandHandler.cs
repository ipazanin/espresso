using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;

namespace Espresso.Dashboard.Application.DeleteOldArticles
{
    public class DeleteOldArticlesCommandHandler : IRequestHandler<DeleteOldArticlesCommand, DeleteOldArticlesCommandResponse>
    {
        #region Fields
        private readonly IEspressoDatabaseContext _espressoDatabaseContext;
        private readonly IRemoveOldArticlesService _removeOldArticlesService;
        #endregion

        #region Constructors
        public DeleteOldArticlesCommandHandler(
            IEspressoDatabaseContext espressoDatabaseContext,
            IRemoveOldArticlesService removeOldArticlesService
        )
        {
            _espressoDatabaseContext = espressoDatabaseContext;
            _removeOldArticlesService = removeOldArticlesService;
        }
        #endregion

        #region Methods
        public async Task<DeleteOldArticlesCommandResponse> Handle(DeleteOldArticlesCommand request, CancellationToken cancellationToken)
        {
            var oldArticles = _removeOldArticlesService.RemoveOldArticlesFromCollection(request.Articles);

            var similarArticlesToDelete = oldArticles
                .SelectMany(oldArticle => oldArticle.SubordinateArticles.Append(oldArticle.MainArticle))
                .Where(similarArticle => similarArticle is not null);

            _espressoDatabaseContext.SimilarArticles.RemoveRange(similarArticlesToDelete!);
            _espressoDatabaseContext.Articles.RemoveRange(oldArticles);
            var numberOfDeletedDatabaseArticles = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

            var response = new DeleteOldArticlesCommandResponse
            {
                NumberOfDeletedDatabaseArticles = numberOfDeletedDatabaseArticles
            };

            return response;
        }
        #endregion
    }
}
