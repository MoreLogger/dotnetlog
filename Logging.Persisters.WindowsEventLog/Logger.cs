
using System;
using System.Diagnostics;
using System.Configuration;
using Logging.Enums;

namespace Logging.Persisters.WindowsEventLog
{
	public class Logger : AbstractLogger
	{
        private EventLog _eventLog;

        public Logger() :
            base()
        {
            Config config = ConfigurationManager.GetSection( "WindowsEventLog.Config" ) as Config;

            _eventLog = new EventLog();
            _eventLog.Source = config.Source;
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            try
            {
                _eventLog.WriteEntry( message );
            }
            catch( Exception e )
            {
                LogManager.EmergencyInternalLogger( e.ToString() );
            }
        }
	}
}
