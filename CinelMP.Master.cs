using System;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class CinelMP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode;
                if (currentNode != null)
                {
                    string title = currentNode.Title;
                    siteNode.InnerText = title;
                }
            }
        }

        protected void lbtn_signout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("MainPage.aspx");
        }
    }
}