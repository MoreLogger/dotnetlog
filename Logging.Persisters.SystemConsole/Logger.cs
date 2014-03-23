
using System;
using Logging.Enums;

namespace Logging.Persisters.SystemConsole
{
    public class Logger : AbstractLogger
    {
        public Logger() :
            base()
        {}

        protected override void Persist( Category category, Severity severity, String message )
        {
            Console.WriteLine( message );
        }

        protected override void  Close()
        {
            Console.Out.Close();
        }

        public override void Flush()
        {
            Console.Out.Flush();
        }
    }
}
