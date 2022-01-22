using Convey;
using Convey.Logging;
using Convey.Secrets.Vault;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Availability.Application;
using Pacco.Services.Availability.Application.Commands;
using Pacco.Services.Availability.Application.DTO;
using Pacco.Services.Availability.Application.Queries;
using Pacco.Services.Availability.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) => services
                        .AddConvey()
                        .AddWebApi()
                        .AddApplication()
                        .AddInfrastructure()
                        .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Get<GetResource, ResourceDto>("resources/{resourceId}")
                        .Get<GetResources, IEnumerable<ResourceDto>>("resources", auth: true)
                        .Post<ReserveResource>("resources/{resourceId}/reservations/{dateTime}")
                        .Post<AddResource>("resources", afterDispatch: (cmd, ctx) =>
                            ctx.Response.Created($"resources/{cmd.ResourceId}"))))
                .UseVault()
                .UseLogging();

    }
}