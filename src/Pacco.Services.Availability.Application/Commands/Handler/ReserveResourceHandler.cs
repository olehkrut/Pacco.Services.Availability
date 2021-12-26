﻿using Convey.CQRS.Commands;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Core.Repositories;
using Pacco.Services.Availability.Core.ValueObjects;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Commands.Handler
{
    public class ReserveResourceHandler : ICommandHandler<ReserveResource>
    {
        private readonly IResourcesRepository _resourceRepository;
        private readonly IEventProcessor _eventProcessor;

        public ReserveResourceHandler(IResourcesRepository resourceRepository, IEventProcessor eventProcessor)
        {
            _resourceRepository = resourceRepository;
            _eventProcessor = eventProcessor;
        }

        public async Task HandleAsync(ReserveResource command)
        {
            var resource = await _resourceRepository.GetAsync(command.ResourceId);
            if (resource == null)
            {
                throw new ResourceNotFoundException(command.ResourceId);
            }

            var reservation = new Reservation(command.DateTime, command.Priority);
            resource.AddReservation(reservation);

            await _resourceRepository.UpdateAsync(resource);
            await _eventProcessor.ProcessAsync(resource.Events);
        }
    }
}
