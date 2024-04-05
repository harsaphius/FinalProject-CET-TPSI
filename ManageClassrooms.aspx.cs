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

                if (!IsPostBack)
                {
                    BindDataClassrooms();
                }


            }
        }

        //Função de ItemDataBound do Repeater
        protected void rpt_Classrooms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lbtEditClassroom = (LinkButton)e.Item.FindControl("lbtEditClassroom");
                LinkButton lbtCancelClassroom = (LinkButton)e.Item.FindControl("lbtCancelClassroom");
                LinkButton lbtDeleteClassroom = (LinkButton)e.Item.FindControl("lbtDeleteClassroom");
                LinkButton lbtConfirmClassroom = (LinkButton)e.Item.FindControl("lbtConfirmClassroom");

                AsyncPostBackTrigger triggerEdit = new AsyncPostBackTrigger();
                triggerEdit.ControlID = lbtEditClassroom.UniqueID;
                triggerEdit.EventName = "Click";

                AsyncPostBackTrigger triggerCancel = new AsyncPostBackTrigger();
                triggerCancel.ControlID = lbtCancelClassroom.UniqueID;
                triggerCancel.EventName = "Click";

                AsyncPostBackTrigger triggerDelete = new AsyncPostBackTrigger();
                triggerDelete.ControlID = lbtDeleteClassroom.UniqueID;
                triggerDelete.EventName = "Click";

                AsyncPostBackTrigger triggerConfirm = new AsyncPostBackTrigger();
                triggerConfirm.ControlID = lbtConfirmClassroom.UniqueID;
                triggerConfirm.EventName = "Click";

                updatePanelListClassrooms.Triggers.Add(triggerEdit);
                updatePanelListClassrooms.Triggers.Add(triggerCancel);
                updatePanelListClassrooms.Triggers.Add(triggerDelete);
                updatePanelListClassrooms.Triggers.Add(triggerConfirm);
            }
        }


        //Função de ItemCommand do Repeater
        protected void rpt_Classrooms_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = rpt_Classrooms.Items[e.Item.ItemIndex];

            TextBox tbNrSala = (TextBox)item.FindControl("tbNrSalaEdit");
            Label lblNrSala = (Label)item.FindControl("lblNrSala");

            DropDownList ddlTipoSalaEdit = (DropDownList)item.FindControl("ddlTipoSalaEdit");
            Label lblTipoSala = (Label)item.FindControl("lblTipoSala");

            DropDownList ddlLocalSalaEdit = (DropDownList)item.FindControl("ddlLocalSalaEdit");
            Label lblLocalSala = (Label)item.FindControl("lblLocalSala");

            LinkButton lbtEditClassroom = (LinkButton)item.FindControl("lbtEditClassroom");
            LinkButton lbtCancelClassroom = (LinkButton)item.FindControl("lbtCancelClassroom");
            LinkButton lbtDeleteClassroom = (LinkButton)item.FindControl("lbtDeleteClassroom");
            LinkButton lbtConfirmClassroom = (LinkButton)item.FindControl("lbtConfirmClassroom");

            HiddenField hdnClassroomID = (HiddenField)item.FindControl("hdnClassroomID");
            string ClassroomID = hdnClassroomID.Value;

            if (e.CommandName == "Edit")
            {
                tbNrSala.Visible = !tbNrSala.Visible;
                lblNrSala.Visible = !lblNrSala.Visible;

                ddlTipoSalaEdit.Visible = !ddlTipoSalaEdit.Visible;
                lblTipoSala.Visible = !lblTipoSala.Visible;

                ListItem selectedTipoSalaItem = ddlTipoSalaEdit.Items.FindByText(lblTipoSala.Text);
                if (selectedTipoSalaItem != null)
                {
                    ddlTipoSalaEdit.ClearSelection();
                    selectedTipoSalaItem.Selected = true;
                }

                ddlLocalSalaEdit.Visible = !ddlLocalSalaEdit.Visible;
                lblLocalSala.Visible = !lblLocalSala.Visible;
                ListItem selectedLocalSalaItem = ddlLocalSalaEdit.Items.FindByText(lblLocalSala.Text);

                if (selectedLocalSalaItem != null)
                {
                    ddlLocalSalaEdit.ClearSelection();
                    selectedLocalSalaItem.Selected = true;
                }

                lbtCancelClassroom.Visible = true;
                lbtConfirmClassroom.Visible = true;

                lbtEditClassroom.Visible = false;
                lbtDeleteClassroom.Visible = false;
            }

            if (e.CommandName == "Confirm")
            {
                if (!string.IsNullOrEmpty(tbNrSala.Text))
                {
                    Classroom classroom = new Classroom();

                    classroom.CodSala = Convert.ToInt32(ClassroomID);
                    classroom.NrSala = tbNrSala.Text;
                    classroom.CodTipoSala = Convert.ToInt32(ddlTipoSalaEdit.SelectedValue);
                    classroom.CodLocalSala = Convert.ToInt32(ddlLocalSalaEdit.SelectedValue);

                    int AnswClassroomUpdated = Classes.Classroom.UpdateClassroom(classroom);

                    if (AnswClassroomUpdated == 1)
                    {
                        tbNrSala.Visible = !tbNrSala.Visible;
                        lblNrSala.Visible = !lblNrSala.Visible;
                        lblNrSala.Text = lblNrSala.Text;

                        ddlLocalSalaEdit.Visible = !ddlLocalSalaEdit.Visible;
                        lblLocalSala.Visible = !lblLocalSala.Visible;
                        lblLocalSala.Text = lblLocalSala.Text;

                        ddlTipoSalaEdit.Visible = !ddlTipoSalaEdit.Visible;
                        lblTipoSala.Visible = !lblTipoSala.Visible;
                        lblTipoSala.Text = lblTipoSala.Text;

                        lbtCancelClassroom.Visible = false;
                        lbtConfirmClassroom.Visible = false;

                        lbtEditClassroom.Visible = true;
                        lbtDeleteClassroom.Visible = true;

                        lblMessageEdit.Visible = true;
                        lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                        lblMessageEdit.Text = "Sala atualizada com sucesso";
                        timerMessageEdit.Enabled = true;
                    }
                    else if (AnswClassroomUpdated == 0)
                    {
                        tbNrSala.Visible = !tbNrSala.Visible;
                        lblNrSala.Visible = !lblNrSala.Visible;
                        lblNrSala.Text = lblNrSala.Text;

                        ddlLocalSalaEdit.Visible = !ddlLocalSalaEdit.Visible;
                        lblLocalSala.Visible = !lblLocalSala.Visible;
                        lblLocalSala.Text = lblLocalSala.Text;

                        ddlTipoSalaEdit.Visible = !ddlTipoSalaEdit.Visible;
                        lblTipoSala.Visible = !lblTipoSala.Visible;
                        lblTipoSala.Text = lblTipoSala.Text;

                        lbtCancelClassroom.Visible = false;
                        lbtConfirmClassroom.Visible = false;

                        lbtEditClassroom.Visible = true;
                        lbtDeleteClassroom.Visible = true;

                        lblMessageEdit.Visible = true;
                        lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                        lblMessageEdit.Text = "Sala não pode ser atualizada por já se encontrar a ser utilizada. Insira uma nova sala.";
                        timerMessageEdit.Enabled = true;
                    }
                    else if (AnswClassroomUpdated == -2)
                    {
                        tbNrSala.Visible = !tbNrSala.Visible;
                        lblNrSala.Visible = !lblNrSala.Visible;
                        lblNrSala.Text = lblNrSala.Text;

                        ddlLocalSalaEdit.Visible = !ddlLocalSalaEdit.Visible;
                        lblLocalSala.Visible = !lblLocalSala.Visible;
                        lblLocalSala.Text = lblLocalSala.Text;

                        ddlTipoSalaEdit.Visible = !ddlTipoSalaEdit.Visible;
                        lblTipoSala.Visible = !lblTipoSala.Visible;
                        lblTipoSala.Text = lblTipoSala.Text;

                        lbtCancelClassroom.Visible = false;
                        lbtConfirmClassroom.Visible = false;

                        lbtEditClassroom.Visible = true;
                        lbtDeleteClassroom.Visible = true;

                        lblMessageEdit.Visible = true;
                        lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                        lblMessageEdit.Text = "Sala não pode ser atualizada por já existir uma sala com estas características!";
                        timerMessageEdit.Enabled = true;
                    }
                }
            }

            if (e.CommandName == "Delete")
            {
                int AnswClassroomDeleted = Classes.Classroom.DeleteClassroom(Convert.ToInt32(ClassroomID));

                if (AnswClassroomDeleted == 1)
                {
                    BindDataClassrooms();

                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Sala apagada com sucesso!";
                    timerMessageEdit.Enabled = true;

                }
                else
                {
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Sala não pode ser eliminada por fazer parte de um horário!";
                    timerMessageEdit.Enabled = true;
                }


            }

            if (e.CommandName == "Cancel")
            {
                tbNrSala.Visible = !tbNrSala.Visible;
                lblNrSala.Visible = !lblNrSala.Visible;
                lblNrSala.Text = lblNrSala.Text;

                ddlLocalSalaEdit.Visible = !ddlLocalSalaEdit.Visible;
                lblLocalSala.Visible = !lblLocalSala.Visible;
                lblLocalSala.Text = lblLocalSala.Text;

                ddlTipoSalaEdit.Visible = !ddlTipoSalaEdit.Visible;
                lblTipoSala.Visible = !lblTipoSala.Visible;
                lblTipoSala.Text = lblTipoSala.Text;

                lbtCancelClassroom.Visible = false;
                lbtConfirmClassroom.Visible = false;

                lbtEditClassroom.Visible = true;
                lbtDeleteClassroom.Visible = true;
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
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "Sala inserida com sucesso!";
                timerMessageInsert.Enabled = true;
            }
            else
            {
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "Sala já existe!";
                timerMessageInsert.Enabled = true;
            }

        }

        //Função de Databinding
        private void BindDataClassrooms()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Classroom.LoadClassrooms();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberClassrooms;
            int PageNumber = PageNumberClassrooms + 1;
            lblPageNumberClassrooms.Text = PageNumber.ToString();

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

        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
        }

        protected void timerMessageInsert_OnTick(object sender, EventArgs e)
        {
            lblMessageInsert.Visible = false;
            timerMessageInsert.Enabled = false;
        }
    }
}