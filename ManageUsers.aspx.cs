using FinalProject.Classes;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageUsers : System.Web.UI.Page
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

                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                if (!IsPostBack)
                {

                    BindDataUsers();


                }
            }
        }

        //Função de Databinding
        private void BindDataUsers()
        {

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberUsers;
            int PageNumber = PageNumberUsers + 1;
            lblPageNumberUsers.Text = PageNumber.ToString();

            rptUsers.DataSource = pagedData;
            rptUsers.DataBind();

            btnPreviousUser.Enabled = !pagedData.IsFirstPage;
            btnNextUser.Enabled = !pagedData.IsLastPage;
        }


        //Função de Paginação
        private int PageNumberUsers
        {
            get
            {
                if (ViewState["PageNumberUsers"] != null)
                    return Convert.ToInt32(ViewState["PageNumberUsers"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberUsers"] = value;
        }


        protected void rptUsers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                User user = (User)e.Item.DataItem;

                CheckBox ckbAtivo = (CheckBox)e.Item.FindControl("ckbAtivo");
                CheckBox ckbFormando = (CheckBox)e.Item.FindControl("ckbFormando");
                CheckBox ckbFormador = (CheckBox)e.Item.FindControl("ckbFormador");
                CheckBox ckbFuncionario = (CheckBox)e.Item.FindControl("ckbFuncionario");


                ckbAtivo.Checked = user.Ativo;
                ckbFormando.Checked = user.UserProfiles.Contains(2);
                ckbFormador.Checked = user.UserProfiles.Contains(3);
                ckbFuncionario.Checked = user.UserProfiles.Contains(4);
            }
        }
    }
}