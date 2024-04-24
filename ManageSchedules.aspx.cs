using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageSchedules : System.Web.UI.Page
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

                Label lbluser = Master.FindControl("lbl_user") as Label;
                if (lbluser != null)
                {
                    lbluser.Text = user;
                }

                List<int> UserProfile = Classes.User.DetermineUserProfile(Convert.ToInt32(userCode));

                foreach (int profileCode in UserProfile)
                {
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
                            document.getElementById('statistics').classList.remove('hidden');
                            
                            ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                    }
                    if (profileCode == 1)
                    {
                        script = @"
                            document.getElementById('manageusers').classList.remove('hidden');
                            
                            ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowUsers", script, true);
                    }

                    if (!IsPostBack)
                    {
                        if (Session["CodTurma"] != null)
                        {
                            string codTurma = Session["CodTurma"].ToString();
                            ClassGroup classGroup = Classes.ClassGroup.LoadClassGroup(codTurma);
                            hdfHorario.Value = classGroup.HorarioTurma;
                            hdfInitialDate.Value = classGroup.DataInicio.ToShortDateString();
                            NrTurma.Text = classGroup.NomeTurma;

                            lblDate.Text = classGroup.DataInicio.ToShortDateString();

                            ddlModulesForClassGroup.DataBind();

                            int moduloID = Convert.ToInt32(ddlModulesForClassGroup.SelectedItem.Value);
                            int moduleDuration = Module.GetUFCDDurationByModuleID(moduloID);
                            hdfDuration.Value = moduleDuration.ToString();

                            (int TeacherID, string TeacherNome) = Teacher.GetTeacherByModuleAndClass(moduloID, Convert.ToInt32(codTurma));

                            hdfTeacher.Value = TeacherID.ToString();
                            hdfTeacherNome.Value = TeacherNome;
                            lblFormador.Text = TeacherNome;
                        }
                    }
                }


            }
        }

        protected void ddlModulesForClassGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string codTurma = Session["CodTurma"].ToString();
            string selectedModuleID = ddlModulesForClassGroup.SelectedValue;
            int moduloID = Convert.ToInt32(ddlModulesForClassGroup.SelectedItem.Value);
            int moduleDuration = Module.GetUFCDDurationByModuleID(Convert.ToInt32(selectedModuleID));
            hdfDuration.Value = moduleDuration.ToString();

            (int TeacherID, string TeacherNome) = Teacher.GetTeacherByModuleAndClass(moduloID, Convert.ToInt32(codTurma));

            hdfTeacher.Value = TeacherID.ToString();
            hdfTeacherNome.Value = TeacherNome;
            lblFormador.Text = TeacherNome;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageClasses.aspx");
        }
    }
}