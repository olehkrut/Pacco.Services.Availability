﻿using Convey.CQRS.Events;
using System;

namespace Pacco.Services.Availability.Application.Events.RejectedEvents
{
    [Contract]
    public class ReserveResourceRejected : IRejectedEvent
    {
        public ReserveResourceRejected(string reason, string code, Guid resourceId)
        {
            Reason = reason;
            Code = code;
            ResourceId = resourceId;
        }

        public string Reason { get; }
        public string Code { get; }

        public Guid ResourceId { get; }
    }
}
