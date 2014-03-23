
using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Logging.Enums;

namespace Logging.Persisters.Email
{
    public class Logger : AbstractLogger
    {
        private String          _mailServer     ;
        private String          _fromMail       ;
        private String          _fromUser       ;
        private String          _fromPassword   ;
        private List<String>    _to             ;

        public Logger() :
            base()
        {
            Config config = ConfigurationManager.GetSection( "Email.Config" ) as Config;

            _mailServer     = config.MailServer;
            _fromMail       = config.FromMail;
            _fromUser       = config.FromUser;
            _fromPassword   = config.FromPassword;

            _to = new List<String>();
            foreach( String to in config.To.Split(':') )
                _to.Add( to );
        }

        protected override void Persist( Category category, Severity severity, String message )
        {
            SendMail( String.Format( "CATEGORY: {0}, TRACE", category ), message );
        }

        #region SendMail()

        private void SendMail( String subject, String message )
        {
            foreach( String to in _to )
            {
                // Setup the mail message.
                MailMessage mail        = new MailMessage( _fromMail, to );
                            mail.Body   = message;
                            mail.Subject= subject;

                NetworkCredential credentials = new NetworkCredential( _fromUser, _fromPassword );

                // Setup the SMTP client.
                SmtpClient  mailClient = new SmtpClient();
                            mailClient.Host                 = _mailServer;
                            mailClient.UseDefaultCredentials= false;
                            mailClient.Credentials          = credentials;

                try
                {
                    mailClient.Send( mail );
                }
                catch( Exception e )
                {
                    LogManager.EmergencyInternalLogger( e.ToString() );
                }
            }
        }

        #endregion
    }
}
