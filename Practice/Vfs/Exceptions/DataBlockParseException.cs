using System;
using System.Runtime.Serialization;

namespace Practice.Vfs.Exceptions
{
    public class DataBlockParseException:VfsException
    {
        public DataBlockParseException(string message)
            : base(message)
        {
        }

        public DataBlockParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DataBlockParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
