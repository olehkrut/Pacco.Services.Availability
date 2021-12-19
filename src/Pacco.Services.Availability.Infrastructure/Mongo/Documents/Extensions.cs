using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;
using System;
using System.Linq;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static Resource AsEntity(this ResourceDocument document)
            => new Resource(
                document.Id,
                document.Tags,
                document.Reservations?.Select(r => new Reservation(r.TimeStamp.AsDateTime(), r.Priority)).ToList());

        public static ResourceDocument AsDocument(this Resource entity)
            => new ResourceDocument
            {
                Id = entity.Id,
                Tags = entity.Tags,
                Reservations = entity.Reservations?.Select(r => new ReservationDocument
                {
                    Priority = r.Priority,
                    TimeStamp = r.DateTime.AsDaysSinceEpoch(),
                }).ToList(),
            };

        public static int AsDaysSinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;

        public static DateTime AsDateTime(this int daysSinceEpoch)
            => new DateTime().AddDays(daysSinceEpoch);
    }
}
