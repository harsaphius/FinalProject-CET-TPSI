using FinalProject.Classes;
using System;
using System.Collections.Generic;
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

                string script = @"
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
            }

            //Reinicializar o FlatPickr
            if (IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "FlatpickrInit", @"
                        <script>
                            document.addEventListener('DOMContentLoaded', function() {
                                flatpickr('#<%= tbDataNascimento.ClientID %>', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    maxDate: new Date()
                                });

                                flatpickr('#<%= tbDataValidade.ClientID %>', {
                                    dateFormat: 'd-m-Y',
                                    theme: 'light',
                                    minDate: new Date()
                                });
                            });
                        </script>
                    ", false);
            }

            if (!IsPostBack)
            {
                BindDataTeachers();
                BindDataModules();
                BindDataUsers();
            }
        }


        //Funções de ItemDataBound dos Repeaters
        protected void rptUserForTeachers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxUser = (CheckBox)e.Item.FindControl("chkBoxUser");
                chkBoxUser.CheckedChanged += chkBoxUser_OnCheckedChanged;
            }
        }


        //Funções de ItemCommand dos Repeaters
        protected void rptUserForTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EditModulesTeachers")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showListModules",
                    "showListModules();", true);

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }
        }

        protected void rptListModulesForTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");
                chkBoxMod.CheckedChanged += chkBoxMod_OnCheckedChanged;
            }
        }

        protected void rptListTeachers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showInsert",
                    "showInsert();", true);
                InitializeFlatpickrDatePickers();

                (Teacher student, User user) = Classes.Teacher.LoadTeacher(Convert.ToInt32(e.CommandArgument));

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

            if (e.CommandName == "EditModulesTeachers")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showListModules",
                    "showListModules();", true);

                Session["CodUtilizadorClicked"] = e.CommandArgument.ToString();
            }

            if (e.CommandName == "Delete")
            {
                int AnswTeacherDeleted = Classes.Teacher.DeleteTeacher(Convert.ToInt32(e.CommandArgument));

                if (AnswTeacherDeleted == 1)
                {
                    BindDataTeachers();
                    lblMessageRegistration.Text = "Formador apagado com sucesso!";

                }
                else
                {
                    lblMessageRegistration.Text = "Formador não pode ser eliminado por fazer parte de uma turma a decorrer!";
                }
            }
        }


        //Funções de DataBinding
        private void BindDataUsers()
        {
            string condition =
                " LEFT JOIN inscricao AS I ON U.codUtilizador=I.codUtilizador WHERE I.codUtilizador IS NULL AND U.ativo = 1";

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers(condition);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 1;
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
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberModules;
            int PageNumber = PageNumberModules + 1;
            lblPageNumberListModulesForTeachers.Text = PageNumber.ToString();

            rptListModulesForTeachers.DataSource = pagedData;
            rptListModulesForTeachers.DataBind();

            btnPreviousListModulesForTeachers.Enabled = !pagedData.IsFirstPage;
            btnNextListModulesForTeachers.Enabled = !pagedData.IsLastPage;
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
        private void chkBoxMod_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnCourseID = (HiddenField)item.FindControl("hdnModuleID");
            HiddenField hdnCourseName = (HiddenField)item.FindControl("hdnModuleName");
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

        private void chkBoxUser_OnCheckedChanged(object sender, EventArgs e)
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
        }
        protected void btnNextListModulesForTeachers_OnClick(object sender, EventArgs e)
        {
            PageNumberModules += 1;
            BindDataModules();
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