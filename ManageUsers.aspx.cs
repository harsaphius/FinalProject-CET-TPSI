using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageUsers : System.Web.UI.Page
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
                            document.getElementById('manageschedules').classList.remove('hidden');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAdminElements", script, true);
                }

                if (!IsPostBack)
                {
                    BindDataUsers();
                }
            }
        }

        //Função de ItemDataBound
        /// <summary>
        /// Função de ItemDataBound do Repeater de Listagem dos Utilizadores para disabilitar as checkboxes e
        /// atribuir o valor de checked conforme valores da base de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptUsers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                User user = (User)e.Item.DataItem;

                CheckBox ckbAtivo = (CheckBox)e.Item.FindControl("ckbAtivo");
                CheckBox ckbFormando = (CheckBox)e.Item.FindControl("ckbFormando");
                CheckBox ckbFormador = (CheckBox)e.Item.FindControl("ckbFormador");
                CheckBox ckbFuncionario = (CheckBox)e.Item.FindControl("ckbFuncionario");

                LinkButton lbtEdit = (LinkButton)e.Item.FindControl("lbtEdit");
                if (lbtEdit != null)
                {
                    lbtEdit.CommandArgument = user.CodUser.ToString();
                }

                ckbAtivo.Checked = user.Ativo;
                ckbFormando.Checked = user.UserProfiles.Contains(2);
                ckbFormador.Checked = user.UserProfiles.Contains(3);
                ckbFuncionario.Checked = user.UserProfiles.Contains(4);

                ckbAtivo.Enabled = false;
                ckbFormando.Enabled = false;
                ckbFuncionario.Enabled = false;
                ckbFormador.Enabled = false;
            }
        }

        //Função de ItemCommand
        /// <summary>
        /// Função de ItemCommand do Repeater de Listagem dos Utilizadores para a Edição do Username, Email e Perfis de um utilizador
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptUsers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = rptUsers.Items[e.Item.ItemIndex];

            TextBox tbUsername = (TextBox)item.FindControl("tbUsername");
            Label lblUsername = (Label)item.FindControl("lblUsername");

            TextBox tbEmail = (TextBox)item.FindControl("tbEmail");
            Label lblEmail = (Label)item.FindControl("lblEmail");

            CheckBox ckbAtivo = (CheckBox)e.Item.FindControl("ckbAtivo");
            CheckBox ckbFormando = (CheckBox)e.Item.FindControl("ckbFormando");
            CheckBox ckbFormador = (CheckBox)e.Item.FindControl("ckbFormador");
            CheckBox ckbFuncionario = (CheckBox)e.Item.FindControl("ckbFuncionario");

            LinkButton lbtEditUser = (LinkButton)item.FindControl("lbtEditUser");
            LinkButton lbtCancelUser = (LinkButton)item.FindControl("lbtCancelUser");
            LinkButton lbtDeleteUser = (LinkButton)item.FindControl("lbtDeleteUser");
            LinkButton lbtConfirmUser = (LinkButton)item.FindControl("lbtConfirmUser");

            HiddenField hdCodUser = (HiddenField)item.FindControl("hdCodUser");
            string CodUser = hdCodUser.Value;

            if (e.CommandName == "Edit")
            {
                tbUsername.Visible = !tbUsername.Visible;
                lblUsername.Visible = !lblUsername.Visible;
                tbUsername.Text = lblUsername.Text;

                tbEmail.Visible = !tbEmail.Visible;
                lblEmail.Visible = !lblEmail.Visible;
                tbEmail.Text = lblEmail.Text;

                ckbAtivo.Enabled = true;
                ckbFormando.Enabled = true;
                ckbFuncionario.Enabled = true;
                ckbFormador.Enabled = true;

                lbtCancelUser.Visible = true;
                lbtConfirmUser.Visible = true;

                lbtEditUser.Visible = false;
                lbtDeleteUser.Visible = false;
            }
            if (e.CommandName == "Confirm")
            {
                List<int> userProfiles = new List<int>();

                userProfiles.Add((Convert.ToInt32(ckbAtivo.Checked ? "1" : "0")));
                userProfiles.Add((Convert.ToInt32(ckbFormando.Checked ? "1" : "0")));
                userProfiles.Add((Convert.ToInt32(ckbFormador.Checked ? "1" : "0")));
                userProfiles.Add((Convert.ToInt32(ckbFuncionario.Checked ? "1" : "0")));

                int AnswUserUpdated =
                    Classes.User.UpdateUsernameEmailAndProfiles(Convert.ToInt32(CodUser), tbUsername.Text, tbEmail.Text, userProfiles);

                if (AnswUserUpdated == 1)
                {
                    BindDataUsers();

                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Utilizador atualizado com sucesso";
                    timerMessageEdit.Enabled = true;
                }
                else
                {
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Não é possível atualizar este utilizador com estes dados, visto que já existem utilizadores registados com os mesmos dados.";
                    timerMessageEdit.Enabled = true;
                }
            }
            if (e.CommandName == "Delete")
            {
                int AnswUserDeleted = Classes.User.DeleteUser(Convert.ToInt32(CodUser));

                if (AnswUserDeleted == 1)
                {
                    BindDataUsers();
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Utilizador eliminado com sucesso!";
                    timerMessageEdit.Enabled = true;
                }
                else
                {
                    lblMessageEdit.Visible = true;
                    lblMessageEdit.CssClass = "alert alert-primary text-white text-center";
                    lblMessageEdit.Text = "Utilizador não pode ser eliminado por estar inserido numa turma!";
                    timerMessageEdit.Enabled = true;
                }
            }
            if (e.CommandName == "Cancel")
            {
                tbUsername.Visible = !tbUsername.Visible;
                lblUsername.Visible = !lblUsername.Visible;
                lblUsername.Text = lblUsername.Text;

                tbEmail.Visible = !tbEmail.Visible;
                lblEmail.Visible = !lblEmail.Visible;
                lblEmail.Text = lblEmail.Text;

                ckbAtivo.Enabled = false;
                ckbFormando.Enabled = false;
                ckbFuncionario.Enabled = false;
                ckbFormador.Enabled = false;

                lbtCancelUser.Visible = false;
                lbtConfirmUser.Visible = false;

                lbtEditUser.Visible = true;
                lbtDeleteUser.Visible = true;

            }

        }

        //Função de Databinding
        /// <summary>
        /// Função para DataBind dos Utilizadores
        /// </summary>
        private void BindDataUsers()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.User.LoadUsers();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 4;
            pagedData.CurrentPageIndex = PageNumberUsers;
            int PageNumber = PageNumberUsers + 1;
            lblPageNumberUsers.Text = PageNumber.ToString();

            rptUsers.DataSource = pagedData;
            rptUsers.DataBind();

            btnPreviousUser.Enabled = !pagedData.IsFirstPage;
            btnNextUser.Enabled = !pagedData.IsLastPage;
        }

        //Função de Paginação
        /// <summary>
        /// Paginação dos Utilizadores
        /// </summary>
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

        /// <summary>
        /// Função de Inserção de um novo utilizador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertUserMain_OnClick(object sender, EventArgs e)
        {
            insertUserDiv.Visible = true;
            listUsersDiv.Visible = false;
            btnInsertUserMain.Visible = false;
            btnBack.Visible = true;

            filtermenu.Visible = false;
            filters.Visible = false;
        }

        /// <summary>
        /// Função para voltar à página de listagem após inserção de novo utilizador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            listUsersDiv.Visible = true;
            insertUserDiv.Visible = false;
            btnInsertUserMain.Visible = true;
            btnBack.Visible = false;
            filtermenu.Visible = true;
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

        /// <summary>
        /// Função Click do Botão de Previous na Listagem de Utilizadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreviousUser_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers -= 1;
            BindDataUsers();
        }

        /// <summary>
        /// Função Click do Botão de Next na Listagem de Utilizadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextUser_OnClick(object sender, EventArgs e)
        {
            PageNumberUsers += 1;
            BindDataUsers();
        }

        protected void btnSignup_OnClick(object sender, EventArgs e)
        {
            (bool password, List<string> failures) = Security.IsPasswordStrong(tb_pw.Text);

            if (!Security.IsValidEmail(tb_email.Text))
            {
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "Introduza um e-mail válido!";
                timerMessageInsert.Enabled = true;
            }
            else if (!password)
            {
                foreach (var failure in failures)
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    lblMessageInsert.Text += failure + "\n";
                    timerMessageInsert.Enabled = true;
                }
            }
            else if (tb_pw.Text != tb_pwR.Text)
            {
                lblMessageInsert.Visible = true;
                lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                lblMessageInsert.Text = "A palavra-passe e a sua repetição não correspondem.";
                timerMessageInsert.Enabled = true;
            }
            else
            {
                User user = new User();

                user.CodPerfil = Convert.ToInt32(ddlPerfil.SelectedValue);
                user.Nome = tb_name.Text;
                user.Username = tb_username.Text;
                user.Email = tb_email.Text;
                user.Password = tb_pw.Text;
                user.CodTipoDoc = Convert.ToInt32(ddl_tipoDocIdent.SelectedValue);
                user.DocIdent = tbCC.Text;
                user.DataValidade = Convert.ToDateTime(tbdataValidade.Text);
                user.CodPrefix = Convert.ToInt32(ddlprefixo.SelectedValue);
                user.Phone = tbTelemovel.Text;

                (int UserRegister, int UserCode) = Classes.User.RegisterUser(user);

                if (UserRegister == 1)
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    if (Convert.ToInt32(ddlPerfil.SelectedValue) == 2) EmailControl.SendEmailActivation(tb_email.Text, tb_username.Text);
                    if (Convert.ToInt32(ddlPerfil.SelectedValue) == 3 || Convert.ToInt32(ddlPerfil.SelectedValue) == 4) EmailControl.SendEmailWaitingValidation(tb_email.Text, tb_username.Text);
                    lblMessageInsert.Text = "Utilizador registado com sucesso!";
                    timerMessageInsert.Enabled = true;

                }
                else
                {
                    lblMessageInsert.Visible = true;
                    lblMessageInsert.CssClass = "alert alert-primary text-white text-center";
                    lblMessageInsert.Text =
                        $"Utilizador já registado! Se não se lembra da sua password recupere a sua conta <a href='{ConfigurationManager.AppSettings["SiteURL"]}/UserLogin.aspx'> link </a>!";
                    timerMessageInsert.Enabled = true;

                }
            }
        }
        protected void timerMessageInsert_OnTick(object sender, EventArgs e)
        {
            lblMessageInsert.Visible = false;
            timerMessageInsert.Enabled = false;
        }

        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
        }

    }
}