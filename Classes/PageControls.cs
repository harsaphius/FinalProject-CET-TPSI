using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalProject.Classes
{
    public class PageControls<T>
    {
        private List<T> items;
        private int pageSize;

        public PageControls(List<T> items, int pageSize)
        {
            this.items = items;
            this.pageSize = pageSize;
        }

        public List<T> GetPage(int pageIndex)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            return items.Skip(startIndex).Take(pageSize).ToList();
        }

        public int GetTotalPages()
        {
            return (int)Math.Ceiling((double)items.Count / pageSize);
        }

        public string GeneratePagination(int currentPage, int totalPages)
        {
            StringBuilder paginationHtml = new StringBuilder();

            // Add the previous page button
            paginationHtml.AppendLine("<li class=\"page-item " + (currentPage == 1 ? "disabled" : "") + "\">");
            paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + (currentPage - 1) + ")\" aria-label=\"Previous\">");
            paginationHtml.AppendLine("<i class=\"fa fa-angle-left\"></i>");
            paginationHtml.AppendLine("<span class=\"sr-only\">Previous</span>");
            paginationHtml.AppendLine("</a>");
            paginationHtml.AppendLine("</li>");

            // Add page numbers
            for (int i = 1; i <= totalPages; i++)
            {
                paginationHtml.AppendLine("<li class=\"page-item " + (i == currentPage ? "active" : "") + "\">");
                paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + i + ")\">" + i + "</a>");
                paginationHtml.AppendLine("</li>");
            }

            // Add the next page button
            paginationHtml.AppendLine("<li class=\"page-item " + (currentPage == totalPages ? "disabled" : "") + "\">");
            paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + (currentPage + 1) + ")\" aria-label=\"Next\">");
            paginationHtml.AppendLine("<i class=\"fa fa-angle-right\"></i>");
            paginationHtml.AppendLine("<span class=\"sr-only\">Next</span>");
            paginationHtml.AppendLine("</a>");
            paginationHtml.AppendLine("</li>");

            return paginationHtml.ToString();
        }

        public static string GetCurrentNodeTitle()
        {
            SiteMapNode currentNode = SiteMap.CurrentNode;
            return currentNode?.Title;
        }
    }
}