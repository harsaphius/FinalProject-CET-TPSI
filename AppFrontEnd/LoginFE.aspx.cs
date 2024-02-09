using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.AppFrontEnd
{
    public partial class LoginFE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecoverPasswordFE_Click(object sender, EventArgs e)
        {
            //Recuperação de password com envio de email
        }

        protected void btnLoginBE_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPageFE.aspx");
        }

        protected void btn_facebook_Click(object sender, EventArgs e)
        {

        }

        protected void btn_google_Click(object sender, EventArgs e)
        {
            string clientid = ConfigurationManager.AppSettings["clientid"];
            string redirectionURL = ConfigurationManager.AppSettings["redirection_url"];

            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + redirectionURL + "&response_type=code&client_id=" + clientid + "";
            Response.Redirect(url);
        }
    }
}