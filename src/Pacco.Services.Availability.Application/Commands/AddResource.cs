using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Application.Commands
{
    [Contract]
    public class AddResource : ICommand
    {
        public AddResource(Guid resourceId, IReadOnlyCollection<string> tags)
        {
            ResourceId = resourceId;
            Tags = tags;
        }

        public Guid ResourceId { get; }
        public IReadOnlyCollection<string> Tags { get; }
    }
}
