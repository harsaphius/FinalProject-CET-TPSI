<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="MainCourses.aspx.cs" Inherits="FinalProject.MainCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <button class="btn btn-icon btn-2 btn-primary" type="button" onclick="toggleFilters()">
            <span class="btn-inner--icon"><i class="fa-solid fa-filter" aria-hidden="true"></i></span>
        </button>
        <div id="filters" class="hidden">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>Designação:</span>
                            <div class="input-group mb-4">
                                <asp:LinkButton runat="server" ID="lbtn_search" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:TextBox runat="server" ID="tb_search" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>Área:</span>
                            <div class="dropdown">
                                <asp:DropDownList ID="ddl_area" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>Tipo:</span>
                            <div class="dropdown">
                                <asp:DropDownList ID="ddl_tipo" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>Data de Início: </span>
                            <div class="input-group mb-4">
                                <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                <asp:TextBox runat="server" ID="tb_dataInicio" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>Data de Fim: </span>
                            <div class="input-group mb-4">
                                <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                <asp:TextBox runat="server" ID="tb_dataFim" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                            <span>
                                <br />
                            </span>
                            <div class="input-group mb-4">
                                <asp:Button runat="server" ID="btn_clear" CssClass="btn btn-outline-primary mb-0" Text="Limpar" OnClick="btn_clear_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_clear" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <%--Example for consultaion  
                    <div class="dropdown">
                        <button class="btn bg-gradient-primary dropdown-toggle" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                            Primary
                        </button>
                        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <li><a class="dropdown-item" href="javascript:;">Action</a></li>
                            <li><a class="dropdown-item" href="javascript:;">Another action</a></li>
                            <li><a class="dropdown-item" href="javascript:;">Something else here</a></li>
                        </ul>
                    </div>--%>

        <div class="row">
            <asp:Repeater ID="rpt_maincourses" runat="server">
                <HeaderTemplate>
                    <div class="card-body p-3">
                        <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-xl-3 col-md-6 mb-xl-0 mb-4">

                        <div class="card card-blog card-plain">
                            <div class="position-relative">
                                <a class="d-block shadow-xl border-radius-xl">
                                    <img src="../assets/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl">
                                </a>
                            </div>
                            <div class="card-body px-1 pb-0">
                                <p class="text-gradient text-dark mb-2 text-sm"><%# Eval("Nome") %></p>
                                <a href="javascript:;">
                                    <h5>Referencial n.º <%# Eval("CodRef") %>
                                    </h5>
                                </a>
                                <p class="mb-4 text-sm">
                                    Nível <%# Eval("CodQNQ") %>
                                </p>
                                <div class="d-flex align-items-center justify-content-between">
                                    <button type="button" class="btn btn-outline-primary btn-sm mb-0">Detalhes</button>
                                    <div class="avatar-group mt-2">
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
                                    </div>
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

        </div>
    </div>

    <script>
        flatpickr('#<%= tb_dataFim.ClientID %>', {
            // Options
            dateFormat: 'Y-m-d',
            theme: 'light'
        });

        flatpickr('#<%= tb_dataInicio.ClientID %>', {
            // Options
            dateFormat: 'Y-m-d',
            theme: 'light'
        });
    </script>
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
