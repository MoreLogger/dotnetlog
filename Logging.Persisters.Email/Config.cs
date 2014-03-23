
using System;
using System.Configuration;

namespace Logging.Persisters.Email
{
    public class Config : ConfigurationSection
    {
        [ConfigurationProperty( "mailServer", IsRequired=true )]
        public String MailServer
        {
            get
            {
                return (String) base[ "mailServer" ];
            }
        }

        [ConfigurationProperty( "fromMail", IsRequired=true )]
        public String FromMail
        {
            get
            {
                return (String) base[ "fromMail" ];
            }
        }

        [ConfigurationProperty( "fromUser", IsRequired=true )]
        public String FromUser
        {
            get
            {
                return (String) base[ "fromUser" ];
            }
        }

        [ConfigurationProperty( "fromPassword", IsRequired=true )]
        public String FromPassword
        {
            get
            {
                return (String) base[ "fromPassword" ];
            }
        }

        [ConfigurationProperty( "to", IsRequired=true )]
        public String To
        {
            get
            {
                return (String) base[ "to" ];
            }
        }
    }
}
