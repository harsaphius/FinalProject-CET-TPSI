using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FinalProject.Classes
{
    public class PageControls<T>
    {
        //public void BindData(List<User> List)
        //{
        //    PagedDataSource pagedDataSource = new PagedDataSource();
        //    pagedDataSource.DataSource = List;
        //    pagedDataSource.AllowPaging = true;
        //    pagedDataSource.PageSize = 8;
        //    pagedDataSource.CurrentPageIndex = PageNumberCount;
        //    int PageNumber = PageNumberCount + 1;
        //    lbl_pageNumber.Text = (PageNumber).ToString();

        //    rpt_mainpage.DataSource = pagedDataSource;
        //    rpt_mainpage.DataBind();

        //    lbtn_previous.Enabled = !pagedDataSource.IsFirstPage;
        //    lbtn_next.Enabled = !pagedDataSource.IsLastPage;
        //}

        //protected void lbtn_previous_Click(object sender, EventArgs e)
        //{
        //    PageNumberCount -= 1;
        //    BindData();
        //}
        //protected void lbtn_next_Click(object sender, EventArgs e)
        //{
        //    PageNumberCount += 1;
        //    BindData();
        //}


        private List<T> items;

        public PageControls(List<T> items)
        {
            this.items = items;
        }

        public List<T> GetPage(int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            return items.Skip(startIndex).Take(pageSize).ToList();
        }

        public int GetTotalPages(int pageSize)
        {
            return (int)Math.Ceiling((double)items.Count / pageSize);
        }
    }

}
