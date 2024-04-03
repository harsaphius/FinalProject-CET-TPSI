﻿using FinalProject.Classes;
using System;
using System.Collections.Generic;
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
                var script = @"
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

                    if (ViewState["SelectedItemsEdit"] == null)
                        ViewState["SelectedItemsEdit"] = new List<int>();

                    if (ViewState["CheckboxStatesEdit"] == null)
                        ViewState["CheckboxStatesEdit"] = new Dictionary<int, bool>();

                    if (ViewState["CheckboxStates"] == null)
                        ViewState["CheckboxStates"] = new Dictionary<int, bool>();

                    //Reinicializar o FlatPickr
                    InitializeFlatpickrDatePickers();

                    BindDataCourses();
                    BindDataModules();

                    rptEditModulesCourse.DataSource = Classes.Module.LoadModules();
                    rptEditModulesCourse.DataBind();

                }

                InitializeFlatpickrDatePickers();
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

                updatePanelInsertCourses.Triggers.Add(trigger);
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
                Dictionary<int, bool> checkboxStates =
                    ViewState["CheckboxStatesEdit"] as Dictionary<int, bool>;
                Course selectedCourse = Classes.Course.CompleteCourse(Convert.ToInt32(e.CommandArgument));

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string courseViewState = serializer.Serialize(selectedCourse);
                ViewState["SelectedCourse"] = courseViewState;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditModulesScript",
                    "showEditModules();", true);

                TextBox tbCourseNameEditCourse =
                    updatePanelEditModulesCourses.FindControl("tbCourseNameEditCourse") as TextBox;
                TextBox tbRefEditCourse = updatePanelEditModulesCourses.FindControl("tbRefEditCourse") as TextBox;
                DropDownList ddlTipoCursoEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlTipoCursoEditCourse");
                DropDownList ddlAreaCursoEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlAreaCursoEditCourse");
                DropDownList ddlQNQEditCourse =
                    (DropDownList)updatePanelEditModulesCourses.FindControl("ddlQNQEditCourse");

                tbCourseNameEditCourse.Text = selectedCourse.Nome;

                string selectedCodTipoCurso = Convert.ToString(selectedCourse.CodTipoCurso);
                ddlTipoCursoEditCourse.SelectedValue = selectedCodTipoCurso;

                string selectedCodAreaCurso = Convert.ToString(selectedCourse.CodArea);
                ddlAreaCursoEditCourse.SelectedValue = selectedCodAreaCurso;

                tbRefEditCourse.Text = selectedCourse.CodRef;

                string selectedValue = Convert.ToString(selectedCourse.CodQNQ);
                ddlQNQEditCourse.SelectedValue = "Nível " + selectedValue;

                foreach (RepeaterItem item in rptEditModulesCourse.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField hdnFieldEditCourseModuleID = (HiddenField)item.FindControl("hdnEditCourseModuleID");
                        Label lblOrderEditModulesCourse = (Label)item.FindControl("lblOrderEditModulesCourse");

                        if (hdnFieldEditCourseModuleID != null)
                        {
                            if (int.TryParse(hdnFieldEditCourseModuleID.Value, out int moduleID))
                            {
                                CheckBox chkBoxEditModulesCourse =
                                    (CheckBox)item.FindControl("chkBoxEditModulesCourse");

                                bool isModuleInCourse = CheckIfModuleIsInCourse(selectedCourse.CodCurso, moduleID);

                                chkBoxEditModulesCourse.Checked = isModuleInCourse;

                                if (checkboxStates != null)
                                {
                                    checkboxStates[moduleID] = isModuleInCourse;
                                    lblOrderEditModulesCourse.Text = checkboxStates[moduleID]
                                        ? "Seleccionado"
                                        : "Selecione este módulo";

                                    ViewState["CheckboxStatesEdit"] = checkboxStates;
                                }
                            }
                        }
                    }
                }

                BindDataModulesEdit();

            }
            if (e.CommandName == "Delete")
            {

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

                string selectedValue = ddlQNQ.SelectedValue;
                string[] parts = selectedValue.Split(' '); // Split the selected value by space
                if (parts.Length == 2) // Ensure there are two parts
                {
                    string codQNQ = parts[1];
                    courseData.CodQNQ = Convert.ToInt32(codQNQ);
                }

                int CourseRegisted = Classes.Course.InsertCourse(courseData, selectedItemsInsert);

                if (CourseRegisted == 1)
                {
                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Curso registado com sucesso!";

                    ViewState["SelectedItemsInsert"] = null;
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

        /// <summary>
        /// Função para a edição de um curso existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditCourse_OnClick(object sender, EventArgs e)
        {
            string courseViewState = ViewState["SelectedCourse"] as String;

            List<int> modulesSelected = (List<int>)ViewState["SelectedItemsEdit"];
            if (!string.IsNullOrEmpty(courseViewState))
            {
                JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                Course selelectCourse = deSerializer.Deserialize<Course>(courseViewState);

                int AnswUpdateCourse = Classes.Course.UpdateCourse(selelectCourse, modulesSelected);

                if (AnswUpdateCourse == 1)
                {
                    lblMessageForEdit.Text = "Curso atualizado com sucesso!";
                }
                else
                {
                    lblMessageForEdit.Text = "Não foi possível atualizar o curso selecionado. Já existem turmas com este curso!";
                }

            }


            Response.AddHeader("REFRESH", "3; URL=ManageCourses.aspx");
        }

        //Funções de Databinding

        /// <summary>
        /// Função para DataBind dos Cursos
        /// </summary>
        private void BindDataCourses(List<string> conditions = null)
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses(conditions);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberListCourses.Text = (PageNumber).ToString();

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
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberModules;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberEditCoursesModules.Text = (PageNumber).ToString();

            rptInsertCourses.DataSource = pagedData;
            rptInsertCourses.DataBind();

            btnPreviousInsertCoursesModules.Enabled = !pagedData.IsFirstPage;
            btnNextInsertCoursesModules.Enabled = !pagedData.IsLastPage;

        }

        private void BindDataModulesEdit()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberModules;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberEditCoursesModules.Text = (PageNumber).ToString();

            rptEditModulesCourse.DataSource = pagedData;
            rptEditModulesCourse.DataBind();

            btnPreviousEditModulesCourses.Enabled = !pagedData.IsFirstPage;
            btnNextEditModulesCourses.Enabled = !pagedData.IsLastPage;

        }

        //Funções para as CheckBoxes

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
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesEdit"] ?? new Dictionary<int, bool>();

                if (checkBox.Checked)
                {
                    lblOrderEditModulesCourse.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnEditCourseModuleID.Value));
                    itemsNames.Add(hdnEditCourseModuleName.Value);
                    lblOrderOfModulesEditSelected.Text = string.Join(" | ", itemsNames);
                    checkboxStates[moduleID] = true;

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

                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesEdit"] = checkboxStates;

            }
        }

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
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"] ?? new Dictionary<int, bool>();

                if (checkBox.Checked)
                {
                    lblOrderInsertModules.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnInsertModuleID.Value));
                    itemsNames.Add(hdnInsertModuleName.Value);
                    lblOrderOfModulesInsertedSelected.Text = string.Join(" | ", itemsNames);
                    checkboxStates[moduleID] = true;
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
                        ViewState["SelectedItemsInsert"] = selectedItems;
                        ViewState["SelectedItemsNamesInsert"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesInsert"] = checkboxStates;

            }
        }

        /// <summary>
        /// Função para verificar se o módulo pertence ao curso
        /// </summary>
        /// <param name="CodCurso"></param>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        private static bool CheckIfModuleIsInCourse(int CodCurso, int moduleID)
        {
            Course completeCourse = Classes.Course.CompleteCourse(CodCurso);

            foreach (Module module in completeCourse.Modules)
            {
                if (module.CodModulo == moduleID)
                {
                    return true;
                }
            }
            return false;
        }

        //Funções para Update de ViewStates

        /// <summary>
        /// Função para update de ViewState ao mudar de paginação no Repeater de InsertCourse
        /// </summary>
        private void UpdateSelectedItemsViewStateInsert()
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"] ?? new List<int>();
            Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"];

            // Loop through the Repeater items to find selected items
            foreach (RepeaterItem item in rptInsertCourses.Items)
            {
                CheckBox chkBoxInsertModulesCourse = (CheckBox)item.FindControl("chkBoxInsertModulesCourse");
                HiddenField hdnInsertModuleID = (HiddenField)item.FindControl("hdnInsertModuleID");
                Label lblOrderInsertModules = (Label)item.FindControl("lblOrderInsertModules");

                if (chkBoxInsertModulesCourse != null && hdnInsertModuleID != null)
                {
                    int moduleID = Convert.ToInt32(hdnInsertModuleID.Value);

                    if (checkboxStates.ContainsKey(moduleID))
                    {
                        chkBoxInsertModulesCourse.Checked = checkboxStates[moduleID];
                        lblOrderInsertModules.Text = checkboxStates[moduleID] ? "Seleccionado" : "Selecione este módulo";
                    }

                    if (chkBoxInsertModulesCourse.Checked)
                    {
                        selectedItems.Add(moduleID);
                    }
                }
            }
            ViewState["SelectedItemsInsert"] = selectedItems;
        }

        /// <summary>
        /// Função para update de ViewState ao mudar de paginação no Repeater de EditCourse
        /// </summary>
        private void UpdateSelectedItemsViewStateEdit()
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"] ?? new List<int>();
            Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesEdit"];

            // Loop through the Repeater items to find selected items
            foreach (RepeaterItem item in rptEditModulesCourse.Items)
            {
                CheckBox chkBoxEditModulesCourse = (CheckBox)item.FindControl("chkBoxEditModulesCourse");
                HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnEditCourseModuleID");
                Label lblOrderEditModulesCourse = (Label)item.FindControl("lblOrderEditModulesCourse");

                if (chkBoxEditModulesCourse != null && hdnEditCourseModuleID != null && lblOrderEditModulesCourse != null)
                {
                    int moduleID = Convert.ToInt32(hdnEditCourseModuleID.Value);

                    if (checkboxStates.ContainsKey(moduleID))
                    {
                        chkBoxEditModulesCourse.Checked = checkboxStates[moduleID];
                        lblOrderEditModulesCourse.Text = checkboxStates[moduleID] ? "Seleccionado" : "Selecione este módulo";
                    }

                    if (chkBoxEditModulesCourse.Checked)
                    {
                        selectedItems.Add(moduleID);
                    }
                }
            }

            ViewState["SelectedItemsEdit"] = selectedItems;
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
            UpdateSelectedItemsViewStateInsert();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert(); return false;", true);
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
            UpdateSelectedItemsViewStateInsert();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsertScript", "showInsert(); return false;", true);
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
            UpdateSelectedItemsViewStateEdit();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditScript", "showEditModules(); return false;", true);
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
            UpdateSelectedItemsViewStateEdit();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "showEditScript", "showEditModules(); return false;", true);
        }

        /// <summary>
        /// Função de inicialização do Flatpickr
        /// </summary>
        private void InitializeFlatpickrDatePickers()
        {
            string script = @"
                        <script>
                            document.addEventListener('DOMContentLoaded', function() {
                                flatpickr('#" + tbDataInicioFilters.ClientID + @"', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    maxDate: new Date()
                                });

                                flatpickr('#" + tbDataFimFilters.ClientID + @"', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    minDate: new Date()
                                });
                            });
                        </script>
                    ";

            ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", script, false);
        }

        /// <summary>
        /// Função Click do Botão de Aplicar os Filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApplyFilters_OnClick(object sender, EventArgs e)
        {
            List<string> conditions = new List<string>();
            conditions.Add(tbSearchFilters.Text);
            conditions.Add(ddlAreaCursoFilters.SelectedValue);
            conditions.Add(ddlTipoCursoFilters.SelectedValue);
            conditions.Add(tbDataInicioFilters.Text);
            conditions.Add(tbDataFimFilters.Text);

            BindDataCourses(conditions);
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
            tbDataInicioFilters.Text = "";
            tbDataFimFilters.Text = "";

            BindDataCourses();

        }
    }
}