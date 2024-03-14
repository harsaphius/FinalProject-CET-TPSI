using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script;
            // Initialize your list of items
            List<int> items = new List<int>(); // Replace int with the type of your items
                                               // Add your items to the list...

            // Define the desired page size
            int pageSize = 2; // Change this to your desired page size

            // Create an instance of PageControls with your items and page size
            PageControls<int> pageControls = new PageControls<int>(items, pageSize);

            // Get the current page number (for demonstration, assuming it's 1)
            int currentPage = 1; // Change this to the actual current page number

            // Generate pagination HTML
           
            string paginationHtml = pageControls.GeneratePagination(currentPage,pageControls.GetTotalPages());

            // Render the pagination HTML to the page
            paginationContainer.InnerHtml = paginationHtml; // Wrapping paginationHtml in <li> for direct replacement

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



                script = @"
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
                            document.getElementById('menuCourses').classList.remove('hidden');
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

            }
        }
        protected void btn_course_insert_Click(object sender, EventArgs e)
        {
            // Access the list of selected items from ViewState
            List<string> selectedItems = (List<string>)ViewState["SelectedItems"];
            List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"];

            if (selectedItems != null && selectedItems.Count > 0)
            {

                // Perform the desired processing, such as inserting data into the database
                foreach (string moduleId in selectedItems)
                {

                    // Insert data into the database for the selected module
                    // Replace this with your actual database insertion logic
                    // Example:
                    // InsertModuleIntoDatabase(moduleId);
                }

                // Clear the ViewState after processing
                ViewState["SelectedItems"] = null;


                // Optionally, you can display a message indicating that the operation was successful
                // Example:
                // lblMessage.Text = "Modules inserted successfully!";
            }
            else
            {
                // Optionally, handle the case where no modules are selected
                // Example:
                // lblMessage.Text = "No modules selected!";
            }
        }

        protected void chkBoxMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnModuleId = (HiddenField)item.FindControl("hdnModuleId");
            HiddenField hdnModuleName = (HiddenField)item.FindControl("hdnModuleName");
            Label lbl_order = (Label)item.FindControl("lbl_order");

            // Add or remove the module ID based on the checkbox state
            if (checkBox.Checked)
            {
                lbl_order.Text = "Seleccionado";

                // Add the module ID to the list of selected items
                List<string> selectedItems = (List<string>)ViewState["SelectedItems"] ?? new List<string>();
                List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"] ?? new List<string>();
                selectedItems.Add(hdnModuleId.Value);
                itemsNames.Add(hdnModuleName.Value);
                lbl_selection.Text = string.Join(" | ", itemsNames); ;
                ViewState["SelectedItems"] = selectedItems;
                ViewState["SelectedItemsNames"] = itemsNames;
            }
            else
            {
                lbl_order.Text = "Selecione este módulo";
                // Remove the module ID from the list of selected items
                List<string> selectedItems = (List<string>)ViewState["SelectedItems"];
                List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"];
                if (selectedItems != null)
                {
                    selectedItems.Remove(hdnModuleId.Value);
                    itemsNames.Remove(hdnModuleName.Value);
                    lbl_selection.Text = string.Join(" | ", itemsNames); ;
                    ViewState["SelectedItems"] = selectedItems;
                    ViewState["SelectedItemsNames"] = itemsNames;
                }
            }
        }
    }
}