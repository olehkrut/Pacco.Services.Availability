using Pacco.Services.Availability.Application.DTO.External;
using System;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Application.Services.Clients
{
    public interface ICustomersServiceClient
    {
        Task<CustomerStateDto> GetStateAsync(Guid id);
    }
}
