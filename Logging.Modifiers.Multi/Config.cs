
using System;
using System.Configuration;
using Logging.Enums;

namespace Logging.Modifiers.Multi
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "persisters", IsRequired=true )]
        public String Persisters
        {
            get
            {
                return (String) base[ "persisters" ];
            }
        }
    }
}
