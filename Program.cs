using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BroadCastEmail
{
    class Program
    {
        private static List<Tuple<string, string>> EmailList;

        // Email Configuration
        private static string FromEmail; // Email remetente
        private static string FromPassword; // Senha do email remetente
        private static string SmtpHost; // Host Servidor Smtp
        private static int SmtpPort; // Port Servidor Smtp

        private static string EmailTitle; // Titulo do e-mail
        static void Main(string[] args)
        {
            try
            {
                EmailList = new List<Tuple<string, string>>();
                ReadJSON();
                SendEmail();


                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("Error : {0}", e.Message));
                Console.ReadKey();
            }
        }

        static void ReadJSON()
        {
            string jsonText = File.ReadAllText(@"emails.json");
            dynamic jsonObject = JObject.Parse(jsonText);

            foreach (dynamic email in jsonObject.emails)
            {
                EmailList.Add(new Tuple<string, string>(email.nome.Value, email.email.Value));
            }
        }

        static void SendEmail()
        {
            int emailSended = 0;
            int emailFailed = 0;

            foreach (Tuple<string, string> email in EmailList)
            {
                try
                {
                    SmtpClient client = new SmtpClient(SmtpHost, SmtpPort);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(FromEmail, FromPassword);

                    MailMessage message = new MailMessage(FromEmail, email.Item2, EmailTitle, GetBodyMessage(email.Item1));
                    message.IsBodyHtml = true;

                    client.Send(message);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(String.Format("Email enviado com sucesso : {0}", email.Item1));
                    Console.ForegroundColor = ConsoleColor.Gray;

                    emailSended++;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(String.Format("Não foi possivel enviar o e-mail para : {0}", email.Item1));
                    Console.ForegroundColor = ConsoleColor.Gray;

                    emailFailed++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("Foram enviados {0} com sucesso e {1} não foram enviados", emailSended, emailFailed));
        }

        static string GetBodyMessage(string nome)
        {
            string emailBody = File.ReadAllText(@"body.html");

            if (emailBody.Contains("%nome%"))
            {
                emailBody = Regex.Replace(emailBody, "%nome%", nome, RegexOptions.IgnoreCase);
            }

            return emailBody;
        }
    }
}
