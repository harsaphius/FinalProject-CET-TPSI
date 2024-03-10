using ASPSnippets.FaceBookAPI;
using ASPSnippets.GoogleAPI;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class UserSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {

                    if (Session["Google"] != null)
                    {
                        if (Session["Google"].ToString() == "Yes")
                        {
                            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
                            string code = Request.QueryString["code"];
                            string json = GoogleConnect.Fetch("me", code);
                            GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                            //tb_user.Text = profile.Name;
                            //tb_email.Text = profile.Email;

                        }
                    }

                    if (Session["Facebook"] != null)
                    {
                        if (Session["Facebook"].ToString() == "Yes")
                        {
                            string data = FaceBookConnect.Fetch(Request.QueryString["code"], "me", "id, name, email");
                            FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                            //tb_user.Text = faceBookUser.Name;
                            //tb_email.Text = faceBookUser.Email;

                      
                        }
                    }
                }

                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    return;
                }
            }
        }
    }
}