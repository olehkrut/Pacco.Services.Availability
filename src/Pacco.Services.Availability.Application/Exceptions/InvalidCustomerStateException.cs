using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public class InvalidCustomerStateException : AppException
    {
        public InvalidCustomerStateException(Guid customerId, string state)
            : base($"Customer with ID: '{customerId}' has invalid state: '{state}'.")
        {
            CustomerId = customerId;
            State = state;
        }

        public Guid CustomerId { get; }
        public string State { get; }
    }
}
