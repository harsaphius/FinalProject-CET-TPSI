using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageCourses : System.Web.UI.Page
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
                script = @"
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
                            document.getElementById('managecourses').classList.add('nav-item');
                            document.getElementById('manageclasses').classList.remove('hidden');
                            document.getElementById('manageclasses').classList.add('nav-item');
                            document.getElementById('managemodules').classList.remove('hidden');
                            document.getElementById('managemodules').classList.add('nav-item');
                            document.getElementById('managestudents').classList.remove('hidden');
                            document.getElementById('managestudents').classList.add('nav-item');
                            document.getElementById('manageteachers').classList.remove('hidden');
                            document.getElementById('manageteachers').classList.add('nav-item');
                            document.getElementById('manageclassrooms').classList.remove('hidden');
                            document.getElementById('manageclassrooms').classList.add('nav-item');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                if (!IsPostBack)
                {
                    BindDataCourses();
                    BindDataModules();
                }
            }
        }

        protected void chkBoxMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnModuleId = (HiddenField)item.FindControl("hdnModuleID");
            HiddenField hdnModuleName = (HiddenField)item.FindControl("hdnModuleName");
            Label lbl_order = (Label)item.FindControl("lbl_order");

            if (hdnModuleId != null && hdnModuleName != null && lbl_order != null)
            {
                if (checkBox.Checked)
                {
                    lbl_order.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnModuleId.Value));
                    itemsNames.Add(hdnModuleName.Value);
                    lbl_selection.Text = string.Join(" | ", itemsNames);
                    ViewState["SelectedItems"] = selectedItems;
                    ViewState["SelectedItemsNames"] = itemsNames;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert();", true);
                }
                else
                {
                    lbl_order.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnModuleId.Value));
                        itemsNames.Remove(hdnModuleName.Value);
                        lbl_selection.Text = string.Join(" | ", itemsNames);
                        ViewState["SelectedItems"] = selectedItems;
                        ViewState["SelectedItemsNames"] = itemsNames;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert();", true);
                    }
                }
            }
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
            List<string> courseData = new List<string>();

            if (selectedItems != null && selectedItems.Count > 0)
            {
                courseData.Add(tbCourseName.Text);
                courseData.Add(ddlTipoCurso.SelectedValue);
                courseData.Add(ddlAreaCurso.SelectedValue);
                courseData.Add(tbRef.Text);
                string selectedValue = ddlQNQ.SelectedValue;
                string[] parts = selectedValue.Split(' '); // Split the selected value by space
                if (parts.Length == 2) // Ensure there are two parts
                {
                    string codQNQ = parts[1];
                    courseData.Add(codQNQ);
                }

                int CourseRegisted = Classes.Course.InsertCourse(courseData, selectedItems);

                if (CourseRegisted == 1)
                {
                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Curso registado com sucesso!";

                    // Clear the ViewState after processing
                    ViewState["SelectedItems"] = null;
                }
                else
                {
                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Curso já registado!";
                }
            }
        }

        protected void rpt_insertCourses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");

                // Create an AsyncPostBackTrigger for the CheckBox control
                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chkBoxMod.UniqueID;
                trigger.EventName = "CheckedChanged";

                // Add the trigger to the UpdatePanel's triggers collection
                updatePanelInsertCourses.Triggers.Add(trigger);
                // Attach an event handler for the CheckedChanged event
                chkBoxMod.CheckedChanged += chkBoxMod_CheckedChanged;
            }
        }

        protected void rpt_Courses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                RepeaterItem item = rpt_Courses.Items[e.Item.ItemIndex];

                // Find TextBox and Label controls
                TextBox tbNome = (TextBox)item.FindControl("tbNome");
                Label lblNome = (Label)item.FindControl("lblNome");

                TextBox tbCodRef = (TextBox)item.FindControl("tbCodRef");
                Label lblCodRef = (Label)item.FindControl("lblCodRef");

                TextBox tbCodQNQ = (TextBox)item.FindControl("tbCodQNQ");
                Label lblCodQNQ = (Label)item.FindControl("lblCodQNQ");

                // Toggle visibility
                tbNome.Visible = !tbNome.Visible;
                lblNome.Visible = !lblNome.Visible;

                tbCodRef.Visible = !tbCodRef.Visible;
                lblCodRef.Visible = !lblCodRef.Visible;

                tbCodQNQ.Visible = !tbCodQNQ.Visible;
                lblCodQNQ.Visible = !lblCodQNQ.Visible;

                // Find the buttons
                LinkButton lbt_edit = (LinkButton)item.FindControl("lbt_edit");
                AsyncPostBackTrigger triggerEdit = new AsyncPostBackTrigger();
                triggerEdit.ControlID = lbt_edit.UniqueID;
                triggerEdit.EventName = "Click";

                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbt_cancel");
                AsyncPostBackTrigger triggerCancel = new AsyncPostBackTrigger();
                triggerCancel.ControlID = lbt_cancel.UniqueID;
                triggerCancel.EventName = "Click";

                LinkButton lbt_delete = (LinkButton)item.FindControl("lbt_delete");
                AsyncPostBackTrigger triggerDelete = new AsyncPostBackTrigger();
                triggerDelete.ControlID = lbt_delete.UniqueID;
                triggerDelete.EventName = "Click";

                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbt_confirm");
                AsyncPostBackTrigger triggerConfirm = new AsyncPostBackTrigger();
                triggerConfirm.ControlID = lbt_confirm.UniqueID;
                triggerConfirm.EventName = "Click";

                // Show "Cancel" and "Confirm" buttons
                lbt_cancel.Visible = true;
                lbt_confirm.Visible = true;

                // Hide "Edit" and "Delete" buttons
                lbt_edit.Visible = false;
                lbt_delete.Visible = false;
            }
        }

        protected void btn_previousC_Click(object sender, EventArgs e)
        {
            PageNumberCourses -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }

        protected void btn_nextC_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }

        protected void btn_previousM_Click(object sender, EventArgs e)
        {
            PageNumberModules -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert();", true);
        }

        protected void btn_nextM_Click(object sender, EventArgs e)
        {
            PageNumberModules += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert();", true);
        }

        protected void UpdatePanel_Load(object sender, EventArgs e)
        {
            hfInsertCoursesVisible.Value = "true"; // or "false" based on your condition
        }

        private void BindDataCourses()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lbl_pageNumber.Text = (PageNumber).ToString();

            rpt_Courses.DataSource = pagedData;
            rpt_Courses.DataBind();

            btn_previousC.Enabled = !pagedData.IsFirstPage;
            btn_nextC.Enabled = !pagedData.IsLastPage;

        }

        private void BindDataModules()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberModules;
            int PageNumber = PageNumberCourses + 1;
            lbl_pageNumber.Text = (PageNumber).ToString();

            rpt_insertCourses.DataSource = pagedData;
            rpt_insertCourses.DataBind();

            btn_previousM.Enabled = !pagedData.IsFirstPage; // Adjust with the respective btn_previous control for Users Repeater
            btn_nextM.Enabled = !pagedData.IsLastPage; // Adjust with the respective btn_next control for Users Repeater

            rpt_EditModulesCourse.DataSource = pagedData;
            rpt_EditModulesCourse.DataBind();

            btn_previousEM.Enabled = !pagedData.IsFirstPage;
            btn_nextEM.Enabled = !pagedData.IsLastPage;

        }

        public int PageNumberCourses
        {
            get
            {
                if (ViewState["PageNumberCourses"] != null)
                    return Convert.ToInt32(ViewState["PageNumberCourses"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumberCourses"] = value;
            }
        }

        public int PageNumberModules
        {
            get
            {
                if (ViewState["PageNumberModules"] != null)
                    return Convert.ToInt32(ViewState["PageNumberModules"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumberModules"] = value;
            }
        }

        protected void rpt_EditModulesCourse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");

            //    chkBoxMod.CheckedChanged += chkBoxMod_CheckedChanged;
            //}
        }

        protected void lbt_cancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ManageCourses.aspx");
        }
    }
}