<%@ Page Title="Home" Language="C#" EnableEventValidation="false" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="FinalProject.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script async src="https://www.youtube.com/iframe_api"></script>
    <style>
        .jumbotron {
            display: flex;
            justify-content: center;
        }

        .container {
            max-width: 100%; /* Ensures the container does not exceed the viewport width */
        }

        iframe {
            width: 100%; /* Ensures the iframe fills the container width */
            height: auto; /* Allows the iframe to maintain its aspect ratio */
            max-width: 100%; /* Ensures the iframe does not exceed the container width */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron jumbotron-fluid flex" style="text-align: center;">
        <div class="container">
            <iframe src="https://youtube.com/embed/2uyOp--G7dQ?controls=1&mute=1&showinfo=0&rel=0&autoplay=1" style="width: 860px; height: 450px" allowfullscreen="yes" frameborder="0"></iframe>
        </div>
    </div>
    <asp:UpdatePanel ID="updatePanelMainPanel" runat="server">
        <ContentTemplate>
            <div class="col-12 mt-4">
                <ul class="pagination ">
                    <li class="page-item mb-4 align-self-center">
                        <asp:LinkButton ID="btnPreviousMainPage" CssClass="page-link" OnClick="btnPreviousMainPage_OnClick" CausesValidation="false" runat="server">
                        <i class="fa fa-angle-left"></i>
                        <span class="sr-only">Previous</span>
                        </asp:LinkButton>
                    </li>
                    <div class="card mb-4 " style="margin-right: 3px;">
                        <div class="card-header pb-0 p-3">
                            <h6 class="mb-1">Últimos Cursos</h6>
                            <p class="text-sm">A iniciar em breve</p>
                        </div>

                        <asp:Repeater ID="rptMainPanel" runat="server">
                            <HeaderTemplate>
                                <div class="card-body p-3">
                                    <div class="row">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="col-xl-4 col-lg-5 col-md-6 mb-xl-0 mb-4">
                                    <div class="card card-blog card-plain">
                                        <div class="position-relative">
                                            <a class="d-block shadow-xl border-radius-xl">
                                                <img src="../assets/img/tpsi.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl">
                                            </a>
                                        </div>
                                        <div class="card-body px-1 pb-0">
                                            <asp:HiddenField ID="hdnCourseID" runat="server" Value='<%# Eval("CodCurso") %>' />
                                            <p class="text-gradient text-dark mb-2 text-sm"><%# Eval("Nome") %></p>
                                            <a href="javascript:;">
                                                <h5>Referencial n.º <%# Eval("CodRef") %>
                                                </h5>
                                            </a>
                                            <p class="mb-4 text-sm">
                                                Nível <%# Eval("CodQNQ") %>
                                            </p>
                                            <div class="d-flex align-items-center justify-content-between">
                                                <asp:Button runat="server" ID="btn_details" class="btn btn-outline-primary btn-sm mb-0" CausesValidation="false" OnClick="btn_details_Click" Text="Detalhes" />
                                                <asp:Button runat="server" ID="btn_enroll" class="btn btn-outline-primary btn-sm mb-0" CausesValidation="false" OnClick="btn_enroll_Click" Text="Inscrever-me" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                    </div>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!-- Paginação dos Cursos -->
                    </div>
                    <li class="page-item mb-4 align-self-center">
                        <asp:LinkButton ID="btnNextMainPage" CssClass="page-link" CausesValidation="false" OnClick="btnNextMainPage_OnClick" runat="server">
                        <i class="fa fa-angle-right"></i>
                        <span class="sr-only">Next</span>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>

            <div class="col-12">
                <ul class="pagination justify-content-center">
                    <li class="page-item active">
                        <span class="page-link">
                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberListCourses"></asp:Label></span>
                    </li>
                </ul>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPreviousMainPage" />
            <asp:AsyncPostBackTrigger ControlID="btnNextMainPage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
