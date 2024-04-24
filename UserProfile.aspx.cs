using FinalProject.Classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UserProfile : System.Web.UI.Page
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
                string userCode = Session["CodUtilizador"].ToString();
                if (Session["Enrollment"] != null)
                {
                    if (Session["CodCursoEnrollment"] != null)
                    {
                        int CodCurso = Convert.ToInt32(Session["CodCursoEnrollment"].ToString());
                        Enrollment enrollment = new Enrollment();
                        enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizador"].ToString());
                        enrollment.CodSituacao = 1;
                        enrollment.CodCurso = CodCurso;

                        (int AnswAnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollmentStudent(enrollment);

                        if (AnswEnrollmentCode == -1 && AnswAnswEnrollmentRegister == -1)
                        {

                            lblMessageEdit.Visible = true;
                            lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                            lblMessageEdit.Text = "Utilizador já registado nesse curso.";
                            timerMessageEdit.Enabled = true;
                        }
                        else
                        {
                            Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizador"]), AnswEnrollmentCode);
                            lblMessageEdit.Visible = true;
                            lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                            lblMessageEdit.Text = "Utilizador registado com sucesso no curso!";
                            timerMessageEdit.Enabled = true;
                        }
                    }
                }

                Label lbluser = Master.FindControl("lbl_user") as Label;
                if (lbluser != null)
                {
                    lbluser.Text = user;
                }

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

                    if (!Page.IsPostBack)
                    {
                        User profileuser = Classes.User.LoadUser(user);
                        (int CodUtilizador, string Username) = Classes.User.DetermineUtilizador(user);
                        Session["Username"] = Username;

                        //Inicializar ViewStates
                        InitializeViewState();

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string modulesViewState = serializer.Serialize(Classes.Module.LoadModules(null, "codUFCD"));
                        ViewState["modulesViewState"] = modulesViewState;

                        if (profileuser != null)
                        {
                            //Carregar informação para as labels da página inicial do profile
                            profilename.Text = profileuser.Username;
                            profileemail.Text = profileuser.Email;
                            infoname.Text = profileuser.Nome;
                            infocell.Text = profileuser.Phone;
                            infoemail.Text = profileuser.Email;
                            lbLifeMotto.Text = profileuser.LifeMotto;

                            //Load informação para as textboxes do menu Edit Profile
                            tbNome.Text = profileuser.Nome;
                            ddlSexo.SelectedValue = profileuser.Sexo.ToString();
                            tbDataNascimento.Text = profileuser.DataNascimento.ToShortDateString();
                            ddlDocumentoIdent.SelectedValue = profileuser.CodTipoDoc.ToString();
                            tbCC.Text = profileuser.DocIdent;
                            tbDataValidade.Text = profileuser.DataValidade.ToShortDateString();
                            tbNrSegSocial.Text = profileuser.NrSegSocial;
                            tbNIF.Text = profileuser.NIF;
                            tbMorada.Text = profileuser.Morada;
                            tbLocalidade.Text = profileuser.Localidade;
                            tbCodPostal.Text = profileuser.CodPostal;
                            foto.ImageUrl = profileuser.Foto;
                            ddlCodPais.SelectedValue = profileuser.CodPais.ToString();
                            ddlCodEstadoCivil.SelectedValue = profileuser.CodEstadoCivil.ToString();
                            tbIBAN.Text = profileuser.IBAN;
                            tbNaturalidade.Text = profileuser.Naturalidade;
                            ddlCodNacionalidade.SelectedValue = profileuser.CodNacionalidade.ToString();
                            ddlCodSituacaoProfissional.SelectedValue = profileuser.CodSituacaoProf.ToString();
                            ddlprefixo.SelectedValue = profileuser.CodPrefix.ToString();
                            tbTelemovel.Text = profileuser.Phone;
                            tbEmail.Text = profileuser.Email;
                            ddlCodGrauAcademico.SelectedValue = profileuser.CodGrauAcademico.ToString();
                            tbLifeMotto.Text = profileuser.LifeMotto;

                            if (LoadSubmittedFiles() == 0)
                            {
                                lblSubmittedFiles.Text = "Ainda não foram submetidos ficheiros!";
                            }
                            else
                            {
                                LoadSubmittedFiles();
                            }
                        }

                        List<int> UserProfiles = Classes.User.DetermineUserProfile(Convert.ToInt32(userCode));

                        foreach (int profile in UserProfiles)
                        {
                            if (profile == 2)
                            {
                                lbtAvaliacoes.Visible = true;
                            }
                            else if (profile == 3)
                            {
                                Session["CodFormador"] = Session["CodUtilizador"];
                                lbtDisponibilidade.Visible = true;
                                lbtModulos.Visible = true;
                            }

                        }
                    }
                }
            }
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

        /// <summary>
        /// Função para editar os dados do utilizador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            User user = new User();
            List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);
           
                user.Nome = tbNome.Text;
            user.Email = tbEmail.Text;
            user.CodTipoDoc = Convert.ToInt32(ddlDocumentoIdent.SelectedValue);
            user.DocIdent = tbCC.Text;

            if (Security.IsValidDate(tbDataValidade.Text)) { user.DataValidade = Convert.ToDateTime(tbDataValidade.Text); }
            user.CodPrefix = Convert.ToInt32(ddlprefixo.SelectedValue);
            user.Phone = tbTelemovel.Text;

            user.CodUser = Convert.ToInt32(Session["CodUtilizador"].ToString());
            user.Sexo = Convert.ToInt32(ddlSexo.SelectedValue);
            if (Security.IsValidDate(tbDataNascimento.Text)) { user.DataNascimento = Convert.ToDateTime(tbDataNascimento.Text); }
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
            if (fuFoto.HasFile)
            {
                HttpPostedFile photoFile = fuFoto.PostedFile;

                byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                user.Foto = (Convert.ToBase64String(photoBytes));
            }

            user.CodGrauAcademico = Convert.ToInt32(ddlCodGrauAcademico.SelectedValue);
            user.CodSituacaoProf = Convert.ToInt32(ddlCodSituacaoProfissional.SelectedValue);
            user.LifeMotto = tbLifeMotto.Text;

            int CompleteUser;

            if (Classes.User.CheckSecondaryData(user.CodUser))
            {
                CompleteUser = Classes.User.UpdateUser(user, uploadedFiles);
            }
            else
            {
                CompleteUser = Classes.User.CompleteRegisterUser(user, uploadedFiles);
            }

            if (CompleteUser == 0)
            {
                lblMessage.CssClass = "alert alert-primary text-white text-center";
                lblMessage.Text = "Perfil atualizado com sucesso.";
                timerMessage.Enabled = true;

                User profileuser = Classes.User.LoadUser(Session["Username"].ToString());

                if (profileuser != null)
                {
                    profileSinapse.Visible = true;
                    registration.Visible = false;
                    registerCompletionpage1.Visible = false;
                    registerCompletionpage2.Visible = false;

                    //Carregar informação para as labels da página inicial do profile
                    profilename.Text = profileuser.Username;
                    profileemail.Text = profileuser.Email;
                    infoname.Text = profileuser.Nome;
                    infocell.Text = profileuser.Phone;
                    infoemail.Text = profileuser.Email;
                    lbLifeMotto.Text = profileuser.LifeMotto;
                    foto.ImageUrl = profileuser.Foto;
                }
            }
            else
            {
                lblMessage.CssClass = "alert alert-primary text-white text-center";
                lblMessage.Text = "Erro na atualização de perfil.";
                timerMessage.Enabled = true;

            }
        }

        /// <summary>
        /// Função para alterar a password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChangePW_Click(object sender, EventArgs e)
        {
            (bool password, List<string> failures) = Security.IsPasswordStrong(tbPwNew.Text);

            if (!password)
            {
                foreach (var failure in failures)
                {
                    lblMessage.CssClass = "alert alert-primary text-white text-center";
                    lblMessage.Visible = true;
                    lblMessage.Text += failure + "\n";
                    timerMessage.Enabled = true;

                }
            }
            else if (tbPwNew.Text != tbPwNewRep.Text)
            {
                lblMessage.CssClass = "alert alert-primary text-white text-center";
                lblMessage.Visible = true;
                lblMessage.Text = "A palavra-passe e a sua repetição não correspondem.";
                timerMessage.Enabled = true;

            }
            else
            {
                int ChangePass = Classes.Security.ChangePassword(tbEmail.Text, tbPwOld.Text, tbPwNew.Text);

                if (ChangePass == 1)
                {
                    lblMessage.CssClass = "alert alert-primary text-white text-center";
                    lblMessage.Visible = true;
                    lblMessage.Text = "Palavra-passe alterada com sucesso!";
                    timerMessage.Enabled = true;


                    Session.Clear();
                    Session.Abandon();
                    Response.AddHeader("REFRESH", "3; URL=MainPage.aspx");
                }
                else
                {
                    lblMessage.CssClass = "alert alert-primary text-white text-center";
                    lblMessage.Visible = true;
                    lblMessage.Text = "Palavra-passe atual incorreta!";
                    timerMessage.Enabled = true;

                }
            }
        }

        /// <summary>
        /// Função para mostrar os ficheiros submetidos pelo utilizador
        /// </summary>
        /// <returns></returns>
        private int LoadSubmittedFiles()
        {
            if ((Session["CodUtilizador"].ToString() != null))
            {
                int CodUtilizador = Convert.ToInt32(Session["CodUtilizador"].ToString());
                List<FileControl> files = FileControl.GetFilesForUser(CodUtilizador);

                if (files != null)
                {
                    if (files.Count > 0)
                    {
                        fileRepeater.DataSource = files;
                        fileRepeater.DataBind();

                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Função do Repeater FileRepeater que permite o download de cada ficheiro
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void fileRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                int fileId;
                if (int.TryParse(e.CommandArgument.ToString(), out fileId))
                {
                    Classes.FileControl.ProcessRequest(HttpContext.Current, fileId);
                }
            }
        }

        protected void btnBackMainPage_OnClick(object sender, EventArgs e)
        {
            registerCompletionpage1.Visible = false;
            registration.Visible = false;
            profileSinapse.Visible = true;
        }

        protected void btnNextPage_OnClick(object sender, EventArgs e)
        {
            registerCompletionpage1.Visible = false;
            registerCompletionpage2.Visible = true;
        }

        protected void btnBackToPage1_OnClick(object sender, EventArgs e)
        {
            registerCompletionpage2.Visible = false;
            registerCompletionpage1.Visible = true;
        }

        protected void editProfile_OnClick(object sender, EventArgs e)
        {
            profileSinapse.Visible = false;
            registration.Visible = true;
            registerCompletionpage1.Visible = true;
        }

        protected void lbtChangePW_OnClick(object sender, EventArgs e)
        {
            profileSinapse.Visible = !profileSinapse.Visible;
            ChangePw.Visible = !ChangePw.Visible;
        }

        protected void btnBackFromPwChange_OnClick(object sender, EventArgs e)
        {
            profileSinapse.Visible = true;
            ChangePw.Visible = false;
        }
        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
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

        protected void fileRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {


        }

        protected void timerMessage_OnTick(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            timerMessage.Enabled = false;
        }

        protected void lbtDisponibilidade_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("TeacherAvailability.aspx");
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string
                pathPDFs = ConfigurationManager
                    .AppSettings
                        ["PathPDFs"]; //Caminho dos PDFs colocado no WebConfig de modo a ser facilmente acessado e modificado

            string pdfTemplate = pathPDFs + "Template\\CinelPersonalFile.pdf"; //Caminho final do template

            string pathFinal = pathPDFs + $"Gerados\\Identification_{Session["User"]}";

            int num = 10;
            //int pageNumber = 1;
            List<string> pdfFiles = new List<string>();

            User profileuser = Classes.User.LoadUser(Session["User"].ToString());

            string classGroups = ClassGroup.LoadClassGroups(FromUser: profileuser.CodUser.ToString());

            string
                nomePDF = Classes.Security.EncryptString(num.ToString()) +
                          ".pdf"; //Gera o nome do pdf através da encriptação da data e hora do dia em que o pdf foi criado - encriptação MD5

            string novoFile = pathPDFs + nomePDF;

            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(pdfTemplate); //Instancia pdfReader para ler o pdfTemplate
            iTextSharp.text.pdf.PdfStamper pdfStamper = new iTextSharp.text.pdf.PdfStamper(pdfReader, new FileStream(novoFile, FileMode.Create));
            iTextSharp.text.pdf.AcroFields pdfFields = pdfStamper.AcroFields; //Encontra os AcroFields no pdfStamper
            pdfFields.SetField("tbNome",
                profileuser.Nome); //Escreve no novoFile no campo do pdf nome o texto da tb_nome

            pdfFields.SetField("tbDataNascimento", profileuser.DataNascimento.ToShortDateString());
            string sexo = (profileuser.Sexo.ToString() == "0") ? "Feminino" : "Masculino";
            pdfFields.SetField("tbSexo", sexo);
            pdfFields.SetField("tbMorada",
                profileuser.Morada + " | " + profileuser.CodPostal + " | " + profileuser.Localidade);
            pdfFields.SetField("tbDocIdent", profileuser.TipoDoc);
            pdfFields.SetField("tbNr", profileuser.DocIdent);
            pdfFields.SetField("tbDataValidade", profileuser.DataValidade.ToShortDateString());
            pdfFields.SetField("tbNIF", profileuser.NIF);
            pdfFields.SetField("tbNrSegSocial", profileuser.NrSegSocial);
            pdfFields.SetField("tbNacionalidade", profileuser.Nacionalidade);
            pdfFields.SetField("tbEmail", profileuser.Email);
            pdfFields.SetField("tbTelemovel", profileuser.Phone);
            pdfFields.SetField("tbSituacaoProfissional", profileuser.SituacaoProf);
            pdfFields.SetField("tbHabilitacoes", profileuser.GrauAcademico);
            pdfFields.SetField("tbIBAN", profileuser.IBAN);
            pdfFields.SetField("tbLifeMotto", profileuser.LifeMotto);
            byte[] imageData = profileuser.FotoBytes;

            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageData);

            // Scale the image if necessary
            image.ScaleToFit(100f, 100f); // Adjust width and height as needed

            image.SetAbsolutePosition(460, 670);
            // Add the image to the document
            iTextSharp.text.pdf.PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1); // Page number to add the image to
            pdfContentByte.AddImage(image);

            pdfStamper.Close(); //Fecha o pdfStamper
            pdfFiles.Add(novoFile);

            //// Define the PDF document and memory stream
            //Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            //MemoryStream msReport = new MemoryStream();

            //try
            //{
            //    using (var msReport = new MemoryStream())
            //    {
            //        using (var document = new Document(PageSize.A4, 50, 50, 50, 50))
            //        {
            //            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            //            document.Open();

            //            // Add user information
            //            PdfPTable userInfoTable = new PdfPTable(2);
            //            userInfoTable.AddCell("Name:");
            //            userInfoTable.AddCell(profileuser.Nome);
            //            userInfoTable.AddCell("Date of Birth:");
            //            userInfoTable.AddCell(profileuser.DataNascimento.ToShortDateString());
            //            // Add more user information cells as needed
            //            document.Add(userInfoTable);

            //            // Add module information for each class group
            //            foreach (ClassGroup classGroup in classGroups)
            //            {
            //                // Add a new page for each class group
            //                document.NewPage();

            //                // Add information for the first module of the class group
            //                foreach (Module module in classGroup.Modules)
            //                {
            //                    PdfPTable moduleInfoTable = new PdfPTable(2);
            //                    moduleInfoTable.AddCell("Module Name:");
            //                    moduleInfoTable.AddCell(module.Nome);
            //                    moduleInfoTable.AddCell("Credits:");
            //                    moduleInfoTable.AddCell(module.Creditos.ToString(CultureInfo.CurrentCulture));
            //                    // Add more module information cells as needed
            //                    document.Add(moduleInfoTable);
            //                }
            //            }
            //        }
            //        File.WriteAllBytes(pathFinal, msReport.ToArray());
            //        Console.WriteLine("PDF file saved successfully.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.Error.WriteLine("An error occurred while generating the PDF: " + ex.Message);
            //}


            byte[] mergedPdf = MergePdfFilesInFolder(pathPDFs);

            //Ficheiro guardado na pasta Gerados com o nome do Cliente
            string outputPath = Path.Combine(Server.MapPath("~"), "Pdfs\\Gerados", $"{Session["User"]}.pdf");
            File.WriteAllBytes(outputPath, mergedPdf);

            //string[] tempPdfFiles = Directory.GetFiles(pathPDFs, "*.pdf");
            //foreach (string tempPdfFile in tempPdfFiles)
            //{
            //    File.Delete(tempPdfFile);
            //}

            //Download do ficheiro para o cliente
            if (mergedPdf != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"attachment; filename={Session["User"]}.pdf");
                Response.BinaryWrite(mergedPdf);
                Response.End();
            }
        }

        protected void lbtAvaliacoes_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("TeacherEvaluations.aspx");
        }

        protected void lbtModulos_Click(object sender, EventArgs e)
        {
            BindDataModules();
            ModulesRegisterForTeacher.Visible = !ModulesRegisterForTeacher.Visible;
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

        protected void btnEnroll_OnClick(object sender, EventArgs e)
        {
            string teacherViewState = ViewState["SelectedTeacher"] as String;
            List<int> modulesSelected = new List<int>();

            if (Session["CodUtilizador"] != null)
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
                        Classes.Enrollment.DeleteEnrollmentTeacher(Convert.ToInt32(Session["CodUtilizador"]));

                        foreach (int selected in selectedItems)
                        {
                            Enrollment enrollment = new Enrollment();
                            enrollment.CodModulo = selected;
                            enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizador"]);
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


        /// <summary>
        /// Função para merge dos pdfs com template
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public byte[] MergePdfFilesInFolder(string folderPath)
        {
            //Lista de pdf da pasta folderPath
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            // Create a MemoryStream to hold the merged PDF
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a Document and PdfCopy instance
                using (Document document = new Document())
                using (PdfSmartCopy copy = new PdfSmartCopy(document, ms))
                {
                    document.Open();

                    foreach (string pdfFile in pdfFiles)
                    {
                        // Add each PDF file to the merged PDF
                        using (PdfReader reader = new PdfReader(pdfFile))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, i));
                            }
                        }
                    }
                }

                // Return the merged PDF as a byte array
                return ms.ToArray();
            }
        }

        protected void lbtEditUsername_Click(object sender, EventArgs e)
        {

        }
    }


}