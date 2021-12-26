using Convey.CQRS.Events;
using System;

namespace Pacco.Services.Availability.Application.Events
{
    [Contract]
    public class ResourceAdded : IEvent
    {
        public ResourceAdded(Guid resourceId)
        {
            ResourceId = resourceId;
        }

        public Guid ResourceId { get; }
    }
}
