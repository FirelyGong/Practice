using System;
using System.Runtime.Serialization;

namespace Practice.Algorithm.Tree.BPlusTree.Exceptions
{
    public class BPTreeInvalidNodeException:Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessage"></param>
        public BPTreeInvalidNodeException(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Constructor to takes inner exception reference.
        /// </summary>
        /// <param name="errorMessage">Human readable description of the error.</param>
        /// <param name="innerException">An inner exception reference.</param>
        public BPTreeInvalidNodeException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public BPTreeInvalidNodeException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected BPTreeInvalidNodeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
