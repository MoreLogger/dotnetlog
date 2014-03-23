
using System;
using System.Threading;
using Logging;

namespace Logging.Tester
{
    class Program
    {
        static void Main( string[] args )
        {
            ILogger logger = LogManager.GetLogger();
                    logger.LogTrace     ( "trace" );
                    logger.LogDebug     ( "a" );
                    logger.LogInfo      ( "b" );
                    logger.LogWarning   ( "c" );
                    logger.LogError     ( "d" );
                    logger.LogFatal     ( "e" );

                    logger.LogTechnicalTrace    ( "trace" );
                    logger.LogTechnicalDebug    ( "a" );
                    logger.LogTechnicalInfo     ( "b" );
                    logger.LogTechnicalWarning  ( "c" );
                    logger.LogTechnicalError    ( "d" );
                    logger.LogTechnicalFatal    ( "e" );

                    logger.Flush();

            Console.WriteLine( "Done." );
            Console.ReadLine();
        }
    }
}
