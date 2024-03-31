using FinalProject.Classes;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageClassrooms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Logado"] == null)
            {
                Response.Redirect("MainPage.aspx");
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

                BindDataClassrooms();
            }
        }

        protected void rpt_Classrooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void rpt_Classrooms_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                RepeaterItem item = rpt_Classrooms.Items[e.Item.ItemIndex];

                TextBox tbNrSala = (TextBox)item.FindControl("tbNrSala");
                Label lblNrSala = (Label)item.FindControl("lblNrSala");

                DropDownList ddlTipoSala = (DropDownList)item.FindControl("ddlTipoSala");
                Label lblTipoSala = (Label)item.FindControl("lblTipoSala");

                DropDownList ddlLocalSala = (DropDownList)item.FindControl("ddlLocalSala");
                Label lblLocalSala = (Label)item.FindControl("lblLocalSala");

                tbNrSala.Visible = !tbNrSala.Visible;
                lblNrSala.Visible = !lblNrSala.Visible;

                ddlTipoSala.Visible = !ddlTipoSala.Visible;
                lblTipoSala.Visible = !lblTipoSala.Visible;
                ListItem selectedTipoSalaItem = ddlTipoSala.Items.FindByText(lblTipoSala.Text);

                if (selectedTipoSalaItem != null)
                {
                    ddlTipoSala.ClearSelection();
                    selectedTipoSalaItem.Selected = true;
                }

                ddlLocalSala.Visible = !ddlLocalSala.Visible;
                lblLocalSala.Visible = !lblLocalSala.Visible;
                ListItem selectedLocalSalaItem = ddlLocalSala.Items.FindByText(lblLocalSala.Text);

                if (selectedLocalSalaItem != null)
                {
                    ddlLocalSala.ClearSelection();
                    selectedLocalSalaItem.Selected = true;
                }

                LinkButton lbt_edit = (LinkButton)item.FindControl("lbt_edit");
                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbt_cancel");
                LinkButton lbt_delete = (LinkButton)item.FindControl("lbt_delete");
                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbt_confirm");

                lbt_cancel.Visible = true;
                lbt_confirm.Visible = true;

                lbt_edit.Visible = false;
                lbt_delete.Visible = false;
            }
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            PageNumberClassrooms -= 1;
            BindDataClassrooms();
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            PageNumberClassrooms += 1;
            BindDataClassrooms();
        }

        private void BindDataClassrooms()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Classroom.LoadClassrooms();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberClassrooms;


            rpt_Classrooms.DataSource = pagedData;
            rpt_Classrooms.DataBind();

            btn_previous.Enabled = !pagedData.IsFirstPage;
            btn_next.Enabled = !pagedData.IsLastPage;
        }

        public int PageNumberClassrooms
        {
            get
            {
                if (ViewState["PageNumberClassrooms"] != null)
                    return Convert.ToInt32(ViewState["PageNumberClassrooms"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberClassrooms"] = value;
        }

        protected void btnInsertClassroom_OnClick(object sender, EventArgs e)
        {
            Classroom classroom = new Classroom();

            classroom.NrSala = tbClassroomNr.Text;
            classroom.CodTipoSala = Convert.ToInt32(ddlTipoSala.SelectedValue);
            classroom.CodLocalSala = Convert.ToInt32(ddlLocalSala.SelectedValue);

            int AnswClassroomRegisted = Classes.Classroom.InsertClassroom(classroom);

            if (AnswClassroomRegisted == 1)
            {
                lbl_message.Text = "Sala inserida com sucesso!";
            }

        }
    }
}