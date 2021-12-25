using Convey.CQRS.Queries;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure.Mongo.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Mongo.Queries.Handlers
{
    public class GetResourecesHandler : IQueryHandler<GetResources, IEnumerable<ResourceDto>>
    {
        private readonly IMongoDatabase _database;

        public GetResourecesHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ResourceDto>> HandleAsync(GetResources query)
        {
            var collection = _database.GetCollection<ResourceDocument>("resources");

            if (query.Tags is null || !query.Tags.Any())
            {
                var allDocuments = await collection.Find(_ => true).ToListAsync();

                return allDocuments.Select(x => x.AsDto());
            }

            var documents = collection.AsQueryable();
            documents = query.MatchAllTags
                ? documents.Where(d => query.Tags.All(t => d.Tags.Contains(t)))
                : documents.Where(d => query.Tags.Any(t => d.Tags.Contains(t)));

            var resources = await documents.ToListAsync();

            return resources.Select(d => d.AsDto());
        }
    }
}
