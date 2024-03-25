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

                //Reinicializar o FlatPickr
                if (!IsPostBack)
                {
                    InitializeFlatpickrDatePickers();
                }

                BindDataStudents();
                BindDataCourses();
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<string> userData = new List<string>();
            List<string> userDataSecondary = new List<string>();

            if (Security.IsValidEmail(tbEmail.Text) == true)
            {
                string email = tbEmail.Text;

                userData.Add(Convert.ToString(2));
                userData.Add(tbNome.Text);

                string[] parts = email.Split('@');
                if (parts.Length == 2)
                {
                    string beforeAt = parts[0];
                    userData.Add(beforeAt);
                }

                userData.Add(tbEmail.Text);
                string NovaPasse = Membership.GeneratePassword(10, 2);
                userData.Add(NovaPasse);
                userData.Add(ddlDocumentoIdent.SelectedValue);
                userData.Add(tbCC.Text);
                userData.Add(tbDataValidade.Text);
                userData.Add(ddlprefixo.SelectedValue);
                userData.Add(tbTelemovel.Text);

                (int UserRegister, int userCode) = Classes.User.RegisterUser(userData);

                if (UserRegister == 0)
                {
                    EmailControl.SendEmailActivationWithPW(tbEmail.Text, parts[0], NovaPasse);

                    userDataSecondary.Add(Convert.ToString(userCode));
                    userDataSecondary.Add(ddlSexo.SelectedValue);
                    userDataSecondary.Add(tbDataNascimento.Text);
                    userDataSecondary.Add(tbNIF.Text);
                    userDataSecondary.Add(tbMorada.Text);
                    userDataSecondary.Add(ddlCodPais.SelectedValue);
                    userDataSecondary.Add(tbCodPostal.Text);
                    userDataSecondary.Add(tbLocalidade.Text);
                    userDataSecondary.Add(ddlCodEstadoCivil.SelectedValue);
                    userDataSecondary.Add(tbNrSegSocial.Text);
                    userDataSecondary.Add(tbIBAN.Text);
                    userDataSecondary.Add(tbNaturalidade.Text);
                    userDataSecondary.Add(ddlCodNacionalidade.SelectedValue);
                    HttpPostedFile photoFile = fuFoto.PostedFile;

                    byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                    userDataSecondary.Add(Convert.ToBase64String(photoBytes));

                    userDataSecondary.Add(ddlCodGrauAcademico.SelectedValue);
                    userDataSecondary.Add(ddlCodSituacaoProfissional.SelectedValue);
                    userDataSecondary.Add(""); //Substituir LifeMotto

                    List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);

                    int CompleteUser = Classes.User.CompleteRegisterUser(userDataSecondary, uploadedFiles);


                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), key: "ShowPageElements", script, true);

                    lbl_message.Text = "Formando registado com sucesso!";
                }
                else
                {
                    string script = @"
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Formando já registado!";
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

                lbl_message.Text = "Introduza um e-mail válido!";
            }

        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageSudents.aspx?Insert");
        }

        protected void btn_previousP_Click(object sender, EventArgs e)
        {
            PageNumberStudents -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
        }

        protected void btn_nextP_Click(object sender, EventArgs e)
        {
            PageNumberStudents += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataStudents();
        }

        protected void btn_previousC_Click(object sender, EventArgs e)
        {
            PageNumberCourses -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }

        protected void btn_nextC_Click(object sender, EventArgs e)
        {
            PageNumberCourses += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataCourses();
        }

        private void BindDataCourses()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 2;
            pagedData.CurrentPageIndex = PageNumberCourses;

            rpt_Courses.DataSource = pagedData;
            rpt_Courses.DataBind();

            btn_previousC.Enabled = !pagedData.IsFirstPage;
            btn_nextC.Enabled = !pagedData.IsLastPage;
        }

        public int PageNumberCourses
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

        private void BindDataStudents()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Student.LoadStudents();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberStudents;
            ;

            rpt_Students.DataSource = pagedData;
            rpt_Students.DataBind();

            btn_previousP.Enabled = !pagedData.IsFirstPage;
            btn_nextP.Enabled = !pagedData.IsLastPage;
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

        protected void rpt_Students_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void rpt_Courses_OnItemCommand(object source, RepeaterCommandEventArgs e)
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

        protected void btn_enroll_OnClick(object sender, EventArgs e)
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
            foreach (int selectedItem in selectedItems)
            {
                if (selectedItems != null && selectedItems.Count > 0)
                {
                    if (Session["CodUtilizador"] != null)
                    {
                        List<string> Enrollment = new List<string>();

                        Enrollment.Add(Session["CodUtilizador"].ToString());
                        Enrollment.Add("1");
                        Enrollment.Add(Convert.ToString(selectedItem));

                        (int AnswAnswEnrollmentRegister, int AnswEnrollmentCode) = Classes.Enrollment.InsertEnrollment(Enrollment);

                        if (AnswEnrollmentCode == -1 && AnswAnswEnrollmentRegister == -1)
                        {
                            lbl_message.Text = "Utilizador já registado nesse curso.";
                        }
                        else
                        {
                            Classes.Student.InsertStudent(Convert.ToInt32(Session["CodUtilizador"]), AnswEnrollmentCode);
                            lbl_message.Text = "Utilizador registado com sucesso no curso!";
                        }

                    }
                }

            }
        }
    }
}