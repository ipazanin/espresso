// NotFoundException.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.WebApi.Application.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="id"></param>
    public NotFoundException(string typeName, string id)
        : base($"{typeName} with id: {id} was not found!")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public NotFoundException(string? message)
        : base(message)
    {
    }

    public NotFoundException()
    {
    }

    public NotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
