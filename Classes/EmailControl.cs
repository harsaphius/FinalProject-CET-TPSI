﻿using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FinalProject.Classes
{
    public class EmailControl
    {
        /// <summary>
        /// Função para envio de e-mail de ativação
        /// </summary>
        /// <param name="email"></param>
        /// <param name="body"></param>
        public static void SendEmailActivation(string email)
        {
            SmtpClient servidor = new SmtpClient();//Servidor SMTP - pass, user, porto
            MailMessage emails = new MailMessage(); //Email
            emails.Subject = "Ativação de Conta Cinel 2.0";
            emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
            emails.To.Add(new MailAddress(email)); //Destinatário

            emails.Body = $"Ex.mo(s) Sr(s), <br /><b>Obrigado pela sua inscrição.</b><br />Para ativar a sua conta clique <a href='{ConfigurationManager.AppSettings["SiteURL"]}/ActivationPage.aspx'>aqui</a>!"; ;
            emails.IsBodyHtml = true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

            servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
            servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

            string user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
            string pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

            servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

            servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

            servidor.Send(emails); //O objeto servidor envia o mail
        }

        /// <summary>
        /// Função para envio de e-mail de recuperação
        /// </summary>
        /// <param name="email"></param>
        /// <param name="novaPasse"></param>
        public static void SendEmailRecover(string email, string novaPasse)
        {
            
            SmtpClient servidor = new SmtpClient();//Servidor SMTP - pass, user, porto
            MailMessage emails = new MailMessage(); //Email
            emails.Subject = "Recuperação de Conta Cinel 2.0";
            emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
            emails.To.Add(new MailAddress(email)); //Destinatário

            emails.Body = $"Ex.mo(s) Sr.(s), <br /> A sua nova palavra-passe para o e-mail {email} é a seguinte: {novaPasse} <br /> Proceda à sua alteração através do seguinte <a href='{ConfigurationManager.AppSettings["SiteURL"]}/UserChangePass.aspx'> link </a>!<br /> Qualquer questão não hesite em contactar-nos para info@cinel.pt! <br>";
            emails.IsBodyHtml = true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

            servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
            servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

            string user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
            string pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

            servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

            servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

            servidor.Send(emails); //O objeto servidor envia o mail
        }

    }
}