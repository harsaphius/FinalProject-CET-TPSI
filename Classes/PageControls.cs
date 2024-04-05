using System;
using System.Collections.Generic;

namespace FinalProject.Classes
{
    public class PageControls<T>
    {
        private readonly List<T> dataSource;
        private readonly int pageSize;

        public PageControls(List<T> dataSource, int pageSize)
        {
            this.dataSource = dataSource;
            this.pageSize = pageSize;
            PageNumber = 0; // Initialize page number
        }

        public int PageNumber { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)dataSource.Count / pageSize);

        public List<T> GetPagedData(int pageNumber)
        {
            PageNumber = pageNumber;
            var startIndex = (pageNumber - 1) * pageSize;
            var count = Math.Min(pageSize, dataSource.Count - startIndex);

            if (startIndex < dataSource.Count)
                return dataSource.GetRange(startIndex, count);
            return new List<T>();
        }

        //public string GeneratePagination(int currentPage, int totalPages)
        //{
        //    StringBuilder paginationHtml = new StringBuilder();

        //    // Add the previous page button
        //    paginationHtml.AppendLine("<li class=\"page-item " + (currentPage == 1 ? "disabled" : "") + "\">");
        //    paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + (currentPage - 1) + ")\" aria-label=\"Previous\">");
        //    paginationHtml.AppendLine("<i class=\"fa fa-angle-left\"></i>");
        //    paginationHtml.AppendLine("<span class=\"sr-only\">Previous</span>");
        //    paginationHtml.AppendLine("</a>");
        //    paginationHtml.AppendLine("</li>");

        //    // Add page numbers
        //    for (int i = 1; i <= totalPages; i++)
        //    {
        //        paginationHtml.AppendLine("<li class=\"page-item " + (i == currentPage ? "active" : "") + "\">");
        //        paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + i + ")\">" + i + "</a>");
        //        paginationHtml.AppendLine("</li>");
        //    }

        //    // Add the next page button
        //    paginationHtml.AppendLine("<li class=\"page-item " + (currentPage == totalPages ? "disabled" : "") + "\">");
        //    paginationHtml.AppendLine("<a class=\"page-link\" href=\"javascript:;\" onclick=\"changePage(" + (currentPage + 1) + ")\" aria-label=\"Next\">");
        //    paginationHtml.AppendLine("<i class=\"fa fa-angle-right\"></i>");
        //    paginationHtml.AppendLine("<span class=\"sr-only\">Next</span>");
        //    paginationHtml.AppendLine("</a>");
        //    paginationHtml.AppendLine("</li>");

        //    return paginationHtml.ToString();
        //}
    }
}