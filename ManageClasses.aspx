<%@ Page Title="Turmas" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClasses.aspx.cs" Inherits="FinalProject.ManageClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <div class="row" style="margin-top: 15px">
            <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Nova Turma" ID="btn_insertModule" OnClientClick="showInsert()" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_back" OnClientClick="showInsert()" />
            </div>
            <div id="filtermenu" class="col-md-6 col-sm-6 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
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
                        <div class="container bg-secundary py-2">
                            <div id="listClassesDiv">
                                <asp:UpdatePanel ID="updatePanelListClasses" runat="server">
                                    <ContentTemplate>
                                        <div class="py-3 align-items-center row" style="padding-left: 28px;">
                                            <div class="col-sm-3">
                                                <small class="text-uppercase font-weight-bold">Turmas:</small>
                                            </div>
                                            <asp:Repeater ID="rptClasses" runat="server">
                                                <HeaderTemplate>
                                                    <div class="card-body px-0 pt-0 pb-2">
                                                        <div class="table-responsive p-0">
                                                            <table class="table align-items-center mb-0">
                                                                <thead>
                                                                <colgroup>
                                                                    <col style="width: 10%;" />
                                                                    <col style="width: 10%;" />
                                                                    <col style="width: 40%;" />
                                                                    <col style="width: 10%;" />
                                                                    <col style="width: 10%;" />
                                                                </colgroup>
                                                                    <tr>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º da Turma</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">N.º do Curso</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome da Turma</th>
                                                                        <th></th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <p class="mb-0 text-sm text-center">
                                                                <asp:Label runat="server" Text='<%# Eval("CodTurma") %>'></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p class="mb-0 text-sm text-center">
                                                                <asp:Label runat="server" Text='<%# Eval("CodCurso") %>'></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <div class="d-flex px-2">
                                                                <div class="my-auto">
                                                                    <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("NomeTurma") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                    <p class="mb-0 text-sm">
                                                                        <asp:Label ID="lblNome" runat="server" Text='<%# Eval("NomeTurma") %>' Visible="true"></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <p class="mb-0 text-sm text-center">
                                                                <asp:Label runat="server" Text='<%# Eval("DataInicio") %>'></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p class="mb-0 text-sm text-center">
                                                                <asp:Label runat="server" Text='<%# Eval("DataFim") %>'></asp:Label>
                                                            </p>
                                                        </td>
                                                        <%--<td class="align-middle font-weight-bold text-center">
                                                    <asp:LinkButton runat="server" ID="lbtEditStudentsCourses" CausesValidation="false" CommandName="EditCoursesStudents" Visible="true" CommandArgument='<%# Eval("CodFormando") %>'
                                                        Text="Escolher Cursos" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton></td>
                                                <td class="align-middle font-weight-bold text-center">
                                                    <asp:LinkButton runat="server" ID="lbtEditStudents" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Eval("CodFormando") %>'
                                                        Text="Edit" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                </td>
                                                <td class="align-middle text-center">
                                                    <asp:LinkButton runat="server" ID="lbt_delete" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Eval("CodFormando") %>'
                                                        Text="Delete" class="text-secondary font-weight-bold text-xs">
                                                    </asp:LinkButton>
                                                </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                </table>
                                                </div>
                                                </div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <div class="col-12">
                                                <ul class="pagination justify-content-center" style="padding: 2px;">
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnPreviousClasses" CssClass="page-link" CausesValidation="false" runat="server">
                                                    <i class="fa fa-angle-left"></i>
                                                    <span class="sr-only">Previous</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li class="page-item active">
                                                        <span class="page-link">
                                                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberClasses"></asp:Label></span>
                                                    </li>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnNextClasses" CssClass="page-link" CausesValidation="false" runat="server">
                                                    <i class="fa fa-angle-right"></i>
                                                    <span class="sr-only">Next</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnPreviousClasses" />
                                        <asp:AsyncPostBackTrigger ControlID="btnNextClasses" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div id="insertClassesDiv" class="hidden">
                                <asp:UpdatePanel ID="updatePanelInsertClassGroup" runat="server">
                                    <ContentTemplate>
                                        <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                            <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                        </div>
                                        <div class="page-header min-vh-30">
                                            <div class="container">
                                                <div class="row ">
                                                    <div class="col-xl-10 col-lg-9 col-md-8">
                                                        <div class="card card-plain">
                                                            <!-- Inserção de Nova Turma -->
                                                            <div class="card-header pb-0 text-left bg-transparent">
                                                                <h5 class="font-weight-bolder text-info text-gradient">Criação de Nova Turma:</h5>
                                                            </div>
                                                            <div class="card-body">
                                                                <div role="form">
                                                                    <label>Designação do Curso</label>
                                                                    <div class="mb-2">
                                                                        <asp:DropDownList ID="ddlCurso" OnSelectedIndexChanged="ddlCurso_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server" DataSourceID="SQLDSCurso" DataTextField="nomeCurso" DataValueField="codCurso"></asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SQLDSCurso" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM curso"></asp:SqlDataSource>
                                                                    </div>
                                                                    <label>Nr.º da Turma</label>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoCurso" runat="server" ErrorMessage="N.º da Turma Obrigatório" Text="*" ControlToValidate="tbTurma" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <div class="mb-2">
                                                                        <asp:TextBox ID="tbTurma" CssClass="form-control" placeholder="Turma n.º" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <label>Data Prevista de Ínicio</label>
                                                                    <asp:RequiredFieldValidator ID="rfvDataInicio" Text="*" ErrorMessage="Data Prevista de Início Obrigatória" runat="server" ControlToValidate="tbDataInicio" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbDataInicio" runat="server" CssClass="form-control datepicker" TextMode="Date"></asp:TextBox>
                                                                    <label>Data Prevista de Fim</label>
                                                                    <asp:RequiredFieldValidator ID="rfvDataFim" Text="*" ErrorMessage="Data Prevista de Fim obrigatória" runat="server" ControlToValidate="tbDataFim" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbDataFim" runat="server" CssClass="form-control datepicker" TextMode="Date"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:Repeater ID="rptStudents" runat="server" OnItemDataBound="rptStudents_OnItemDataBound">
                                                                        <HeaderTemplate>
                                                                            <div class="card-body px-0 pt-0 pb-2">
                                                                                <div class="table-responsive p-0">
                                                                                    <table class="table align-items-center mb-0">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º de Formando</th>
                                                                                                <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                                                <th class="col-sm-2"></th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <p class="mb-0 text-sm text-center">
                                                                                        <asp:Label runat="server" Text='<%# Eval("CodFormando") %>'></asp:Label>
                                                                                    </p>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="d-flex px-2">
                                                                                        <asp:Image ID="imgPhoto" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />
                                                                                        <div class="my-auto">
                                                                                            <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                                            <p class="mb-0 text-sm">
                                                                                                <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                                                            </p>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td id="ForMyCkbEdit" class="text-xs font-weight-bold">
                                                                                    <div class="stats">
                                                                                        <asp:HiddenField ID="hdnStudentForClassGroupID" runat="server" Value='<%# Eval("CodFormando") %>' />
                                                                                        <asp:HiddenField ID="hdnStudentForClassGroupName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                                        <div class="form-check">
                                                                                            <asp:CheckBox runat="server" AutoPostBack="True" ID="chkBoxStudentForClassGroup" OnCheckedChanged="chkBoxStudentForClassGroup_OnCheckedChanged" EnableViewState="true" />
                                                                                        </div>
                                                                                    </div>
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
                                                                    <div class="col-12">
                                                                        <ul class="pagination justify-content-center" style="padding: 2px;">
                                                                            <li class="page-item">
                                                                                <asp:LinkButton ID="btnPreviousStudents" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousStudents_Click" runat="server">
                                                                    <i class="fa fa-angle-left"></i>
                                                                    <span class="sr-only">Previous</span>
                                                                                </asp:LinkButton>
                                                                            </li>
                                                                            <li class="page-item active">
                                                                                <span class="page-link">
                                                                                    <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberStudents"></asp:Label></span>
                                                                            </li>
                                                                            <li class="page-item">
                                                                                <asp:LinkButton ID="btnNextStudents" CssClass="page-link" CausesValidation="false" OnClick="btnNextStudents_Click" runat="server">
                                                                    <i class="fa fa-angle-right"></i>
                                                                    <span class="sr-only">Next</span>
                                                                                </asp:LinkButton>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div id="listBoxStudentsForCourse" runat="server" class="hidden">
                                                                        <br />
                                                                        Listagem de Formandos:<br />
                                                                        <asp:ListBox ID="listBoxStudents" class="list-group" runat="server"></asp:ListBox>
                                                                    </div>
                                                                </div>
                                                                <%--  </div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <br />
                                                                    Módulos:
                                                                    <div class="dropdown">
                                                                        <asp:DropDownList ID="ddlModulesOfCourse" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModulesOfCourse_OnSelectedIndexChanged" AutoPostBack="True" class="form-control" runat="server" DataTextField="nomeModulos" DataValueField="codModulo"></asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SQLDSModulesForCourse" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
                                                                    </div>
                                                                    Formadores:
                                                                    <div class="dropdown">
                                                                        <asp:DropDownList ID="ddlTeacherForModules" AutoPostBack="True" class="form-control" runat="server" DataTextField="nome" DataValueField="codFormador"></asp:DropDownList>
                                                                        <asp:SqlDataSource ID="SQLDSTeachersForModules" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
                                                                    </div>
                                                                    <div class="text-center">
                                                                        <div class="col-md-8">
                                                                            <asp:Button ID="btnAddTeacherModuleClassGroup" runat="server" Text="Adicionar Módulo|Formador" CausesValidation="False" OnClick="btnAddTeacherModuleClassGroup_OnClick" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                        </div>
                                                                    </div>
                                                                    <div id="listBoxTeachersModules" class="hidden" runat="server">
                                                                        <br />
                                                                        Listagem de Formadores por Módulos:
                                                                        <br />
                                                                        <asp:ListBox ID="listBoxTeachersForModules" class="list-group" runat="server"></asp:ListBox>

                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:Button ID="btnRemoveTeacherModuleClassGroup" runat="server" Text="Remover Módulo|Formador" CausesValidation="False" OnClick="btnRemoveTeacherModuleClassGroup_OnClick" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="text-center">
                                                                <asp:Button ID="btnInsertClass" runat="server" Text="Inserir" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnNextStudents" />
                                        <asp:AsyncPostBackTrigger ControlID="btnPreviousStudents" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAddTeacherModuleClassGroup" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCurso" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlModulesOfCourse" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlTeacherForModules" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-md-2">
                                    <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n12">
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

    <!-- Função de Javascript para Mostrar a Div de Inserir após click no Button Inserir Módulo -->
    <script>
        function showInsert() {
            var insertDiv = document.getElementById('insertClassesDiv');
            var listDiv = document.getElementById('listClassesDiv');
            var btnInsert = document.getElementById('<%= btn_insertModule.ClientID %>');
            var btnBack = document.getElementById('<%= btn_back.ClientID %>');
            var filterMenu = document.getElementById('filtermenu')

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
    <script>
        function handleLinkButtonClick(action) {
            var url, title;
            switch (action) {
                case 'List':
                    url = '../ManageClasses.aspx?List';
                    document.getElementById('listClassesDiv').classList.remove('hidden');
                    document.getElementById('editClassesDiv').classList.add('hidden');
                    document.getElementById('insertClassesDiv').classList.add('hidden');
                    title = "Listar Turmas";
                    break;
                case 'Edit':
                    url = '../ManageClasses.aspx?Edit';
                    document.getElementById('editClassesDiv').classList.remove('hidden');
                    document.getElementById('listClassesDiv').classList.add('hidden');
                    document.getElementById('insertClassesDiv').classList.add('hidden');
                    title = "Editar Turmas";
                    break;
                case 'Insert':
                    url = '../ManageClasses.aspx?Insert';
                    document.getElementById('insertClassesDiv').classList.remove('hidden');
                    document.getElementById('listClassesDiv').classList.add('hidden');
                    document.getElementById('editClassesDiv').classList.add('hidden');
                    title = "Inserir Turmas";
                    break;
                default:
                    // Default URL or action if not recognized
                    url = '../ManageClasses.aspx';
                    title = "Gestão de Turmas";
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
                breadcrumbItems.push('<a href="../ManageClasses.aspx">Gestão de Turmas</a>');

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
</asp:Content>
