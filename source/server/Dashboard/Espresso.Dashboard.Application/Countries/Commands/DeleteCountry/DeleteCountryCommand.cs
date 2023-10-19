// DeleteCountryCommand.cs
//
// © 2022 Espresso News. All rights reserved.

using MediatR;

namespace Espresso.Dashboard.Application.Countries.Commands.DeleteCountry;

public sealed class DeleteCountryCommand : IRequest
{
    public DeleteCountryCommand(int countryId)
    {
        CountryId = countryId;
    }

    public int CountryId { get; }
}
