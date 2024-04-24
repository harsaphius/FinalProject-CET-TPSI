<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ErrorPage404.aspx.cs" Inherits="FinalProject.ErrorPage404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-form-bg h-100">
        <div class="container h-100">
            <div class="row justify-content-center h-100">
                <div class="col-xl-6">
                    <div class="error-content">
                        <div class="card mb-0">
                            <div class="card-body text-center pt-5">
                                <h1 class="error-text text-primary">404</h1>
                                <h4 class="mt-4"><i class="fa fa-thumbs-down text-danger"></i>Bad Request</h4>
                                <p>Your Request resulted in an error.</p>
                                <div class="mt-5 mb-5">

                                    <div class="text-center mb-4 mt-4">
                                        <a href="MainPage.aspx" class="btn btn-primary">Go to Homepage</a>
                                    </div>
                                </div>
                                <div class="text-center">
                               
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>