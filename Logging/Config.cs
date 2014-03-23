
using System;
using System.Configuration;
using Logging.Enums;

namespace Logging
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "verbosity", IsRequired=true )]
        public Verbosity VerbosityLevel
        {
            get
            {
                return (Verbosity) base[ "verbosity" ];
            }
        }

        [ConfigurationProperty( "persister", IsRequired=true )]
        public String Persister
        {
            get
            {
                return (String) base[ "persister" ];
            }
        }

        [ConfigurationProperty( "emergencyLogs", IsRequired=true )]
        public String EmergencyLogs
        {
            get
            {
                return (String) base[ "emergencyLogs" ];
            }
        }
    }
}
