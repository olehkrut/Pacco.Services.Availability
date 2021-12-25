using Convey.CQRS.Queries;
using Pacco.Services.Availability.Application.DTO;
using System;

namespace Pacco.Services.Availability.Application.Queries
{
    public class GetResource : IQuery<ResourceDto>
    {
        public Guid ResourceId { get; set; }
    }
}