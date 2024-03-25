using System;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class MainCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rpt_maincourses.DataSource = Classes.Course.LoadCourses();
            rpt_maincourses.DataBind();

            if (!IsPostBack)
            {
            }
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            tb_search.Text = "";
            ddl_area.SelectedIndex = 0;
            ddl_tipo.SelectedIndex = 0;
            tb_dataInicio.Text = "";
            tb_dataFim.Text = "";
        }

        protected void btn_details_Click(object sender, EventArgs e)
        {
            Button btn_details = (Button)sender;
            HiddenField hdnCourseID = (HiddenField)btn_details.NamingContainer.FindControl("hdnCourseID");
            string codCurso = hdnCourseID.Value;

            Session["CodCurso"] = codCurso;
            Session["MainCourses"] = "Yes";

            Response.Redirect("CourseDetails.aspx?id=" + codCurso);
        }

        protected void btn_enroll_Click(object sender, EventArgs e)
        {
            Session["Enrollment"] = "Yes";
            Button btn_enroll = (Button)sender;

            HiddenField hdnCourseID = (HiddenField)btn_enroll.NamingContainer.FindControl("hdnCourseID");
            string codCurso = hdnCourseID.Value;

            Session["CodCursoEnrollment"] = codCurso;

            if (Session["Logado"] == null)
            {
                Response.Redirect("UserSignIn.aspx");
            }
            else
            {
                Response.Redirect("UserCourses.aspx");
            }
        }
    }
}