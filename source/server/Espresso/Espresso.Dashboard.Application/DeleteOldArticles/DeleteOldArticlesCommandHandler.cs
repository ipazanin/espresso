﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Domain.Entities;
using Espresso.Domain.IServices;
using Espresso.Persistence.Database;
using MediatR;

using Microsoft.Extensions.Caching.Memory;

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
