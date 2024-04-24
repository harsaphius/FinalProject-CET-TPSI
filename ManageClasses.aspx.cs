using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageClasses : System.Web.UI.Page
    {
        //public ClassGroup classGroup = new ClassGroup();
        int minStudent = 2;
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
                            document.getElementById('statistics').classList.remove('hidden');
                            
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }
            }


            if (!IsPostBack)
            {
                ddlOrderFilters.Items.Insert(0, new ListItem("None", "0"));

                if (ViewState["SelectedItemsInsert"] == null)
                    ViewState["SelectedItemsInsert"] = new List<int>();

                if (ViewState["CheckboxStatesInsert"] == null)
                    ViewState["CheckboxStatesInsert"] = new Dictionary<int, bool>();

                if (ViewState["SelectedItemsNamesInsert"] == null)
                    ViewState["SelectedItemsNamesInsert"] = new List<string>();


                BindDataClassGroups();

            }


        }

        //Nota: opção desconsiderada porque não consegui ultrapassar o limite do maxJsonLength mesmo aumentando no WebConfig
        //Função de ItemDataBound
        /// <summary>
        /// Função de ItemDataBound do Repeater de Listagem das Turmas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptClasses_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    Repeater rptTeachersClassGroup = e.Item.FindControl("rptTeachersClassGroup") as Repeater;
            //    Repeater rptStudentsClassGroup = e.Item.FindControl("rptStudentsClassGroup") as Repeater;
            //    if (rptTeachersClassGroup != null && rptStudentsClassGroup != null)
            //    {
            //        HiddenField hfCodTurma = e.Item.FindControl("hfCodTurma") as HiddenField;
            //        if (hfCodTurma != null)
            //        {
            //            string codTurma = hfCodTurma.Value;
            //            string queryFormador =
            //                $"SELECT DISTINCT TOP 5 T.codFormador, UD.nome, UDS.foto, I.codTurma FROM formador AS T LEFT JOIN moduloFormadorTurma AS I ON T.codFormador=I.codFormador LEFT JOIN utilizador AS U ON I.codFormador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador WHERE I.codTurma={codTurma}";
            //            string queryFormando =
            //                $"SELECT DISTINCT TOP 5 T.codFormando, UD.nome, UDS.foto, I.codTurma FROM formando AS T LEFT JOIN turmaFormando AS I ON T.codFormando=I.codFormando LEFT JOIN utilizador AS U ON I.codFormando=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador WHERE  I.codTurma={codTurma}";
            //List<Teacher> teachers = Teacher.LoadTeachers(null, null, queryFormador);
            //List<Student> students = Student.LoadStudents(null, null, queryFormando);

            //            rptTeachersClassGroup.DataSource = teachers;
            //            rptTeachersClassGroup.DataBind();

            //            rptStudentsClassGroup.DataSource = students;
            //            rptStudentsClassGroup.DataBind();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Função de ItemDataBound do Repeater de Listagem dos Formandos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //Nota: opção desconsiderada porque não consegui ultrapassar o limite do maxJsonLength mesmo aumentando no WebConfig
        //Funções OnItemCommand
        //protected void rptStudentsClassGroup_OnItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    Session["CodTurma"] = e.CommandArgument.ToString();

        //    Response.Redirect("ClassesDetails.aspx");
        //}

        //protected void rptTeachersClassGroup_OnItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    Session["CodTurma"] = e.CommandArgument.ToString();

        //    Response.Redirect("ClassesDetails.aspx");
        //}

        protected void rptClasses_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Schedule")
            {
                Session["CodTurma"] = e.CommandArgument.ToString();
                Response.Redirect("ManageSchedules.aspx");
            }
            if (e.CommandName == "Details")
            {
                Session["CodTurma"] = e.CommandArgument.ToString();
                Response.Redirect("ClassesDetails.aspx");
            }
        }

        //Funções de DataBind
        private void BindDataStudents(string selectCourseID = null)
        {
            selectCourseID = !IsPostBack ? null : ddlCurso.SelectedValue;

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Student.LoadStudents(null, selectCourseID);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberStudents;
            int PageNumber = PageNumberStudents + 1;
            lblPageNumbersStudents.Text = PageNumber.ToString();

            rptStudents.DataSource = pagedData;
            rptStudents.DataBind();

            btnPreviousStudents.Enabled = !pagedData.IsFirstPage;
            btnNextStudents.Enabled = !pagedData.IsLastPage;
        }

        private void BindDataClassGroups()
        {
            List<string> conditions = new List<string>();

            conditions.Add(string.IsNullOrEmpty(tbSearchFilters.Text) ? "" : tbSearchFilters.Text);
            conditions.Add(string.IsNullOrEmpty(tbDataInicioFilters.Text) ? "" : tbDataInicioFilters.Text);
            conditions.Add(string.IsNullOrEmpty(tbDataFimFilters.Text) ? "" : tbDataFimFilters.Text);
            string order = ddlOrderFilters.SelectedValue == "0" ? null : ddlOrderFilters.SelectedValue;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string classGroupsJson = ClassGroup.LoadClassGroups(conditions, order);

            List<ClassGroup> classGroups = serializer.Deserialize<List<ClassGroup>>(classGroupsJson);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = classGroups;
            pagedData.AllowPaging = true;
            pagedData.PageSize = 6;
            pagedData.CurrentPageIndex = PageNumberClassGroups;
            int PageNumber = PageNumberClassGroups + 1;
            lblPageNumberClassGroups.Text = PageNumber.ToString();

            rptClasses.DataSource = pagedData;
            rptClasses.DataBind();

            btnPreviousClasses.Enabled = !pagedData.IsFirstPage;
            btnNextClasses.Enabled = !pagedData.IsLastPage;
        }

        /// <summary>
        /// Função de BindData dos Módulos consoante o Curso
        /// </summary>
        /// <param name="selectCourseID"></param>
        private void BindModules(string selectCourseID)
        {
            ddlTeacherForModules.Items.Clear();

            SQLDSModulesForCourse.SelectCommand =
                $"SELECT * FROM moduloCurso AS MC INNER JOIN curso AS C ON MC.codCurso=C.codCurso INNER JOIN modulo AS M ON MC.codModulo = M.codModulos WHERE MC.codCurso={selectCourseID}";
            ddlModulesOfCourse.DataSource = SQLDSModulesForCourse;
            ddlModulesOfCourse.DataTextField = "nomeModulos";
            ddlModulesOfCourse.DataValueField = "codModulo";
            ddlModulesOfCourse.DataBind();
        }

        /// <summary>
        /// Função para BindData da DDL Teachers consoante os módulos dados
        /// </summary>
        /// <param name="selectedModuleID"></param>
        private void BindTeacherForModules(string selectedModuleID)
        {
            SQLDSTeachersForModules.SelectCommand =
                $"SELECT * FROM moduloFormador AS MT INNER JOIN utilizador AS U ON MT.codFormador=U.codUtilizador INNER JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador INNER JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador WHERE MT.codModulo={selectedModuleID}";
            ddlTeacherForModules.DataSource = SQLDSTeachersForModules;
            ddlTeacherForModules.DataTextField = "nome";
            ddlTeacherForModules.DataValueField = "codFormador";
            ddlTeacherForModules.DataBind();
        }


        //Funções de Paginação
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
            ddlModulesOfCourse.Items.Clear();
            BindModules(selectedCourseID);
            BindDataStudents(selectedCourseID);

            Course course = new Course();
            course = Course.CompleteCourse(Convert.ToInt32(selectedCourseID));
            List<Module> modules = course.Modules;
            Module firstModule = modules[0];
            int firstModuleID = firstModule.CodModulo;
            BindTeacherForModules(firstModuleID.ToString());

            CalculateExpectedEndDate();
            BuildStringNrTurma();

            listBoxStudents.Items.Clear();
            listBoxTeachersForModules.Items.Clear();

            PageNumberStudents = 0;

        }

        protected void ddlModulesOfCourse_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModuleID = ddlModulesOfCourse.SelectedValue;
            ddlTeacherForModules.Items.Clear();

            BindTeacherForModules(selectedModuleID);
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
                Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"];
                ListItem newItem = new ListItem(hdnStudentForClassGroupName.Value, hdnStudentForClassGroupID.Value);

                if (checkBox.Checked)
                {
                    List<int> selectedItems = (List<int>)ViewState["SelectedItemsInsert"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNamesInsert"];
                    selectedItems.Add(Convert.ToInt32(hdnStudentForClassGroupID.Value));
                    itemsNames.Add(hdnStudentForClassGroupName.Value);

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

        private void UpdateSelectedCheckBoxesInsert()
        {
            Dictionary<int, bool> checkboxStates = (Dictionary<int, bool>)ViewState["CheckboxStatesInsert"];

            foreach (RepeaterItem item in rptStudents.Items)
            {
                CheckBox chkBoxStudentForClassGroup = (CheckBox)item.FindControl("chkBoxStudentForClassGroup");
                HiddenField hdnStudentForClassGroupID = (HiddenField)item.FindControl("hdnStudentForClassGroupID");

                if (chkBoxStudentForClassGroup != null && hdnStudentForClassGroupID != null)
                {
                    if (checkboxStates.ContainsKey(Convert.ToInt32(hdnStudentForClassGroupID.Value)))
                    {
                        chkBoxStudentForClassGroup.Checked = checkboxStates[Convert.ToInt32(hdnStudentForClassGroupID.Value)];
                    }
                }
            }

            ViewState["CheckboxStatesInsert"] = checkboxStates;
        }

        protected void btnAddTeacherModuleClassGroup_OnClick(object sender, EventArgs e)
        {
            if (ddlTeacherForModules.SelectedItem != null && ddlModulesOfCourse.SelectedItem != null)
            {
                string moduleName = ddlModulesOfCourse.SelectedItem.Text;
                string teacherName = ddlTeacherForModules.SelectedItem.Text;

                string combinedName = moduleName + " | " + teacherName;

                listBoxTeachersForModules.Items.Add(new ListItem(combinedName,
                    $"{ddlModulesOfCourse.SelectedValue} | {ddlTeacherForModules.SelectedValue}"));
                btnRemoveTeacherModuleClassGroup.Enabled = true;

                ddlModulesOfCourse.Items[ddlModulesOfCourse.SelectedIndex].Enabled = false;
                int nextModuleIndex = ddlModulesOfCourse.SelectedIndex + 1;
                if (nextModuleIndex < ddlModulesOfCourse.Items.Count)
                {
                    // Select the next module
                    ddlModulesOfCourse.SelectedIndex = nextModuleIndex;
                    // Bind the teachers for the next module
                    BindTeacherForModules(ddlModulesOfCourse.SelectedValue);
                }
                else
                {
                    btnAddTeacherModuleClassGroup.Enabled = false;
                    btnAddTeacherModuleClassGroup.CssClass = "btn bg-gradient-info w-100 mt-4 mb-0";

                }
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
                    string codModulo = partsValues[0].Trim();
                    string codTeacher = partsValues[1].Trim();

                    ListItem disabledModule = ddlModulesOfCourse.Items.FindByValue(codModulo);
                    if (disabledModule != null)
                    {
                        disabledModule.Enabled = true;
                        btnAddTeacherModuleClassGroup.Enabled = true;

                    }
                }

                listBoxTeachersForModules.Items.Remove(listBoxTeachersForModules.SelectedItem);

                // Check if there are no items left in the listBoxTeachersForModules and remove the class attribute
                if (listBoxTeachersForModules.Items.Count == 0)
                {
                    btnRemoveTeacherModuleClassGroup.Enabled = false;
                }

            }
            else
            {
                lbl_message.Text = "Seleccione na lista o item a remover!";
            }


        }


        //Funções dos botões de paginação
        protected void btnPreviousClasses_OnClick(object sender, EventArgs e)
        {
            PageNumberClassGroups -= 1;
            BindDataClassGroups();
        }

        protected void btnNextClasses_OnClick(object sender, EventArgs e)
        {
            PageNumberClassGroups += 1;
            BindDataClassGroups();
        }

        protected void btnPreviousStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
            UpdateSelectedCheckBoxesInsert();
        }

        protected void btnNextStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
            UpdateSelectedCheckBoxesInsert();
        }


        /// <summary>
        /// Função para inserção de uma nova turma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertClassMain_OnClick(object sender, EventArgs e)
        {
            insertClassesDiv.Visible = true;
            listClassesDiv.Visible = false;
            btnBack.Visible = true;
            btnInsertClassMain.Visible = false;
            filtermenu.Visible = false;
            filters.Visible = false;

        }

        /// <summary>
        /// Função para voltar à página de listagem após inserção de nova turma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            listClassesDiv.Visible = true;
            insertClassesDiv.Visible = false;
            btnBack.Visible = false;
            btnInsertClassMain.Visible = true;
            filtermenu.Visible = true;

            CleanTextBoxes();

            Response.Redirect("ManageClasses.aspx");
        }

        private void CleanTextBoxes()
        {
            tbDataInicio.Text = "";
            lblTurma.Text = "";
            lblDataFim.Text = "";

            listBoxStudents.Items.Clear();
            listBoxTeachersForModules.Items.Clear();
        }

        /// <summary>
        /// Função para toggle aos filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filtermenu_OnClick(object sender, EventArgs e)
        {
            filters.Visible = !filters.Visible;
        }

        protected void ddlHorarioTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateExpectedEndDate();
            BuildStringNrTurma();
        }

        protected void tbDataInicio_OnTextChanged(object sender, EventArgs e)
        {
            CalculateExpectedEndDate();
            BuildStringNrTurma();
        }

        protected void btnInsertClass_OnClick(object sender, EventArgs e)
        {
            int totalModulesCount = Course.GetTotalModulesCountForCourse(Convert.ToInt32(ddlCurso.SelectedValue)); // Replace courseId with the ID of the course
            int addedModulesCount = ddlModulesOfCourse.Items.Count;

            if (listBoxStudents.Items.Count >= minStudent && addedModulesCount == totalModulesCount)
            {
                //if (Convert.ToDateTime(tbDataInicio.Text) > DateTime.Now || Convert.ToDateTime(tbDataInicio.Text).DayOfWeek == DayOfWeek.Sunday || Convert.ToDateTime(tbDataInicio.Text).DayOfWeek == DayOfWeek.Saturday)
                //{
                ClassGroup classGroup = new ClassGroup();
                classGroup.CodCurso = Convert.ToInt32(ddlCurso.SelectedValue);
                classGroup.NomeTurma = BuildStringNrTurma();
                classGroup.CodRegime = Convert.ToInt32(ddlRegime.SelectedValue);
                classGroup.CodHorarioTurma = Convert.ToInt32(ddlHorarioTurma.SelectedValue);
                classGroup.DataInicio = Convert.ToDateTime(tbDataInicio.Text);
                classGroup.DataFim = Convert.ToDateTime(lblDataFim.Text);

                classGroup.Students = new List<Student>();
                classGroup.Teachers = new List<Teacher>();
                classGroup.Modules = new List<Module>();

                foreach (ListItem studItem in listBoxStudents.Items)
                {
                    Student student = new Student();
                    student.CodFormando = Convert.ToInt32(studItem.Value);
                    student.Nome = studItem.Text;
                    student.CodInscricao =
                        Classes.ClassGroup.GetCodInscricaoByCodUtilizador(student.CodFormando, classGroup.CodCurso, null);

                    classGroup.Students.Add(student);
                }

                foreach (ListItem item in listBoxTeachersForModules.Items)
                {
                    string[] parts = item.Value.Split('|');
                    int moduleId = Convert.ToInt32(parts[0]);
                    int teacherId = Convert.ToInt32(parts[1]);

                    Teacher teacher = classGroup.Teachers.FirstOrDefault(t => t.CodFormador == teacherId);
                    if (teacher == null)
                    {
                        teacher = new Teacher();
                        teacher.CodFormador = teacherId;
                        classGroup.Teachers.Add(teacher);
                    }

                    Module module = new Module();
                    module.CodModulo = moduleId;
                    classGroup.Modules.Add(module);
                }

                int AnswClassGroup = Classes.ClassGroup.InsertClassGroup(classGroup);

                if (AnswClassGroup == 1)
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    lblMessageInsert.Text = "Turma inserida com sucesso";
                    timerMessageInsert.Enabled = true;
                }
                else
                {

                }
            }
            else
            {
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "Data de início deve ser superior ao dia de hoje!";
                timerMessageInsert.Enabled = true;
                //}
            }
        }

        protected void ddlRegime_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BuildStringNrTurma();
        }


        /// <summary>
        /// Função para devolução da Data Final Prevista
        /// </summary>
        private void CalculateExpectedEndDate()
        {
            if (!string.IsNullOrEmpty(tbDataInicio.Text))
            {
                Course course = Course.CompleteCourse(Convert.ToInt32(ddlCurso.SelectedValue));

                int remainingHours = course.Duracao;
                int hoursPerDay = 0;
                DateTime currentDate = Convert.ToDateTime(tbDataInicio.Text);

                if (ddlHorarioTurma.SelectedValue == "1") hoursPerDay = 8;
                else if (ddlHorarioTurma.SelectedValue == "2") hoursPerDay = 4;

                while (remainingHours > 0)
                {
                    // Skip Saturdays and Sundays
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        remainingHours -= hoursPerDay;
                    }

                    currentDate = currentDate.AddDays(1);
                }

                DateTime expectedEndDate = currentDate.AddDays(-1);

                // Add two more months to the expected end date
                expectedEndDate = expectedEndDate.AddMonths(2);

                lblDataFim.Text = expectedEndDate.ToShortDateString();

            }
        }

        /// <summary>
        /// Função para construção do Nr.º da Turma
        /// </summary>
        /// <returns></returns>
        private string BuildStringNrTurma()
        {
            string cursoValue = Course.GetCourseTypeNameFromDDL(ddlCurso.SelectedValue);
            string selectedItemText = ddlCurso.SelectedItem.Text;
            string[] words = selectedItemText.Split(' ');
            string initials = "";
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    initials += word[0];
                }
            }
            string regimeValue = ddlRegime.SelectedItem.Text.Replace("-", "").Substring(0, 2).ToUpper();
            string horarioValue = ddlHorarioTurma.SelectedValue == "1" ? "L" :
                ddlHorarioTurma.SelectedValue == "2" ? "PL" : "";
            string turmaNumber = ClassGroup.GetClassGroupNumber(ddlCurso.SelectedValue, ddlRegime.SelectedValue, ddlHorarioTurma.SelectedValue);

            string AnswTurmaNumber = $"{cursoValue}.{regimeValue}.{horarioValue}.{initials.ToUpper()}.{turmaNumber}";

            lblTurma.Text = AnswTurmaNumber;

            return AnswTurmaNumber;
        }

        /// <summary>
        /// Função Click do Botão de Aplicar os Filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApplyFilters_OnClick(object sender, EventArgs e)
        {
            PageNumberClassGroups = 0;

            BindDataClassGroups();
        }

        /// <summary>
        /// Função Click do Botão de Limpar os filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearFilters_OnClick(object sender, EventArgs e)
        {
            tbSearchFilters.Text = "";
            tbDataInicioFilters.Text = "";
            tbDataFimFilters.Text = "";
            ddlOrderFilters.SelectedIndex = 0;

            PageNumberClassGroups = 0;

            BindDataClassGroups();

        }

        protected void timerMessageInsert_OnTick(object sender, EventArgs e)
        {
            lblMessageInsert.Visible = false;
            timerMessageInsert.Enabled = false;

            Response.Redirect("ManageClasses.aspx");
        }

    }


}
