using System.Web;

namespace FinalProject.Classes
{
    public class SiteMapManagement
    {
        public static string GetCurrentNodeTitle()
        {
            SiteMapNode currentNode = SiteMap.CurrentNode;
            return currentNode?.Title;
        }
        public static SiteMapNode GetParentNode(SiteMapNode node)
        {
            if (node == null)
            {
                return null;
            }

            // Get the parent node
            SiteMapNode parentNode = node.ParentNode;

            return parentNode;
        }
        public static void SetCurrentNodeTitle(string newTitle)
        {
            string currentTitle = GetCurrentNodeTitle();
            SiteMapNode currentNode = SiteMap.CurrentNode;

            if (currentNode != null)
            {
                currentNode.Title = newTitle;
            }
            else
            {
                SiteMapNode parentNode = GetParentNode(currentNode);

                // You can now access the parent node's properties if needed
                if (parentNode != null)
                {
                    // Access parent node's properties here
                    parentNode.Title = currentTitle + newTitle;
                }
            }

        }

    }
}