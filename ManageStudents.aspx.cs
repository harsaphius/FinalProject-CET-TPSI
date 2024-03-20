using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script;

            if (Session["Logado"] == null)
            {
                Response.Redirect("MainPage.aspx");
            }
            else if (Session["Logado"].ToString() == "Yes")
            {
                string user = Session["User"].ToString();

                Label lbluser = Master.FindControl("lbl_user") as Label;
                if (lbluser != null)
                {
                    lbluser.Text = user;
                }

                LinkButton lbtncourses = Master.FindControl("courses") as LinkButton;
                if (lbtncourses != null)
                {
                    lbtncourses.PostBackUrl = "./UserCourses.aspx";
                }

                script = @"
                            document.getElementById('courses').href = './UserCourses.aspx'
                            document.getElementById('signout').classList.remove('hidden');
                            document.getElementById('signout').classList.add('nav-item');
                            document.getElementById('signin').classList.add('hidden');
                            document.getElementById('signin').classList.remove('nav-item');
                            document.getElementById('signup').classList.remove('nav-item');
                            document.getElementById('signup').classList.add('hidden');
                            document.getElementById('navButtonSignOut').classList.remove('hidden');
                            document.getElementById('navButtonSignOut').classList.add('nav-item');
                            document.getElementById('navButtonSignOut').classList.add('d-flex');
                            document.getElementById('navButtonSignOut').classList.add('align-items-center');
                            document.getElementById('navButtonSignIn').classList.remove('nav-item'); 
                            document.getElementById('navButtonSignIn').classList.remove('d-flex');
                            document.getElementById('navButtonSignIn').classList.remove('align-items-center');
                            document.getElementById('navButtonSignIn').classList.add('hidden');
                            document.getElementById('courses').classList.remove('hidden');
                            document.getElementById('profile').classList.remove('hidden');
                            document.getElementById('usercalendar').classList.remove('hidden');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                if (Session["CodUtilizador"] != null && Session["CodUtilizador"].ToString() == "4" || Session["CodUtilizador"].ToString() == "1")
                {
                    script = @"  
                                document.getElementById('management').classList.remove('hidden');
                                document.getElementById('managecourses').classList.remove('hidden');
                                document.getElementById('manageclasses').classList.remove('hidden');
                                document.getElementById('managemodules').classList.remove('hidden');
                                document.getElementById('managestudents').classList.remove('hidden');
                                document.getElementById('manageteachers').classList.remove('hidden');
                                document.getElementById('manageclassrooms').classList.remove('hidden');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                //Reinicializar o FlatPickr
                if (IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", @"
                        <script>
                            document.addEventListener('DOMContentLoaded', function() {
                                flatpickr('#<%= tbDataNascimento.ClientID %>', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    maxDate: new Date()
                                });

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

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<string> userData = new List<string>();
            userData.Add(Convert.ToString(2));
            userData.Add(tbEmail.Text);
            userData.Add(tbEmail.Text);
            string NovaPasse = Membership.GeneratePassword(8, 2);
            userData.Add(NovaPasse);
            userData.Add(tbNome.Text);
            userData.Add(ddlDocumentoIdent.SelectedValue);
            userData.Add(tbCC.Text);
            userData.Add(tbDataValidade.Text);
            userData.Add(ddlprefixo.SelectedValue);
            userData.Add(tbTelemovel.Text);
            userData.Add(ddlSexo.SelectedValue);
            userData.Add(tbDataNascimento.Text);
            userData.Add(tbNIF.Text);
            userData.Add(tbMorada.Text);
            userData.Add(ddlCodPais.SelectedValue);
            userData.Add(tbCodPostal.Text);
            userData.Add(ddlCodEstadoCivil.SelectedValue);
            userData.Add(tbNrSegSocial.Text);
            userData.Add(tbIBAN.Text);
            userData.Add(tbNaturalidade.Text);
            userData.Add(ddlCodNacionalidade.SelectedValue);
            HttpPostedFile photoFile = fuFoto.PostedFile;

            byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
            userData.Add(Convert.ToBase64String(photoBytes));

            List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);

            int UserRegister = Classes.User.registerUser(userData);
            Classes.User.completeRegisterUser(userData, uploadedFiles);

            if (UserRegister == 1)
            {
                string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "Utilizador registado com sucesso!";

                Classes.EmailControl.SendEmailActivation(tbEmail.Text);
            }
            else
            {
                string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "Utilizador já registado!";
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageSudents.aspx?Insert");
        }
    }
}