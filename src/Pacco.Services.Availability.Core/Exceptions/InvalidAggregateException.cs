using System;

namespace Pacco.Services.Availability.Core.Exceptions
{
    public class InvalidAggregateException : DomainException
    {
        public InvalidAggregateException(Guid id) : base($"Invalid aggregate id: '{id}'")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
