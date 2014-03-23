
using System;
using Logging.Enums;

namespace Logging.Adapters.Out.Trace
{
    public class Logger : AbstractLogger
    {
        public Logger() :
            base()
		{}

        protected override void Persist( Category category, Severity severity, String message )
        {
            System.Diagnostics.Trace.WriteLine( message );
        }

        public override void Flush()
        {
            System.Diagnostics.Trace.Flush();
        }
    }
}
