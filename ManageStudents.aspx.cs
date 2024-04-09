﻿using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
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
                    BindDataStudents();
                    BindDataCourses();
                    BindDataUsers();
                }

            }
        }

        //Funções de ItemDataBound dos Repeaters
        protected void rptUserForStudents_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxUser = (CheckBox)e.Item.FindControl("chkBoxUser");
                chkBoxUser.CheckedChanged += chkBoxUser_OnCheckedChanged;
            }
        }

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

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }

            if (e.CommandName == "Delete")
            {
                int AnswStudentDeleted = Classes.Student.DeleteStudent(Convert.ToInt32(e.CommandArgument));

                if (AnswStudentDeleted == 1)
                {
                    BindDataStudents();
                    lblMessageRegistration.Text = "Formando apagado com sucesso!";
                }
                else
                {
                    lblMessageRegistration.Text = "Formando não pode ser eliminado por fazer parte de uma turma a decorrer!";
                }
            }

        }

        protected void rptUserForStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditCoursesStudents")
            {
                listUsersDiv.Visible = false;
                CourseRegisterForStudent.Visible = true;
                btnInsertStudentFromList.Visible = false;
                btnInsertStudentMain.Visible = false;
                btnBack.Visible = true;

                filtermenu.Visible = false;
                filters.Visible = false;

                hdnSourceDiv.Value = "listUsersDiv";

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }
        }

        protected void rptListCoursesForStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");
                chkBoxMod.CheckedChanged += chkBoxMod_OnCheckedChanged;
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

                    lblMessageRegistration.Text = "Formando registado com sucesso!";

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

                    lblMessageRegistration.Text = "Formando já registado!";
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

                lblMessageRegistration.Text = "Introduza um e-mail válido!";
            }

        }

        /// <summary>
        /// Função Click do Botão de Inscrição num curso do aluno clicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnroll_OnClick(object sender, EventArgs e)
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItems"];

            foreach (int selectedItem in selectedItems)
            {
                if (selectedItems != null && selectedItems.Count > 0)
                {
                    if (Session["CodUtilizadorClicked"] != null)
                    {
                        Enrollment enrollment = new Enrollment();

                        enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizadorClicked"]);
                        enrollment.CodSituacao = 1;
                        enrollment.CodCurso = selectedItem;

                        (int AnswAnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollmentStudent(enrollment);

                        if (AnswEnrollmentCode == -1 && AnswAnswEnrollmentRegister == -1)
                        {
                            lblMessageRegistration.Text = "Utilizador já registado nesse curso.";
                        }
                        else
                        {
                            Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizadorClicked"]), AnswEnrollmentCode);
                            lblMessageRegistration.Text = "Utilizador registado com sucesso no curso!";
                        }
                    }


                }

            }
        }


        //Funções de DataBinding
        private void BindDataStudents()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Student.LoadStudents();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
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
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 5;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberListCoursesForStudents.Text = PageNumber.ToString();

            rptListCoursesForStudents.DataSource = pagedData;
            rptListCoursesForStudents.DataBind();

            btnPreviousListCoursesForStudents.Enabled = !pagedData.IsFirstPage;
            btnNextListCoursesForStudents.Enabled = !pagedData.IsLastPage;
        }

        private void BindDataUsers()
        {
            string condition =
                " LEFT JOIN inscricao AS I ON U.codUtilizador=I.codUtilizador WHERE I.codUtilizador IS NULL AND U.ativo = 1";

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers(condition);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 3;
            pagedData.CurrentPageIndex = PageNumberUsers;
            int PageNumber = PageNumberUsers + 1;
            lblPageNumbersUsersForStudents.Text = PageNumber.ToString();

            rptUserForStudents.DataSource = pagedData;
            rptUserForStudents.DataBind();

            btnPreviousUsersForStudents.Enabled = !pagedData.IsFirstPage;
            btnNextsUsersForStudents.Enabled = !pagedData.IsLastPage;
        }

        //Funções para as CheckBoxes
        protected void chkBoxMod_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnCourseID = (HiddenField)item.FindControl("hdnCourseID");
            HiddenField hdnCourseName = (HiddenField)item.FindControl("hdnCourseName");
            Label lblSelected = (Label)item.FindControl("lbl_order");

            if (hdnCourseID != null && hdnCourseName != null && lblSelected != null)
            {
                if (checkBox.Checked)
                {
                    lblSelected.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnCourseID.Value));
                    itemsNames.Add(hdnCourseName.Value);
                    ViewState["SelectedItems"] = selectedItems;
                    ViewState["SelectedItemsNames"] = itemsNames;
                }
                else
                {
                    lblSelected.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnCourseID.Value));
                        itemsNames.Remove(hdnCourseName.Value);
                        ViewState["SelectedItems"] = selectedItems;
                        ViewState["SelectedItemsNames"] = itemsNames;
                    }
                }
            }
        }

        protected void chkBoxUser_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnUserID = (HiddenField)item.FindControl("hdnUserID");

            if (hdnUserID != null)
            {
                if (checkBox.Checked)
                {
                    List<int> selectedItems = (List<int>)ViewState["SelectedUsers"] ?? new List<int>();
                    selectedItems.Add(Convert.ToInt32(hdnUserID.Value));
                    ViewState["SelectedUsers"] = selectedItems;
                }
                else
                {
                    List<int> selectedItems = (List<int>)ViewState["SelectedUsers"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnUserID.Value));
                        ViewState["SelectedUsers"] = selectedItems;
                    }
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
        }

        protected void btnNextListCoursesForStudents_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1;
            BindDataCourses();
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
    }
}