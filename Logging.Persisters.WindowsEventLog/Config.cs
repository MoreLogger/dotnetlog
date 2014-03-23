
using System;
using System.Configuration;
using Logging.Enums;

namespace Logging.Persisters.WindowsEventLog
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "source", IsRequired=true )]
        public String Source
        {
            get
            {
                return (String) base[ "source" ];
            }
        }
    }
}
