using FinalProject.Classes;
using System;
using System.Collections.Generic;
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
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                if (!IsPostBack)
                {
                    //InitializeFlatpickrDatePickers();    
                    
                    BindDataStudents();
                    BindDataCourses();
                }

            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            User user = new User();

            if (Security.IsValidEmail(tbEmail.Text) == true)
            {
                string email = tbEmail.Text;

                user.CodPerfil = 2;
                user.Nome = tbNome.Text;
                string[] parts = email.Split('@');
                if (parts.Length == 2)
                {
                    string beforeAt = parts[0];
                    user.Username = beforeAt;
                }

                user.Email = tbEmail.Text;
                string NovaPasse = Membership.GeneratePassword(10, 2);
                user.Password = NovaPasse;
                user.DocIdent = ddlDocumentoIdent.SelectedValue;
                user.DocIdent = tbCC.Text;
                user.DataValidade = Convert.ToDateTime(tbDataValidade.Text);
                user.CodPrefix = Convert.ToInt32(ddlPrefixo.SelectedValue);
                user.Phone = tbTelemovel.Text;

                (int UserRegister, int userCode) = Classes.User.RegisterUser(user);

                if (UserRegister == 0)
                {
                    EmailControl.SendEmailActivationWithPW(tbEmail.Text, parts[0], NovaPasse);

                    user.CodUser = userCode;
                    user.Sexo = Convert.ToInt32(ddlSexo.SelectedValue);
                    user.DataNascimento = Convert.ToDateTime(tbDataNascimento.Text);
                    user.NIF = tbNIF.Text;
                    user.Morada = tbMorada.Text;
                    user.CodPais = Convert.ToInt32(ddlCodPais.SelectedValue);
                    user.CodPostal =tbCodPostal.Text;
                    user.Localidade = tbLocalidade.Text;
                    user.CodEstadoCivil = Convert.ToInt32(ddlCodEstadoCivil.SelectedValue);
                    user.NrSegSocial = tbNrSegSocial.Text;
                    user.IBAN = tbIBAN.Text;
                    user.Naturalidade = (tbNaturalidade.Text);
                    user.CodNacionalidade = Convert.ToInt32(ddlCodNacionalidade.SelectedValue);
                    HttpPostedFile photoFile = fuFoto.PostedFile;

                    byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                    user.Foto = (Convert.ToBase64String(photoBytes));

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
        

        protected void rptListStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsert",
                    "showInsert();", true);
                InitializeFlatpickrDatePickers();

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
        }

        protected void rptListCoursesForStudents_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");
                // Attach an event handler for the CheckedChanged event
                chkBoxMod.CheckedChanged += chkBoxMod_CheckedChanged;
            }
        }
        protected void chkBoxMod_CheckedChanged(object sender, EventArgs e)
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

        protected void btnEnroll_OnClick(object sender, EventArgs e)
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
            foreach (int selectedItem in selectedItems)
            {
                if (selectedItems != null && selectedItems.Count > 0)
                {
                    if (Session["CodUtilizador"] != null)
                    {
                        Enrollment enrollment = new Enrollment();

                        enrollment.CodUtilizador = Convert.ToInt32(Session["CodUtilizador"].ToString());
                        enrollment.CodSituacao = 1;
                        enrollment.CodCurso = selectedItem;

                        (int AnswAnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollment(enrollment);

                        if (AnswEnrollmentCode == -1 && AnswAnswEnrollmentRegister == -1)
                        {
                            lblMessageRegistration.Text = "Utilizador já registado nesse curso.";
                        }
                        else
                        {
                            Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizador"]), AnswEnrollmentCode);
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
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;

            rptListCoursesForStudents.DataSource = pagedData;
            rptListCoursesForStudents.DataBind();

            btnPreviousListCoursesForStudents.Enabled = !pagedData.IsFirstPage;
            btnNextListCoursesForStudents.Enabled = !pagedData.IsLastPage;
        }

        //Funções de movimentação na página
        protected void btnBackManageStudents_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ManageStudents.aspx");
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
            set
            {
                ViewState["PageNumberCourses"] = value;
            }
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
            set
            {
                ViewState["PageNumberStudents"] = value;
            }
        }


        //Funções para os botões de paginação
        protected void btnPreviousListStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
        }

        protected void btnNextListStudents_Click(object sender, EventArgs e)
        {
            PageNumberStudents += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
        }

        protected void btnPreviousListCoursesForStudents_Click(object sender, EventArgs e)
        {
            PageNumberCourses -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }

        protected void btnNextListCoursesForStudents_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }


        //Função de Reinicialização do FlatPickr
        private void InitializeFlatpickrDatePickers()
        {
            string script = @"
                        <script>
                            document.addEventListener('DOMContentLoaded', function() {
                                flatpickr('#" + tbDataNascimento.ClientID + @"', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    maxDate: new Date()
                                });

                                flatpickr('#" + tbDataValidade.ClientID + @"', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    minDate: new Date()
                                });
                            });
                        </script>
                    ";

            ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", script, false);
        }

    }
}