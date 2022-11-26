// DeleteOldArticlesCommandResponse.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Dashboard.Application.Articles.Commands.DeleteOldArticles;

public record DeleteOldArticlesCommandResponse
{
    public int NumberOfDeletedDatabaseArticles { get; init; }
}
