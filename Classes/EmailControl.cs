using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace FinalProject.Classes
{
    public class EmailControl
    {
        /// <summary>
        ///     Função para envio de e-mail de ativação
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        public static void SendEmailActivation(string Email, string User)
        {
            var servidor = new SmtpClient(); //Servidor SMTP - pass, user, porto
            var emails = new MailMessage(); //Email
            emails.Subject = "Ativação de Conta Cinel 2.0";
            emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
            emails.To.Add(new MailAddress(Email)); //Destinatário

            emails.Body =
                $"Ex.mo(s) Sr(s), <br /><b>Obrigado pela sua inscrição.</b><br />Para ativar a sua conta clique <a href='{ConfigurationManager.AppSettings["SiteURL"]}/ActivationPage.aspx?user={Security.EncryptString(User)}&redirected=true'>aqui</a>!";
            ;
            emails.IsBodyHtml =
                true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

            servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
            servidor.Port =
                int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

            var user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
            var pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

            servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

            servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

            servidor.Send(emails); //O objeto servidor envia o mail
        }

        /// <summary>
        ///     Função para envio de e-mail de recuperação
        /// </summary>
        /// <param name="email"></param>
        /// <param name="novaPasse"></param>
        public static void SendEmailRecover(string Email, string NovaPasse)
        {
            var servidor = new SmtpClient(); //Servidor SMTP - pass, user, porto
            var emails = new MailMessage(); //Email
            emails.Subject = "Recuperação de Conta Cinel 2.0";
            emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
            emails.To.Add(new MailAddress(Email)); //Destinatário

            emails.Body =
                $"Ex.mo(s) Sr.(s), <br /> A sua nova palavra-passe para o e-mail {Email} é a seguinte: {NovaPasse} <br /> Proceda à sua alteração através do seguinte <a href='{ConfigurationManager.AppSettings["SiteURL"]}/UserChangePass.aspx'> link </a>!<br /> Qualquer questão não hesite em contactar-nos para info@cinel.pt! <br>";
            emails.IsBodyHtml =
                true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

            servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
            servidor.Port =
                int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

            var user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
            var pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

            servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

            servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

            servidor.Send(emails); //O objeto servidor envia o mail
        }

        public static void SendEmailActivationWithPW(string Email, string User, string NovaPasse)
        {
            var servidor = new SmtpClient(); //Servidor SMTP - pass, user, porto
            var emails = new MailMessage(); //Email
            emails.Subject = "Ativação de Conta Cinel 2.0";
            emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
            emails.To.Add(new MailAddress(Email)); //Destinatário

            emails.Body =
                $"Ex.mo(s) Sr(s), <br /><b>Obrigado pela sua inscrição.</b><br />Para ativar a sua conta clique <a href='{ConfigurationManager.AppSettings["SiteURL"]}/ActivationPage.aspx?user={Security.EncryptString(User)}&redirected=true'>aqui</a>! A sua palavra-passe para o e-mail {Email} é a seguinte: {NovaPasse} <br />. Após o login aconselhamos que altere a sua password na sua área de perfil. <br /> Qualquer questão não hesite em contactar-nos para info@cinel.pt! <br>";
            ;
            emails.IsBodyHtml =
                true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

            servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
            servidor.Port =
                int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

            var user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
            var pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

            servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

            servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

            servidor.Send(emails); //O objeto servidor envia o mail
        }
    }
}