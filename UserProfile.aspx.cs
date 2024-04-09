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
                            document.getElementById('manageusers').classList.remove('hidden');

                            ";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                    }

                    if (!Page.IsPostBack)
                    {
                        User profileuser = Classes.User.LoadUser(user);
                        (int CodUtilizador, string Username) = Classes.User.DetermineUtilizador(user);
                        Session["Username"] = Username;

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
                                lbtDisponibilidade.Visible = true;
                            }
                        }
                    }
                }
            }
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
            user.DataValidade = Convert.ToDateTime(tbDataValidade.Text);
            user.CodPrefix = Convert.ToInt32(ddlprefixo.SelectedValue);
            user.Phone = tbTelemovel.Text;

            user.CodUser = Convert.ToInt32(Session["CodUtilizador"].ToString());
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
            if (fuFoto.HasFile)
            {
                HttpPostedFile photoFile = fuFoto.PostedFile;

                byte[] photoBytes = FileControl.ProcessPhotoFile(photoFile);
                user.Foto = (Convert.ToBase64String(photoBytes));
            }
            user.CodGrauAcademico = Convert.ToInt32(ddlCodGrauAcademico.SelectedValue);
            user.CodSituacaoProf = Convert.ToInt32(ddlCodSituacaoProfissional.SelectedValue);
            user.LifeMotto = tbLifeMotto.Text;

            int CompleteUser = Classes.User.CompleteRegisterUser(user, uploadedFiles);

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




        protected void fileRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {


        }

        protected void timerMessage_OnTick(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            timerMessage.Enabled = false;
        }
    }
}