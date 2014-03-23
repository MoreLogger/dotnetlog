
using System;
using System.Configuration;

namespace Logging.Modifiers.SimpleLogFormatter
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
    }
}
