
using System;
using System.Configuration;
using Logging.Enums;

namespace Logging.Modifiers.Buffered
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "persister", IsRequired=true )]
        public String Persister
        {
            get
            {
                return (String) base[ "persister" ];
            }
        }

        [ConfigurationProperty( "bufferSize", IsRequired=true )]
        public Int32 BufferSize
        {
            get
            {
                return (Int32) base[ "bufferSize" ];
            }
        }

        [ConfigurationProperty( "sizeUnit", IsRequired=true )]
        public String SizeUnit
        {
            get
            {
                return (String) base[ "sizeUnit" ];
            }
        }
    }
}
