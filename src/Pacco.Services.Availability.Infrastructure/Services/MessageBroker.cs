using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Microsoft.Extensions.Logging;
using Pacco.Services.Availability.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Services
{
    public class MessageBroker : IMessageBroker
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<MessageBroker> _logger;

        public MessageBroker(IBusPublisher busPublisher, ILogger<MessageBroker> logger)
        {
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public Task PublishAsync(params IEvent[] events) => PublishAsync(events.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events == null)
            {
                return;
            }

            foreach (var @event in events)
            {
                if (@event == null)
                {
                    continue;
                }

                var messageId = Guid.NewGuid().ToString("N");
                _logger.LogTrace($"Publishing an integration event: '{@event.GetType().Name.Underscore()}' with ID '{messageId}'");

                await _busPublisher.PublishAsync(@event, messageId);
            }
        }
    }
}
