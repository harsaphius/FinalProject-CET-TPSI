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

                BindDataModules();
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

                TextBox tbDescricao = (TextBox)item.FindControl("tbDescricao");
                Label lblDescricao = (Label)item.FindControl("lblDescricao");

                // Toggle visibility
                tbNome.Visible = !tbNome.Visible;
                lblNome.Visible = !lblNome.Visible;

                tbUFCD.Visible = !tbUFCD.Visible;
                lblUFCD.Visible = !lblUFCD.Visible;

                tbDescricao.Visible = !tbDescricao.Visible;
                lblDescricao.Visible = !lblDescricao.Visible;

                // Find the buttons
                LinkButton lbt_edit = (LinkButton)item.FindControl("lbt_edit");
                LinkButton lbt_cancel = (LinkButton)item.FindControl("lbt_cancel");
                LinkButton lbt_delete = (LinkButton)item.FindControl("lbt_delete");
                LinkButton lbt_confirm = (LinkButton)item.FindControl("lbt_confirm");

                // Show "Cancel" and "Confirm" buttons
                lbt_cancel.Visible = true;
                lbt_confirm.Visible = true;

                // Hide "Edit" and "Delete" buttons
                lbt_edit.Visible = false;
                lbt_delete.Visible = false;
            }
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
        }

        protected void btn_previousM_Click(object sender, EventArgs e)
        {
            PageNumberModules -= 1; // Adjust with the respective PageNumber property for Users Repeater
            BindDataModules();
        }

        protected void btn_nextM_Click(object sender, EventArgs e)
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

            btn_previousM.Enabled = !pagedData.IsFirstPage; // Adjust with the respective btn_previous control for Users Repeater
            btn_nextM.Enabled = !pagedData.IsLastPage; // Adjust with the respective btn_next control for Users Repeater
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
            set
            {
                ViewState["PageNumberModules"] = value;
            }
        }

        protected void rpt_Modules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
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