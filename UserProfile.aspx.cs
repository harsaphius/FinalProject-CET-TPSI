using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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

                if (!IsPostBack)
                {
                    User profileuser = Classes.User.LoadUser(user);
                    if (profileuser != null)
                    {
                        profilename.Text = profileuser.Username;
                        profileemail.Text = profileuser.Email;
                        infoname.Text = profileuser.Name;
                        infocell.Text = profileuser.Phone;
                        infoemail.Text = profileuser.Email;
                        tbNome.Text = profileuser.Name;
                        
                        ddlSexo.SelectedValue = profileuser.Sexo.ToString();
                        tbDataNascimento.Text = profileuser.DataNascimento.ToString();
                        ddlDocumentoIdent.SelectedValue = profileuser.CodTipoDoc.ToString();
                        tbCC.Text = profileuser.DocIdent;
                        tbDataValidade.Text = profileuser.DataValidade.ToString();
                        tbNrSegSocial.Text = profileuser.NrSegSocial;
                        tbNIF.Text = profileuser.NIF;
                        tbMorada.Text = profileuser.Morada;
                        tbCodPostal.Text = profileuser.CodPostal;
                        foto.ImageUrl = "data:image/jpeg;base64," + profileuser.Foto;

                        ddlCodPais.SelectedValue= profileuser.CodPais.ToString();
                        ddlCodEstadoCivil.SelectedValue = profileuser.CodEstadoCivil.ToString();
                        tbIBAN.Text = profileuser.IBAN;
                        tbNaturalidade.Text = profileuser.Naturalidade;
                        ddlCodNacionalidade.SelectedValue = profileuser.CodNacionalidade.ToString();
                        ddlCodSituacaoProfissional.SelectedValue = profileuser.CodSituacaoProf.ToString();
                        ddlprefixo.SelectedValue = profileuser.CodPrefix.ToString();
                        tbTelemovel.Text = profileuser.Phone;
                        tbEmail.Text = profileuser.Email;
                        ddlCodGrauAcademico.SelectedValue = profileuser.CodGrauAcademico.ToString();

                        //Falta processar anexos

                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserProfile.aspx");
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            List<FileControl> uploadedFiles = FileControl.ProcessUploadedFiles(Request.Files);
            List<string> userData = new List<string>();
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
            userData.Add(tbfreguesia.Text);
            userData.Add(ddlCodEstadoCivil.SelectedValue);
            userData.Add(tbNrSegSocial.Text);
            userData.Add(tbIBAN.Text);
            userData.Add(tbNaturalidade.Text);
            userData.Add(ddlCodNacionalidade.SelectedValue);
            byte[] fileBytes;

            if (fuFoto.HasFile)
            {
                using (BinaryReader reader = new BinaryReader(fuFoto.PostedFile.InputStream))
                {
                    fileBytes = reader.ReadBytes(fuFoto.PostedFile.ContentLength);
                }

                string forFoto = Convert.ToBase64String(fileBytes);
                userData.Add(forFoto);
            }
            else
            {
                string imagePath = "~/assets/img/small-logos/default.svg";
                string physicalPath = Server.MapPath(imagePath);

                using (FileStream fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read))
                {
                    fileBytes = new byte[fileStream.Length];
                    fileStream.Read(fileBytes, 0, (int)fileStream.Length);
                }

                string forFoto = Convert.ToBase64String(fileBytes);
                userData.Add(forFoto);
            }


            userData.Add(ddlCodGrauAcademico.SelectedValue);
            userData.Add(ddlCodSituacaoProfissional.SelectedValue);

            Classes.User.completeRegisterUser(userData, uploadedFiles);
            //Classes.User.updateRegisterUser(userData, uploadedFiles);

        }
    }
}