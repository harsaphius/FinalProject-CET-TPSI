using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace FinalProject
{
    public partial class UserSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string script;

                    if (Session["Google"] != null)
                    {
                        if (Session["Google"].ToString() == "Yes")
                        {
                            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
                            string code = Request.QueryString["code"];
                            string json = GoogleConnect.Fetch("me", code);
                            GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                            tb_username.Text = profile.Name;
                            tb_email.Text = profile.Email;

                            (int AnswUserCode, string Username) = Classes.User.DetermineUtilizador(profile.Email);
                            (int AnswUserExists, int AnswAccountActive) = Classes.User.CheckEmail(profile.Email);

                            if (AnswUserExists == 1 && AnswAccountActive == 1)
                            {
                                if (AnswUserCode == 1 || AnswUserCode == 4) Session["Admin"] = "Yes";
                                else Session["Admin"] = "No";

                                Session["User"] = Username;
                                Session["CodUtilizador"] = AnswUserCode;
                                Session["Logado"] = "Yes";

                                Response.Redirect("./UserCourses.aspx");
                            }
                            else if (AnswUserExists == 1 && AnswAccountActive == 0)
                            {
                                Session["ActivatedUser"] = "Conta ativada com sucesso!";

                                lbl_message.CssClass = "alert alert-primary text-white text-center";

                                Classes.EmailControl.SendEmailActivation(profile.Email, Username);

                                lblUserFromGoogle.Text = $"A sua conta ainda não se encontra ativa! Procedemos ao reenvio do e-mail de ativação!";
                            }
                            else if (AnswUserExists == -1 && AnswAccountActive == -1)
                            {
                                lbl_message.CssClass = "alert alert-primary text-white text-center";

                                lblUserFromGoogle.Text = "Este utilizador não está associado a nenhuma conta! Registe-se.";
                            }
                        }
                    }
                    if (Session["Facebook"] != null)
                    {
                        if (Session["Facebook"].ToString() == "Yes")
                        {
                            string data = FaceBookConnect.Fetch(Request.QueryString["code"], "me", "id, name, email");
                            FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                            tb_username.Text = faceBookUser.Name;
                            tb_email.Text = faceBookUser.Email;
                        }
                    }
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    return;
                }

                //Reinicializar o FlatPickr
                if (IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", @"
                        <script>
                            document.addEventListener('DOMContentLoaded', function() {
                                flatpickr('#<%= tbDataValidade.ClientID %>', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    minDate: new Date()
                                });
                            });
                        </script>
                    ", false);
                }
            }
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            if ((bool)flexCheckDefault.Checked == true)
            {
                (bool password, List<string> failures) = Security.IsPasswordStrong(tb_pw.Text);

                if (Security.IsValidEmail(tb_email.Text) == false)
                {
                    lbl_message.CssClass = "alert alert-primary text-white text-center";

                    lbl_message.Text = "Introduza um e-mail válido!";
                }
                else if (!password)
                {
                    foreach (var failure in failures)
                    {
                        lbl_message.CssClass = "alert alert-primary text-white text-center";

                        lbl_message.Text += failure + "\n";
                    }
                }
                else if (tb_pw.Text != tb_pwR.Text)
                {
                    lbl_message.CssClass = "alert alert-primary text-white text-center";

                    lbl_message.Text = "A palavra-passe e a sua repetição não correspondem.";
                }
                else
                {
                    User user = new User();

                    user.CodPerfil = Convert.ToInt32(ddlPerfil.SelectedValue);
                    user.Nome = tb_name.Text;
                    user.Username = tb_username.Text;
                    user.Email = tb_email.Text;
                    user.Password = tb_pw.Text;
                    user.CodTipoDoc = Convert.ToInt32(ddl_tipoDocIdent.SelectedValue);
                    user.DocIdent = tbCC.Text;
                    user.DataValidade = Convert.ToDateTime(tbdataValidade.Text);
                    user.CodPrefix = Convert.ToInt32(ddlprefixo.SelectedValue);
                    user.Phone = tbTelemovel.Text;

                    (int UserRegister, int UserCode) = Classes.User.RegisterUser(user);

                    if (UserRegister == 1)
                    {
                        lbl_message.CssClass = "alert alert-primary text-white text-center";

                        EmailControl.SendEmailActivation(tb_email.Text, tb_username.Text);

                        lbl_message.Text = "Utilizador registado com sucesso!";
                    }
                    else
                    {
                        lbl_message.CssClass = "alert alert-primary text-white text-center";

                        lbl_message.Text = $"Utilizador já registado! Se não se lembra da sua password recupere a sua conta <a href='{ConfigurationManager.AppSettings["SiteURL"]}/UserLogin.aspx'> link </a>!";
                    }
                }
            }
        }
    }
}