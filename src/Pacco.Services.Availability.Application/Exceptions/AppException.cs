using System;

namespace Pacco.Services.Availability.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message)
        {
        }

        public virtual string Code { get; }
    }
}
