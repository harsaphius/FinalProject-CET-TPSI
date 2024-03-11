﻿using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace FinalProject
{
    public partial class UserSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Google Data
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["clientid"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["clientsecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["redirection_url"];

            //Facebook Data
            FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookKey"];
            FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookSecret"];
            FaceBookConnect.Version = ConfigurationManager.AppSettings["FacebookVersion"];

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
            string script;
            
            List<int> isLoginAllowed = Classes.User.IsLoginAllowed(tb_username.Text, tb_pw.Text);

            if (isLoginAllowed[0] == 1 && isLoginAllowed[1] == 1)
            {
                if (isLoginAllowed[0] == 1) Session["Admin"] = "Yes";                
                else Session["Admin"] = "No";

                


                Session["User"] = tb_username.Text;
                Session["CodUtilizador"] = isLoginAllowed[3];
                Session["Logado"] = "Yes";

                Response.Redirect("./UserCourses.aspx");
            }
            else if (isLoginAllowed[0] == 1 && isLoginAllowed[1] == 0)
            {
                Session["ActivatedUser"] = "Conta ativada com sucesso!";

                script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = $"A sua conta ainda não se encontra ativa! <a href='https://localhost:44352/ActivationPage.aspx?user={Security.EncryptString(tb_username.Text)}&redirected=true'> Clique aqui para a ativar </a>!";

            }
            else if (isLoginAllowed[0] == -1 && isLoginAllowed[1] == -1)
            {
                script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "Este utilizador não está associado a nenhuma conta! Registe-se.";
            }
            else
            {
                script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "O seu utilizador ou palavra-passe estão errados! Tente novamente ou recupere a password!";
            }
        }
    }
}