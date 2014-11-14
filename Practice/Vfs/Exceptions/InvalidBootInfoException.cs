using System;
using System.Runtime.Serialization;

namespace Practice.Vfs.Exceptions
{
    public class InvalidBootInfoException:VfsException
    {
        public InvalidBootInfoException(string message)
            : base(message)
        {
        }

        public InvalidBootInfoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidBootInfoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
