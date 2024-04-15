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
                            document.getElementById('manageusers').classList.remove('hidden');
                            document.getElementById('statistics').classList.remove('hidden');
                            document.getElementById('manageschedules').classList.remove('hidden');
                            ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                    }

                    if (!IsPostBack)
                    {
                        ddlClassGroup.DataBind();

                        if (Session["CodTurma"] != null)
                        {
                            string codTurma = Session["CodTurma"].ToString();
                            int CodCurso = 0;
                            foreach (ListItem item in ddlClassGroup.Items)
                            {
                                if (item.Value == codTurma)
                                {
                                    item.Selected = true;
                                    ClassGroup classGroup = Classes.ClassGroup.LoadClassGroup(codTurma);
                                    hdfHorario.Value = classGroup.HorarioTurma;
                                    CodCurso = classGroup.CodCurso;
                                    break; // Exit the loop once the item is found
                                }
                            }

                           
                            //First Module of First Course
                            Course course = new Course();
                            course = Course.CompleteCourse(CodCurso);
                            List<Module> modules = course.Modules;
                            Module firstModule = modules[0];
                            int firstModuleID = firstModule.CodModulo;

                            BindDdlModules(firstModuleID.ToString());
                            //Bind Teacher of First Module of First Course
                            BindDdlTeachers(firstModuleID.ToString(),codTurma);
                        }
                        
                    }
                }
            }

        }

        protected void ddlModulesForClassGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModuleID = ddlModulesForClassGroup.SelectedValue;
            ddlTeachersForModulesOfClassGroup.Items.Clear();

            BindDdlTeachers(selectedModuleID, ddlClassGroup.SelectedValue);
        }

        private void BindDdlModules(string selectClassGroup)
        {
            ddlTeachersForModulesOfClassGroup.Items.Clear();

            SQLDSModules.SelectCommand =
                $"SELECT * FROM moduloFormadorTurma AS MFT INNER JOIN modulo AS M ON MFT.codModulo = M.codModulos WHERE MFT.codTurma={ddlClassGroup.SelectedValue}";
            ddlModulesForClassGroup.DataSource = SQLDSModules;
            ddlModulesForClassGroup.DataTextField = "nomeModulos";
            ddlModulesForClassGroup.DataValueField = "codModulo";
            ddlModulesForClassGroup.DataBind();
        }

        private void BindDdlTeachers(string selectedModule, string CodTurma)
        {
            SQLDSModules.SelectCommand =
                $"SELECT * FROM moduloFormadorTurma AS MT INNER JOIN utilizador AS U ON MT.codFormador=U.codUtilizador INNER JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador INNER JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador WHERE MT.codModulo={selectedModule} AND MT.codTurma={CodTurma}";
            ddlTeachersForModulesOfClassGroup.DataSource = SQLDSModules;
            ddlTeachersForModulesOfClassGroup.DataTextField = "nome";
            ddlTeachersForModulesOfClassGroup.DataValueField = "codFormador";
            ddlTeachersForModulesOfClassGroup.DataBind();
        }

        protected void ddlClassGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedClassGroupID = ddlClassGroup.SelectedValue;
            ClassGroup classGroup = Classes.ClassGroup.LoadClassGroup(selectedClassGroupID);
            hdfHorario.Value = classGroup.HorarioTurma;
           
            ddlModulesForClassGroup.Items.Clear();
            int selectedCourseID = classGroup.CodCurso;
            BindDdlModules(selectedCourseID.ToString());

           

            Course course = new Course();
            course = Course.CompleteCourse(Convert.ToInt32(selectedCourseID));
            List<Module> modules = course.Modules;
            Module firstModule = modules[0];
            int firstModuleID = firstModule.CodModulo;
            BindDdlTeachers(firstModuleID.ToString(), selectedClassGroupID);
        }
    }
}