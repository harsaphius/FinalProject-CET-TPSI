using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class MainCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				for (int i = 0; i < ddl_area.Items.Count; i++)
				{
					ddl_area.Items[i].Attributes["class"] = "dropdown-item";
				}
			}

		}
    }
}