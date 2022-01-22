using Convey;
using Convey.CQRS.Commands;
using OpenTracing;
using OpenTracing.Tag;
using System;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Decorators
{
    internal sealed class JaegerCommandHalderDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly ITracer _tracer;

        public JaegerCommandHalderDecorator(ICommandHandler<T> handler, ITracer tracer)
        {
            _handler = handler;
            _tracer = tracer;
        }

        public async Task HandleAsync(T command)
        {
            var commandName = command.GetType().Name.Underscore();

            using var scope = BuildScope(commandName);
            var span = scope.Span;

            try
            {
                span.Log($"Handling a message: '{commandName}'");
                await _handler.HandleAsync(command);
                span.Log($"Handled a message: '{commandName}'");
            }
            catch (Exception ex)
            {
                span.Log(ex.Message);
                span.SetTag(Tags.Error, true);
                throw;
            }
        }

        private IScope BuildScope(string commandName)
        {
            var scope = _tracer
                .BuildSpan($"handling-{commandName}")
                .WithTag("message-type", commandName);

            if (_tracer.ActiveSpan is { })
            {
                scope.AddReference(References.ChildOf, _tracer.ActiveSpan.Context);
            }

            return scope.StartActive(true);
        }
    }
}
