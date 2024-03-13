<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="MainCourses.aspx.cs" Inherits="FinalProject.MainCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
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
                <asp:PostBackTrigger ControlID="btn_clear" />
            </Triggers>
        </asp:UpdatePanel>




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
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>

                <ItemTemplate>
                </ItemTemplate>

                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <div class="card-group">
                <div class="card">
                    <div class="card-header p-0 mx-3 mt-3 position-relative z-index-1">
                        <a href="javascript:;" class="d-block">
                            <img src="./assets/img/kit/pro/anastasia.jpg" class="img-fluid border-radius-lg">
                        </a>
                    </div>

                    <div class="card-body pt-2">
                        <span class="text-gradient text-primary text-uppercase text-xs font-weight-bold my-2">House</span>
                        <a href="javascript:;" class="card-title h5 d-block text-darker">Shared Coworking
                        </a>
                        <p class="card-description mb-4">
                            Use border utilities to quickly style the border and border-radius of an element. Great for images, buttons.
                        </p>
                        <div class="author align-items-center">
                            <img src="./assets/img/kit/pro/team-2.jpg" alt="..." class="avatar shadow">
                            <div class="name ps-3">
                                <span>Mathew Glock</span>
                                <div class="stats">
                                    <small>Posted on 28 February</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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

</asp:Content>
