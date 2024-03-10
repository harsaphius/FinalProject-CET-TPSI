using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = @"                      
                            document.getElementById('mainpage').classList.add('hidden');
                            document.getElementById('register').classList.remove('hidden');
                            ";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPageElements", script, true);
        }
    }
}