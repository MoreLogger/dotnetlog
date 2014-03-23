
using System;
using System.Collections.Generic;
using log4net;
using Logging.Enums;

namespace Logging.Adapters.Out.Log4Net
{
    public class Logger : AbstractLogger
    {
        private delegate void LogHelper( object message );

        private log4net.ILog                        _log4Net;
        private Dictionary< Severity, LogHelper >   _log4NetMapper;


        public Logger() :
            base()
		{
            log4net.Config.XmlConfigurator.Configure();

            _log4Net = log4net.LogManager.GetLogger( String.Empty );

            _log4NetMapper = new Dictionary <Severity, LogHelper >();
            _log4NetMapper.Add( Severity.eTrace,    LogTraceHelper  );
            _log4NetMapper.Add( Severity.eFatal,    LogFatalHelper );
            _log4NetMapper.Add( Severity.eError,    LogErrorHelper );
            _log4NetMapper.Add( Severity.eWarning,  LogWarningHelper );
            _log4NetMapper.Add( Severity.eInfo,     LogInfoHelper );
            _log4NetMapper.Add( Severity.eDebug,    LogDebugHelper );
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            _log4NetMapper[ severity ]( message );
        }

        #region Helpers

        private void LogTraceHelper( object message )
        {
            _log4Net.Info( message );
        }

        private void LogFatalHelper( object message )
        {
            _log4Net.Fatal( message );
        }

        private void LogErrorHelper( object message )
        {
            _log4Net.Error( message );
        }

        private void LogWarningHelper( object message )
        {
            _log4Net.Warn( message );
        }

        private void LogInfoHelper( object message )
        {
            _log4Net.Info( message );
        }

        private void LogDebugHelper( object message )
        {
            _log4Net.Debug( message );
        } 

        #endregion
    }
}
