using Pacco.Services.Availability.Core.Events;
using Pacco.Services.Availability.Core.Exceptions;
using Pacco.Services.Availability.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacco.Services.Availability.Core.Entities
{
    public class Resource : AggregateRoot
    {
        private IReadOnlyCollection<string> _tags = new HashSet<string>();
        private IReadOnlyCollection<Reservation> _reservations = new HashSet<Reservation>();

        public IReadOnlyCollection<string> Tags
        {
            get => _tags;
            private set => _tags = new HashSet<string>(value);
        }

        public IReadOnlyCollection<Reservation> Reservations
        {
            get => _reservations;
            private set => _reservations = new HashSet<Reservation>(value);
        }

        public Resource(AggregateId id, IReadOnlyCollection<string> tags, IReadOnlyCollection<Reservation> reservations = null, int version = 0)
        {
            ValidateTags(tags);
            Id = id;
            Tags = tags;
            Reservations = reservations ?? Array.Empty<Reservation>();
            Version = version;
        }

        private static void ValidateTags(IEnumerable<string> tags)
        {
            if (tags is null || !tags.Any())
            {
                throw new MissingResourceTagsException();
            }

            if (tags.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidResourceTagsException();
            }
        }

        public static Resource Create(
            AggregateId id,
            IReadOnlyCollection<string> tags,
            IReadOnlyCollection<Reservation> reservations = null)
        {
            var resource = new Resource(id, tags, reservations);
            resource.AddEvent(new ResourceCreated(resource));

            return resource;

        }
    }
}
