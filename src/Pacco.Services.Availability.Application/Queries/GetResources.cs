using Convey.CQRS.Queries;
using Pacco.Services.Availability.Application.DTO;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Application.Queries
{
    public class GetResources : IQuery<IEnumerable<ResourceDto>>
    {
        public IEnumerable<string> Tags { get; set; }
        public bool MatchAllTags { get; set; }
    }
}
