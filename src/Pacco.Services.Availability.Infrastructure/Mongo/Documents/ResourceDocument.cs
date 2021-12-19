using Convey.Types;
using System;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Documents
{
    internal sealed class ResourceDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public IReadOnlyCollection<string> Tags { get; set; }
        public IReadOnlyCollection<ReservationDocument> Reservations { get; set; }
    }
}
