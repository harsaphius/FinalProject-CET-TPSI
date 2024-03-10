using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UserSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Google Data
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["clientid"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["clientsecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["redirection_url"];

            //Facebook Data
            FaceBookConnect.API_Key = ConfigurationManager.AppSettings["FacebookKey"];
            FaceBookConnect.API_Secret = ConfigurationManager.AppSettings["FacebookSecret"];
            FaceBookConnect.Version = ConfigurationManager.AppSettings["FacebookVersion"];
        }

        protected void btn_facebook_Click(object sender, EventArgs e)
        {
            Session["Facebook"] = "Yes";
            FaceBookConnect.Authorize("public_profile,email", ConfigurationManager.AppSettings["redirection_url"]);
        }

        protected void btn_google_Click(object sender, EventArgs e)
        {
            Session["Google"] = "Yes";
            GoogleConnect.Authorize("profile", "email");
        }

    }
}