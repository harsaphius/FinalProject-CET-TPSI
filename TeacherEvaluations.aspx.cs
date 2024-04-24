using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class TeacherEvaluations : System.Web.UI.Page
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

                string userCode = Session["CodUtilizador"].ToString();
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
                }
                if (Session["CodFormador"] != null)
                {
                    string codFormador = Session["CodFormador"].ToString();

                    rptClassGroups.DataSource = Classes.ClassGroup.LoadClassGroupsByFormador(codFormador);
                    rptClassGroups.DataBind();
                }

            }
        }

        protected void rptClassGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptModules = e.Item.FindControl("rptModules") as Repeater;
                string codFormador = Session["CodFormador"].ToString();

                string codTurma = "";
                HiddenField hdnCodTurma = e.Item.FindControl("hdnCodTurma") as HiddenField;
                if (hdnCodTurma != null)
                {
                    codTurma = hdnCodTurma.Value;
                }

                ClassGroup classGroup = e.Item.DataItem as ClassGroup;
                if (classGroup != null)
                {
                    rptModules.DataSource = Module.LoadModulesByClass(codTurma, codFormador);
                    rptModules.DataBind();
                }
            }
        }

        protected void rptModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem rptClassGroups = (RepeaterItem)e.Item.Parent.Parent;
                Repeater rptStudents = e.Item.FindControl("rptStudents") as Repeater;
                string codFormador = Session["CodFormador"].ToString();
                rptStudents.ItemCommand += rptStudents_ItemCommand; // Adiciona o manipulador de evento ao Repeater interno

                foreach (RepeaterItem item in rptStudents.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        LinkButton lbtEvaluations = (LinkButton)item.FindControl("lbtEvaluations");
                        LinkButton lbtnCancel = (LinkButton)item.FindControl("lbtnCancel");
                        LinkButton lbtnCommit = (LinkButton)item.FindControl("lbtnCommit");
                        HiddenField hdnCodTurmaInner = (HiddenField)item.FindControl("hdnCodTurmaInner");

                    }
                }
                HiddenField hdnCodTurma = (HiddenField)rptClassGroups.FindControl("hdnCodTurma");
                string codTurma = "", codModulo = "";

                if (hdnCodTurma != null) { codTurma = hdnCodTurma.Value; }
                HiddenField hdnCodModulo = e.Item.FindControl("hdnCodModulo") as HiddenField;
                if (hdnCodModulo != null) { codModulo = hdnCodModulo.Value; }

                rptStudents.DataSource = ClassGroup.GetStudentsByTurmaModuloFormador(Convert.ToInt32(codTurma), Convert.ToInt32(codModulo), Convert.ToInt32(codFormador));
                rptStudents.DataBind();

            }
        }

        protected void timerMessageEdit_OnTick(object sender, EventArgs e)
        {
            lblMessageEdit.Visible = false;
            timerMessageEdit.Enabled = false;
        }

        protected void rptStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblAvaliacao = (Label)e.Item.FindControl("lblAvaliacao");
            TextBox tbAvaliacao = (TextBox)e.Item.FindControl("tbAvaliacao");
            if (e.CommandName == "Evaluate")
            {
                tbAvaliacao.Visible = true;
                lblAvaliacao.Visible = false;

            }
            if (e.CommandName == "Cancel")
            {
                tbAvaliacao.Visible = false;
                lblAvaliacao.Visible = true;

            }
            if (e.CommandName == "Commit")
            {
                HiddenField hdnCodTurma = (HiddenField)e.Item.FindControl("hdnCodTurmaInner");
                HiddenField hdnCodModulo = (HiddenField)e.Item.FindControl("hdnCodModulo");

                if (hdnCodTurma != null && hdnCodModulo != null)
                {
                    int codTurma = Convert.ToInt32(hdnCodTurma.Value);
                    int codModulo = Convert.ToInt32(hdnCodModulo.Value);
                    // Agora você tem o codTurma disponível para uso

                    int codModuloFormador = ClassGroup.ObterCodModuloFormador(codTurma, codModulo, Convert.ToInt32(Session["CodFormador"]));

                    if (Security.IsValidDecimal(tbAvaliacao.Text))
                    {
                        ClassGroup.InserirAvaliacao(Convert.ToInt32(e.CommandArgument), codModuloFormador, codTurma, Convert.ToDecimal(tbAvaliacao.Text));
                    }
                    else
                    {
                        lblMessageEdit.Text = "Introduza um decimal válido!";
                    }


                    tbAvaliacao.Visible = true;
                    lblAvaliacao.Visible = false;
                }
            }
        }
    }
}
