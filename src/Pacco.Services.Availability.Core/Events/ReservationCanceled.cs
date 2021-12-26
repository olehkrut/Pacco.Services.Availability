﻿using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.ValueObjects;

namespace Pacco.Services.Availability.Core.Events
{
    public class ReservationCanceled : IDomainEvent
    {
        public ReservationCanceled(Resource resource, Reservation reservation)
        {
            Resource = resource;
            Reservation = reservation;
        }

        public Resource Resource { get; }
        public Reservation Reservation { get; }
    }
}
