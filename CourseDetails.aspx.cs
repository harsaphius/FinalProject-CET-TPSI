using FinalProject.Classes;
using System;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MainCourses"] == null && Session["MainPage"] == null)
            {
                Response.Redirect("MainCourses.aspx");
            }
            else if (Session["MainCourses"] != null && Session["MainCourses"].ToString() == "Yes" &&
                     Session["Logado"] == null)
            {
                Course CompleteCourse = new Course();
                if (CompleteCourse != null)
                {
                    int CodCurso = Convert.ToInt32(Session["CodCurso"].ToString());
                    CompleteCourse = Course.CompleteCourse(CodCurso);

                    lblNome.Text = CompleteCourse.Nome;
                    lblArea.Text = CompleteCourse.Area;
                    lblTipo.Text = CompleteCourse.TipoCurso;
                    lblRef.Text = CompleteCourse.CodRef;
                    lblQNQ.Text = CompleteCourse.CodQNQ.ToString();

                    rptModules.DataSource = CompleteCourse.Modules;
                    rptModules.DataBind();
                }
            }
            else if (Session["MainPage"] != null && Session["MainPage"].ToString() == "Yes" &&
                     Session["Logado"] == null)
            {
                Course CompleteCourse = new Course();
                if (CompleteCourse != null)
                {
                    int CodCurso = Convert.ToInt32(Session["CodCurso"].ToString());
                    CompleteCourse = Course.CompleteCourse(CodCurso);

                    lblNome.Text = CompleteCourse.Nome;
                    lblArea.Text = CompleteCourse.Area;
                    lblTipo.Text = CompleteCourse.TipoCurso;
                    lblRef.Text = CompleteCourse.CodRef;
                    lblQNQ.Text = CompleteCourse.CodQNQ.ToString();

                    rptModules.DataSource = CompleteCourse.Modules;
                    rptModules.DataBind();
                }
            }
            else if (Session["Logado"].ToString() == "Yes")
            {
                string user = Session["User"].ToString();

                //Find lbl_user on MasterPage and
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

                string script = @"
                            document.getElementById('courses').href = './UserCourses.aspx';
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

                if (Session["CodUtilizador"] != null && Session["CodUtilizador"].ToString() == "4" ||
                    Session["CodUtilizador"].ToString() == "1")
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

                Course CompleteCourse = new Course();
                if (CompleteCourse != null)
                {
                    int CodCurso = Convert.ToInt32(Session["CodCurso"].ToString());
                    CompleteCourse = Course.CompleteCourse(CodCurso);

                    lblNome.Text = CompleteCourse.Nome;
                    lblArea.Text = CompleteCourse.Area;
                    lblTipo.Text = CompleteCourse.TipoCurso;
                    lblRef.Text = CompleteCourse.CodRef;
                    lblQNQ.Text = CompleteCourse.CodQNQ.ToString();

                    rptModules.DataSource = CompleteCourse.Modules;
                    rptModules.DataBind();
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (Session["MainPage"] != null)
            {
                if (Session["MainPage"].ToString() == "Yes")
                {
                    Response.Redirect("MainPage.aspx");
                }
            }
            if (Session["MainCourses"] != null)
            {
                if (Session["MainCourses"].ToString() == "Yes")
                {
                    Response.Redirect("MainCourses.aspx");
                }
            }
        }
    }
}