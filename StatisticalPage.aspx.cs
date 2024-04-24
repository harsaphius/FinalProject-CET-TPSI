using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class StatisticalPage : System.Web.UI.Page
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


                    if (!IsPostBack)
                    {
                        GetAndDisplayStatisticalData(out int totalCursos, out int totalCursosDecorrer, out int totalFormando);
                        lblClassgroupOver.Text = totalCursos.ToString();
                        lblClassgroupOngoing.Text = totalCursosDecorrer.ToString();
                        lblStudentsOngoing.Text = totalFormando.ToString();
                    }
                }
            }
        }
        public static void GetAndDisplayStatisticalData(out int totalCursos, out int totalCursosDecorrer, out int totalFormando)
        {
            totalCursos = 0;
            totalCursosDecorrer = 0;
            totalFormando = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("StatisticalData", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Parâmetros de saída
                SqlParameter totalCursosParam = new SqlParameter("@TotalCursos", SqlDbType.Int);
                totalCursosParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(totalCursosParam);

                SqlParameter totalCursosDecorrerParam = new SqlParameter("@TotalCursosDecorrer", SqlDbType.Int);
                totalCursosDecorrerParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(totalCursosDecorrerParam);

                SqlParameter totalFormandoParam = new SqlParameter("@TotalFormando", SqlDbType.Int);
                totalFormandoParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(totalFormandoParam);

                connection.Open();
                command.ExecuteNonQuery();

                // Atribuir valores dos parâmetros de saída às variáveis de saída
                totalCursos = (int)totalCursosParam.Value;
                totalCursosDecorrer = (int)totalCursosDecorrerParam.Value;
                totalFormando = (int)totalFormandoParam.Value;
            }
        }
    }
}
