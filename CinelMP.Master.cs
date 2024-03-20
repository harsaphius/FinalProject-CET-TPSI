using System;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class CinelMP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

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
        public SiteMapNode GetCurrentSiteMapNode()
        {
            // Access the SiteMap for the current request
            SiteMapNode currentNode = SiteMap.CurrentNode;

            // Return the current SiteMapNode
            return currentNode;
        }

    }
}