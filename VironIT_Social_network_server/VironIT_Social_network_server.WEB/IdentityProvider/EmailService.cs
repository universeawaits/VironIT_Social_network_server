using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace VironIT_Social_network_server.WEB.IdentityProvider
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public async Task<bool> SendAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("herns", ""));
            email.To.Add(new MailboxAddress("", to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 587);
                    await client.AuthenticateAsync("myowngollum@gmail.com", "opend00r");
                    await client.SendAsync(email);

                    await client.DisconnectAsync(true);
                } 
                catch (SocketException)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
