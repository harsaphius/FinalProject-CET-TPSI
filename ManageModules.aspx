<%@ Page Title="Módulos" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageModules.aspx.cs" Inherits="FinalProject.ManageModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <div class="row" style="margin-top: 15px">
            <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Módulo" ID="btn_insertModule" OnClientClick="showInsert(); return false;" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_back" OnClientClick="showInsert(); return false;" />
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
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Designação:</span>
                                <div class="input-group mb-4">
                                    <asp:LinkButton runat="server" ID="lbtSearchFilters" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Type here..."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-2 col-sm-6 mb-xl-0 mb-4">
                                <span>Nr.º de Horas:</span>
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlNrHoras" runat="server" class="btn bg-gradient-secundary dropdown-toggle">
                                        <asp:ListItem Value="25">25 horas</asp:ListItem>
                                        <asp:ListItem Value="50">50 Horas</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-md-4 col-sm-4 mb-xl-0 mb-0">
                                <span>
                                    <br />
                                </span>
                                <div class="input-group mb-0 text-end">
                                    <asp:DropDownList runat="server" ID="ddlOrder" class="btn bg-gradient-secundary dropdown-toggle">
                                        <asp:ListItem Value="ASC">A-Z</asp:ListItem>
                                        <asp:ListItem Value="DESC">Z-A</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6 mb-xl-0 mb-0">
                                <br />
                                <div class="input-group mb-0">
                                    <asp:Button runat="server" ID="btnApplyFilters" CausesValidation="False" CssClass="btn btn-outline-primary mb-0" Text="Aplicar" AutoPostBack="True" OnClick="btnApplyFilters_OnClick" />
                                    <span>&nbsp; &nbsp;</span>
                                    <asp:Button runat="server" ID="btnClearFilters" CausesValidation="False" CssClass="btn btn-outline-primary mb-0" Text="Limpar" OnClick="btnClearFilters_OnClick" />
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
                        <div id="listModulesDiv">
                            <asp:UpdatePanel ID="updatePanelListModules" runat="server">
                                <ContentTemplate>
                                    <div class="card-header pb-0">
                                        <h6>Módulos</h6>
                                    </div>
                                    <asp:Repeater ID="rptModules" runat="server" OnItemDataBound="rptModules_ItemDataBound" OnItemCommand="rptModules_ItemCommand">
                                        <HeaderTemplate>
                                            <div class="card-body px-0 pt-0 pb-2">
                                                <div class="table-responsive p-0">
                                                    <table class="table align-items-center mb-0" style="width: 100%;">
                                                        <colgroup>
                                                            <col style="width: 30%;" />
                                                            <col style="width: 8%;" />
                                                            <col style="width: 12%;" />
                                                            <col style="width: 32%;" />
                                                            <col style="width: 8%;" />
                                                            <col style="width: 5%;" />
                                                            <col style="width: 5%;" />
                                                        </colgroup>
                                                        <thead>
                                                            <tr>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Duração</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Descrição</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Créditos</th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td id="cellNome">
                                                    <asp:HiddenField runat="server" ID="hdnModuleID" Value='<%# Eval("CodModulo") %>' />
                                                    <div class="d-flex px-1">
                                                        <%-- <a runat="server" href="javascript:;">--%>
                                                        <%--   <asp:FileUpload ID="fileUpload" runat="server" Visible="false" onchange="uploadFile(this)" /> <%--onclick="triggerFileUpload('<%= fileUpload.ClientID %>')"
                                                                    </a>--%>

                                                        <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("SVG") %>' />

                                                        <p class="text-sm">
                                                            <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false"></asp:TextBox>
                                                            <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                        </p>
                                                    </div>
                                                    </div>
                                                </td>
                                                <td id="cellUFCD" style="width: 5%;">
                                                    <p class="text-sm">
                                                        <asp:TextBox ID="tbUFCD" CssClass="form-control" runat="server" Text='<%# Bind("UFCD") %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("UFCD") %>' Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:TextBox ID="tbDuracao" CssClass="form-control" runat="server" Text='<%# Bind("Duracao") %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblDuracao" class="text-sm" runat="server" Text='<%# Eval("Duracao") %>' Style="width: 100%;" Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td id="cellDescricao">
                                                    <p class="text-xs">
                                                        <asp:TextBox ID="tbDescricao" CssClass="form-control" runat="server" Text='<%# Bind("Descricao") %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblDescricao" class="text-xs" runat="server" Text='<%# Eval("Descricao") %>' Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:TextBox ID="tbCreditos" CssClass="form-control" runat="server" Text='<%# Bind("Creditos") %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblCreditos" class="text-sm" runat="server" Text='<%# Eval("Creditos") %>' Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td class="align-middle font-weight-bold text-center">
                                                    <p>
                                                        <asp:LinkButton runat="server" ID="lbtEditModules" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Edit" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtCancelModules" CausesValidation="false" CommandName="Cancel" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Cancel" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                    </p>
                                                </td>
                                                <td class="align-middle text-center">
                                                    <p>
                                                        <asp:LinkButton runat="server" ID="lbtDeleteModules" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Delete" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtConfirmModules" CausesValidation="false" CommandName="Confirm" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Confirm" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                    </p>
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
                                    <%-- <script type="text/javascript">
                                            function triggerFileUpload(clientId) {
                                                // Get the FileUpload control
                                                var fileUpload = document.getElementById(clientId);
                                                // Trigger a click event on the FileUpload control
                                                fileUpload.click();
                                            }
                                            function uploadFile(input) {
                                                // Handle file upload logic here
                                                // For example, you can access the uploaded file using input.files[0]
                                            }
                                        </script>--%>
                                    <!--Paginação -->
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="btnPreviousModule" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousModule_Click" runat="server">
                                                        <i class="fa fa-angle-left"></i>
                                                        <span class="sr-only">Previous</span>
                                            </asp:LinkButton>
                                        </li>
                                        <li></li>
                                        <li class="page-item">
                                            <asp:LinkButton ID="btnNextModule" CssClass="page-link" CausesValidation="false" OnClick="btnNextModule_Click" runat="server">
                                                        <i class="fa fa-angle-right"></i>
                                                        <span class="sr-only">Next</span>
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousModule" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextModule" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="insertModulesDiv" class="hidden">
                            <asp:UpdatePanel runat="server" ID="updatePanelInsertModules">
                                <ContentTemplate>
                                    <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                        <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                    </div>
                                    <div class="page-header min-vh-50">
                                        <div class="container-fluid">
                                            <div class="row ">
                                                <div class="col-xl-6 col-lg-5 col-md-6 flex-column">
                                                    <div class="card card-plain">
                                                        <div class="card-header pb-0 text-left bg-transparent">
                                                            <h5 class="font-weight-bolder text-info text-gradient">Inserção de novo módulo:</h5>
                                                        </div>
                                                        <div class="card-body">
                                                            <div role="form">
                                                                <label>Nome do Módulo</label>
                                                                <asp:RequiredFieldValidator ID="rfvModuleName" ErrorMessage="Nome do módulo obrigatório" Text="*" runat="server" ControlToValidate="tbModuleName" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbModuleName" CssClass="form-control" placeholder="Nome do Módulo" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label>Duração</label>
                                                                <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ErrorMessage="Duração obrigatória" Text="*" ControlToValidate="tbDuration" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbDuration" CssClass="form-control" placeholder="Duração" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label>Código da UFCD</label>
                                                                <asp:RequiredFieldValidator ID="rvfUFCD" runat="server" ErrorMessage="Código da UFCD obrigatório" Text="*" ControlToValidate="tbUFCD" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbUFCD" CssClass="form-control" placeholder="Código da UFCD" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label>Descrição</label>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbDescricao" CssClass="form-control" placeholder="Descrição" runat="server" Rows="3"></asp:TextBox>
                                                                </div>
                                                                <label>Créditos</label>
                                                                <asp:RequiredFieldValidator ID="rvfCredits" runat="server" ErrorMessage="Quantidade de créditos obrigatória" Text="*" ControlToValidate="tbCredits" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revCredits" runat="server" ErrorMessage="Please enter a valid decimal number" Text="*" ControlToValidate="tbCredits" ForeColor="#8392AB" ValidationExpression="^[0-9]{0,7}\,[0-9]{1,9}$"></asp:RegularExpressionValidator>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbCredits" CssClass="form-control" placeholder="Créditos" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label>SVG da UFCD</label>
                                                                <div class="mb-3">
                                                                    <asp:FileUpload ID="fuSvgUFCD" runat="server" CssClass="form-control" placeholder="SVG da UFCD" />
                                                                </div>
                                                                <div class="text-center">
                                                                    <asp:Button ID="btnInsertModule" runat="server" Text="Inserir" OnClick="btnInsertModule_Click" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n8">
                                                            <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnInsertModule" />
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
            var insertDiv = document.getElementById('insertModulesDiv');
            var listDiv = document.getElementById('listModulesDiv');
            var btnInsert = document.getElementById('<%= btn_insertModule.ClientID %>');
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

    <!-- Funções da Javascript para Atualizar o URL, as Divs consoante o click e atualizar o breadcrumb -->
    <script>
        function handleLinkButtonClick(action) {
            var url, title;
            switch (action) {
                case 'List':
                    url = '../ManageModules.aspx?List';
                    document.getElementById('listModulesDiv').classList.remove('hidden');
                    document.getElementById('insertModulesDiv').classList.add('hidden');
                    title = "Listar Módulos";
                    break;
                case 'Edit':
                    url = '../ManageModules.aspx?Edit';
                    document.getElementById('listModulesDiv').classList.add('hidden');
                    document.getElementById('insertModulesDiv').classList.add('hidden');
                    title = "Editar Módulos";
                    break;
                case 'Insert':
                    url = '../ManageModules.aspx?Insert';
                    document.getElementById('insertModulesDiv').classList.remove('hidden');
                    document.getElementById('listModulesDiv').classList.add('hidden');
                    title = "Inserir Módulos";
                    break;
                default:
                    // Default URL or action if not recognized
                    url = '../ManageModules.aspx';
                    title = "Gestão de Módulos";
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
                breadcrumbItems.push('<a href="../ManageModules.aspx">Gestão de Módulos</a>');

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
