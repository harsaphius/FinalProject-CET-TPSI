using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageStudents : System.Web.UI.Page
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
                    InitializeViewState();

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string coursesViewState = serializer.Serialize(Classes.Course.LoadCourses(null, "codCurso"));
                    ViewState["coursesViewState"] = coursesViewState;

                    BindDataStudents();

                    BindDataUsers();
                }

            }
        }

        //Funções de ItemDataBound dos Repeaters

        //Funções de ItemCommand dos Repeaters
        protected void rptListStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                insertStudentsDiv.Visible = true;
                listStudentsDiv.Visible = false;
                btnInsertStudentMain.Visible = false;
                btnInsertStudentFromList.Visible = false;
                btnBack.Visible = true;
                filtermenu.Visible = false;
                filtermenu.Visible = false;

                hdnSourceDiv.Value = "listStudentsDiv";

                (Student student, User user) = Classes.Student.LoadStudent(Convert.ToInt32(e.CommandArgument));

                tbNome.Text = student.Nome;
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

            if (e.CommandName == "EditCoursesStudents")
            {
                listStudentsDiv.Visible = false;
                CourseRegisterForStudent.Visible = true;
                btnInsertStudentFromList.Visible = false;
                btnInsertStudentMain.Visible = false;
                btnBack.Visible = true;

                filtermenu.Visible = false;
                filters.Visible = false;

                hdnSourceDiv.Value = "listStudentsDiv";

                (Student student, User user) = Classes.Student.LoadStudent(Convert.ToInt32(e.CommandArgument));

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string studentViewState = serializer.Serialize(student);
                ViewState["SelectedStudent"] = studentViewState;

                BindDataCourses();

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }

            if (e.CommandName == "Delete")
            {
                int AnswStudentDeleted = Classes.Student.DeleteStudent(Convert.ToInt32(e.CommandArgument));

                if (AnswStudentDeleted == 1)
                {
                    BindDataStudents();
                    lblMessage.Text = "Formando apagado com sucesso!";
                }
                else
                {
                    lblMessage.Text = "Formando não pode ser eliminado por fazer parte de uma turma a decorrer!";
                }
            }

        }

        protected void rptUserForStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditCoursesUsers")
            {
                listUsersDiv.Visible = false;
                CourseRegisterForStudent.Visible = true;
                btnInsertStudentFromList.Visible = false;
                btnInsertStudentMain.Visible = false;
                btnBack.Visible = true;

                filtermenu.Visible = false;
                filters.Visible = false;

                hdnSourceDiv.Value = "listUsersDiv";

                User user = Classes.User.LoadUser(null, e.CommandArgument.ToString());

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string studentViewState = serializer.Serialize(user);
                ViewState["SelectedStudent"] = studentViewState;

                BindDataCourses();

                Session["FromUser"] = "true";

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();

            }
        }

        protected void rptListCoursesForStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chckBoxCourses = (CheckBox)e.Item.FindControl("chckBoxCourses");

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = chckBoxCourses.UniqueID;
                trigger.EventName = "CheckedChanged";

                updatePanelListStudent.Triggers.Add(trigger);
                chckBoxCourses.CheckedChanged += chckBoxCourses_CheckedChanged;
            }
        }


        //Funções de Inserção/Edição
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            User user = new User();

            if (Security.IsValidEmail(tbEmail.Text) && Security.IsValidDate(tbDataValidade.Text) && Security.IsValidDate(tbDataNascimento.Text))
            {
                string email = tbEmail.Text;

                user.CodPerfil = 2;
                user.Nome = tbNome.Text;
                string[] parts = email.Split('@');
                if (parts.Length == 2)
                {
                    string beforeAt = parts[0];

                    string cleanUsername = Regex.Replace(beforeAt, @"[^a-zA-Z0-9]", "");

                    if (cleanUsername.Length > 30)
                    {
                        cleanUsername = cleanUsername.Substring(0, 30);
                    }

                    user.Username = cleanUsername;
                }

                user.Email = tbEmail.Text;
                string NovaPasse = Membership.GeneratePassword(10, 2);
                user.Password = NovaPasse;
                user.CodTipoDoc = Convert.ToInt32(ddlDocumentoIdent.SelectedValue);
                user.DocIdent = tbCC.Text;
                user.DataValidade = Convert.ToDateTime(tbDataValidade.Text);
                user.CodPrefix = Convert.ToInt32(ddlPrefixo.SelectedValue);
                user.Phone = tbTelemovel.Text;

                (int UserRegister, int userCode) = Classes.User.RegisterUser(user);

                if (UserRegister == 1)
                {
                    EmailControl.SendEmailActivationWithPW(tbEmail.Text, parts[0], NovaPasse);

                    user.CodUser = userCode;
                    user.Sexo = Convert.ToInt32(ddlSexo.SelectedValue);
                    user.DataNascimento = Convert.ToDateTime(tbDataNascimento.Text);
                    user.NIF = tbNIF.Text;
                    user.Morada = tbMorada.Text;
                    user.CodPais = Convert.ToInt32(ddlCodPais.SelectedValue);
                    user.CodPostal = tbCodPostal.Text;
                    user.Localidade = tbLocalidade.Text;
                    user.CodEstadoCivil = Convert.ToInt32(ddlCodEstadoCivil.SelectedValue);
                    user.NrSegSocial = tbNrSegSocial.Text;
                    user.IBAN = tbIBAN.Text;
                    user.Naturalidade = (tbNaturalidade.Text);
                    user.CodNacionalidade = Convert.ToInt32(ddlCodNacionalidade.SelectedValue);
                    HttpPostedFile photoFile = fuFoto.PostedFile;

                    if (fuFoto.HasFile && photoFile != null)
                    {
                        byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                        user.Foto = (Convert.ToBase64String(photoBytes));
                    }

                    user.CodGrauAcademico = Convert.ToInt32(ddlCodGrauAcademico.SelectedValue);
                    user.CodSituacaoProf = Convert.ToInt32(ddlCodSituacaoProfissional.SelectedValue);
                    user.LifeMotto = ""; //Substituir LifeMotto

                    List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);

                    int CompleteUser = Classes.User.CompleteRegisterUser(user, uploadedFiles);

                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), key: "ShowPageElements", script, true);

                    lblMessage.Text = "Formando registado com sucesso!";

                    CleanTextBoxes(this);
                }
                else
                {
                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lblMessage.Text = "Formando já registado!";
                    CleanTextBoxes(this);

                }
            }
            else
            {
                string resetScript = @"
                            document.getElementById('<%= tbDataNascimento.ClientID %>')._flatpickr.clear();
                            document.getElementById('<%= tbDataValidade.ClientID %>')._flatpickr.clear();
                        ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ResetDatePickers", resetScript, true);

                string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lblMessage.Text = "Introduza um e-mail válido!";
            }

        }

        /// <summary>
        /// Função Click do Botão de Inscrição num curso do aluno clicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnroll_OnClick(object sender, EventArgs e)
        {
            string studentViewState = ViewState["SelectedStudent"] as String;
            List<int> coursesSelected = new List<int>();

            if (Session["CodUtilizadorClicked"] != null)
            {
                if (!string.IsNullOrEmpty(studentViewState))
                {
                    JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                    if (Session["FromUser"] != null && Session["FromUser"].ToString() == "true")
                    {
                        User selectedUser = deSerializer.Deserialize<User>(studentViewState);
                        Session["FromUser"] = "false";
                    }
                    else
                    {
                        Student selectedStudent = deSerializer.Deserialize<Student>(studentViewState);
                    }
                    List<int> selectedItems = ViewState["SelectedItemsEdit"] as List<int>;

                    if (selectedItems != null)
                    {
                        Classes.Enrollment.DeleteEnrollmentStudent(Convert.ToInt32(Session["CodUtilizadorClicked"]));

                        foreach (int selected in selectedItems)
                        {
                            Enrollment enrollment = new Enrollment();
                            enrollment.CodCurso = selected;
                            enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizadorClicked"]);
                            enrollment.CodSituacao = 1;

                            (int AnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollmentStudent(enrollment);

                            if (AnswEnrollmentCode == -1 && AnswEnrollmentRegister == -1)
                            {
                                lblMessage.Visible = true;
                                lblMessage.CssClass = "alert alert-primary text-white text-center";
                                lblMessage.Text = "Falha ao atualizar inscrições!";
                                timerMessage.Enabled = true;
                            }
                            else
                            {
                                //Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizadorClicked"]), AnswEnrollmentCode);
                                lblMessage.Visible = true;
                                lblMessage.CssClass = "alert alert-primary text-white text-center";
                                lblMessage.Text = "Inscrições atualizadas com sucesso!";
                                timerMessage.Enabled = true;

                            }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "alert alert-primary text-white text-center";
                lblMessage.Text = "Tem de adicionar pelo menos um curso!";
                timerMessage.Enabled = true;
            }
        }


        //Funções de DataBinding
        private void BindDataStudents()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Student.LoadStudents();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberStudents;
            int PageNumber = PageNumberStudents + 1;
            lblPageNumberListStudents.Text = PageNumber.ToString();

            rptListStudents.DataSource = pagedData;
            rptListStudents.DataBind();

            btnPreviousListStudents.Enabled = !pagedData.IsFirstPage;
            btnNextListStudents.Enabled = !pagedData.IsLastPage;
        }

        private void BindDataCourses()
        {
            string courses = ViewState["coursesViewState"] as string;

            if (!string.IsNullOrEmpty(courses))
            {
                // Deserialize course information
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Course> allCourses = serializer.Deserialize<List<Course>>(courses);

                string studentViewState = ViewState["SelectedStudent"] as string;
                string courseKey = "IsFirstEnteringEdit_" + studentViewState; // Append course code to the ViewState key

                bool isFirstEnteringEdit = ViewState[courseKey] == null || (bool)ViewState[courseKey];

                if (isFirstEnteringEdit) // Check if it's the first time the page is being loaded
                {
                    if (!string.IsNullOrEmpty(studentViewState))
                    {
                        JavaScriptSerializer deSerializer = new JavaScriptSerializer();
                        Student selectedStudent = deSerializer.Deserialize<Student>(studentViewState);
                        List<Course> selectedCourses = selectedStudent.Courses;

                        // Update IsChecked property of each course based on whether it's selected or not
                        Dictionary<int, bool> checkboxStatesEdit = new Dictionary<int, bool>();
                        if (selectedCourses != null && selectedCourses.Any())
                        {
                            foreach (Course course in allCourses)
                            {
                                course.IsChecked = selectedCourses.Any(c => c.CodCurso == course.CodCurso);
                                checkboxStatesEdit.Add(course.CodCurso, course.IsChecked);
                            }
                            ViewState["CheckboxStatesEdit"] = checkboxStatesEdit;

                            // Update SelectedItemsEdit and SelectedItemsNamesEdit
                            List<int> selectedItems = new List<int>();
                            List<string> itemsNames = new List<string>();

                            foreach (Course course in selectedCourses)
                            {
                                selectedItems.Add(course.CodCurso);
                                itemsNames.Add(course.Nome);
                            }

                            ViewState["SelectedItemsEdit"] = selectedItems;
                            ViewState["SelectedItemsNamesEdit"] = itemsNames;

                            ViewState[courseKey] = false;
                        }

                    }
                }
                else // Not the first time entering, retrieve course information from ViewState
                {
                    if (!string.IsNullOrEmpty(studentViewState))
                    {
                        Dictionary<int, bool> checkboxStatesEdit = ViewState["CheckboxStatesEdit"] as Dictionary<int, bool>;
                        List<int> selectedItems = ViewState["SelectedItemsEdit"] as List<int>;
                        List<string> itemsNames = ViewState["SelectedItemsNamesEdit"] as List<string>;

                        foreach (Course course in allCourses)
                        {
                            if (checkboxStatesEdit.ContainsKey(course.CodCurso))
                            {
                                course.IsChecked = checkboxStatesEdit[course.CodCurso];

                                // If the course is checked and not already in the selected items list, add it
                                if (course.IsChecked && !selectedItems.Contains(course.CodCurso))
                                {
                                    selectedItems.Add(course.CodCurso);
                                    itemsNames.Add(course.Nome);
                                }
                                // If the course is unchecked and already in the selected items list, remove it
                                else if (!course.IsChecked && selectedItems.Contains(course.CodCurso))
                                {
                                    int indexToRemove = selectedItems.IndexOf(course.CodCurso);
                                    selectedItems.RemoveAt(indexToRemove);
                                    itemsNames.RemoveAt(indexToRemove);
                                }
                            }
                        }
                        ViewState["SelectedItemsEdit"] = selectedItems;
                        ViewState["SelectedItemsNamesEdit"] = itemsNames;

                        UpdateSelectedLabels();
                    }
                }

                PagedDataSource pagedData = new PagedDataSource();

                pagedData.DataSource = allCourses;
                pagedData.AllowPaging = true;
                pagedData.PageSize = 5;
                pagedData.CurrentPageIndex = PageNumberCourses;
                int PageNumber = PageNumberCourses + 1;
                lblPageNumberListCoursesForStudents.Text = PageNumber.ToString();

                rptListCoursesForStudents.DataSource = pagedData;
                rptListCoursesForStudents.DataBind();

                UpdateSelectedLabels();

                btnPreviousListCoursesForStudents.Enabled = !pagedData.IsFirstPage;
                btnNextListCoursesForStudents.Enabled = !pagedData.IsLastPage;
            }
        }

        private void BindDataUsers()
        {
            string condition =
                "WITH UserProfiles AS (SELECT U.codUtilizador, UP.codPerfil, ROW_NUMBER() OVER (PARTITION BY U.codUtilizador ORDER BY UP.codPerfil) AS RowNumber FROM utilizador AS U LEFT JOIN utilizadorPerfil AS UP ON U.codUtilizador = UP.codUtilizador WHERE UP.codPerfil != 2 AND U.ativo = 1) SELECT DISTINCT U.*, UD.*, UDS.*, UP.* FROM UserProfiles AS UP LEFT JOIN utilizador AS U ON UP.codUtilizador = U.codUtilizador LEFT JOIN utilizadorData AS UD ON U.codUtilizador = UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador = UDS.codUtilizador WHERE UP.RowNumber = 1 AND U.codUtilizador NOT IN (SELECT codFormando FROM formando);";

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers(null, condition);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberUsers;
            int PageNumber = PageNumberUsers + 1;
            lblPageNumbersUsersForStudents.Text = PageNumber.ToString();

            rptUserForStudents.DataSource = pagedData;
            rptUserForStudents.DataBind();

            btnPreviousUsersForStudents.Enabled = !pagedData.IsFirstPage;
            btnNextsUsersForStudents.Enabled = !pagedData.IsLastPage;
        }

        //Funções para as CheckBoxes
        protected void chckBoxCourses_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnCourseID");
            HiddenField hdnEditCourseModuleName = (HiddenField)item.FindControl("hdnCourseName");
            Label lblOrderEditCoursesCourse = (Label)item.FindControl("lblOrder");

            if (hdnEditCourseModuleID != null && hdnEditCourseModuleName != null && lblOrderEditCoursesCourse != null)
            {
                int courseID = Convert.ToInt32(hdnEditCourseModuleID.Value);
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesEdit"];

                if (checkBox.Checked)
                {
                    lblOrderEditCoursesCourse.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"];
                    selectedItems.Add(Convert.ToInt32(hdnEditCourseModuleID.Value));
                    itemsNames.Add(hdnEditCourseModuleName.Value);
                    checkboxStates[courseID] = true;

                    ViewState["SelectedItemsEdit"] = selectedItems;
                    ViewState["SelectedItemsNamesEdit"] = itemsNames;
                }
                else
                {
                    lblOrderEditCoursesCourse.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsEdit"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesEdit"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnEditCourseModuleID.Value));
                        itemsNames.Remove(hdnEditCourseModuleName.Value);
                        checkboxStates[courseID] = false;

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
            // Loop through the Repeater items to find selected items in rptEditCoursesCourse
            foreach (RepeaterItem item in rptListCoursesForStudents.Items)
            {
                CheckBox chkBoxEditCoursesCourse = (CheckBox)item.FindControl("chckBoxCourses");
                HiddenField hdnEditCourseModuleID = (HiddenField)item.FindControl("hdnCourseID");
                Label lblOrderEditCoursesCourse = (Label)item.FindControl("lblOrder");

                if (chkBoxEditCoursesCourse != null && hdnEditCourseModuleID != null && lblOrderEditCoursesCourse != null)
                {
                    lblOrderEditCoursesCourse.Text = chkBoxEditCoursesCourse.Checked ? "Seleccionado" : "Selecione este módulo";
                }
            }
        }

        //Funções de Paginação
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


        //Funções para os botões de paginação
        protected void btnPreviousListStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents -= 1;
            BindDataStudents();
        }

        protected void btnNextListStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents += 1;
            BindDataStudents();
        }

        protected void btnPreviousListCoursesForStudents_Click(object sender, EventArgs e)
        {
            PageNumberCourses -= 1;
            BindDataCourses();

            UpdateSelectedLabels();
        }

        protected void btnNextListCoursesForStudents_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1;
            BindDataCourses();

            UpdateSelectedLabels();
        }

        protected void btnPreviousUsersForStudents_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers -= 1;
            BindDataUsers();
        }

        protected void btnNextsUsersForStudents_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers += 1;
            BindDataUsers();
        }


        protected void btnNextPage_OnClick(object sender, EventArgs e)
        {
            if (!Security.IsValidDate(tbDataValidade.Text) && (!Security.IsValidDate(tbDataNascimento.Text) || !string.IsNullOrEmpty(tbDataNascimento.Text)))
            {
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "Formato de Data Inválida!";

                timerMessageInsert.Enabled = true;
            }
            else
            {
                registerCompletionpage1.Visible = false;
                registerCompletionpage2.Visible = true;

            }
        }

        protected void timerMessageInsert_OnTick(object sender, EventArgs e)
        {
            lblMessageInsert.Visible = false;
            timerMessageInsert.Enabled = false;
        }


        protected void timerMessage_OnTick(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            timerMessage.Enabled = false;
            Response.Redirect("ManageStudents.aspx");
        }

        private void CleanTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).SelectedIndex = 0;
                }
                else if (control.HasControls())
                {
                    CleanTextBoxes(control);
                }
            }
        }

        protected void btnInsertStudentMain_OnClick(object sender, EventArgs e)
        {
            insertStudentsDiv.Visible = true;
            listStudentsDiv.Visible = false;
            btnInsertStudentFromList.Visible = false;
            btnInsertStudentMain.Visible = false;
            btnBack.Visible = true;

            filtermenu.Visible = false;
            filters.Visible = false;

            hdnSourceDiv.Value = "insertStudentsDiv";

        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            string sourceDiv = hdnSourceDiv.Value;

            switch (sourceDiv)
            {
                case "listStudentsDiv":
                    listStudentsDiv.Visible = true;
                    listUsersDiv.Visible = false;
                    insertStudentsDiv.Visible = false;
                    CourseRegisterForStudent.Visible = false;
                    btnInsertStudentFromList.Visible = true;
                    btnBack.Visible = false;
                    btnInsertStudentMain.Visible = true;
                    filtermenu.Visible = true;
                    break;
                case "listUsersDiv":
                    listStudentsDiv.Visible = false;
                    listUsersDiv.Visible = true;
                    insertStudentsDiv.Visible = false;
                    CourseRegisterForStudent.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "listStudentsDiv";
                    break;
                case "insertStudentsDiv":
                    listStudentsDiv.Visible = true;
                    listUsersDiv.Visible = false;
                    insertStudentsDiv.Visible = false;
                    CourseRegisterForStudent.Visible = false;
                    btnInsertStudentFromList.Visible = true;
                    btnBack.Visible = false;
                    btnInsertStudentMain.Visible = true;
                    filtermenu.Visible = true;
                    break;
                case "insertStudentsDivPage2":
                    listStudentsDiv.Visible = false;
                    listUsersDiv.Visible = false;
                    insertStudentsDiv.Visible = true;
                    registerCompletionpage1.Visible = false;
                    registerCompletionpage2.Visible = true;
                    CourseRegisterForStudent.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "insertStudentsDivPage1";
                    break;
                case "insertStudentsDivPage1":
                    listStudentsDiv.Visible = false;
                    listUsersDiv.Visible = false;
                    insertStudentsDiv.Visible = true;
                    registerCompletionpage1.Visible = true;
                    registerCompletionpage2.Visible = false;
                    CourseRegisterForStudent.Visible = false;
                    btnBack.Visible = true;
                    hdnSourceDiv.Value = "listStudentsDiv";
                    break;

            }


        }

        protected void filtermenu_OnClick(object sender, EventArgs e)
        {
            filters.Visible = !filters.Visible;
        }

        protected void btnInsertStudentFromList_OnClick(object sender, EventArgs e)
        {
            listUsersDiv.Visible = true;
            listStudentsDiv.Visible = false;
            btnInsertStudentFromList.Visible = false;
            btnInsertStudentMain.Visible = false;
            btnBack.Visible = true;

            filtermenu.Visible = false;
            filters.Visible = false;

            hdnSourceDiv.Value = "insertStudentsDiv";
        }

        protected void btnBackPageOne_OnClick(object sender, EventArgs e)
        {
            registerCompletionpage1.Visible = true;
            registerCompletionpage2.Visible = false;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            insertStudentsDiv.Visible = false;
            CourseRegisterForStudent.Visible = true;
        }


        private void ClearSelectedItemsViewState()
        {
            ViewState["SelectedItemsEdit"] = null;
            ViewState["SelectedItemsNamesEdit"] = null;
            ViewState["CheckboxStatesEdit"] = null;
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