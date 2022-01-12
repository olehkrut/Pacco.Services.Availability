using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public class CustomerNotFoundException : AppException
    {
        public CustomerNotFoundException(Guid customerId) : base($"Customer with ID: '{customerId}' was not found.")
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
