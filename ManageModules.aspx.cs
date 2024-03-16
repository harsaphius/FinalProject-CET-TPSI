using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageModules : System.Web.UI.Page
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
                          document.getElementById('menuModules').classList.remove('hidden');
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

                rpt_Modules.DataSource = Classes.Module.LoadModules();
                rpt_Modules.DataBind();

            }
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            byte[] imageBytes;
            //Imagem identificativa do módulo - Funcionalidade passível de ser desenvolvida pelos designers
            if (fuSvgUFCD.HasFile)
            {
                using (BinaryReader reader = new BinaryReader(fuSvgUFCD.PostedFile.InputStream))
                {
                    imageBytes = reader.ReadBytes(fuSvgUFCD.PostedFile.ContentLength);
                }
            }
            else
            {
                string imagePath = "~/assets/img/small-logos/default.svg";
                string physicalPath = Server.MapPath(imagePath);

                using (FileStream fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read))
                {
                    imageBytes = new byte[fileStream.Length];
                    fileStream.Read(imageBytes, 0, (int)fileStream.Length);
                }
            }

            List<string> moduleData = new List<string>();
            moduleData.Add(tbModuleName.Text);
            moduleData.Add(tbDuration.Text);
            moduleData.Add(tbUFCD.Text);
            moduleData.Add(tbDescricao.Text);
            moduleData.Add(tbCredits.Text);

            int ModuleRegisted = Classes.Module.InsertModule(moduleData, imageBytes);

            if (ModuleRegisted == 1)
            {
                string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "Módulo registado com sucesso!";
            }
            else
            {
                string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                lbl_message.Text = "Módulo já registado!";
            }
        }

        protected void lbtn_edit_Click(object sender, EventArgs e)
        {

        }

        protected void rpt_editModules_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }


        protected void rpt_Modules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton Lbtn_edit = (LinkButton)e.Item.FindControl("lbt_edit");

            }
        }

        protected void rpt_Modules_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Find the RepeaterItem corresponding to the clicked LinkButton
                RepeaterItem item = (RepeaterItem)((LinkButton)e.CommandSource).NamingContainer;

                // Create TextBox controls dynamically and add them to the container control (e.g., Panel)
                Panel editPanel = (Panel)item.FindControl("editPanel");

                TextBox tbNome = new TextBox();
                tbNome.ID = "tbNome_" + e.Item.ItemIndex;
                tbNome.Text = ((Label)item.FindControl("lblNome")).Text;
                editPanel.Controls.Add(tbNome);

                TextBox tbUFCD = new TextBox();
                tbUFCD.ID = "tbUFCD_" + e.Item.ItemIndex;
                tbUFCD.Text = ((Label)item.FindControl("lblUFCD")).Text;
                editPanel.Controls.Add(tbUFCD);

                TextBox tbDescricao = new TextBox();
                tbDescricao.ID = "tbDescricao_" + e.Item.ItemIndex;
                tbDescricao.Text = ((Label)item.FindControl("lblDescricao")).Text;
                editPanel.Controls.Add(tbDescricao);
            }

        }
    }
}