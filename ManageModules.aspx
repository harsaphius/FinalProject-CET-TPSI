<%@ Page Title="Módulos" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageModules.aspx.cs" Inherits="FinalProject.ManageModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div class="row" style="margin-top: 15px">
                    <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Módulo" ID="btn_insertModule" OnClientClick="showInsert()" />
                    </div>
                    <div class="col-md-6 col-sm-6 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
                        <a href="javascript:;" onclick="toggleFilters()">
                            <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                        </a>
                    </div>
                    <div id="filters" class="col-md-12 col-md-6 hidden" style="padding-left: 30px;">
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
                </div>

                <div class="container-fluid py-4">
                    <div class="row">
                        <div class="col-12">
                            <div class="card mb-4">
                                <div id="listModulesDiv">
                                    <div class="card-header pb-0">
                                        <h6>Módulos</h6>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Repeater ID="rpt_Modules" runat="server" OnItemDataBound="rpt_Modules_ItemDataBound" OnItemCommand="rpt_Modules_ItemCommand">
                                                <HeaderTemplate>
                                                    <div class="card-body px-0 pt-0 pb-2">
                                                        <div class="table-responsive p-0">
                                                            <table class="table align-items-center mb-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                                        <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                                        <th class="col-sm-4 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Descrição</th>
                                                                        <th class="col-sm-2"></th>
                                                                        <th class="col-sm-2"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td id="cellNome">
                                                            <div class="d-flex px-2">

                                                                <div>

                                                                    <%-- <a runat="server" href="javascript:;">--%>
                                                                </div>
                                                                <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("SVG") %>' onclick="triggerFileUpload('<%= fileUpload.ClientID %>')" />
                                                                <%--   <asp:FileUpload ID="fileUpload" runat="server" Visible="false" onchange="uploadFile(this)" />
                                                                    </a>--%>
                                                                <div class="my-auto">
                                                                    <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                    <p class="mb-0 text-sm">
                                                                        <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td id="cellUFCD">
                                                            <asp:TextBox ID="tbUFCD" CssClass="form-control" runat="server" Text='<%# Bind("UFCD") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                            <p class="text-sm mb-0">
                                                                <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("UFCD") %>' Visible="true" Style="width: 100%;"></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td id="cellDescricao">
                                                            <asp:TextBox ID="tbDescricao" CssClass="form-control" runat="server" Text='<%# Bind("Descricao") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                            <asp:Label ID="lblDescricao" class="text-xs" runat="server" Text='<%# Eval("Descricao") %>' Style="width: 100%;" Visible="true"></asp:Label></p>
                                                        </td>
                                                        <td class="align-middle font-weight-bold text-center">
                                                            <asp:LinkButton runat="server" ID="lbt_edit" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                                Text="Edit" class="text-secondary font-weight-bold text-xs">
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbt_cancel" CausesValidation="false" CommandName="Cancel" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                                Text="Cancel" class="text-secondary font-weight-bold text-xs">
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td class="align-middle text-center">
                                                            <asp:LinkButton runat="server" ID="lbt_delete" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                                Text="Delete" class="text-secondary font-weight-bold text-xs">
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbt_confirm" CausesValidation="false" CommandName="Confirm" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
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

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                            <asp:LinkButton ID="btn_previousM" CssClass="page-link" CausesValidation="false" OnClick="btn_previousM_Click" runat="server">
                                                    <i class="fa fa-angle-left"></i>
                                                    <span class="sr-only">Previous</span>
                                            </asp:LinkButton>
                                        </li>
                                        <li></li>
                                        <li class="page-item">
                                            <asp:LinkButton ID="btn_nextM" CssClass="page-link" CausesValidation="false" OnClick="btn_nextM_Click" runat="server">
                                                    <i class="fa fa-angle-right"></i>
                                                    <span class="sr-only">Next</span>
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                                <div id="insertModulesDiv" class="hidden">
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
                                                                <asp:RegularExpressionValidator ID="revCredits" runat="server" ErrorMessage="Please enter a valid decimal number" Text="*" ControlToValidate="tbCredits" ForeColor="#8392AB" ValidationExpression="^[0-9]{0,7}\,[0-9]{1,9}$"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="rvfCredits" runat="server" ErrorMessage="Quantidade de créditos obrigatória" Text="*" ControlToValidate="tbCredits" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                <div class="mb-3">
                                                                    <asp:TextBox ID="tbCredits" CssClass="form-control" placeholder="Créditos" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label>SVG da UFCD</label>
                                                                <div class="mb-3">
                                                                    <asp:FileUpload ID="fuSvgUFCD" runat="server" CssClass="form-control" placeholder="SVG da UFCD" />
                                                                </div>
                                                                <div class="text-center">
                                                                    <asp:Button ID="btn_insert" runat="server" Text="Inserir" OnClick="btn_insert_Click" class="btn bg-gradient-info w-100 mt-4 mb-0" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




                </div>
                        </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_previousM" />
                <asp:AsyncPostBackTrigger ControlID="btn_nextM" />
                <asp:AsyncPostBackTrigger ControlID="btn_insertModule" />
            </Triggers>
        </asp:UpdatePanel>
    </div>



    <!-- Função de Javascript para Mostrar a Div de Editar após click no LinkButton Edit do Repeater -->
    <script>
        function showInsert() {
            var insertDiv = document.getElementById('insertModulesDiv');
            var listDiv = document.getElementById('listModulesDiv');
            if (insertDiv.classList.contains('hidden')) {
                insertDiv.classList.remove('hidden');
                listDiv.classList.add('hidden');
            } else {
                insertDiv.classList.add('hidden');
                listDiv.classList.remove('hidden');
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
