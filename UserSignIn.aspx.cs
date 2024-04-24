using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;

namespace FinalProject
{
    public partial class UserSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Google Data
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["ClientID"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["RedirectionURL"];

            //Facebook Data
            FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookKey"];
            FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookSecret"];
            FaceBookConnect.Version = ConfigurationManager.AppSettings["FacebookVersion"];

            if (Request.QueryString["redirected"] != null && Request.QueryString["redirected"] == "true")
            {
                lbl_message.Text = Session["ActivatedUser"].ToString();
            }

        }

        protected void btn_facebook_Click(object sender, EventArgs e)
        {
            Session["Facebook"] = "Yes";
            FaceBookConnect.Authorize("public_profile,email", ConfigurationManager.AppSettings["redirection_url"]);
        }

        protected void btn_google_Click(object sender, EventArgs e)
        {
            Session["Google"] = "Yes";
            GoogleConnect.Authorize("profile", "email");
        }

        protected void btn_signin_Click(object sender, EventArgs e)
        {

            if (Security.IsValidUsername(tb_username.Text) == true || Security.IsValidEmail(tb_username.Text) == true)
            {
                List<int> isLoginAllowed = Classes.User.IsLoginAllowed(tb_username.Text, tb_pw.Text);

                if (isLoginAllowed[0] == 1 && isLoginAllowed[1] == 1)
                {
                    (int AnswUserCode, string Username) = Classes.User.DetermineUtilizador(tb_username.Text);
                    List<int> UserProfile = Classes.User.DetermineUserProfile(Convert.ToInt32(AnswUserCode));

                    if (AnswUserCode == 1 || AnswUserCode == 4) Session["Admin"] = "Yes";
                    else Session["Admin"] = "No";

                    Session["User"] = tb_username.Text;
                    Session["CodUtilizador"] = isLoginAllowed[2];
                    Session["Logado"] = "Yes";

                    Response.Redirect("UserProfile.aspx");
                }
                else if (isLoginAllowed[0] == 1 && isLoginAllowed[1] == 0)
                {
                    (int UserCode, string Username) = Classes.User.DetermineUtilizador(tb_username.Text);
                    List<int> UserProfile = Classes.User.DetermineUserProfile(Convert.ToInt32(UserCode));
                    string email = Classes.User.GetEmailFromDatabase(UserCode);

                    foreach (int profile in UserProfile)
                    {
                        EmailControl.SendEmailWaitingValidation(email, Username);
                        lbl_message.CssClass = "alert alert-primary text-white text-center";
                        lbl_message.Text = $"A sua conta ainda aguarda validação!";
                    }
                }
                else if (isLoginAllowed[0] == -1 && isLoginAllowed[1] == -1)
                {
                    lbl_message.CssClass = "alert alert-primary text-white text-center";

                    lbl_message.Text = "Este utilizador não está associado a nenhuma conta! Registe-se.";
                }
                else
                {
                    lbl_message.CssClass = "alert alert-primary text-white text-center";

                    lbl_message.Text = "O seu utilizador ou palavra-passe estão errados! Tente novamente ou recupere a password!";
                }
            }
            else
            {
                lbl_message.CssClass = "alert alert-primary text-white text-center";

                lbl_message.Text = "Introduz um e-mail ou nome de utilizador válido!";
            }
        }

        protected void btn_recuperarPW_Click(object sender, EventArgs e)
        {
            if (Security.IsValidEmail(tbEmailRecover.Text) == false)
            {
                lbl_message.CssClass = "alert alert-primary text-white text-center";

                lbl_message.Text = "Introduza um e-mail válido!";
            }
            else
            {
                //Recuperação de password com envio de email
                string NovaPasse = Membership.GeneratePassword(10, 2);

                (int AnswUserExist, int AnswAccountActive) = Security.RecoverPassword(tbEmailRecover.Text, NovaPasse);
                if (AnswUserExist == 1 && AnswAccountActive == 1) //Caso a conta esteja ativa, envia NovaPasse
                {
                    EmailControl.SendEmailRecover(tbEmailRecover.Text, NovaPasse);
                    lbl_message.Text = "E-mail enviado com sucesso! Verifique a sua caixa de correio!";
                }
                else if (AnswUserExist == 1 && AnswAccountActive == 0) //Caso a conta não esteja ativa
                {
                    Session["ActivatedUser"] = "OK";

                    (int userCode, string username) = Classes.User.DetermineUtilizador(tbEmailRecover.Text);
                    List<int> UserProfile = Classes.User.DetermineUserProfile(Convert.ToInt32(userCode));

                    foreach (int profile in UserProfile)
                    {
                        string email = Classes.User.GetEmailFromDatabase(userCode);
                        EmailControl.SendEmailWaitingValidation(email, username);
                        lbl_message.Text = $"A sua conta ainda aguarda validação!";
                    }
                }
                else //Caso o e-mail não esteja associado a nenhuma conta
                {
                    lbl_message.CssClass = "alert alert-primary text-white text-center";

                    lbl_message.Text = "Este e-mail não está associado a nenhuma conta! Registe-se.";
                }
            }
        }
    }
}