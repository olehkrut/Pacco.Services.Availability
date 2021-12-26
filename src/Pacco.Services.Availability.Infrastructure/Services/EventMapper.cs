using Convey.CQRS.Events;
using Pacco.Services.Availability.Application.Events;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Events;
using System.Collections.Generic;
using System.Linq;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    internal sealed class EventMapper : IEventMapper
    {
        public IEvent Map(IDomainEvent domainEvent)
            => domainEvent switch
            {
                ResourceCreated e => new ResourceAdded(e.Resource.Id),
                ReservationAdded e => new ResourceReserved(e.Resource.Id, e.Reservation.DateTime),
                ReservationCanceled e => new ResourceReservationCanceled(e.Resource.Id, e.Reservation.DateTime),
                _ => null,
            };

        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> domainEvents) => domainEvents?.Select(Map);
    }
}
