using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected FileControl FileControlInstance;
        protected void Page_Load(object sender, EventArgs e)
        {
            string script;
            FileControlInstance = new FileControl();

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
                    document.getElementById('usercalendar').classList.remove('hidden');
                    document.getElementById('profile').classList.remove('hidden'); ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                if (Session["CodUtilizador"] != null && Session["CodUtilizador"].ToString() == "4" || Session["CodUtilizador"].ToString() == "1")
                {
                    script = @"                      
                            document.getElementById('management').classList.remove('hidden');
                            document.getElementById('managecourses').classList.remove('hidden');
                            document.getElementById('managecourses').classList.add('nav-item');
                            document.getElementById('manageclasses').classList.remove('hidden');
                            document.getElementById('manageclasses').classList.add('nav-item');
                            document.getElementById('managemodules').classList.remove('hidden');
                            document.getElementById('managemodules').classList.add('nav-item');
                            document.getElementById('managestudents').classList.remove('hidden');
                            document.getElementById('managestudents').classList.add('nav-item');
                            document.getElementById('manageteachers').classList.remove('hidden');
                            document.getElementById('manageteachers').classList.add('nav-item');
                            document.getElementById('manageclassrooms').classList.remove('hidden');
                            document.getElementById('manageclassrooms').classList.add('nav-item');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                //if (!IsPostBack)
                //{
                    User profileuser = Classes.User.LoadUser(user);
                    if (profileuser != null)
                    {
                        profilename.Text = profileuser.Username;
                        profileemail.Text = profileuser.Email;
                        infoname.Text = profileuser.Name;
                        infocell.Text = profileuser.Phone;
                        infoemail.Text = profileuser.Email;
                        lbLifeMotto.Text = profileuser.LifeMotto;

                        tbNome.Text = profileuser.Name;
                        ddlSexo.SelectedValue = profileuser.Sexo.ToString();
                        tbDataNascimento.Text = profileuser.DataNascimento.ToString();
                        ddlDocumentoIdent.SelectedValue = profileuser.CodTipoDoc.ToString();
                        tbCC.Text = profileuser.DocIdent;
                        tbDataValidade.Text = profileuser.DataValidade.ToString();
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
                        lbLifeMotto.Text = profileuser.LifeMotto;

                        //Falta processar anexos
                        LoadSubmittedFiles();

                    //}
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserProfile.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<string> userData = new List<string>();
            List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(fuAnexo);

            userData.Add(Convert.ToString(Session["CodUtilizador"]));
            userData.Add(tbNome.Text);
            userData.Add(tbEmail.Text);
            userData.Add(ddlDocumentoIdent.SelectedValue);
            userData.Add(tbCC.Text);
            userData.Add(tbDataValidade.Text);
            userData.Add(ddlprefixo.SelectedValue);
            userData.Add(tbTelemovel.Text);
            userData.Add(ddlSexo.SelectedValue);
            userData.Add(tbDataNascimento.Text);
            userData.Add(tbNIF.Text);
            userData.Add(tbMorada.Text);
            userData.Add(ddlCodPais.SelectedValue);
            userData.Add(tbCodPostal.Text);
            userData.Add(tbLocalidade.Text);
            userData.Add(ddlCodEstadoCivil.SelectedValue);
            userData.Add(tbNrSegSocial.Text);
            userData.Add(tbIBAN.Text);
            userData.Add(tbNaturalidade.Text);
            userData.Add(ddlCodNacionalidade.SelectedValue);
            HttpPostedFile photoFile = fuFoto.PostedFile;

            byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
            userData.Add(Convert.ToBase64String(photoBytes));

            userData.Add(ddlCodGrauAcademico.SelectedValue);
            userData.Add(ddlCodSituacaoProfissional.SelectedValue);
            userData.Add(tbLifeMotto.Text);
            

            Classes.User.completeRegisterUser(userData, uploadedFiles);
        }

        protected void btn_changepw_Click(object sender, EventArgs e)
        {
            (bool password, List<string> failures) = Security.IsPasswordStrong(tbEmail.Text);

            if (!password)
            {
                foreach (var failure in failures)
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text += failure + "\n";
                }
            }
            else if (tb_pw.Text != tb_pwr.Text)
            {
                string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "A palavra-passe e a sua repetição não correspondem.";
            }
            else
            {
                int ChangePass = Classes.Security.ChangePassword(tbEmail.Text, tb_pw.Text, tb_pwr.Text);
                if (ChangePass == 1)
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Palavra-passe alterada com sucesso!";

                    Session.Clear();
                    Session.Abandon();
                    Response.AddHeader("REFRESH", "3; URL=MainPage.aspx");

                }
                else
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Palavra-passe atual incorreta!";

                }
            }
        }

        public void LoadSubmittedFiles()
        {
            if ((Session["CodUtilizador"].ToString() != null))
            {
                int CodUtilizador = Convert.ToInt32(Session["CodUtilizador"].ToString());
                List<FileControl> files = FileControl.GetFilesForUser(CodUtilizador);

                fileRepeater.DataSource = files;
                fileRepeater.DataBind();
            }
        }
    }
}