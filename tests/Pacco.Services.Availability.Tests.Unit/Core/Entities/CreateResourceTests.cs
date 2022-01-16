using Pacco.Services.Availability.Core.Entities;
using Pacco.Services.Availability.Core.Events;
using Pacco.Services.Availability.Core.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pacco.Services.Availability.Tests.Unit.Core.Entities
{
    public class CreateResourceTests
    {
        private Resource Act(AggregateId id, IReadOnlyCollection<string> tags) => Resource.Create(id, tags);

        [Fact]
        public void given_valid_id_and_tags_resoure_should_be_created()
        {
            // Arrange
            var id = new AggregateId();
            var tags = new[] { "tag" };

            // Act
            var resource = Act(id, tags);

            // Assert
            resource.ShouldNotBeNull();
            resource.Id.ShouldBe(id);
            resource.Tags.ShouldBe(tags);
            resource.Events.ShouldHaveSingleItem();

            var @event = resource.Events.First();
            @event.ShouldBeOfType<ResourceCreated>();
        }

        [Fact]
        public void given_empty_tags_resource_creation_should_throw_exception()
        {
            // Arrange
            var id = new AggregateId();
            var tags = Array.Empty<string>();

            // Act
            var exception = Record.Exception(() => Act(id, tags));

            // Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<MissingResourceTagsException>();
        }
    }
}
