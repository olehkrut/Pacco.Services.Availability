using Pacco.Services.Availability.Core.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Services
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> domainEvents);
    }
}
