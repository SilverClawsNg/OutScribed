using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class GenericDomainException : Exception
    {

        public GenericDomainException() : base("Unknown domain error occured.") { }

        public GenericDomainException(string message) : base(message) { }

        public GenericDomainException(string message, Exception innerException) : base(message, innerException) { }

    }
}
