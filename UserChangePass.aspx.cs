using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace FinalProject
{
    public partial class UserChangePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_changepw_Click(object sender, EventArgs e)
        {
            (bool password, List<string> failures) = Security.IsPasswordStrong(tbEmail.Text);

            if (Security.IsValidEmail(tbEmail.Text) == false)
            {

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
            else if (tb_pw.Text != tb_pwr.Text)
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
                int ChangePass = Classes.Security.ChangePassword(tbEmail.Text, tb_pw.Text, tb_pwr.Text);
                if(ChangePass == 1)
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Palavra-passe alterada com sucesso!";

                }
                else
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Palavra-passe atual incorreta!";

                }

            }
        }
    }
}