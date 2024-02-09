using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class CinelMP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginFE.aspx");
        }
    }
}