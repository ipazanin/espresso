using System;

namespace Espresso.Domain.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(string modelName, string propertyName, object propertyValue, string message = "")
            : base($"Invalid {propertyName}: {(propertyValue ?? "null")} on model {modelName}. {message}") { }

        public InvalidModelException(string modelName, string message = "")
            : base($"{modelName} cann't be null. {message}") { }
    }
}
