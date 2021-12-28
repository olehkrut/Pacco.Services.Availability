using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.MessageBrokers.Outbox;
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
        private readonly IMessageOutbox _messageOutbox;
        private readonly IMessagePropertiesAccessor _messagePropertiesAccessor;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;
        private readonly ILogger<MessageBroker> _logger;

        public MessageBroker(
            IBusPublisher busPublisher,
            IMessageOutbox messageOutbox,
            IMessagePropertiesAccessor messagePropertiesAccessor,
            ICorrelationContextAccessor correlationContextAccessor,
            ILogger<MessageBroker> logger)
        {
            _busPublisher = busPublisher;
            _messageOutbox = messageOutbox;
            _messagePropertiesAccessor = messagePropertiesAccessor;
            _correlationContextAccessor = correlationContextAccessor;
            _logger = logger;
        }

        public Task PublishAsync(params IEvent[] events) => PublishAsync(events.AsEnumerable());

        public async Task PublishAsync(IEnumerable<IEvent> events)
        {
            if (events == null)
            {
                return;
            }

            var correlationId = _messagePropertiesAccessor.MessageProperties?.CorrelationId;
            var correlationContext = _correlationContextAccessor.CorrelationContext;

            foreach (var @event in events)
            {
                if (@event == null)
                {
                    continue;
                }

                var messageId = Guid.NewGuid().ToString("N");
                _logger.LogTrace($"Publishing an integration event: '{@event.GetType().Name.Underscore()}' with ID '{messageId}'");

                if (_messageOutbox.Enabled)
                {
                    await _messageOutbox.SendAsync(@event, messageId: messageId, correlationId: correlationId, messageContext: correlationContext);
                    continue;
                }

                await _busPublisher.PublishAsync(@event, messageId, correlationId: correlationId, messageContext: correlationContext);
            }
        }
    }
}
