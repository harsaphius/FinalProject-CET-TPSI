using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FinalProject.Classes
{
    public class EmailControl
    {
        /// <summary>
        /// Função para envio de E-mails
        /// </summary>
        /// <param name="email"></param>
        /// <param name="body"></param>
        public static void SendEmail(string email, string body, string subject)
        {
            //Tratamento de exceção.... Try_Catch
            //Try_Catch para a messagem para prevenir que a mensagem está correta
            try
            {
                SmtpClient servidor = new SmtpClient();//Servidor SMTP - pass, user, porto

                MailMessage emails = new MailMessage(); //Email
                emails.Subject = subject; //Nome de quem é o email (tb_mail)
                emails.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_MailUser"]); //Sender
                emails.To.Add(new MailAddress(email)); //Destinatário

                emails.Body = body;
                emails.IsBodyHtml = true; //Reconhece a formatação do HTML, tags, p.e., links,<b>,<p>,etc. Poderemos usar plain-text IsBodyHtml= false.

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"]; //SMTP URL configurada no WebConfig
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]); //SMTP Port configurada no WebConfig

                string user = ConfigurationManager.AppSettings["SMTP_MailUser"]; //SMTP User Mail configurado no WebConfig
                string pw = ConfigurationManager.AppSettings["SMTP_Pass"]; //SMTP Pass Mail configurado no WebConfig

                servidor.Credentials = new NetworkCredential(user, pw); //Indicar as credenciais do utilizador

                servidor.EnableSsl = true; //Habilitar o SSL - o SMTP Client usa o SSL para criptografar a ligação

                servidor.Send(emails); //O objeto servidor envia o mail

            }
            catch (Exception ex)
            {
                /*ex.Message = ""*/
                ;//Mensagem de insucesso conforme a exceção encontrada.
            }

        }

    }
}