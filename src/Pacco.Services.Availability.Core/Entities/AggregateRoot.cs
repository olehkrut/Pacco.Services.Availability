using Pacco.Services.Availability.Core.Events;
using System.Collections.Generic;
using System.Linq;

namespace Pacco.Services.Availability.Core.Entities
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }
        public IReadOnlyCollection<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
            {
                Version++;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}
