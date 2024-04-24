using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ClassesDetails : System.Web.UI.Page
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

                string userCode = Session["CodUtilizador"].ToString();
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
                }
            }


            if (!IsPostBack)
            {
                ClassGroup CompleteClassGroup = new ClassGroup();
                if (CompleteClassGroup != null)
                {
                    string codTurma = Session["CodTurma"].ToString();
                    CompleteClassGroup = Classes.ClassGroup.LoadClassGroup(codTurma);

                    rptTeachersDetail.DataSource = CompleteClassGroup.Teachers;
                    rptTeachersDetail.DataBind();

                    rptStudentsDetail.DataSource = CompleteClassGroup.Students;
                    rptStudentsDetail.DataBind();

                    lblNomeCurso.Text = CompleteClassGroup.NomeCurso;
                    lblNomeTurma.Text = CompleteClassGroup.NomeTurma;
                    lblRegime.Text = CompleteClassGroup.Regime;
                    lblHorario.Text = CompleteClassGroup.HorarioTurma;
                    lblDataInicioDetail.Text = CompleteClassGroup.DataInicio.ToShortDateString();
                    lblDataFimDetail.Text = CompleteClassGroup.DataFim.ToShortDateString();
                    lblNrTurma.Text = CompleteClassGroup.CodTurma.ToString();

                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageClasses.aspx");
        }


        protected void lbtDesistencia_Click(object sender, EventArgs e)
        {
            string codTurma = Session["CodTurma"].ToString();

            LinkButton lbtDesistencia = (LinkButton)sender;
            string codFormando = lbtDesistencia.CommandArgument;

            int AnswSituacao = Student.ChangeSituationForFormandoInTurma(Convert.ToInt32(codFormando), Convert.ToInt32(codTurma), 2);

            if (AnswSituacao == 1)
            {
                lblMessageEdit.Visible = true;
                lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                lblMessageEdit.Text = "Situação atualizada com sucesso!";
                timerMessageEdit.Enabled = true;
            }

        }

        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
        }
    }
}