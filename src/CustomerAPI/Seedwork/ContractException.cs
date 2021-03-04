using System;
using System.Runtime.Serialization;

namespace CustomerAPI.Seedwork
{
    public class ContractException : Exception
    {
        public ContractException()
        {
        }

        public ContractException(string message) : base(message)
        {
        }

        public ContractException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
