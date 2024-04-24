using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageCourses : System.Web.UI.Page
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
                string script;
                string userCode = Session["CodUtilizador"].ToString();
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
                            document.getElementById('statistics').classList.remove('hidden');
                            
                            ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                    }
                    if (profileCode == 1)
                    {
                        script = @"
                            document.getElementById('manageusers').classList.remove('hidden');
                            
                            ";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowUsers", script, true);
                    }
                }
                if (!IsPostBack)
                {
                    ddlAreaCursoFilters.Items.Insert(0, new ListItem("Todas", "0"));
                    ddlTipoCursoFilters.Items.Insert(0, new ListItem("Todas", "0"));
                    ddlOrderFilters.Items.Insert(0, new ListItem("None", "0"));

                    //Inicializar ViewStates
                    if (ViewState["SelectedItemsEdit"] == null)
                        ViewState["SelectedItemsEdit"] = new List<int>();

                    if (ViewState["SelectedItemsInsert"] == null)
                        ViewState["SelectedItemsInsert"] = new List<int>();

                    if (ViewState["CheckboxStatesEdit"] == null)
                        ViewState["CheckboxStatesEdit"] = new Dictionary<int, bool>();

                    if (ViewState["CheckboxStatesInsert"] == null)
                        ViewState["CheckboxStatesInsert"] = new Dictionary<int, bool>();

                    if (ViewState["SelectedItemsNamesInsert"] == null)
                        ViewState["SelectedItemsNamesInsert"] = new List<string>();

                    if (ViewState["SelectedItemsNamesEdit"] == null)
                        ViewState["SelectedItemsNamesEdit"] = new List<string>();

                    BindDataCourses();
                    BindDataModules();

                    //Serialização dos Módulos para usar as suas propriedades no BindDataModulesEdit() 
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string modulesViewState = serializer.Serialize(Classes.Module.LoadModules(null, "codUFCD"));
                    ViewState["modulesViewState"] = modulesViewState;


                }
            }
        }


        //Funções de ItemDataBound dos Repeaters

        /// <summary>
        /// Função do Repeater ListCourses que anexa o respetivo AsynPostBackTrigger Event a cada botão EDit/Delete do Repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptListCourses_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the buttons
                LinkButton lbtEditEditCourse = (LinkButton)e.Item.FindControl("lbtEditEditCourse");
                AsyncPostBackTrigger triggerEdit = new AsyncPostBackTrigger();
                triggerEdit.ControlID = lbtEditEditCourse.UniqueID;
                triggerEdit.EventName = "Click";

                LinkButton lbtDeleteEditCourse = (LinkButton)e.Item.FindControl("lbtDeleteEditCourse");
                AsyncPostBackTrigger triggerDelete = new AsyncPostBackTrigger();
                triggerDelete.ControlID = lbtDeleteEditCourse.UniqueID;
                triggerDelete.EventName = "Click";

            }
        }

        /// <summary>
        /// Função do Repeater InsertCourses que anexa o respetivo AsyncPostBackTrigger Event às CheckBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptInsertCourses_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxInsertModulesCourse = (CheckBox)e.Item.FindControl("chkBoxInsertModulesCourse");

                // Create an AsyncPostBackTrigger for the CheckBox control
                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chkBoxInsertModulesCourse.UniqueID;
                trigger.EventName = "CheckedChanged";

                // Add the trigger to the UpdatePanel's triggers collection
                updatePanelInsertCourses.Triggers.Add(trigger);
                // Attach an event handler for the CheckedChanged event
                chkBoxInsertModulesCourse.CheckedChanged += chkBoxInsertModulesCourse_CheckedChanged;

            }

        }

        /// <summary>
        /// Função do Repeater EditCourses que anexa o respetivo AsyncPostBackTrigger Event às CheckBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptEditModulesCourse_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxEditModulesCourse = (CheckBox)e.Item.FindControl("chkBoxEditModulesCourse");

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chkBoxEditModulesCourse.UniqueID;
                trigger.EventName = "CheckedChanged";

                updatePanelListCourses.Triggers.Add(trigger);
                chkBoxEditModulesCourse.CheckedChanged += chkBoxEditModulesCourse_CheckedChanged;

            }
        }

        //Funções de ItemCommand dos Repeaters
        /// <summary>
        /// Função de ItemCommand do Repeater de Listagem dos Cursos para a Edição de um Curso
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptListCourses_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                editModulesCourse.Visible = true;
                listCoursesDiv.Visible = false;
                btnInsertCourseMain.Visible = false;
                btnBack.Visible = true;
                filtermenu.Visible = false;
                filtermenu.Visible = false;

                Course selectedCourse = Classes.Course.CompleteCourse(Convert.ToInt32(e.CommandArgument));

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string courseViewState = serializer.Serialize(selectedCourse);
                ViewState["SelectedCourse"] = courseViewState;

                //Binding Data dos Módulos Referentes ao Curso Seleccionado
                BindDataModulesEdit();

                //Atualização da Label com os Módulos do Curso Seleccionado
                List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"];
                if (itemsNames != null)
                    lblOrderOfModulesEditSelected.Text = string.Join(" | ", itemsNames);

                TextBox tbCourseNameEditCourse =
                    updatePanelEditModulesCourses.FindControl("tbCourseNameEditCourse") as TextBox;
                TextBox tbRefEditCourse = updatePanelEditModulesCourses.FindControl("tbRefEditCourse") as TextBox;
                DropDownList ddlTipoCursoEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlTipoCursoEditCourse");
                DropDownList ddlAreaCursoEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlAreaCursoEditCourse");
                DropDownList ddlQNQEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlQNQEditCourse");
                TextBox tbDuracaoEstagioEdit =
                    updatePanelEditModulesCourses.FindControl("tbDuracaoEstagioEdit") as TextBox;

                tbCourseNameEditCourse.Text = selectedCourse.Nome;

                string selectedCodTipoCurso = Convert.ToString(selectedCourse.CodTipoCurso);
                ddlTipoCursoEditCourse.SelectedValue = selectedCodTipoCurso;

                string selectedCodAreaCurso = Convert.ToString(selectedCourse.CodArea);
                ddlAreaCursoEditCourse.SelectedValue = selectedCodAreaCurso;

                tbRefEditCourse.Text = selectedCourse.CodRef;

                string selectedValue = Convert.ToString(selectedCourse.CodQNQ);
                ddlQNQEditCourse.SelectedValue = "Nível " + selectedValue;

                tbDuracaoEstagioEdit.Text = Convert.ToString(selectedCourse.DuracaoEstagio);

            }
            if (e.CommandName == "Delete")
            {
                int AnswCourseDeleted = Classes.Course.DeleteCourse(Convert.ToInt32(e.CommandArgument));

                if (AnswCourseDeleted == 1)
                {
                    BindDataCourses();

                    lblMessageListCourses.Visible = true;
                    lblMessageListCourses.CssClass = "alert alert-primary text-white text-center";
                    lblMessageListCourses.Text = "Curso apagado com sucesso!";
                    timerMessageListCourses.Enabled = true;
                }
                else
                {
                    lblMessageListCourses.Visible = true;
                    lblMessageListCourses.CssClass = "alert alert-primary text-white text-center";
                    lblMessageListCourses.Text = "Curso não pode ser eliminado por existir turmas com este curso!";
                    timerMessageListCourses.Enabled = true;
                }
            }
        }

        //Funções de Inserção/Edição

        /// <summary>
        /// Função para Inserção de um novo curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertCourse_Click(object sender, EventArgs e)
        {
            List<int> selectedItemsInsert = (List<int>)ViewState["SelectedItemsInsert"];
            Course courseData = new Course();

            if (selectedItemsInsert != null && selectedItemsInsert.Count > 0)
            {
                courseData.Nome = tbCourseName.Text;
                courseData.CodTipoCurso = Convert.ToInt32(ddlTipoCurso.SelectedValue);
                courseData.CodArea = Convert.ToInt32(ddlAreaCurso.SelectedValue);
                courseData.CodRef = tbRef.Text;
                if (string.IsNullOrEmpty(tbDuracaoEstagio.Text))
                    courseData.DuracaoEstagio = 0;
                else
                    courseData.DuracaoEstagio = Convert.ToInt32(tbDuracaoEstagio.Text);

                string selectedValue = ddlQNQ.SelectedValue;
                string[] parts = selectedValue.Split(' ');
                if (parts.Length == 2)
                {
                    string codQNQ = parts[1];
                    courseData.CodQNQ = Convert.ToInt32(codQNQ);
                }

                courseData.Duracao = Convert.ToInt32(lblDuracaoCurso.Text) + Convert.ToInt32(courseData.DuracaoEstagio);

                int CourseRegisted = Classes.Course.InsertCourse(courseData, selectedItemsInsert);

                if (CourseRegisted == 1)
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    lblMessageInsert.Text = "Curso registado com sucesso!";
                    timerMessageInsert.Enabled = true;
                }
                else
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    lblMessageInsert.Text = "Curso já registado!";
                    timerMessageInsert.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Função para a edição de um curso existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditCourse_OnClick(object sender, EventArgs e)
        {
            string courseViewState = ViewState["SelectedCourse"] as String;
            List<int> modulesSelected = new List<int>();

            if (!string.IsNullOrEmpty(courseViewState))
            {
                JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                Course selectedCourse = deSerializer.Deserialize<Course>(courseViewState);

                selectedCourse.Nome = tbCourseNameEditCourse.Text;
                selectedCourse.CodTipoCurso = Convert.ToInt32(ddlTipoCursoEditCourse.SelectedValue);
                selectedCourse.CodArea = Convert.ToInt32(ddlAreaCursoEditCourse.SelectedValue);
                selectedCourse.CodRef = tbRefEditCourse.Text;

                string selectedValue = ddlQNQEditCourse.SelectedValue;
                string[] parts = selectedValue.Split(' '); // Split the selected value by space
                if (parts.Length == 2) // Ensure there are two parts
                {
                    string codQNQ = parts[1];
                    selectedCourse.CodQNQ = Convert.ToInt32(codQNQ);
                }
                selectedCourse.Duracao = Convert.ToInt32(lblDuracaoCursoEdit.Text);
                if (string.IsNullOrEmpty(tbDuracaoEstagioEdit.Text))
                    selectedCourse.DuracaoEstagio = 0;
                else
                    selectedCourse.DuracaoEstagio = Convert.ToInt32(tbDuracaoEstagioEdit.Text);

                List<int> selectedItems = ViewState["SelectedItemsEdit"] as List<int>;

                if (selectedItems != null)
                {
                    modulesSelected.AddRange(selectedItems);
                }

                int AnswUpdateCourse = Classes.Course.UpdateCourse(selectedCourse, modulesSelected);

                if (AnswUpdateCourse == 1)
                {
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Curso atualizado com sucesso!";
                    timerMessageEdit.Enabled = true;
                }
                else
                {
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Não foi possível atualizar o curso selecionado. Já existem turmas com este curso!";
                    timerMessageEdit.Enabled = true;

                }

            }

        }

        //Funções de Databinding
        /// <summary>
        /// Função para DataBind dos Cursos
        /// </summary>
        private void BindDataCourses()
        {
            List<string> conditions = new List<string>();

            conditions.Add(string.IsNullOrEmpty(tbSearchFilters.Text) ? "" : tbSearchFilters.Text);
            conditions.Add(ddlAreaCursoFilters.SelectedValue == "0" ? "" : ddlAreaCursoFilters.SelectedValue);
            conditions.Add(ddlTipoCursoFilters.SelectedValue == "0" ? "" : ddlTipoCursoFilters.SelectedValue);
            string order = ddlOrderFilters.SelectedValue == "0" ? null : ddlOrderFilters.SelectedValue;

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses(conditions, order);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberListCourses.Text = PageNumber.ToString();

            rptListCourses.DataSource = pagedData;
            rptListCourses.DataBind();

            btnPreviousListCourses.Enabled = !pagedData.IsFirstPage;
            btnNextListCourses.Enabled = !pagedData.IsLastPage;
        }

        /// <summary>
        /// Função para DataBind dos Módulos
        /// </summary>
        private void BindDataModules()
        {
            List<string> conditions = new List<string>();
            string order = "codUFCD";

            conditions.Add(string.IsNullOrEmpty(tbSearchModules.Text) ? "" : tbSearchModules.Text);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules(conditions, order);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberModules;
            int PageNumber = PageNumberModules + 1;
            lblPageNumberInsertCourses.Text = PageNumber.ToString();

            rptInsertCourses.DataSource = pagedData;
            rptInsertCourses.DataBind();

            UpdateSelectedLabels();

            btnPreviousInsertCoursesModules.Enabled = !pagedData.IsFirstPage;
            btnNextInsertCoursesModules.Enabled = !pagedData.IsLastPage;

        }

        /// <summary>
        /// Função para DataBind dos Módulos na Edição de um Curso
        /// </summary>
        private void BindDataModulesEdit()
        {
            string modules = ViewState["modulesViewState"] as string;

            if (!string.IsNullOrEmpty(modules))
            {
                // Deserialize module information
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Module> allModules = serializer.Deserialize<List<Module>>(modules);

                string courseViewState = ViewState["SelectedCourse"] as string;
                string courseKey = "IsFirstEnteringEdit_" + courseViewState; // Append course code to the ViewState key

                bool isFirstEnteringEdit = ViewState[courseKey] == null || (bool)ViewState[courseKey];

                if (isFirstEnteringEdit) // Check if it's the first time the page is being loaded
                {
                    if (!string.IsNullOrEmpty(courseViewState))
                    {
                        JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                        Course selectedCourse = deSerializer.Deserialize<Course>(courseViewState);
                        List<Module> selectedModules = Classes.Module.LoadModules(selectedCourse.CodCurso);

                        // Update IsChecked property of each module based on whether it's selected or not
                        Dictionary<int, bool> checkboxStatesEdit = new Dictionary<int, bool>();
                        foreach (Module module in allModules)
                        {
                            module.IsChecked = selectedModules.Any(m => m.CodModulo == module.CodModulo);
                            checkboxStatesEdit.Add(module.CodModulo, module.IsChecked);
                        }

                        ViewState["CheckboxStatesEdit"] = checkboxStatesEdit;

                        // Update SelectedItemsEdit and SelectedItemsNamesEdit
                        List<int> selectedItems = new List<int>();
                        List<string> itemsNames = new List<string>();

                        foreach (Module module in selectedModules)
                        {
                            selectedItems.Add(module.CodModulo);
                            itemsNames.Add(module.Nome);
                        }

                        UpdateDurationLabel(selectedItems);

                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;

                        ViewState[courseKey] = false;

                    }
                }
                else // Not the first time entering, retrieve module information from ViewState
                {
                    if (!string.IsNullOrEmpty(courseViewState))
                    {
                        Dictionary<int, bool> checkboxStatesEdit = ViewState["CheckboxStatesEdit"] as Dictionary<int, bool>;
                        List<int> selectedItems = ViewState["SelectedItemsEdit"] as List<int>;
                        List<string> itemsNames = ViewState["SelectedItemsNamesEdit"] as List<string>;

                        foreach (Module module in allModules)
                        {
                            if (checkboxStatesEdit.ContainsKey(module.CodModulo))
                            {
                                module.IsChecked = checkboxStatesEdit[module.CodModulo];

                                // If the module is checked and not already in the selected items list, add it
                                if (module.IsChecked && !selectedItems.Contains(module.CodModulo))
                                {
                                    selectedItems.Add(module.CodModulo);
                                    itemsNames.Add(module.Nome);
                                }
                                // If the module is unchecked and already in the selected items list, remove it
                                else if (!module.IsChecked && selectedItems.Contains(module.CodModulo))
                                {
                                    int indexToRemove = selectedItems.IndexOf(module.CodModulo);
                                    selectedItems.RemoveAt(indexToRemove);
                                    itemsNames.RemoveAt(indexToRemove);
                                }
                            }
                        }

                        UpdateDurationLabel(selectedItems);
                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;
                    }
                }

                PagedDataSource pagedData = new PagedDataSource();
                pagedData.DataSource = allModules;
                pagedData.AllowPaging = true;
                pagedData.PageSize = 8;
                pagedData.CurrentPageIndex = PageNumberModules;
                int PageNumber = PageNumberModules + 1;
                lblPageNumberEditCoursesModules.Text = PageNumber.ToString();

                rptEditModulesCourse.DataSource = pagedData;
                rptEditModulesCourse.DataBind();

                UpdateSelectedLabels();

                btnPreviousEditModulesCourses.Enabled = !pagedData.IsFirstPage;
                btnNextEditModulesCourses.Enabled = !pagedData.IsLastPage;
            }
        }

        //Funções para as CheckBoxes
        /// <summary>
        /// Função para determinar se a CheckBox do Repeater InsertModulesCourse is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkBoxInsertModulesCourse_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnInsertModuleID = (HiddenField)item.FindControl("hdnInsertModuleID");
            HiddenField hdnInsertModuleName = (HiddenField)item.FindControl("hdnInsertModuleName");
            Label lblOrderInsertModules = (Label)item.FindControl("lblOrderInsertModules");

            if (hdnInsertModuleID != null && hdnInsertModuleName != null && lblOrderInsertModules != null)
            {
                int moduleID = Convert.ToInt32(hdnInsertModuleID.Value);
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"];

                if (checkBox.Checked)
                {
                    lblOrderInsertModules.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"];
                    selectedItems.Add(Convert.ToInt32(hdnInsertModuleID.Value));
                    itemsNames.Add(hdnInsertModuleName.Value);
                    lblOrderOfModulesInsertedSelected.Text = string.Join(" | ", itemsNames);
                    checkboxStates[moduleID] = true;
                    UpdateDurationLabel(selectedItems);

                    ViewState["SelectedItemsInsert"] = selectedItems;
                    ViewState["SelectedItemsNamesInsert"] = itemsNames;
                }
                else
                {
                    lblOrderInsertModules.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnInsertModuleID.Value));
                        itemsNames.Remove(hdnInsertModuleName.Value);
                        lblOrderOfModulesInsertedSelected.Text = string.Join(" | ", itemsNames);
                        checkboxStates[moduleID] = false;
                        UpdateDurationLabel(selectedItems);

                        ViewState["SelectedItemsInsert"] = selectedItems;
                        ViewState["SelectedItemsNamesInsert"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesInsert"] = checkboxStates;

            }
        }


        /// <summary>
        /// Função para determinar se a CheckBox do Repeater EditModulesCourse is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkBoxEditModulesCourse_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnEditCourseModuleID");
            HiddenField hdnEditCourseModuleName = (HiddenField)item.FindControl("hdnEditCourseModuleName");
            Label lblOrderEditModulesCourse = (Label)item.FindControl("lblOrderEditModulesCourse");

            if (hdnEditCourseModuleID != null && hdnEditCourseModuleName != null && lblOrderEditModulesCourse != null)
            {
                int moduleID = Convert.ToInt32(hdnEditCourseModuleID.Value);
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesEdit"];

                if (checkBox.Checked)
                {
                    lblOrderEditModulesCourse.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"];
                    selectedItems.Add(Convert.ToInt32(hdnEditCourseModuleID.Value));
                    itemsNames.Add(hdnEditCourseModuleName.Value);
                    lblOrderOfModulesEditSelected.Text = string.Join(" | ", itemsNames);
                    checkboxStates[moduleID] = true;
                    UpdateDurationLabel(selectedItems);

                    ViewState["SelectedItemsEdit"] = selectedItems;
                    ViewState["SelectedItemsNamesEdit"] = itemsNames;
                }
                else
                {
                    lblOrderEditModulesCourse.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnEditCourseModuleID.Value));
                        itemsNames.Remove(hdnEditCourseModuleName.Value);
                        lblOrderOfModulesEditSelected.Text = string.Join(" | ", itemsNames);
                        checkboxStates[moduleID] = false;
                        UpdateDurationLabel(selectedItems);

                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesEdit"] = checkboxStates;
                UpdateSelectedLabels();

            }
        }

        //Funções para Update de ViewStates
        /// <summary>
        /// Função para update de ViewState ao mudar de paginação no Repeater de InsertCourse
        /// </summary>
        private void UpdateSelectedCheckBoxesInsert()
        {
            Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"];

            foreach (RepeaterItem item in rptInsertCourses.Items)
            {
                CheckBox chkBoxInsertModulesCourse = (CheckBox)item.FindControl("chkBoxInsertModulesCourse");
                HiddenField hdnInsertModuleID = (HiddenField)item.FindControl("hdnInsertModuleID");

                if (chkBoxInsertModulesCourse != null && hdnInsertModuleID != null)
                {
                    if (checkboxStates.ContainsKey(Convert.ToInt32(hdnInsertModuleID.Value)))
                    {
                        chkBoxInsertModulesCourse.Checked = checkboxStates[Convert.ToInt32(hdnInsertModuleID.Value)];
                    }
                }
            }

            ViewState["CheckboxStatesInsert"] = checkboxStates;

            UpdateSelectedLabels();
        }

        /// <summary>
        /// Função para update de ViewState ao mudar de paginação no Repeater de EditCourse
        /// </summary>
        private void UpdateSelectedLabels()
        {
            // Loop through the Repeater items to find selected items in rptEditModulesCourse
            foreach (RepeaterItem item in rptEditModulesCourse.Items)
            {
                CheckBox chkBoxEditModulesCourse = (CheckBox)item.FindControl("chkBoxEditModulesCourse");
                HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnEditCourseModuleID");
                Label lblOrderEditModulesCourse = (Label)item.FindControl("lblOrderEditModulesCourse");

                if (chkBoxEditModulesCourse != null && hdnEditCourseModuleID != null && lblOrderEditModulesCourse != null)
                {
                    int moduleID = Convert.ToInt32(hdnEditCourseModuleID.Value);

                    // Update label text based on checkbox state
                    lblOrderEditModulesCourse.Text = chkBoxEditModulesCourse.Checked ? "Seleccionado" : "Selecione este módulo";
                }
            }

            // Loop through the Repeater items to find selected items in rptInsertCourses
            foreach (RepeaterItem item in rptInsertCourses.Items)
            {
                CheckBox chkBoxInsertModulesCourse = (CheckBox)item.FindControl("chkBoxInsertModulesCourse");
                HiddenField hdnInsertModuleID = (HiddenField)item.FindControl("hdnInsertModuleID");
                Label lblOrderInsertModules = (Label)item.FindControl("lblOrderInsertModules");

                if (chkBoxInsertModulesCourse != null && hdnInsertModuleID != null && lblOrderInsertModules != null)
                {
                    int moduleID = Convert.ToInt32(hdnInsertModuleID.Value);

                    // Update label text based on checkbox state
                    lblOrderInsertModules.Text = chkBoxInsertModulesCourse.Checked ? "Seleccionado" : "Selecione este módulo";
                }
            }
        }

        private void UpdateDurationLabel(List<int> selectedItems)
        {
            int totalDuration = Classes.Module.CalculateTotalCourseDuration(selectedItems);

            lblDuracaoCurso.Text = totalDuration.ToString();
            lblDuracaoCursoEdit.Text = totalDuration.ToString();
        }

        //Funções de paginação

        /// <summary>
        /// Paginação dos Cursos
        /// </summary>
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

        /// <summary>
        /// Paginação dos Módulos
        /// </summary>
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

        //Funções para os botões de paginação

        /// <summary>
        /// Função Click do Botão de Previous na Listagem de Cursos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousListCourses_Click(object sender, EventArgs e)
        {
            PageNumberCourses -= 1;
            BindDataCourses();
        }

        /// <summary>
        /// Função Click do Botão de Next na Listagem de Cursos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextListCourses_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1;
            BindDataCourses();

        }

        /// <summary>
        /// Função Click do Botão de Previous na Inserção de Módulos de um Novo Curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousInsertCoursesModules_Click(object sender, EventArgs e)
        {
            PageNumberModules -= 1;
            BindDataModules();
            UpdateSelectedCheckBoxesInsert();
            UpdateSelectedLabels();

        }

        /// <summary>
        /// Função Click do Botão de Next na Inserção de Módulos de um Novo Curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextInsertCoursesModules_Click(object sender, EventArgs e)
        {
            PageNumberModules += 1;
            BindDataModules();
            UpdateSelectedCheckBoxesInsert();
            UpdateSelectedLabels();

        }

        /// <summary>
        /// Função Click do Botão de Previous na Edição de Módulos de um Novo Curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousEditModulesCourses_OnClick(object sender, EventArgs e)
        {
            PageNumberModules -= 1;
            BindDataModulesEdit();
            UpdateSelectedLabels();
        }

        /// <summary>
        /// Função Click do Botão de Next na Edição de Módulos de um Novo Curso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextEditModulesCourses_OnClick(object sender, EventArgs e)
        {
            PageNumberModules += 1;
            BindDataModulesEdit();
            UpdateSelectedLabels();
        }


        /// <summary>
        /// Função Click do Botão de Aplicar os Filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApplyFilters_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses = 0;

            BindDataCourses();
        }

        /// <summary>
        /// Função Click do Botão de Limpar os filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearFilters_OnClick(object sender, EventArgs e)
        {
            tbSearchFilters.Text = "";
            ddlAreaCursoFilters.SelectedIndex = 0;
            ddlTipoCursoFilters.SelectedIndex = 0;
            ddlOrderFilters.SelectedIndex = 0;

            PageNumberCourses = 0;

            BindDataCourses();

        }

        protected void timerMessageInsert_OnTick(object sender, EventArgs e)
        {
            lblMessageInsert.Visible = false;
            timerMessageInsert.Enabled = false;

            Response.Redirect("ManageCourses.aspx");
        }

        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            listCoursesDiv.Visible = true;
            insertCoursesDiv.Visible = false;
            btnBack.Visible = false;
            btnInsertCourseMain.Visible = true;
            editModulesCourse.Visible = false;
            filtermenu.Visible = true;

            lblDuracaoCurso.Text = "";
        }

        protected void timerMessageListCourses_OnTick(object sender, EventArgs e)
        {
            lblMessageListCourses.Visible = false;
            timerMessageListCourses.Enabled = false;
        }

        protected void filtermenu_OnClick(object sender, EventArgs e)
        {
            filters.Visible = !filters.Visible;
        }

        protected void btnInsertCourseMain_OnClick(object sender, EventArgs e)
        {
            insertCoursesDiv.Visible = true;
            listCoursesDiv.Visible = false;
            btnBack.Visible = true;
            btnInsertCourseMain.Visible = false;
            filtermenu.Visible = false;
            filters.Visible = false;
        }

        protected void lbtnSearch_OnClick(object sender, EventArgs e)
        {
            BindDataModules();
        }
    }
}