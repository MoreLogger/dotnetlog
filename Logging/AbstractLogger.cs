
using System;
using System.Collections.Generic;
using Logging.Enums;

namespace Logging
{
    public abstract class AbstractLogger : MarshalByRefObject, ILogger
    {
        private delegate void LogHelper( Category category, Severity severity, String message );

        #region Variables

        protected   Verbosity                               _verbosityLevel;
        private     Dictionary< Verbosity, LogHelper[] >    _verbosityHandler;

        #endregion

        #region VerbosityLevel
        
        public virtual Verbosity VerbosityLevel
        {
            get { return _verbosityLevel; }
            set { _verbosityLevel = value; }
        }

        #endregion

        #region AbstractLogger

        public AbstractLogger()
        {
            // implement log verbosity filtering in a centralized way
            _verbosityHandler = new Dictionary<Verbosity, LogHelper[]>();// eTrace    eFatal          eError          eWarning        eInfo           eDebug
            _verbosityHandler.Add( Verbosity.eNone      , new LogHelper[]{ Persist  , PersistNone   , PersistNone   , PersistNone   , PersistNone   , PersistNone } );
            _verbosityHandler.Add( Verbosity.eFatals    , new LogHelper[]{ Persist  , Persist       , PersistNone   , PersistNone   , PersistNone   , PersistNone } );
            _verbosityHandler.Add( Verbosity.eErrors    , new LogHelper[]{ Persist  , Persist       , Persist       , PersistNone   , PersistNone   , PersistNone } );
            _verbosityHandler.Add( Verbosity.eWarnings  , new LogHelper[]{ Persist  , Persist       , Persist       , Persist       , PersistNone   , PersistNone } );
            _verbosityHandler.Add( Verbosity.eInfos     , new LogHelper[]{ Persist  , Persist       , Persist       , Persist       , Persist       , PersistNone } );
            _verbosityHandler.Add( Verbosity.eDebugs    , new LogHelper[]{ Persist  , Persist       , Persist       , Persist       , Persist       , Persist     } );
        }

        ~AbstractLogger()
        {
            Flush();
            Close();
        }

        #endregion

        #region Default implementation

        #region The one method called by all others

        public void Log( Category category, Severity severity, String message )
        {
            _verbosityHandler[ VerbosityLevel ][ (int) severity ]( category, severity, message );
        }

        #endregion

        #region Comodity methods 1

        public void LogTrace( String message )
        {
            Log( Category.eLogical, Severity.eTrace, message );
        }

        public void LogFatal( String message )
        {
            Log( Category.eLogical, Severity.eFatal, message );
        }

        public void LogError( String message )
        {
            Log( Category.eLogical, Severity.eError, message );
        }

        public void LogWarning( String message )
        {
            Log( Category.eLogical, Severity.eWarning, message );
        }

        public void LogInfo( String message )
        {
            Log( Category.eLogical, Severity.eInfo, message );
        }

        public void LogDebug( String message )
        {
            Log( Category.eLogical, Severity.eDebug, message );
        }

        #endregion

        #region Comodity methods 2

        public void LogTechnicalTrace( String message )
        {
            Log( Category.eTechnical, Severity.eTrace, message );
        }

        public void LogTechnicalFatal( String message )
        {
            Log( Category.eTechnical, Severity.eFatal, message );
        }

        public void LogTechnicalError( String message )
        {
            Log( Category.eTechnical, Severity.eError, message );
        }

        public void LogTechnicalWarning( String message )
        {
            Log( Category.eTechnical, Severity.eWarning, message );
        }

        public void LogTechnicalInfo( String message )
        {
            Log( Category.eTechnical, Severity.eInfo, message );
        }

        public void LogTechnicalDebug( String message )
        {
            Log( Category.eTechnical, Severity.eDebug, message );
        }

        #endregion

        #endregion

        protected abstract void Persist( Category category, Severity severity, String message );

        private void PersistNone( Category category, Severity severity, String message )
        {}

        protected virtual void Close()
        {}

        public virtual void Flush()
        {}
    }
}
