
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using Logging.Enums;
using Logging.Exceptions;

namespace Logging.Modifiers.Async
{
    public class Logger : AbstractLogger
    {
        private ILogger     _persister;
        private Queue<bool> _notDoneEvents;

        private class CallbakParameters
        {
            public Category     _category;
            public Severity     _severity;
            public String       _message;
        }

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
            Config config = ConfigurationManager.GetSection( "Async.Config" ) as Config;

            String persisterLoggerType = String.Format( "{0}.Logger", config.Persister );

            _persister = Activator.CreateInstance( config.Persister, persisterLoggerType ).Unwrap() as ILogger;

            _notDoneEvents = new Queue<bool>();
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            bool noteDoneEvent = true;

            lock( _persister )
            {
                _notDoneEvents.Enqueue( noteDoneEvent );
            }

            CallbakParameters   callbakParameters = new CallbakParameters();
                                callbakParameters._category = category;
                                callbakParameters._severity = severity;
                                callbakParameters._message  = message;

            ThreadPool.QueueUserWorkItem( new WaitCallback( PersistHelperThreadCallbak ), callbakParameters );
        }

        protected void PersistHelperThreadCallbak( Object stateInfo )
        {
            CallbakParameters callbakParameters = stateInfo as CallbakParameters;

            _persister.Log( callbakParameters._category, callbakParameters._severity, callbakParameters._message );

            lock( _persister )
            {
                _notDoneEvents.Dequeue();
            }
        }

        public override void Flush()
        {
            bool done = false;

            while( !done )
            {
                lock( _persister )
                {
                    if( 0 == _notDoneEvents.Count )
                        done = true;
                }

                Thread.Sleep( 100 );
            }
        }
    }
}
