using System;

namespace Espresso.WebApi.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string typeName, string id) : base($"{typeName} with id: {id} was not found!")
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
