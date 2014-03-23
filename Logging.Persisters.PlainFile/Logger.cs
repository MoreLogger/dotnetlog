
using System;
using System.IO;
using System.Configuration;
using Logging.Enums;

namespace Logging.Persisters.PlainFile
{
    public class Logger : AbstractLogger
    {
        private StreamWriter _streamWriter;

        public Logger() :
            base()
        {
            try
            {
                Config config = ConfigurationManager.GetSection( "PlainFile.Config" ) as Config;

                _streamWriter = new StreamWriter( config.LogFile, true );
                _streamWriter.AutoFlush = true;
            }
            catch( Exception e )
            {
                LogManager.EmergencyInternalLogger( e.ToString() );
            }
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            try
            {
                _streamWriter.WriteLine( message );
            }
            catch( Exception e )
            {
                LogManager.EmergencyInternalLogger( e.ToString() );
            }
        }

        protected override void Close()
        {
            _streamWriter.Close();
        }

        public override void Flush()
        {
            _streamWriter.Flush();
        }
    }
}
