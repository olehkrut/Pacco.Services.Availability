using Pacco.Services.Availability.Application.DTO;
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
                document.Reservations?.Select(r => new Reservation(r.TimeStamp.AsDateTime(), r.Priority)).ToList(),
                document.Version);

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
                Version = entity.Version
            };

        public static ResourceDto AsDto(this ResourceDocument document)
            => new ResourceDto
            {
                Id = document.Id,
                Tags = document.Tags ?? Enumerable.Empty<string>(),
                Reservations = document.Reservations?.Select(r => AsDto(r)) ?? Enumerable.Empty<ReservationDto>(),
            };

        private static ReservationDto AsDto(ReservationDocument r)
            => new ReservationDto
            {
                Priority = r.Priority,
                DateTime = r.TimeStamp.AsDateTime()
            };

        public static int AsDaysSinceEpoch(this DateTime dateTime)
            => (dateTime - new DateTime()).Days;

        public static DateTime AsDateTime(this int daysSinceEpoch)
            => new DateTime().AddDays(daysSinceEpoch);
    }
}
