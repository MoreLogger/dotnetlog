
using System;
using System.Configuration;

namespace Logging.Persisters.PlainFile
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "logFile", IsRequired=true )]
        public String LogFile
        {
            get
            {
                return (String) base[ "logFile" ];
            }
        }
    }
}
