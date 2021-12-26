using Convey.CQRS.Events;
using Pacco.Services.Availability.Core.Events;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent domainEvent);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> domainEvents);
    }
}
