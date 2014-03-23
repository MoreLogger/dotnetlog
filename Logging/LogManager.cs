
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.IO;

using Logging.Exceptions;
using Logging.Enums;

namespace Logging
{
    public static class LogManager
    {
        private static Object s_emergencyInternalLoggerLockObject = new Object();

        public static ILogger GetLogger()
        {
            Config config = ConfigurationManager.GetSection( "Logging.Config" ) as Config;

            String persisterLoggerType = String.Format( "{0}.Logger", config.Persister );

            ILogger logger = Activator.CreateInstance( config.Persister, persisterLoggerType ).Unwrap() as ILogger;
                    logger.VerbosityLevel = config.VerbosityLevel;

            return logger;
        }

        public static void EmergencyInternalLogger( String message )
        {
            Config config = ConfigurationManager.GetSection( "Logging.Config" ) as Config;

            lock( s_emergencyInternalLoggerLockObject )
            {
                StreamWriter    streamWriter = new StreamWriter( config.EmergencyLogs, true );
                                streamWriter.WriteLine( message );
                                streamWriter.Close();
            }
        }
    }
}
