using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageTeachers : System.Web.UI.Page
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
            }

            //Reinicializar o FlatPickr
            //if (IsPostBack)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", @"
            //            <script>
            //                document.addEventListener('DOMContentLoaded', function() {
            //                    flatpickr('#<%= tbDataNascimento.ClientID %>', {
            //                        dateFormat: 'd-m-Y',
            //                        theme: 'light',
            //                        maxDate: new Date()
            //                    });

            //                    flatpickr('#<%= tbDataValidade.ClientID %>', {
            //                        dateFormat: 'd-m-Y',
            //                        theme: 'light',
            //                        minDate: new Date()
            //                    });
            //                });
            //            </script>
            //        ", false);
            //}

            if (!IsPostBack)
            {
                //Inicializar ViewStates
                InitializeViewState();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string modulesViewState = serializer.Serialize(Classes.Module.LoadModules(null, "codUFCD"));
                ViewState["modulesViewState"] = modulesViewState;

                BindDataTeachers();
                BindDataUsers();
            }
        }


        //Funções de ItemDataBound dos Repeaters


        //Funções de ItemCommand dos Repeaters
        protected void rptUserForTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditModulesUser")
            {
                listUsersDiv.Visible = false;
                ModulesRegisterForTeacher.Visible = true;
                btnInsertTeacherFromList.Visible = false;
                btnInsertTeacherMain.Visible = false;
                btnBack.Visible = true;

                filtermenu.Visible = false;
                filters.Visible = false;

                hdnSourceDiv.Value = "listUsersDiv";

                User user = Classes.User.LoadUser(null, e.CommandArgument.ToString());

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string teacherViewState = serializer.Serialize(user);
                ViewState["SelectedTeacher"] = teacherViewState;

                BindDataModules();

                Session["FromUser"] = "true";
                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }
        }

        protected void rptListModulesForTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chckBoxModules = (CheckBox)e.Item.FindControl("chckBoxModules");

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chckBoxModules.UniqueID;
                trigger.EventName = "CheckedChanged";

                updatePanelModulesForTeachers.Triggers.Add(trigger);
                chckBoxModules.CheckedChanged += chckBoxModules_CheckedChanged;
            }
        }

        protected void rptListTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //Mostrar/Esconder Divs
                insertTeachersDiv.Visible = true;
                listTeachersDiv.Visible = false;
                btnInsertTeacherMain.Visible = false;
                btnInsertTeacherFromList.Visible = false;
                btnBack.Visible = true;
                filtermenu.Visible = false;
                filtermenu.Visible = false;

                hdnSourceDiv.Value = "listTeachersDiv";

                (Teacher teacher, User user) = Classes.Teacher.LoadTeacher(Convert.ToInt32(e.CommandArgument));

                tbNome.Text = teacher.Nome;
                ddlSexo.SelectedValue = Convert.ToString(user.Sexo);
                tbDataNascimento.Text = user.DataNascimento.ToShortDateString();
                ddlDocumentoIdent.SelectedValue = Convert.ToString(user.CodTipoDoc);
                tbCC.Text = user.DocIdent;
                tbDataValidade.Text = user.DataValidade.ToShortDateString();
                tbNrSegSocial.Text = user.NrSegSocial;
                tbNIF.Text = user.NIF;
                tbMorada.Text = user.Morada;
                tbCodPostal.Text = user.CodPostal;
                tbLocalidade.Text = user.Localidade;
                ddlCodPais.SelectedValue = Convert.ToString(user.CodPais);

                ddlCodEstadoCivil.SelectedValue = Convert.ToString(user.CodEstadoCivil);
                tbIBAN.Text = user.IBAN;
                tbNaturalidade.Text = user.Naturalidade;
                ddlCodNacionalidade.SelectedValue = Convert.ToString(user.CodNacionalidade);
                ddlCodGrauAcademico.SelectedValue = Convert.ToString(user.CodGrauAcademico);
                ddlPrefixo.SelectedValue = Convert.ToString(user.CodPrefix);
                tbTelemovel.Text = user.Phone;
                tbEmail.Text = user.Email;

                ddlCodGrauAcademico.SelectedValue = Convert.ToString(user.CodGrauAcademico);

                //HttpPostedFile photoFile = fuFoto.PostedFile;

                //byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                //user.Foto = (Convert.ToBase64String(photoBytes));

                //List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);

                //int CompleteUser = Classes.User.CompleteRegisterUser(user, uploadedFiles);

                //if (CompleteUser == 0) lblMessageRegistration.Text = "Perfil atualizado com sucesso.";
                //else lblMessageRegistration.Text = "Erro na atualização de perfil.";

            }

            if (e.CommandName == "EditModulesTeachers")
            {
                listTeachersDiv.Visible = false;
                ModulesRegisterForTeacher.Visible = true;
                btnInsertTeacherFromList.Visible = false;
                btnInsertTeacherMain.Visible = false;
                btnBack.Visible = true;

                filtermenu.Visible = false;
                filters.Visible = false;

                hdnSourceDiv.Value = "listTeachersDiv";

                (Teacher teacher, User user) = Classes.Teacher.LoadTeacher(Convert.ToInt32(e.CommandArgument));

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string teacherViewState = serializer.Serialize(teacher);
                ViewState["SelectedTeacher"] = teacherViewState;

                BindDataModules();

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }

            if (e.CommandName == "Delete")
            {
                int AnswTeacherDeleted = Classes.Teacher.DeleteTeacher(Convert.ToInt32(e.CommandArgument));

                if (AnswTeacherDeleted == 1)
                {
                    BindDataTeachers();
                    lblMessage.Visible = true;
                    lblMessage.CssClass = "alert alert-primary text-white text-center";
                    lblMessage.Text = "Formador apagado com sucesso!";
                    timerMessage.Enabled = true;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.CssClass = "alert alert-primary text-white text-center";
                    lblMessage.Text = "Formador não pode ser eliminado por fazer parte de uma turma a decorrer ou estar inscrito para leccionar módulos!";
                    timerMessage.Enabled = true;
                }
            }

            if (e.CommandName == "Schedule")
            {
                Session["CodFormador"] = e.CommandArgument.ToString();
                Response.Redirect("TeacherAvailability.aspx");
            }
        }


        //Funções de DataBinding
        private void BindDataUsers()
        {
            string condition =
                "WITH UserProfiles AS (SELECT U.codUtilizador, UP.codPerfil, ROW_NUMBER() OVER (PARTITION BY U.codUtilizador ORDER BY UP.codPerfil) AS RowNumber FROM utilizador AS U LEFT JOIN utilizadorPerfil AS UP ON U.codUtilizador = UP.codUtilizador WHERE UP.codPerfil != 3 AND U.ativo = 1) SELECT U.*, UD.*, UDS.*, UP.* FROM UserProfiles AS UP LEFT JOIN utilizador AS U ON UP.codUtilizador = U.codUtilizador LEFT JOIN utilizadorData AS UD ON U.codUtilizador = UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador = UDS.codUtilizador WHERE UP.RowNumber = 1 AND U.codUtilizador NOT IN (SELECT codFormador FROM formador);";

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers(null, condition);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberUsers;
            int PageNumber = PageNumberUsers + 1;
            lblPageNumberUsersForTeachers.Text = PageNumber.ToString();

            rptUserForTeachers.DataSource = pagedData;
            rptUserForTeachers.DataBind();

            btnPreviousUsersForTeachers.Enabled = !pagedData.IsFirstPage;
            btnNextsUsersForTeachers.Enabled = !pagedData.IsLastPage;
        }

        private void BindDataModules()
        {
            string modules = ViewState["modulesViewState"] as string;

            if (!string.IsNullOrEmpty(modules))
            {
                // Deserialize module information
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Module> allModules = serializer.Deserialize<List<Module>>(modules);

                string teacherViewState = ViewState["SelectedTeacher"] as string;
                string courseKey = "IsFirstEnteringEdit_" + teacherViewState; // Append course code to the ViewState key

                bool isFirstEnteringEdit = ViewState[courseKey] == null || (bool)ViewState[courseKey];

                if (isFirstEnteringEdit) // Check if it's the first time the page is being loaded
                {
                    if (!string.IsNullOrEmpty(teacherViewState))
                    {
                        JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                        Teacher selectedTeacher = deSerializer.Deserialize<Teacher>(teacherViewState);
                        List<Module> selectedModules = selectedTeacher.Modules;

                        // Update IsChecked property of each module based on whether it's selected or not
                        Dictionary<int, bool> checkboxStatesEdit = new Dictionary<int, bool>();
                        if (selectedModules != null && selectedModules.Any())
                        {
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

                            ViewState["SelectedItemsEdit"] = selectedItems;
                            ViewState["SelectedItemsNamesEdit"] = itemsNames;

                            ViewState[courseKey] = false;
                        }

                    }
                }
                else // Not the first time entering, retrieve module information from ViewState
                {
                    if (!string.IsNullOrEmpty(teacherViewState))
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
                lblPageNumberListModulesForTeachers.Text = PageNumber.ToString();

                rptListModulesForTeachers.DataSource = pagedData;
                rptListModulesForTeachers.DataBind();

                UpdateSelectedLabels();

                btnPreviousListModulesForTeachers.Enabled = !pagedData.IsFirstPage;
                btnNextListModulesForTeachers.Enabled = !pagedData.IsLastPage;
            }
        }

        private void BindDataTeachers()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Teacher.LoadTeachers();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberTeachers;
            int PageNumber = PageNumberTeachers + 1;
            lblPageNumberListTeachers.Text = PageNumber.ToString();

            rptListTeachers.DataSource = pagedData;
            rptListTeachers.DataBind();

            btnPreviousListTeachers.Enabled = !pagedData.IsFirstPage;
            btnNextListTeachers.Enabled = !pagedData.IsLastPage;
        }

        //Funções de Paginação
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

        private int PageNumberTeachers
        {
            get
            {
                if (ViewState["PageNumberTeachers"] != null)
                    return Convert.ToInt32(ViewState["PageNumberTeachers"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberTeachers"] = value;
        }


        //Funções para as CheckBoxes

        protected void chckBoxModules_CheckedChanged(object sender, EventArgs e)
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
                        checkboxStates[moduleID] = false;

                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;
                    }
                }

                ViewState["CheckboxStatesEdit"] = checkboxStates;

                UpdateSelectedLabels();

            }
        }

        private void UpdateSelectedLabels()
        {
            // Loop through the Repeater items to find selected items in rptEditModulesCourse
            foreach (RepeaterItem item in rptListModulesForTeachers.Items)
            {
                CheckBox chkBoxEditModulesCourse = (CheckBox)item.FindControl("chckBoxModules");
                HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnEditCourseModuleID");
                Label lblOrderEditModulesCourse = (Label)item.FindControl("lblOrderEditModulesCourse");

                if (chkBoxEditModulesCourse != null && hdnEditCourseModuleID != null && lblOrderEditModulesCourse != null)
                {
                    lblOrderEditModulesCourse.Text = chkBoxEditModulesCourse.Checked ? "Seleccionado" : "Selecione este módulo";
                }
            }
        }

        //Funções para os botões de paginação
        protected void btnPreviousListTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberTeachers -= 1;
            BindDataTeachers();
        }

        protected void btnNextListTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberTeachers += 1;
            BindDataTeachers();
        }

        protected void btnPreviousUsersForTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers -= 1;
            BindDataUsers();

        }

        protected void btnNextUsersForTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers += 1;
            BindDataUsers();
        }

        protected void btnPreviousListModulesForTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberModules -= 1;
            BindDataModules();

            UpdateSelectedLabels();

        }

        protected void btnNextListModulesForTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberModules += 1;
            BindDataModules();

            UpdateSelectedLabels();

        }


        //Função de Reinicialização do FlatPickr
        //private void InitializeFlatpickrDatePickers()
        //{
        //    string script = @"
        //                <script>
        //                    document.addEventListener('DOMContentLoaded', function() {
        //                        flatpickr('#" + tbDataNascimento.ClientID + @"', {
        //                            dateFormat: 'd-m-Y',
        //                            theme: 'light',
        //                            maxDate: new Date()
        //                        });

        //                        flatpickr('#" + tbDataValidade.ClientID + @"', {
        //                            dateFormat: 'd-m-Y',
        //                            theme: 'light',
        //                            minDate: new Date()
        //                        });
        //                    });
        //                </script>
        //            ";

        //    ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", script, false);
        //}

        protected void filtermenu_OnClick(object sender, EventArgs e)
        {
            filters.Visible = !filters.Visible;
        }

        protected void btnInsertTeacherMain_OnClick(object sender, EventArgs e)
        {
            insertTeachersDiv.Visible = true;
            listTeachersDiv.Visible = false;
            btnInsertTeacherMain.Visible = false;
            btnInsertTeacherFromList.Visible = false;
            btnBack.Visible = true;

            filtermenu.Visible = false;
            filters.Visible = false;

            hdnSourceDiv.Value = "listTeachersDiv";
        }

        protected void btnInsertTeacherFromList_OnClick(object sender, EventArgs e)
        {
            listUsersDiv.Visible = true;
            listTeachersDiv.Visible = false;
            btnInsertTeacherMain.Visible = false;
            btnInsertTeacherFromList.Visible = false;
            btnBack.Visible = true;

            filtermenu.Visible = false;
            filters.Visible = false;

            hdnSourceDiv.Value = "listTeachersDiv";
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            string sourceDiv = hdnSourceDiv.Value;

            switch (sourceDiv)
            {
                case "listTeachersDiv":
                    listTeachersDiv.Visible = true;
                    listUsersDiv.Visible = false;
                    insertTeachersDiv.Visible = false;
                    ModulesRegisterForTeacher.Visible = false;
                    btnInsertTeacherMain.Visible = true;
                    btnBack.Visible = false;
                    btnInsertTeacherFromList.Visible = true;
                    filtermenu.Visible = true;
                    break;
                case "listUsersDiv":
                    listTeachersDiv.Visible = false;
                    listUsersDiv.Visible = true;
                    insertTeachersDiv.Visible = false;
                    ModulesRegisterForTeacher.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "listTeachersDiv";
                    break;
                case "insertTeachersDiv":
                    listTeachersDiv.Visible = true;
                    listUsersDiv.Visible = false;
                    insertTeachersDiv.Visible = false;
                    ModulesRegisterForTeacher.Visible = false;
                    btnInsertTeacherFromList.Visible = true;
                    btnBack.Visible = false;
                    btnInsertTeacherMain.Visible = true;
                    filtermenu.Visible = true;
                    break;
                case "insertTeachersDivPage2":
                    listTeachersDiv.Visible = false;
                    listUsersDiv.Visible = false;
                    insertTeachersDiv.Visible = true;
                    registerCompletionpage1.Visible = false;
                    registerCompletionpage2.Visible = true;
                    ModulesRegisterForTeacher.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "insertTeachersDivPage1";
                    break;
                case "insertTeachersDivPage1":
                    listTeachersDiv.Visible = false;
                    listUsersDiv.Visible = false;
                    insertTeachersDiv.Visible = true;
                    registerCompletionpage1.Visible = true;
                    registerCompletionpage2.Visible = false;
                    ModulesRegisterForTeacher.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "listTeachersDiv";
                    break;
            }

            //ClearSelectedItemsViewState();
        }

        protected void btnNextPage_OnClick(object sender, EventArgs e)
        {
            if (!Security.IsValidDate(tbDataValidade.Text) && (!Security.IsValidDate(tbDataNascimento.Text) ||
                                                               !string.IsNullOrEmpty(tbDataNascimento.Text)))
            {
                //lblMessageInsert.Visible = true;
                //lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                //lblMessageInsert.Text = "Formato de Data Inválida!";

                //timerMessageInsert.Enabled = true;
            }
            else
            {
                registerCompletionpage1.Visible = false;
                registerCompletionpage2.Visible = true;

            }
        }

        protected void btnBackPageOne_OnClick(object sender, EventArgs e)
        {
            registerCompletionpage1.Visible = true;
            registerCompletionpage2.Visible = false;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            insertTeachersDiv.Visible = false;
            ModulesRegisterForTeacher.Visible = true;
        }

        protected void btnEnroll_OnClick(object sender, EventArgs e)
        {
            string teacherViewState = ViewState["SelectedTeacher"] as String;
            List<int> modulesSelected = new List<int>();

            if (Session["CodUtilizadorClicked"] != null)
            {
                if (!string.IsNullOrEmpty(teacherViewState))
                {
                    JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                    if (Session["FromUser"] != null && Session["FromUser"].ToString() == "true")
                    {
                        User selectedUser = deSerializer.Deserialize<User>(teacherViewState);
                        Session["FromUser"] = "false";
                    }
                    else
                    {
                        Teacher selectedTeacher = deSerializer.Deserialize<Teacher>(teacherViewState);
                    }
                    List<int> selectedItems = ViewState["SelectedItemsEdit"] as List<int>;

                    if (selectedItems != null)
                    {
                        Classes.Enrollment.DeleteEnrollmentTeacher(Convert.ToInt32(Session["CodUtilizadorClicked"]));

                        foreach (int selected in selectedItems)
                        {
                            Enrollment enrollment = new Enrollment();
                            enrollment.CodModulo = selected;
                            enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizadorClicked"]);
                            enrollment.CodSituacao = 1;

                            (int AnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollmentTeacher(enrollment);

                            if (AnswEnrollmentCode == -1 && AnswEnrollmentRegister == -1)
                            {
                                lblMessage.Visible = true;
                                lblMessage.CssClass = "alert alert-primary text-white text-center";
                                lblMessage.Text = "Falha ao atualizar inscrições!";
                                timerMessage.Enabled = true;
                            }
                            else
                            {
                                Classes.Teacher.InsertTeacher(Convert.ToInt32(Session["CodUtilizadorClicked"]), AnswEnrollmentCode);
                                lblMessage.Visible = true;
                                lblMessage.CssClass = "alert alert-primary text-white text-center";
                                lblMessage.Text = "Inscrições atualizadas com sucesso!";
                                timerMessage.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void ClearSelectedItemsViewState()
        {
            ViewState["SelectedItemsEdit"] = null;
            ViewState["SelectedItemsNamesEdit"] = null;
            ViewState["CheckboxStatesEdit"] = null;
        }

        protected void timerMessage_OnTick(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            timerMessage.Enabled = false;
            Response.Redirect("ManageTeachers.aspx");
        }

        private void InitializeViewState()
        {
            if (ViewState["SelectedItemsEdit"] == null)
                ViewState["SelectedItemsEdit"] = new List<int>();

            if (ViewState["CheckboxStatesEdit"] == null)
                ViewState["CheckboxStatesEdit"] = new Dictionary<int, bool>();

            if (ViewState["SelectedItemsNamesEdit"] == null)
                ViewState["SelectedItemsNamesEdit"] = new List<string>();
        }


    }
}