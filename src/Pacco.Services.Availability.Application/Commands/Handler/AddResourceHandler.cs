using Convey.CQRS.Commands;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Handler
{
    public class AddResourceHandler : ICommandHandler<AddResource>
    {
        private readonly IResourcesRepository _resourcesRepository;
        private readonly IEventProcessor _eventProcessor;

        public AddResourceHandler(IResourcesRepository resourcesRepository, IMessageBroker messageBroker, IEventProcessor eventProcessor)
        {
            _resourcesRepository = resourcesRepository;
            _eventProcessor = eventProcessor;
        }

        public async Task HandleAsync(AddResource command)
        {
            var resource = await _resourcesRepository.GetAsync(command.ResourceId);

            if (resource is { })
            {
                throw new ResourceAlreadyExistsException(command.ResourceId);
            }

            resource = Resource.Create(command.ResourceId, command.Tags);
            await _resourcesRepository.AddAsync(resource);

            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}
