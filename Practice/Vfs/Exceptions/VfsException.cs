using System;
using System.Runtime.Serialization;

namespace Practice.Vfs.Exceptions
{
    public class VfsException:Exception
    {
        public VfsException(string message)
            : base(message)
        {
        }

        public VfsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected VfsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
