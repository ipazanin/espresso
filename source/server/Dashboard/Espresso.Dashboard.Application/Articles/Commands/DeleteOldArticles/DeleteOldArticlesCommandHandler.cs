// DeleteOldArticlesCommandHandler.cs
//
// © 2021 Espresso News. All rights reserved.

using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Espresso.Dashboard.Application.Articles.Commands.DeleteOldArticles;

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
        var oldArticleIds = oldArticles.Select(article => article.Id).ToArray();

        var databaseArticlesToRemove = await _espressoDatabaseContext
            .Articles
            .Where(article => oldArticleIds.Contains(article.Id))
            .ToListAsync(cancellationToken);

        _espressoDatabaseContext.Articles.RemoveRange(databaseArticlesToRemove);
        var numberOfDeletedDatabaseArticles = await _espressoDatabaseContext.SaveChangesAsync(cancellationToken);

        var response = new DeleteOldArticlesCommandResponse
        {
            NumberOfDeletedDatabaseArticles = numberOfDeletedDatabaseArticles,
        };

        return response;
    }
}
