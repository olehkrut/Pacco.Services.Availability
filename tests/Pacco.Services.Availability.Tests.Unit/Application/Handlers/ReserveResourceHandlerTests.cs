using Convey.CQRS.Commands;
using NSubstitute;
using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.Commands.Handler;
using Pacco.Services.Availability.Application.DTO.External;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Application.Services;
using Pacco.Services.Availability.Application.Services.Clients;
using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Repositories;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pacco.Services.Availability.Tests.Unit.Application.Handlers
{
    public class ReserveResourceHandlerTests
    {
        private Task Act(ReserveResource command) => _handler.HandleAsync(command);

        [Fact]
        public async Task given_invalid_id_reserve_resource_should_throw_exception()
        {
            // Arrange
            var command = new ReserveResource(Guid.NewGuid(), DateTime.UtcNow, 0, Guid.NewGuid());

            // Act
            var exception = await Record.ExceptionAsync(() => Act(command));
             
            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ResourceNotFoundException>();
        }

        [Fact]
        public async Task given_valid_resource_id_for_valid_customer_reserve_resource_should_succeed()
        {
            // Arrange
            var command = new ReserveResource(Guid.NewGuid(), DateTime.UtcNow, 0, Guid.NewGuid());

            var resource = Resource.Create(command.CustomerId, new[] { "tags" });
            _resourceRepository.GetAsync(command.ResourceId).Returns(resource);

            var customerState = new CustomerStateDto
            {
                State = "valid",
            };
            _customersServiceClient.GetStateAsync(command.CustomerId).Returns(customerState);

            // Act
            await Act(command);

            // Assert
            resource.Reservations.ShouldNotBeEmpty();

            await _resourceRepository.Received().UpdateAsync(resource);
            await _eventProcessor.Received().ProcessAsync(resource.Events);
        }

        #region Arrange

        private readonly IResourcesRepository _resourceRepository;
        private readonly IEventProcessor _eventProcessor;
        private readonly ICustomersServiceClient _customersServiceClient;
        private readonly ICommandHandler<ReserveResource> _handler;

        public ReserveResourceHandlerTests()
        {
            _resourceRepository = Substitute.For<IResourcesRepository>();
            _customersServiceClient = Substitute.For<ICustomersServiceClient>();
            _eventProcessor = Substitute.For<IEventProcessor>();

            _handler = new ReserveResourceHandler(_resourceRepository, _eventProcessor, _customersServiceClient);
        }

        #endregion
    }
}
