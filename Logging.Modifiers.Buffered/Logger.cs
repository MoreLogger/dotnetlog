
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting;
using Logging.Enums;

namespace Logging.Modifiers.Buffered
{
    public class Logger : AbstractLogger
    {
        private ILogger _persister;
        private Int32   _bufferSize;
        private Int32   _currentBufferSize;
        
        private class DelaiedCallParameters
        {
            public Category _category;
            public Severity _severity;
            public String   _message;
        }

        private List<DelaiedCallParameters> _buffers;

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
            Config config = ConfigurationManager.GetSection( "Buffered.Config" ) as Config;

            String persisterLoggerType = String.Format( "{0}.Logger", config.Persister );

            _persister = Activator.CreateInstance( config.Persister, persisterLoggerType ).Unwrap() as ILogger;
            _persister.VerbosityLevel = VerbosityLevel;

            _bufferSize = config.BufferSize;
            switch( config.SizeUnit )
            {
                case "B":
                    break;
                case "K":
                    _bufferSize *= 1024;
                    break;
                case "M":
                    _bufferSize *= 1024 * 1024;
                    break;
                case "G":
                    _bufferSize *= 1024 * 1024 * 1024;
                    break;
            }

            _currentBufferSize = 0;

            _buffers = new List<DelaiedCallParameters>();
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            _currentBufferSize += message.Length;

            DelaiedCallParameters   delaiedCallParameters = new DelaiedCallParameters();
                                    delaiedCallParameters._category = category;
                                    delaiedCallParameters._severity = severity;
                                    delaiedCallParameters._message  = message;

            _buffers.Add( delaiedCallParameters );

            if( _currentBufferSize >= _bufferSize )
                Flush();            
        }

        public override void Flush()
        {
            _currentBufferSize = 0;

            foreach( DelaiedCallParameters callParameters in _buffers )
                _persister.Log( callParameters._category, callParameters._severity, callParameters._message );

            _buffers.Clear();
        }
     }
}
