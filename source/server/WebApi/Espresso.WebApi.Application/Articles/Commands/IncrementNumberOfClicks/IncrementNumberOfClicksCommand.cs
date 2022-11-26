// IncrementNumberOfClicksCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using Espresso.Application.Infrastructure.MediatorInfrastructure;
using MediatR;

namespace Espresso.WebApi.Application.Articles.Commands.IncrementNumberOfClicks;

public record IncrementNumberOfClicksCommand : Request<Unit>
{
    public Guid Id { get; init; }
}
