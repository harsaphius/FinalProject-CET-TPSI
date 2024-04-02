using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UserCourses : System.Web.UI.Page
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
                string userCode = Session["CodUtilizador"].ToString();

                if (Session["Enrollment"] != null)
                {
                    if (Session["CodCursoEnrollment"] != null)
                    {
                        int CodCurso = Convert.ToInt32(Session["CodCursoEnrollment"].ToString());
                        Enrollment enrollment = new Enrollment();
                        enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizador"].ToString());
                        enrollment.CodSituacao = 1;
                        enrollment.CodCurso = CodCurso;

                        (int AnswAnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollment(enrollment);

                        if (AnswEnrollmentCode == -1 && AnswAnswEnrollmentRegister == -1)
                        {
                            lblEnrollment.Text = "Utilizador já registado nesse curso.";
                        }
                        else
                        {
                            Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizador"]), AnswEnrollmentCode);
                            lblEnrollment.Text = "Utilizador registado com sucesso no curso!";
                        }
                    }
                }

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

                List<int> UserProfile = Classes.User.DetermineUserProfile(Convert.ToInt32(userCode));

                foreach (int profileCode in UserProfile)
                {
                    if (profileCode == 2)
                    {
                        LinkButton linkButton = new LinkButton();

                        linkButton.ID = "profileLinkButton_" + profileCode; // Unique ID for each LinkButton
                        linkButton.Text = "Formando";
                        linkButton.CssClass = "nav-link mb-0 px-0 py-1";

                        linkButton.Click += FormandoPerfil_Click;
                        panelContainer.Controls.Add(linkButton);
                    }
                    if (profileCode == 3)
                    {
                        LinkButton linkButton = new LinkButton();

                        linkButton.ID = "profileLinkButton_" + profileCode; // Unique ID for each LinkButton
                        linkButton.Text = "Formador";
                        linkButton.CssClass = "nav-link mb-0 px-0 py-1";

                        linkButton.Click += FormadorPerfil_Click;
                        panelContainer.Controls.Add(linkButton);
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
                            document.getElementById('profile').classList.remove('hidden');
                            document.getElementById('usercalendar').classList.remove('hidden');
                            document.getElementById('courses').classList.remove('hidden');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    if (profileCode == 4 || profileCode == 1)
                    {
                        script = @"
                            document.getElementById('management').classList.remove('hidden');
                            document.getElementById('managecourses').classList.remove('hidden');
                            document.getElementById('manageclasses').classList.remove('hidden');
                            document.getElementById('managemodules').classList.remove('hidden');
                            document.getElementById('managestudents').classList.remove('hidden');
                            document.getElementById('manageteachers').classList.remove('hidden');
                            document.getElementById('manageclassrooms').classList.remove('hidden');
                            document.getElementById('manageusers').classList.remove('hidden');

                            ";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                    }
                }
            }
        }

        public void FormandoPerfil_Click(object sender, EventArgs e)
        {
        }

        public void FormadorPerfil_Click(object sender, EventArgs e)
        {
        }
    }
}