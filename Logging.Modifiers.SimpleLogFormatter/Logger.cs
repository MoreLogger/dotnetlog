
using System;
using System.Configuration;
using System.Threading;
using Logging.Enums;
using Logging.Exceptions;

namespace Logging.Modifiers.SimpleLogFormatter
{
    public class Logger : AbstractLogger
    {
        private ILogger _persister;

        public override Verbosity VerbosityLevel
        {
            set
            {
                _verbosityLevel = value;

                _persister.VerbosityLevel = value;
            }
        }

        public Logger() :
            base()
        {
            Config config = ConfigurationManager.GetSection( "SimpleLogFormatter.Config" ) as Config;

            String persisterLoggerType = String.Format( "{0}.Logger", config.Persister );

            _persister = Activator.CreateInstance( config.Persister, persisterLoggerType ).Unwrap() as ILogger;
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            String formattedMessage = String.Format
            (
                "{0} | {1,10} | {2,8} | {3}",
                DateTime.Now.ToString( "yyyy.MM.dd | HH:mm:ss.ffffff" ),
                category,
                severity,
                message
            );

            _persister.Log( category, severity, formattedMessage );
        }

        public override void Flush()
        {
            _persister.Flush();
        }
    }
}
