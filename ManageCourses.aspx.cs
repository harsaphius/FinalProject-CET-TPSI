using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class ManageCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script;
            //// Initialize your list of items
            //List<Course> Courses = new List<Course>(); // Replace int with the type of your items
            //                                   // Add your items to the list...

            //// Define the desired page size
            //int pageSize = 2; // Change this to your desired page size

            //// Create an instance of PageControls with your items and page size
            //PageControls<int> pageControls = new PageControls<int>(Courses, pageSize);

            //// Get the current page number (for demonstration, assuming it's 1)
            //int currentPage = 1; // Change this to the actual current page number

            //// Generate pagination HTML

            //string paginationHtml = pageControls.GeneratePagination(currentPage, pageControls.GetTotalPages());

            //// Render the pagination HTML to the page
            //paginationContainer.InnerHtml = paginationHtml; // Wrapping paginationHtml in <li> for direct replacement
          
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

                rpt_Courses.DataSource = Classes.Course.LoadCourses();
                rpt_Courses.DataBind();

                if (!Page.IsPostBack)
                {
                    rpt_insertCourses.DataSource = Classes.Module.LoadModules();
                    rpt_insertCourses.DataBind();
                }
            }
        }

        protected void chkBoxMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)checkBox.NamingContainer;
            HiddenField hdnModuleId = (HiddenField)item.FindControl("hdnModuleId");
            HiddenField hdnModuleName = (HiddenField)item.FindControl("hdnModuleName");
            Label lbl_order = (Label)item.FindControl("lbl_order");

            if (hdnModuleId != null && hdnModuleName != null && lbl_order != null)
            {
                if (checkBox.Checked)
                {
                    lbl_order.Text = "Seleccionado";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"] ?? new List<int>();
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"] ?? new List<string>();
                    selectedItems.Add(Convert.ToInt32(hdnModuleId.Value));
                    itemsNames.Add(hdnModuleName.Value);
                    //lbl_selection.Text = string.Join(" | ", itemsNames);
                    ViewState["SelectedItems"] = selectedItems;
                    ViewState["SelectedItemsNames"] = itemsNames;
                }
                else
                {
                    lbl_order.Text = "Selecione este módulo";
                    List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
                    List<string> itemsNames = (List<string>)ViewState["SelectedItemsNames"];
                    if (selectedItems != null)
                    {
                        selectedItems.Remove(Convert.ToInt32(hdnModuleId.Value));
                        itemsNames.Remove(hdnModuleName.Value);
                        //lbl_selection.Text = string.Join(" | ", itemsNames);
                        ViewState["SelectedItems"] = selectedItems;
                        ViewState["SelectedItemsNames"] = itemsNames;
                    }
                }
            }
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            List<int> selectedItems = (List<int>)ViewState["SelectedItems"];
            List<string> courseData = new List<string>();

            if (selectedItems != null && selectedItems.Count > 0)
            {
                courseData.Add(tbCourseName.Text);
                courseData.Add(ddlTipoCurso.SelectedValue);
                courseData.Add(ddlAreaCurso.SelectedValue);
                courseData.Add(tbRef.Text);
                string selectedValue = ddlQNQ.SelectedValue;
                string[] parts = selectedValue.Split(' '); // Split the selected value by space
                if (parts.Length == 2) // Ensure there are two parts
                {
                    string codQNQ = parts[1];
                    courseData.Add(codQNQ);
                }

                int CourseRegisted = Classes.Course.InsertCourse(courseData, selectedItems);

                if (CourseRegisted == 1)
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Curso registado com sucesso!";

                    // Clear the ViewState after processing
                    ViewState["SelectedItems"] = null;

                }
                else
                {
                    string script = @"                      
                            document.getElementById('alert').classList.remove('hidden');
                            document.getElementById('alert').classList.add('alert');
                            document.getElementById('alert').classList.add('alert-primary');
                            ";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);

                    lbl_message.Text = "Curso já registado!";
                }
            }
        }

        protected void rpt_insertCourses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBoxMod = (CheckBox)e.Item.FindControl("chckBox");

                // Attach an event handler for the CheckedChanged event
                chkBoxMod.CheckedChanged += chkBoxMod_CheckedChanged;
            }
        }

        //protected void listCourses_Click(object sender, EventArgs e)
        //{
        //    string script = @"                      
        //                    document.getElementById('listCoursesDiv').classList.remove('hidden');
        //                    document.getElementById('insertCoursesDiv').classList.add('hidden');
        //                    document.getElementById('editCoursesDiv').classList.add('hidden');
        //                    ";

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDivElements", script, true);
        //}

        //protected void insertCourses_Click(object sender, EventArgs e)
        //{
        //    string script = @"                      
        //                    document.getElementById('listCoursesDiv').classList.add('hidden');
        //                    document.getElementById('insertCoursesDiv').classList.remove('hidden');
        //                    document.getElementById('editCoursesDiv').classList.add('hidden');
        //                    ";

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDivElements", script, true);

        //}

        //protected void editCourses_Click(object sender, EventArgs e)
        //{
        //    string script = @"                      
        //                    document.getElementById('listCoursesDiv').classList.add('hidden');
        //                    document.getElementById('insertCoursesDiv').classList.add('hidden');
        //                    document.getElementById('editCoursesDiv').classList.remove('hidden');
        //                    ";

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDivElements", script, true);
        //}
    }
}