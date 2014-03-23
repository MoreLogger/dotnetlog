
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using Logging.Enums;

namespace Logging.Modifiers.Multi
{
    public class Logger : AbstractLogger
    {
        private List<ILogger> _persisters;

        public override Verbosity VerbosityLevel
        {
            set
            {
                _verbosityLevel = value;

                _persisters.ForEach( logger => logger.VerbosityLevel = value );
            }
        }

        public Logger() :
            base()
        {
            Config config = ConfigurationManager.GetSection( "Multi.Config" ) as Config;

            _persisters = new List<ILogger>();

            foreach( String persister in config.Persisters.Split(new char[]{';'}) )
            {
                String persisterLoggerType = String.Format( "{0}.Logger", persister );

                ILogger logger = Activator.CreateInstance( persister, persisterLoggerType ).Unwrap() as ILogger;

                _persisters.Add( logger );
            }
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            _persisters.ForEach( logger => logger.Log( category, severity, message ) );
        }

        public override void Flush()
        {
            _persisters.ForEach( logger => logger.Flush() );
        }
    }
}
