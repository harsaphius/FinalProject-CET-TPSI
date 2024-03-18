using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class MainCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rpt_maincourses.DataSource = Classes.Course.LoadCourses();
            rpt_maincourses.DataBind();

            if (!IsPostBack)
			{
                
            }

		}

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            tb_search.Text = "";
            ddl_area.SelectedIndex = 0;
            ddl_tipo.SelectedIndex = 0;
            tb_dataInicio.Text = "";
            tb_dataFim.Text = "";
        }
    }
}