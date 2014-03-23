
using System;
using System.Text;

namespace Logging.Adapters.In.TraceListener
{
    public class Logger : System.Diagnostics.TraceListener
    {
        ILogger         _logger;
        StringBuilder   _stringManipulator;

        public Logger() :
            base()
		{
            try
            {
                _logger             = LogManager.GetLogger();
                _stringManipulator  = new StringBuilder();
            }
            catch( System.Reflection.TargetInvocationException e )
            {
                LogManager.EmergencyInternalLogger( e.ToString() );
            }
        }

        ~Logger()
        {
            Flush();
        }

        public override void Write( string message )
        {
            _stringManipulator.Remove( 0, _stringManipulator.Length );

            for( int i=0; i < this.IndentLevel; i++ )
                for( int j=0; j < this.IndentSize; j++ )
                    _stringManipulator.Append( " " );

            _stringManipulator.Append( message );

            _logger.LogInfo( _stringManipulator.ToString() );
        }

        public override void WriteLine( string message )
        {
            Write( message );
        }

        public override void Flush()
        {
            _logger.Flush();
        }
    }
}
