
using System;

namespace Logging.Exceptions
{
    public class NoLoggerFoundException : Exception
    {
        public NoLoggerFoundException() :
            base()
        {}

        public NoLoggerFoundException( String message ) :
            base( message )
        {}
    }
}
