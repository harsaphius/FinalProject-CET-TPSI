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
                            document.getElementById('manageusers').classList.remove('hidden');

                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                BindDataClassrooms();
            }
        }

        //Função de ItemDataBound do Repeater
        protected void rpt_Classrooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }


        //Função de ItemCommand do Repeater
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
            if (e.CommandName == "Confirm")
            {
                RepeaterItem item = rpt_Classrooms.Items[e.Item.ItemIndex];

                HiddenField hdnClassroomID = (HiddenField)item.FindControl("hdnClassroomID");
                string ClassroomID = hdnClassroomID.Value;

                TextBox tbNrSala = (TextBox)item.FindControl("tbNrSala");
                DropDownList ddlTipoSala = (DropDownList)item.FindControl("ddlTipoSala");
                DropDownList ddlLocalSala = (DropDownList)item.FindControl("ddlLocalSala");

                Label lblNrSala = (Label)item.FindControl("lblNrSala");
                Label lblTipoSala = (Label)item.FindControl("lblTipoSala");
                Label lblLocalSala = (Label)item.FindControl("lblLocalSala");


                LinkButton lbt_edit = (LinkButton)item.FindControl("lbt_edit");
                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbt_cancel");
                LinkButton lbt_delete = (LinkButton)item.FindControl("lbt_delete");
                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbt_confirm");

                if (!string.IsNullOrEmpty(tbNrSala.Text))
                {
                    Classroom classroom = new Classroom();

                    classroom.CodSala = Convert.ToInt32(ClassroomID);
                    classroom.NrSala = tbNrSala.Text;
                    classroom.CodTipoSala = Convert.ToInt32(ddlTipoSala.SelectedValue);
                    classroom.CodLocalSala = Convert.ToInt32(ddlLocalSala.SelectedValue);

                    int AnswClassroomUpdated = Classes.Classroom.UpdateClassroom(classroom);

                    if (AnswClassroomUpdated == 1)
                    {
                        tbNrSala.Visible = !tbNrSala.Visible;
                        lblNrSala.Visible = !lblNrSala.Visible;
                        lblNrSala.Text = tbNrSala.Text;

                        ddlLocalSala.Visible = !ddlLocalSala.Visible;
                        lblLocalSala.Visible = !lblLocalSala.Visible;
                        lblLocalSala.Text = ddlLocalSala.Text;

                        ddlTipoSala.Visible = !ddlTipoSala.Visible;
                        lblTipoSala.Visible = !lblTipoSala.Visible;
                        lblTipoSala.Text = ddlTipoSala.Text;

                        // Show "Cancel" and "Confirm" buttons
                        lbt_cancel.Visible = false;
                        lbt_confirm.Visible = false;

                        // Hide "Edit" and "Delete" buttons
                        lbt_edit.Visible = true;
                        lbt_delete.Visible = true;

                        lbl_message.Text = "Sala atualizada com sucesso";
                    }
                    else if (AnswClassroomUpdated == -2)
                    {
                        tbNrSala.Visible = !tbNrSala.Visible;
                        lblNrSala.Visible = !lblNrSala.Visible;
                        lblNrSala.Text = lblNrSala.Text;

                        ddlLocalSala.Visible = !ddlLocalSala.Visible;
                        lblLocalSala.Visible = !lblLocalSala.Visible;
                        lblLocalSala.Text = lblLocalSala.Text;

                        ddlTipoSala.Visible = !ddlTipoSala.Visible;
                        lblTipoSala.Visible = !lblTipoSala.Visible;
                        lblTipoSala.Text = lblTipoSala.Text;

                        // Show "Cancel" and "Confirm" buttons
                        lbt_cancel.Visible = false;
                        lbt_confirm.Visible = false;

                        // Hide "Edit" and "Delete" buttons
                        lbt_edit.Visible = true;
                        lbt_delete.Visible = true;

                        lbl_message.Text = "Sala não pode ser atualizada por já se encontrar a ser utilizada. Insira uma nova sala.";
                    }
                }
            }

            if (e.CommandName == "Delete")
            {
                RepeaterItem item = rpt_Classrooms.Items[e.Item.ItemIndex];

                HiddenField hdnClassroomID = (HiddenField)item.FindControl("hdnClassroomID");
                string ClassroomID = hdnClassroomID.Value;

                int AnswClassroomDeleted = Classes.Classroom.DeleteClassroom(Convert.ToInt32(ClassroomID));

                if (AnswClassroomDeleted == 1)
                {
                    BindDataClassrooms();
                    lbl_message.Text = "Sala apagada com sucesso!";

                }
                else
                {
                    lbl_message.Text = "Sala não pode ser eliminada por fazer parte de um horário!";
                }


            }
        }


        //Função de Inserção
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

        //Função de Databinding
        private void BindDataClassrooms()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Classroom.LoadClassrooms();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberClassrooms;


            rpt_Classrooms.DataSource = pagedData;
            rpt_Classrooms.DataBind();

            btnPreviousClassroom.Enabled = !pagedData.IsFirstPage;
            btnNextClassroom.Enabled = !pagedData.IsLastPage;
        }

        //Função de Paginação
        private int PageNumberClassrooms
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

        //Funções para os botões de paginação
        protected void btnPreviousClassroom_OnClick(object sender, EventArgs e)
        {
            PageNumberClassrooms -= 1;
            BindDataClassrooms();
        }

        protected void btnNextClassroom_OnClick(object sender, EventArgs e)
        {
            PageNumberClassrooms += 1;
            BindDataClassrooms();
        }

    }
}