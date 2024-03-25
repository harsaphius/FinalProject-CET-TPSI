using FinalProject.Classes;
using System;

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
            else if (Session["MainCourses"] != null && Session["MainCourses"].ToString() == "Yes")
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
            else if (Session["MainPage"] != null && Session["MainPage"].ToString() == "Yes")
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