<%@ Page Title="Salas" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClassrooms.aspx.cs" Inherits="FinalProject.ManageClassrooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-md-9 ">
                <div class="nav-wrapper position-relative end-0">
                    <ul class="nav nav-pills nav-fill p-1">
                        <li class="nav-item">
                            <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="listClassrooms" OnClientClick="return handleLinkButtonClick('List');" href="javascript:void(0);">Listar
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link mb-0 px-0 py-1" runat="server" ID="insertClassrooms" OnClientClick="return handleLinkButtonClick('Insert');" href="javascript:void(0);">Inserir
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="editClassrooms" OnClientClick="return handleLinkButtonClick('Edit');" href="javascript:void(0);"> Editar/Eliminar
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>

                <asp:UpdatePanel ID="updatePanel" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12 col-sm-6 text-end" style="padding-right: 20px; font-family: var(--bs-font-sans-serif)">
                            <a href="javascript:;" onclick="toggleFilters()">
                                <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                            </a>
                        </div>
                        <div id="filters" class="hidden">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                                <asp:Button runat="server" ID="btn_clear" CssClass="btn btn-outline-primary mb-0" Text="Limpar" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_clear" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div class="container bg-secundary py-2">
                            <div id="listClassroomsDiv" class="hidden">
                                <div class="py-3 align-items-center row" style="padding-left: 28px;">
                                    <div class="col-sm-3">
                                        <small class="text-uppercase font-weight-bold">Turmas:</small>
                                    </div>
                                </div>
                            </div>
                            <div id="insertClassroomsDiv" class="hidden">
                                <div style="padding: 5px;" id="alert" role="alert">
                                    <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                </div>
                                <div class="page-header min-vh-30">
                                    <div class="container">
                                        <div class="row ">
                                            <div class="col-xl-8 col-lg-7 col-md-6">
                                                <div class="card card-plain">
                                                    <!-- Inserção de Dados de Sala -->
                                                    <div class="card-header pb-0 text-left bg-transparent">
                                                        <h5 class="font-weight-bolder text-info text-gradient">Inserção de nova sala:</h5>
                                                    </div>
                                                    <div class="card-body">
                                                        <div role="form">
                                                            <label>Nr. da Sala</label>
                                                            <asp:RequiredFieldValidator ID="rfvClassroomNr" ErrorMessage="Nr.º da Sala Obrigatório" Text="*" runat="server" ControlToValidate="tbClassroomNr" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:TextBox ID="tbClassroomNr" CssClass="form-control" placeholder="Nr.º da Sala" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label>Tipo de Sala</label>
                                                            <asp:RequiredFieldValidator ID="rfvTipoSala" runat="server" ErrorMessage="Tipo de sala Obrigatório" Text="*" ControlToValidate="ddlTipoSala" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:DropDownList ID="ddlTipoSala" CssClass="form-control" runat="server" DataSourceID="SQLDSTipoSala" DataTextField="tipoSala" DataValueField="codTipoSala"></asp:DropDownList>
                                                                <asp:SqlDataSource ID="SQLDSTipoSala" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoSala]"></asp:SqlDataSource>

                                                            </div>
                                                            <label>Local da Sala</label>
                                                            <asp:RequiredFieldValidator ID="rvfLocalSala" runat="server" ErrorMessage="Local de Sala Obrigatório" Text="*" ControlToValidate="ddlLocalSala" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:DropDownList ID="ddlLocalSala" CssClass="form-control" runat="server" DataSourceID="SQLDSLocalSala" DataTextField="localSala" DataValueField="codLocalSala"></asp:DropDownList>
                                                                <asp:SqlDataSource ID="SQLDSLocalSala" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [localSala]"></asp:SqlDataSource>
                                                            </div>
                                                            <div class="text-center">
                                                                <asp:Button ID="btn_insert" runat="server" Text="Inserir" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rpt_insertClassrooms" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="row px-2" style="padding: 10px;">
                                                                    <!-- Inserção de Dados de Curso - Seleção dos Módulos -->
                                                                    <small class="text-uppercase font-weight-bold">Módulos do Curso:</small>
                                                                    <p>Seleccione os módulos pertencentes a este curso</p>
                                                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                                                    <div class="input-group mb-4">
                                                                        <asp:LinkButton runat="server" ID="lbtn_search" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:TextBox runat="server" ID="tb_search" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table class="table align-items-center justify-content-center mb-0">
                                                                        <thead>
                                                                            <tr>
                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Pertence</th>
                                                                                <th></th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex px-2">
                                                                            <div>
                                                                                <img src="<%# Eval("SVG") %>" class="avatar avatar-sm rounded-circle me-3">
                                                                            </div>
                                                                            <div class="my-auto">
                                                                                <h6 class="mb-0 text-sm"><%# Eval("Nome") %></h6>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <p class="text-sm font-weight-bold mb-0"><%# Eval("UFCD") %></p>
                                                                    </td>
                                                                    <td id="formyckb" class="text-xs font-weight-bold">
                                                                        <div class="stats">
                                                                            <small><%# Eval("CodModulo") %></small>
                                                                            <asp:HiddenField ID="hdnModuleID" runat="server" Value='<%# Eval("CodModulo") %>' />
                                                                            <asp:HiddenField ID="hdnModuleName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                            <div class="form-check">
                                                                                <asp:CheckBox runat="server" ID="chckBox" />
                                                                                <asp:Label runat="server" ID="lbl_order">Selecione este módulo</asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                        </div>
                                            </table>                                      
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </div>
                                            <div class="col-md-3">
                                                <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n12">
                                                    <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div id="editClassroomsDiv" class="hidden">
                                Editar Salas
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="rpt_manageCourses" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script>
        function handleLinkButtonClick(action) {
            var url, title;
            switch (action) {
                case 'List':
                    url = '../ManageClassrooms.aspx?List';
                    document.getElementById('listClassroomsDiv').classList.remove('hidden');
                    document.getElementById('editClassroomsDiv').classList.add('hidden');
                    document.getElementById('insertClassroomsDiv').classList.add('hidden');
                    title = "Listar Salas";
                    break;
                case 'Edit':
                    url = '../ManageClassrooms.aspx?Edit';
                    document.getElementById('editClassroomsDiv').classList.remove('hidden');
                    document.getElementById('listClassroomsDiv').classList.add('hidden');
                    document.getElementById('insertClassroomsDiv').classList.add('hidden');
                    title = "Editar Salas";
                    break;
                case 'Insert':
                    url = '../ManageClassrooms.aspx?Insert';
                    document.getElementById('insertClassroomsDiv').classList.remove('hidden');
                    document.getElementById('listClassroomsDiv').classList.add('hidden');
                    document.getElementById('editClassroomsDiv').classList.add('hidden');
                    title = "Inserir Salas";
                    break;
                default:
                    // Default URL or action if not recognized
                    url = '../ManageClassrooms.aspx';
                    title = "Gestão de Salas";
                    break;
            }

            //// Update URL
            window.history.replaceState(null, null, url);

            updateBreadcrumb(title, action);

            // Return false to prevent default postback behavior
            return false;
        }

        function updateBreadcrumb(title, action) {
            var breadcrumbContainer = document.getElementById('<%= Master.FindControl("SiteMapPath1").ClientID %>');
            var siteNode = document.getElementById('<%= Master.FindControl("siteNode").ClientID %>')
            if (breadcrumbContainer) {
                breadcrumbContainer.innerHTML = '';
                siteNode.innerHTML = '';

                var breadcrumbItems = [];
                breadcrumbItems.push('<a href="/">Home</a>');
                breadcrumbItems.push('<a href="../Manage.aspx">Gestão</a>');
                breadcrumbItems.push('<a href="../ManageClassrooms.aspx">Gestão de Salas</a>');

                if (action === 'List') {
                    breadcrumbContainer.innerHTML = breadcrumbItems.join('>');
                    breadcrumbContainer.innerHTML += '>' + title;
                    siteNode.innerHTML = title;
                }
                if (action === 'Edit') {
                    breadcrumbContainer.innerHTML = breadcrumbItems.join(' > ');
                    breadcrumbContainer.innerHTML += '>' + title;
                    siteNode.innerHTML = title;
                }
                if (action === 'Insert') {
                    breadcrumbContainer.innerHTML = breadcrumbItems.join(' > ');
                    breadcrumbContainer.innerHTML += '>' + title;
                    siteNode.innerHTML = title;
                }
            }
        }
    </script>

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
