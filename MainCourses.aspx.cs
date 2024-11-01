﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class MainCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlAreaCursoFilters.Items.Insert(0, new ListItem("Todas", "0"));
                ddlTipoCursoFilters.Items.Insert(0, new ListItem("Todas", "0"));

                BindDataCourses();
            }
        }

        protected void btnClearFilters_OnClick(object sender, EventArgs e)
        {
            tbSearchFilters.Text = "";
            ddlAreaCursoFilters.SelectedIndex = 0;
            ddlTipoCursoFilters.SelectedIndex = 0;
            //tbDataInicioFilters.Text = "";
            //tbDataFimFilters.Text = "";

            PageNumberCourses = 0;

            BindDataCourses();
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            Button btnDetails = (Button)sender;
            HiddenField hdnCourseID = (HiddenField)btnDetails.NamingContainer.FindControl("hdnCourseID");
            string codCurso = hdnCourseID.Value;

            Session["CodCurso"] = codCurso;
            Session["MainCourses"] = "Yes";

            Response.Redirect("CourseDetails.aspx?id=" + codCurso);
        }

        protected void btnEnroll_Click(object sender, EventArgs e)
        {
            Session["Enrollment"] = "Yes";
            Button btn_enroll = (Button)sender;

            HiddenField hdnCourseID = (HiddenField)btn_enroll.NamingContainer.FindControl("hdnCourseID");
            string codCurso = hdnCourseID.Value;

            Session["CodCursoEnrollment"] = codCurso;

            if (Session["Logado"] == null)
            {
                Response.Redirect("UserSignIn.aspx");
            }
            else
            {
                Response.Redirect("UserCourses.aspx");
            }
        }

        protected void btnApplyFilters_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses = 0;

            BindDataCourses();
        }

        //Função de Databinding
        private void BindDataCourses()
        {
            List<string> conditions = new List<string>();

            conditions.Add(string.IsNullOrEmpty(tbSearchFilters.Text) ? "" : tbSearchFilters.Text);
            conditions.Add(ddlAreaCursoFilters.SelectedValue == "0" ? "" : ddlAreaCursoFilters.SelectedValue);
            conditions.Add(ddlTipoCursoFilters.SelectedValue == "0" ? "" : ddlTipoCursoFilters.SelectedValue);

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Classes.Course.LoadCourses(conditions, null);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 6;
            pagedData.CurrentPageIndex = PageNumberCourses;
            int PageNumber = PageNumberCourses + 1;
            lblPageNumberListCourses.Text = PageNumber.ToString();

            rptMainCourses.DataSource = pagedData;
            rptMainCourses.DataBind();

            btnPreviousMainCourses.Enabled = !pagedData.IsFirstPage;
            btnNextMainCourses.Enabled = !pagedData.IsLastPage;

        }

        //Função de Paginação
        private int PageNumberCourses
        {
            get
            {
                if (ViewState["PageNumberCourses"] != null)
                    return Convert.ToInt32(ViewState["PageNumberCourses"]);
                else
                    return 0;
            }
            set => ViewState["PageNumberCourses"] = value;
        }

        //Funções para os botões de paginação
        protected void btnNextMainCourses_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses += 1;
            BindDataCourses();
        }

        protected void btnPreviousMainCourses_OnClick(object sender, EventArgs e)
        {
            PageNumberCourses -= 1;
            BindDataCourses();
        }
    }
}