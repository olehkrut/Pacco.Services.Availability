using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public class ResourceNotFoundException : AppException
    {
        public ResourceNotFoundException(Guid id) : base($"Resource with ID: '{id}' wasn't found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}