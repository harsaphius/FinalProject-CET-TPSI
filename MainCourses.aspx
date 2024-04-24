<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MainCourses.aspx.cs" Inherits="FinalProject.MainCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 15px">
            <div id="filtermenu" class="col-md-12 col-sm-12 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
                <a href="javascript:;" onclick="toggleFilters()">
                    <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                </a>
            </div>
            <div id="filters" class="col-md-12 col-md-6 hidden" style="padding-left: 30px;">
                <asp:UpdatePanel ID="updatePanelFilters" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Designação:</span>
                                <div class="input-group mb-4">
                                    <asp:LinkButton runat="server" ID="lbtnSearchFilters" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Type here..."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Área:</span>
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlAreaCursoFilters" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea" AppendDataBoundItems="True"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Tipo:</span>
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlTipoCursoFilters" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso" AppendDataBoundItems="True"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                                </div>
                            </div>
                          <%--  <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Data de Início: </span>
                                <div class="input-group mb-4">
                                    <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                    <asp:TextBox runat="server" ID="tbDataInicioFilters" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Data de Fim: </span>
                                <div class="input-group mb-4">
                                    <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                    <asp:TextBox runat="server" ID="tbDataFimFilters" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <br />
                                <div class="input-group mb-4">
                                    <asp:Button runat="server" ID="btnApplyFilters" CssClass="btn btn-outline-primary mb-0" Text="Aplicar" AutoPostBack="True" OnClick="btnApplyFilters_OnClick" />
                                    <span>&nbsp; &nbsp;</span>
                                    <asp:Button runat="server" ID="btnClearFilters" CssClass="btn btn-outline-primary mb-0" Text="Limpar" OnClick="btnClearFilters_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnApplyFilters" />
                        <asp:AsyncPostBackTrigger ControlID="btnClearFilters" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="updatePanelMainCourses" runat="server">
                <ContentTemplate>
                    <div class="col-12 mt-4">
                        <ul class="pagination ">
                            <li class="page-item mb-4 align-self-center">
                                <asp:LinkButton ID="btnPreviousMainCourses" CssClass="page-link" OnClick="btnPreviousMainCourses_OnClick" CausesValidation="false" runat="server">
                            <i class="fa fa-angle-left"></i>
                            <span class="sr-only">Previous</span>
                                </asp:LinkButton>
                            </li>
                            <asp:Repeater ID="rptMainCourses" runat="server">
                                <HeaderTemplate>
                                    <div class="card-body p-3">
                                        <div class="row">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="col-xl-4 col-lg-4 col-md-5 mb-xl-0 mb-4">
                                        <div class="card card-blog card-plain p-4">
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
                                                    <asp:Button runat="server" ID="btnDetails" class="btn btn-outline-primary btn-sm mb-0" CausesValidation="false" OnClick="btnDetails_Click" Text="Detalhes" />
                                                    <asp:Button runat="server" ID="btnEnroll" class="btn btn-outline-primary btn-sm mb-0" CausesValidation="false" OnClick="btnEnroll_Click" Text="Inscrever-me" />

                                                    <%--  <div class="avatar-group mt-2">
                                        <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Elena Morison">
                                            <img alt="Image placeholder" src="../assets/img/team-1.jpg">
                                        </a>
                                        <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Ryan Milly">
                                            <img alt="Image placeholder" src="../assets/img/team-2.jpg">
                                        </a>
                                        <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Nick Daniel">
                                            <img alt="Image placeholder" src="../assets/img/team-3.jpg">
                                        </a>
                                        <a href="javascript:;" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Peterson">
                                            <img alt="Image placeholder" src="../assets/img/team-4.jpg">
                                        </a>
                                    </div>--%>
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

                            <li class="page-item mb-4 align-self-center">
                                <asp:LinkButton ID="btnNextMainCourses" CssClass="page-link" CausesValidation="false" OnClick="btnNextMainCourses_OnClick" runat="server">
                                <i class="fa fa-angle-right"></i>
                                <span class="sr-only">Next</span>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
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
            </asp:UpdatePanel>
        </div>
    </div>


 <%--   <!--Javascript do Flatpickr -->
    <script>
        flatpickr('#<%= tbDataInicioFilters.ClientID %>', {
            dateFormat: 'Y-m-d',
            theme: 'light'
        });

        flatpickr('#<%= tbDataFimFilters.ClientID %>', {
            dateFormat: 'Y-m-d',
            theme: 'light',
            minDate: new Date()
        });
    </script>--%>

    <!-- Javascript para ativar/desativar a div dos filtros -->
    <script>
        function toggleFilters() {
            var filtersDiv = document.getElementById('filters');
            if (filtersDiv.classList.contains('hidden')) {
                filtersDiv.classList.remove('hidden');
            } else {
                filtersDiv.classList.add('hidden');
            }
        }
    </script>
</asp:Content>
