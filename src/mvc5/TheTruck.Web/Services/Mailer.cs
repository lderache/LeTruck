using System;
using System.Net.Mail;

namespace TheTruck.Web.Services
{
    public class Mailer
    {
        string _sender = "";
        string _password = "";
        string _server = "";
        int _port = 587;

        public Mailer(string sender, string password, string server, int port)
        {
            _sender = sender;
            _password = password;
            _server = server;
            _port = port;
        }


        public void SendMail(string recipient, string subject, string message)
        {
            SmtpClient client = new SmtpClient(_server);

            client.Port = _port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(_sender.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}