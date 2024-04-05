<%@ Page Title="Salas" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClassrooms.aspx.cs" Inherits="FinalProject.ManageClassrooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <div class="row" style="margin-top: 15px">
            <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Nova Sala" ID="btn_insertClassroom" OnClientClick="showInsert(); return false;" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_back" OnClientClick="showInsert(); return false;" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_backEditClassroom" OnClientClick="showEditClassroom(); return false;" />
            </div>
            <div id="filtermenu" class="col-md-6 col-sm-6 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
                <a href="javascript:;" onclick="toggleFilters()">
                    <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                </a>
            </div>
            <div id="filters" class="col-md-12 col-md-6 hidden" style="padding-left: 30px;">
                <asp:UpdatePanel ID="updatePanelFilters" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-xl-3 col-sm-6 mb-xl-0 mb-4">
                                <span>Designação:</span>
                                <div class="input-group mb-4">
                                    <asp:LinkButton runat="server" ID="lbtSearch" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:TextBox runat="server" ID="tbSearch" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-2 col-sm-6 mb-xl-0 mb-4">
                                <span>Tipo de Sala: </span>
                                <div class="input-group mb-4">
                                    <asp:DropDownList ID="ddlTipoSalaFilters" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipoSala" DataTextField="tipoSala" DataValueField="codTipoSala"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-xl-2 col-sm-6 mb-xl-0 mb-4">
                                <span>Local da Sala: </span>
                                <div class="input-group mb-4">
                                    <asp:DropDownList ID="ddlLocalSalaFilters" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSLocalSala" DataTextField="localSala" DataValueField="codLocalSala"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-3 col-sm-6 mb-xl-0 mb-4">
                                <br />
                                <div class="input-group mb-4">
                                    <asp:Button runat="server" ID="btnApplyFilters" CssClass="btn btn-outline-primary mb-0" Text="Aplicar" />
                                    <span>&nbsp; &nbsp;</span>
                                    <asp:Button runat="server" ID="btnClearFilters" CssClass="btn btn-outline-primary mb-0" Text="Limpar" />
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
        <div class="container-fluid py-4">
            <div class="row">
                <div class="col-12">
                    <div class="card mb-4">
                        <div id="listClassroomsDiv">
                            <asp:UpdatePanel ID="updatePanelListClassrooms" runat="server">
                                <ContentTemplate>
                                    <div class="container row justify-content-center">
                                        <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; width: 70%; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                        <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" OnTick="timerMessageEdit_OnTick"></asp:Timer>
                                    </div>
                                    <div class="card-header pb-0">
                                        <h6>Salas</h6>
                                    </div>
                                    <asp:Repeater ID="rpt_Classrooms" runat="server" OnItemDataBound="rpt_Classrooms_ItemDataBound" OnItemCommand="rpt_Classrooms_ItemCommand">
                                        <HeaderTemplate>
                                            <div class="card-body px-0 pt-0 pb-2">
                                                <div class="table-responsive p-0">
                                                    <table class="table align-items-center mb-0">
                                                        <thead>
                                                            <colgroup>
                                                                <col style="width: 10%;" />
                                                                <col style="width: 15%;" />
                                                                <col style="width: 20%;" />
                                                                <col style="width: 10%;" />
                                                                <col style="width: 10%;" />
                                                            </colgroup>
                                                            <tr>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º da Sala</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Tipo de Sala</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Local da Sala</th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField runat="server" ID="hdnClassroomID" Value='<%# Eval("CodSala") %>' />
                                                    <div class="d-flex px-2">
                                                        <div class="my-auto">
                                                            <asp:TextBox ID="tbNrSalaEdit" CssClass="form-control" runat="server" Text='<%# Bind("NrSala") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                            <p class="mb-0 text-sm text-center">
                                                                <asp:Label ID="lblNrSala" runat="server" Text='<%# Eval("NrSala") %>' Visible="true"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoSalaEdit" CssClass="form-control" runat="server" DataSourceID="SQLDSTipoSala" DataTextField="tipoSala" DataValueField="codTipoSala" Visible="false"></asp:DropDownList>
                                                    <p class="text-sm mb-0">
                                                        <asp:Label ID="lblTipoSala" runat="server" Text='<%# Eval("TipoSala") %>' Visible="true" Style="width: 100%;"></asp:Label>
                                                    </p>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLocalSalaEdit" CssClass="form-control" runat="server" DataSourceID="SQLDSLocalSala" DataTextField="localSala" DataValueField="codLocalSala" Visible="false"></asp:DropDownList>
                                                    <p class="text-sm mb-0">
                                                        <asp:Label ID="lblLocalSala" class="text-xs" runat="server" Text='<%# Eval("LocalSala") %>' Style="width: 100%;" Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td class="align-middle font-weight-bold text-center">
                                                    <asp:LinkButton runat="server" ID="lbtEditClassroom" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                        Text="Edit" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbtCancelClassroom" CausesValidation="false" CommandName="Cancel" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                        Text="Cancel" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                </td>
                                                <td class="align-middle text-center">
                                                    <asp:LinkButton runat="server" ID="lbtDeleteClassroom" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                        Text="Delete" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbtConfirmClassroom" CausesValidation="false" CommandName="Confirm" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                        Text="Confirm" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                       </table>
                                 </div>
                             </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <!--Paginação -->
                                    <div class="col-12">
                                        <ul class="pagination justify-content-center" style="padding: 2px;">
                                            <li class="page-item">
                                                <asp:LinkButton ID="btnPreviousClassroom" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousClassroom_OnClick" runat="server">
                                                    <i class="fa fa-angle-left"></i>
                                                    <span class="sr-only">Previous</span>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="page-item active">
                                                <span class="page-link">
                                                    <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberClassrooms"></asp:Label></span>
                                            </li>
                                            <li class="page-item">
                                                <asp:LinkButton ID="btnNextClassroom" CssClass="page-link" OnClick="btnNextClassroom_OnClick" CausesValidation="false" runat="server">
                                                    <i class="fa fa-angle-right"></i>
                                                    <span class="sr-only">Next</span>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="timerMessageEdit" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousClassroom" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextClassroom" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="insertClassroomsDiv" class="hidden">
                            <asp:UpdatePanel ID="updatePanelInsertClassrooms" runat="server">
                                <ContentTemplate>
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
                                                                    <asp:Button ID="btnInsertClassroom" runat="server" OnClick="btnInsertClassroom_OnClick" Text="Inserir" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                </div>
                                                                <div>
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lblMessageInsert" Style="display: flex; align-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                                    <asp:Timer ID="timerMessageInsert" runat="server" Interval="3000" OnTick="timerMessageInsert_OnTick"></asp:Timer>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n12">
                                                        <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnInsertClassroom" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Função de Javascript para Mostrar a Div de Inserir após click no Button Inserir Módulo -->
    <script>
        function showInsert() {
            var insertDiv = document.getElementById('insertClassroomsDiv');
            var listDiv = document.getElementById('listClassroomsDiv');
            var btnInsert = document.getElementById('<%= btn_insertClassroom.ClientID %>');
            var btnBack = document.getElementById('<%= btn_back.ClientID %>');
            var filterMenu = document.getElementById('filtermenu');

            if (insertDiv.classList.contains('hidden')) {
                insertDiv.classList.remove('hidden');
                listDiv.classList.add('hidden');
                btnInsert.classList.add('hidden');
                btnBack.classList.remove('hidden');
                filterMenu.classList.add('hidden');

            } else {
                insertDiv.classList.add('hidden');
                listDiv.classList.remove('hidden');
                btnInsert.classList.remove('hidden');
                btnBack.classList.add('hidden');
                filterMenu.classList.remove('hidden');
            }
        }
    </script>

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
