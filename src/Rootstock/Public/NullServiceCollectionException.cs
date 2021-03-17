using System;

namespace Rootstock
{
    public class NullServiceCollectionException : Exception
    {
        private const string ExceptionMessage = "Service Collection cannot be Null";
        public NullServiceCollectionException() : base(ExceptionMessage) { }
        public NullServiceCollectionException(Exception? innerException) : base(ExceptionMessage, innerException) { }
    }
}