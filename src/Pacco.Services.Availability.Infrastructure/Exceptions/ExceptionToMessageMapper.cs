using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.Events.RejectedEvents;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Exceptions;
using System;

namespace Pacco.Services.Availability.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                ResourceAlreadyExistsException ex => new AddResourceRejected(ex.Message, ex.Code, ex.Id),
                ResourceNotFoundException ex => message switch
                {
                    ReserveResource command => new ReserveResourceRejected(ex.Message, ex.Code, ex.Id),
                    _ => null
                },
                CannotExpropriateReservationException ex => new ReserveResourceRejected(ex.Message, ex.Code, ex.ResourceId),
                _ => null
            };
    }
}
