using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SearchFight.Domain.Exceptions
{
    public class SearchTermAlreadyAddedException : Exception
    {
        public SearchTermAlreadyAddedException()
        {
        }

        public SearchTermAlreadyAddedException(string message) : base(message)
        {
        }

        public SearchTermAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchTermAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
