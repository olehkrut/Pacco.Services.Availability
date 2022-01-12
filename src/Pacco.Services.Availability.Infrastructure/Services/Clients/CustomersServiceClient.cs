﻿using Convey.HTTP;
using Pacco.Services.Availability.Application.DTO.External;
using Pacco.Services.Availability.Application.Services.Clients;
using System;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Services.Clients
{
    internal sealed class CustomersServiceClient : ICustomersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public CustomersServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["customers"];
        }

        public Task<CustomerStateDto> GetStateAsync(Guid id)
            => _httpClient.GetAsync<CustomerStateDto>($"{_url}/customers/{id}/state");
    }
}
