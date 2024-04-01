using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageClasses : System.Web.UI.Page
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
            }

            BindDataClassGroups();
            BindDataStudents();
        }

        protected void btnPreviousStudents_Click(object sender, EventArgs e)
        {
            PageNumberModules -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
        }

        protected void btnNextStudents_Click(object sender, EventArgs e)
        {
            PageNumberModules += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
        }

        private void BindDataStudents()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Student.LoadStudents();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberStudents;
            int PageNumber = PageNumberStudents + 1;

            rptStudents.DataSource = pagedData;
            rptStudents.DataBind();

            btnPreviousStudents.Enabled = !pagedData.IsFirstPage;
            btnNextStudents.Enabled = !pagedData.IsLastPage;
        }

        private void BindDataClassGroups()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.ClassGroup.LoadClassGroups();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberClassGroups;
            int PageNumber = PageNumberClassGroups + 1;

            rptClasses.DataSource = pagedData;
            rptClasses.DataBind();

            btnPreviousClasses.Enabled = !pagedData.IsFirstPage;
            btnNextClasses.Enabled = !pagedData.IsLastPage;
        }


        private void BindDataModules()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberModules;

        }

        private int PageNumberStudents
        {
            get
            {
                if (ViewState["PageNumberStudents"] != null)
                    return Convert.ToInt32(ViewState["PageNumberStudents"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberStudents"] = value;
        }

        private int PageNumberModules
        {
            get
            {
                if (ViewState["PageNumberModules"] != null)
                    return Convert.ToInt32(ViewState["PageNumberModules"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberModules"] = value;
        }

        private int PageNumberClassGroups
        {
            get
            {
                if (ViewState["PageNumberClassGroups"] != null)
                    return Convert.ToInt32(ViewState["PageNumberClassGroups"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberClassGroups"] = value;
        }


    }
}