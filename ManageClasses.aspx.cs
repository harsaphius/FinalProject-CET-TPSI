using System;
using System.Collections.Generic;
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
            }


            if (!IsPostBack)
            {
                BindDataClassGroups();
                BindDataStudents();
                BindDdlModules("1");
            }

            if (listBoxTeachersForModules.Items.Count != 0)
                listBoxTeachersModules.Attributes.Remove("class");
            if (listBoxStudents.Items.Count != 0)
                listBoxStudentsForCourse.Attributes.Remove("class");

        }

        protected void rptStudents_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxStudentForClassGroup = (CheckBox)e.Item.FindControl("chkBoxStudentForClassGroup");

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chkBoxStudentForClassGroup.UniqueID;
                trigger.EventName = "CheckedChanged";

                updatePanelInsertClassGroup.Triggers.Add(trigger);
                chkBoxStudentForClassGroup.CheckedChanged += chkBoxStudentForClassGroup_OnCheckedChanged;

            }
        }

        protected void btnPreviousStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
        }

        protected void btnNextStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
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


        protected void ddlCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourseID = ddlCurso.SelectedValue;

            BindDdlModules(selectedCourseID);
        }


        private void BindDdlModules(string selectCourseID)
        {
            ddlTeacherForModules.ClearSelection();
            SQLDSModulesForCourse.SelectCommand =
                $"SELECT * FROM moduloCurso AS MC INNER JOIN curso AS C ON MC.codCurso=C.codCurso INNER JOIN modulo AS M ON MC.codModulo = M.codModulos WHERE MC.codCurso={selectCourseID}";
            ddlModulesOfCourse.DataSource = SQLDSModulesForCourse;
            ddlModulesOfCourse.DataTextField = "nomeModulos";
            ddlModulesOfCourse.DataValueField = "codModulo";
            ddlModulesOfCourse.DataBind();
        }

        private void BindDdlTeacherForModules(string selectedModuleID)
        {
            SQLDSTeachersForModules.SelectCommand =
                $"SELECT * FROM moduloFormador AS MT INNER JOIN formador AS T ON MT.codFormador=T.codFormador INNER JOIN inscricao AS I ON T.codInscricao=I.codInscricao INNER JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador INNER JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador INNER JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador WHERE MT.codModulo={selectedModuleID}";
            ddlTeacherForModules.DataSource = SQLDSTeachersForModules;
            ddlTeacherForModules.DataTextField = "nome";
            ddlTeacherForModules.DataValueField = "codFormador";
            ddlTeacherForModules.DataBind();
        }

        protected void ddlModulesOfCourse_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModuleID = ddlModulesOfCourse.SelectedValue;
            ddlTeacherForModules.ClearSelection();

            BindDdlTeacherForModules(selectedModuleID);
        }

        protected void chkBoxStudentForClassGroup_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnStudentForClassGroupID = (HiddenField)item.FindControl("hdnStudentForClassGroupID");
            HiddenField hdnStudentForClassGroupName = (HiddenField)item.FindControl("hdnStudentForClassGroupName");

            if (hdnStudentForClassGroupID != null && hdnStudentForClassGroupName != null)
            {
                int hdnStudentID = Convert.ToInt32(hdnStudentForClassGroupID.Value);
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"] ??
                                                       new Dictionary<int, bool>();
                ListItem newItem = new ListItem(hdnStudentForClassGroupName.Value, hdnStudentForClassGroupID.Value);

                if (checkBox.Checked)
                {
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnStudentForClassGroupID.Value));
                    itemsNames.Add(hdnStudentForClassGroupName.Value);
                    listBoxStudentsForCourse.Attributes.Remove("class");

                    listBoxStudents.Items.Add(newItem);
                    checkboxStates[hdnStudentID] = true;
                    ViewState["SelectedItemsInsert"] = selectedItems;
                    ViewState["SelectedItemsNamesInsert"] = itemsNames;

                }
                else
                {
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnStudentForClassGroupID.Value));
                        itemsNames.Remove(hdnStudentForClassGroupName.Value);
                        listBoxStudents.Items.Remove(newItem);

                        checkboxStates[hdnStudentID] = false;

                        ViewState["SelectedItemsInsert"] = selectedItems;
                        ViewState["SelectedItemsNamesInsert"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesInsert"] = checkboxStates;
            }
        }


        protected void btnAddTeacherModuleClassGroup_OnClick(object sender, EventArgs e)
        {
            if (ddlTeacherForModules.SelectedItem != null && ddlModulesOfCourse.SelectedItem != null)
            {
                listBoxTeachersModules.Attributes.Remove("class");
                listBoxTeachersForModules.CssClass = "";

                string moduleName = ddlModulesOfCourse.SelectedItem.Text;
                string teacherName = ddlTeacherForModules.SelectedItem.Text;

                string combinedName = moduleName + " | " + teacherName;

                listBoxTeachersForModules.Items.Add(new ListItem(combinedName, $"{ddlModulesOfCourse.SelectedValue} | {ddlTeacherForModules.SelectedValue}"));

                ddlModulesOfCourse.Items.RemoveAt(ddlModulesOfCourse.SelectedIndex);

            }
            else
            {
                lbl_message.Text = "Não pode adicionar módulos sem o respetivo formador!";
            }


        }

        protected void btnRemoveTeacherModuleClassGroup_OnClick(object sender, EventArgs e)
        {
            if (listBoxTeachersForModules.SelectedItem != null)
            {
                string selectedText = listBoxTeachersForModules.SelectedItem.Text;
                string selectedValue = listBoxTeachersForModules.SelectedValue;

                string[] partsText = selectedText.Split('|'); // Split the selected value by space
                string[] partsValues = selectedValue.Split('|');
                if (partsText.Length == 2 && partsValues.Length == 2) // Ensure there are two parts
                {
                    string nomeModulo = partsText[0].Trim();
                    string nomeFormador = partsText[1].Trim();

                    string codModulo = partsValues[0].Trim();
                    string codFormador = partsValues[1].Trim();


                    ddlModulesOfCourse.Items.Add(new ListItem(nomeModulo, codModulo));
                }

                listBoxTeachersForModules.Items.Remove(listBoxTeachersForModules.SelectedItem);


            }
            else
            {
                lbl_message.Text = "Seleccione na lista o item a remover!";
            }
        }
    }
}