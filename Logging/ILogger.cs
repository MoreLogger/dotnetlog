
using System;
using Logging.Enums;

namespace Logging
{
	public interface ILogger
	{
        Verbosity VerbosityLevel{ get; set; }

        // The one method called by all others
        void Log( Category category, Severity severity, String message );

        // Comodity methods
        void LogTrace   ( String message );
        void LogFatal   ( String message );
        void LogError   ( String message );
        void LogWarning ( String message );
        void LogInfo    ( String message );
        void LogDebug   ( String message );

        void LogTechnicalTrace  ( String message );
        void LogTechnicalFatal  ( String message );
        void LogTechnicalError  ( String message );
        void LogTechnicalWarning( String message );
        void LogTechnicalInfo   ( String message );
        void LogTechnicalDebug  ( String message );

        // Ask to write all its data to the persister
        void Flush();
    }
}
