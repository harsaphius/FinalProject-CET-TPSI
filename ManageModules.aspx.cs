using FinalProject.Classes;
using System;
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
                    BindDataModules();
                }
            }
        }

        protected void btnInsertModule_Click(object sender, EventArgs e)
        {
            Module moduleData = new Module();

            //Imagem identificativa do módulo - Funcionalidade passível de ser desenvolvida pelos designers
            if (fuSvgUFCD.HasFile)
            {
                using (BinaryReader reader = new BinaryReader(fuSvgUFCD.PostedFile.InputStream))
                {
                    moduleData.SVGBytes = reader.ReadBytes(fuSvgUFCD.PostedFile.ContentLength);
                }
            }
            else
            {
                byte[] imageBytes;
                string imagePath = "~/assets/img/small-logos/default.svg";
                string physicalPath = Server.MapPath(imagePath);

                using (FileStream fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read))
                {
                    imageBytes = new byte[fileStream.Length];
                    fileStream.Read(imageBytes, 0, (int)fileStream.Length);
                    moduleData.SVGBytes = imageBytes;
                }
            }

            moduleData.Nome = tbModuleName.Text;
            moduleData.Duracao = Convert.ToInt32(tbDuration.Text);
            moduleData.UFCD = tbUFCD.Text;
            moduleData.Descricao = tbDescricao.Text;
            moduleData.Creditos = Convert.ToDecimal(tbCredits.Text);

            int ModuleRegisted = Classes.Module.InsertModule(moduleData);

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

        protected void rpt_Modules_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                RepeaterItem item = rpt_Modules.Items[e.Item.ItemIndex];

                // Find TextBox and Label controls
                TextBox tbNome = (TextBox)item.FindControl("tbNome");
                Label lblNome = (Label)item.FindControl("lblNome");

                TextBox tbUFCD = (TextBox)item.FindControl("tbUFCD");
                Label lblUFCD = (Label)item.FindControl("lblUFCD");

                TextBox tbDuracao = (TextBox)item.FindControl("tbDuracao");
                Label lblDuracao = (Label)item.FindControl("lblDuracao");

                TextBox tbDescricao = (TextBox)item.FindControl("tbDescricao");
                Label lblDescricao = (Label)item.FindControl("lblDescricao");

                TextBox tbCreditos = (TextBox)item.FindControl("tbCreditos");
                Label lblCreditos = (Label)item.FindControl("lblCreditos");

                // Toggle visibility
                tbNome.Visible = !tbNome.Visible;
                lblNome.Visible = !lblNome.Visible;
                tbNome.Text = lblNome.Text;

                tbUFCD.Visible = !tbUFCD.Visible;
                lblUFCD.Visible = !lblUFCD.Visible;
                tbUFCD.Text = lblUFCD.Text;

                tbDuracao.Visible = !tbDuracao.Visible;
                lblDuracao.Visible = !lblDuracao.Visible;
                tbDuracao.Text = lblDuracao.Text;

                tbDescricao.Visible = !tbDescricao.Visible;
                lblDescricao.Visible = !lblDescricao.Visible;
                tbDescricao.Text = lblDescricao.Text;

                tbCreditos.Visible = !tbCreditos.Visible;
                lblCreditos.Visible = !lblCreditos.Visible;
                tbCreditos.Text = lblCreditos.Text;

                // Find the buttons
                LinkButton lbt_edit = (LinkButton)item.FindControl("lbtEditModules");
                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbtCancelModules");
                LinkButton lbt_delete = (LinkButton)item.FindControl("lbtDeleteModules");
                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbtConfirmModules");

                // Show "Cancel" and "Confirm" buttons
                lbt_cancel.Visible = true;
                lbt_confirm.Visible = true;

                // Hide "Edit" and "Delete" buttons
                lbt_edit.Visible = false;
                lbt_delete.Visible = false;
            }

            if (e.CommandName == "Confirm")
            {
                RepeaterItem item = rpt_Modules.Items[e.Item.ItemIndex];

                HiddenField hdnModuleID = (HiddenField)item.FindControl("hdnModuleID");
                string ModuleID = hdnModuleID.Value;

                // Find TextBox and Label controls
                TextBox tbNome = (TextBox)item.FindControl("tbNome");
                TextBox tbUFCD = (TextBox)item.FindControl("tbUFCD");
                TextBox tbDuracao = (TextBox)item.FindControl("tbDuracao");
                TextBox tbDescricao = (TextBox)item.FindControl("tbDescricao");
                TextBox tbCreditos = (TextBox)item.FindControl("tbCreditos");

                Label lblNome = (Label)item.FindControl("lblNome");
                Label lblUFCD = (Label)item.FindControl("lblUFCD");
                Label lblDuracao = (Label)item.FindControl("lblDuracao");
                Label lblDescricao = (Label)item.FindControl("lblDescricao");
                Label lblCreditos = (Label)item.FindControl("lblCreditos");

                // Find the buttons
                LinkButton lbt_edit = (LinkButton)item.FindControl("lbtEditModules");
                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbtCancelModules");
                LinkButton lbt_delete = (LinkButton)item.FindControl("lbtDeleteModules");
                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbtConfirmModules");

                if (Security.IsValidDecimal(tbCreditos.Text))
                {
                    Module module = new Module();

                    module.CodModulo = Convert.ToInt32(ModuleID);
                    module.Nome = tbNome.Text;
                    module.UFCD = tbUFCD.Text;
                    module.Descricao = tbDescricao.Text;
                    module.Duracao = Convert.ToInt32(tbDuracao.Text);
                    module.Creditos = Convert.ToDecimal(tbCreditos.Text);

                    int AnswModuleUpdate = Classes.Module.UpdateModule(module);

                    if (AnswModuleUpdate == 1)
                    {
                        tbNome.Visible = !tbNome.Visible;
                        lblNome.Visible = !lblNome.Visible;
                        lblNome.Text = tbNome.Text;

                        tbUFCD.Visible = !tbUFCD.Visible;
                        lblUFCD.Visible = !lblUFCD.Visible;
                        lblUFCD.Text = tbUFCD.Text;

                        tbDuracao.Visible = !tbDuracao.Visible;
                        lblDuracao.Visible = !lblDuracao.Visible;
                        lblDuracao.Text = tbDuracao.Text;

                        tbDescricao.Visible = !tbDescricao.Visible;
                        lblDescricao.Visible = !lblDescricao.Visible;
                        lblDescricao.Text = tbDescricao.Text;

                        tbCreditos.Visible = !tbCreditos.Visible;
                        lblCreditos.Visible = !lblCreditos.Visible;
                        lblCreditos.Text = tbCreditos.Text;

                        // Show "Cancel" and "Confirm" buttons
                        lbt_cancel.Visible = false;
                        lbt_confirm.Visible = false;

                        // Hide "Edit" and "Delete" buttons
                        lbt_edit.Visible = true;
                        lbt_delete.Visible = true;

                        lbl_message.Text = "Módulo atualizado com sucesso";
                    }
                    else if (AnswModuleUpdate == -2)
                    {
                        tbNome.Visible = !tbNome.Visible;
                        lblNome.Visible = !lblNome.Visible;
                        lblNome.Text = lblNome.Text;

                        tbUFCD.Visible = !tbUFCD.Visible;
                        lblUFCD.Visible = !lblUFCD.Visible;
                        lblUFCD.Text = lblUFCD.Text;

                        tbDuracao.Visible = !tbDuracao.Visible;
                        lblDuracao.Visible = !lblDuracao.Visible;
                        lblDuracao.Text = lblDuracao.Text;

                        tbDescricao.Visible = !tbDescricao.Visible;
                        lblDescricao.Visible = !lblDescricao.Visible;
                        lblDescricao.Text = lblDescricao.Text;

                        tbCreditos.Visible = !tbCreditos.Visible;
                        lblCreditos.Visible = !lblCreditos.Visible;
                        lblCreditos.Text = lblCreditos.Text;

                        // Show "Cancel" and "Confirm" buttons
                        lbt_cancel.Visible = false;
                        lbt_confirm.Visible = false;

                        // Hide "Edit" and "Delete" buttons
                        lbt_edit.Visible = true;
                        lbt_delete.Visible = true;

                        lbl_message.Text = "Módulo não pode ser atualizado por já se encontrar inserido num curso. Insira um novo módulo.";
                    }
                }
                else
                {
                    lbl_message.Text = "Os créditos deverão ser um decimal.";
                }
            }

            if (e.CommandName == "Delete")
            {
                RepeaterItem item = rpt_Modules.Items[e.Item.ItemIndex];

                HiddenField hdnModuleID = (HiddenField)item.FindControl("hdnModuleID");
                string ModuleID = hdnModuleID.Value;

                int AnswModuleDeleted = Classes.Module.DeleteModule(Convert.ToInt32(ModuleID));

                if (AnswModuleDeleted == 1)
                {
                    BindDataModules();
                    lbl_message.Text = "Módulo apagado com sucesso!";

                }
                else
                {
                    lbl_message.Text = "Módulo não pode ser eliminado por fazer parte do programa de um curso!";
                }


            }
        }

        protected void btnPreviousModule_Click(object sender, EventArgs e)
        {
            PageNumberModules -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
        }

        protected void btnNextModule_Click(object sender, EventArgs e)
        {
            PageNumberModules += 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
        }

        private void BindDataModules()
        {
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Module.LoadModules();
            pagedData.AllowPaging = true;
            pagedData.PageSize = 8;
            pagedData.CurrentPageIndex = PageNumberModules;
            ; // Adjust with the respective pagination helper instance

            rpt_Modules.DataSource = pagedData;
            rpt_Modules.DataBind();

            btnPreviousModule.Enabled = !pagedData.IsFirstPage; // Adjust with the respective btn_previous control for Users Repeater
            btnNextModule.Enabled = !pagedData.IsLastPage; // Adjust with the respective btn_next control for Users Repeater
        }

        public int PageNumberModules
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

        protected void rpt_Modules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the LinkButtons within the repeater item
                LinkButton lbtEditEditCourse = (LinkButton)e.Item.FindControl("lbtEditModules");
                LinkButton lbtCancelEditCourse = (LinkButton)e.Item.FindControl("lbtCancelModules");
                LinkButton lbtDeleteEditCourse = (LinkButton)e.Item.FindControl("lbtDeleteModules");
                LinkButton lbtConfirmEditCourse = (LinkButton)e.Item.FindControl("lbtConfirmModules");

                // Create AsyncPostBackTriggers for each LinkButton
                AsyncPostBackTrigger triggerEdit = new AsyncPostBackTrigger();
                triggerEdit.ControlID = lbtEditEditCourse.UniqueID;
                triggerEdit.EventName = "Click";

                AsyncPostBackTrigger triggerCancel = new AsyncPostBackTrigger();
                triggerCancel.ControlID = lbtCancelEditCourse.UniqueID;
                triggerCancel.EventName = "Click";

                AsyncPostBackTrigger triggerDelete = new AsyncPostBackTrigger();
                triggerDelete.ControlID = lbtDeleteEditCourse.UniqueID;
                triggerDelete.EventName = "Click";

                AsyncPostBackTrigger triggerConfirm = new AsyncPostBackTrigger();
                triggerConfirm.ControlID = lbtConfirmEditCourse.UniqueID;
                triggerConfirm.EventName = "Click";

                // Add the triggers to the UpdatePanel's Triggers collection
                updatePanel.Triggers.Add(triggerEdit);
                updatePanel.Triggers.Add(triggerCancel);
                updatePanel.Triggers.Add(triggerDelete);
                updatePanel.Triggers.Add(triggerConfirm);
            }

            FileUpload fileUpload = e.Item.FindControl("fileUpload") as FileUpload;
            if (fileUpload != null)
            {
                // Set an ID for the file upload control to identify it uniquely
                fileUpload.ID = "fileUpload_" + e.Item.ItemIndex.ToString();
                // You can set other properties as needed

                if (fileUpload.HasFile)
                {
                    // Get the uploaded file's name
                    string fileName = Path.GetFileName(fileUpload.FileName);

                    // Save the file to a specific location on the server
                    string filePath = Server.MapPath("~/Uploads/" + fileName);
                    fileUpload.SaveAs(filePath);

                    // You can also process the file contents here if needed

                    // Display a success message
                    Response.Write("File uploaded successfully!");
                }
                else
                {
                    // Display an error message if no file is selected
                    Response.Write("Please select a file to upload.");
                }
            }
        }
    }
}