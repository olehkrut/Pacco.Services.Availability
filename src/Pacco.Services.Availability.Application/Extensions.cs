using Convey;
using Convey.CQRS.Commands;

namespace Pacco.Services.Availability.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
           => builder
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher();
    }
}
