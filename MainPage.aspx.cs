using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script;

            if (Session["Logado"] == null)
            {
            }
            else if (Session["Logado"] != null && !Page.IsPostBack)
            {
                string user = Session["User"].ToString();
                string userCode = Session["CodUtilizador"].ToString();

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

            if (!IsPostBack)
            {
                BindDataCourses();
            }
        }

        protected void btn_details_Click(object sender, EventArgs e)
        {
            Button btn_details = (Button)sender;

            HiddenField hdnCourseID = (HiddenField)btn_details.NamingContainer.FindControl("hdnCourseID");
            string codCurso = hdnCourseID.Value;

            Session["CodCurso"] = codCurso;
            Session["MainPage"] = "Yes";

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

        private void BindDataCourses(List<string> conditions = null)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses(conditions);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 3;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberListCourses.Text = (PageNumber).ToString();

            rptMainPanel.DataSource = pagedData;
            rptMainPanel.DataBind();

            btnPreviousMainPage.Enabled = !pagedData.IsFirstPage;
            btnNextMainPage.Enabled = !pagedData.IsLastPage;

        }

        private int PageNumberCourses
        {
            get
            {
                if (ViewState["PageNumberCourses"] != null)
                    return Convert.ToInt32(ViewState["PageNumberCourses"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberCourses"] = value;
        }

        protected void btnNextMainPage_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses += 1;
            BindDataCourses();
        }

        protected void btnPreviousMainPage_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses -= 1;
            BindDataCourses();
        }
    }
}