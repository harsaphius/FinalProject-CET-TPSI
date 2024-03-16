using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            }
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            if((bool)flexCheckDefault.Checked == true)
            { 
                (bool password, List<string> failures) = Security.IsPasswordStrong(tb_pw.Text);

                if (Security.IsValidEmail(tb_email.Text) == false){

                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Introduza um e-mail válido!";
                }              
                else if (!password)
                {                    
                    foreach (var failure in failures)
                    {
                       string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                        lbl_message.Text += failure + "\n";
                    }
                }
                else if(tb_pw.Text != tb_pwR.Text)
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "A palavra-passe e a sua repetição não correspondem.";
                }
                else
                {
                    List<string> userData = new List<string>();
                    userData.Add(ddlPerfil.SelectedValue);
                    userData.Add(tb_name.Text);
                    userData.Add(tb_username.Text);
                    userData.Add(tb_email.Text);
                    userData.Add(tb_pw.Text);
                    userData.Add(ddl_tipoDocIdent.SelectedValue);
                    userData.Add(tbCC.Text);
                    userData.Add(tbdataValidade.Text);
                    userData.Add(ddlprefixo.SelectedValue);
                    userData.Add(tbTelemovel.Text);

                    int UserRegister = Classes.User.registerUser(userData);

                    if(UserRegister == 1)
                    {
                        string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                        lbl_message.Text = "Utilizador registado com sucesso!";
                    }
                    else {
                        string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                        lbl_message.Text = "Utilizador já registado!";
                    }
                }
            }
        }
    }
}