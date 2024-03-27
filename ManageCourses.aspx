<%@ Page Title="Gestão de Cursos" EnableEventValidation="false" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" EnableViewState="true" Inherits="FinalProject.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 15px">
            <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Curso" ID="btn_insertCourse" OnClientClick="showInsert(); return false;" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_back" OnClientClick="showInsert(); return false;" OnClick="btn_back_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btn_backEditModules" OnClientClick="showEditModules(); return false;" />
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
                                    <asp:LinkButton runat="server" ID="lbtnSearchFilters" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Área:</span>
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlAreaCursoFilters" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                <span>Tipo:</span>
                                <div class="dropdown">
                                    <asp:DropDownList ID="ddlTipoCursoFilters" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
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
                            </div>
                            <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
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
                        <asp:AsyncPostBackTrigger ControlID="btnClearFilters" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="container-fluid py-4">
            <div class="row">
                <div class="col-12">
                    <div class="card mb-4">
                        <div id="listCoursesDiv">
                            <asp:UpdatePanel ID="updatePanelListCourses" runat="server">
                                <ContentTemplate>
                                    <!-- Listagem de Cursos -->
                                    <div class="card-header pb-0">
                                        <h6>Cursos</h6>
                                    </div>
                                    <asp:Repeater ID="rptListCourses" runat="server" OnItemCommand="rptListCourses_OnItemCommand" OnItemDataBound="rptListCourses_OnItemDataBound">
                                        <HeaderTemplate>
                                            <div class="card-body px-0 pt-0 pb-2">
                                                <div class="table-responsive p-0">
                                                    <table class="table align-items-center mb-0">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Curso</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Referencial</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código QNQ</th>
                                                                <th></th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class="d-flex px-2">
                                                        <div class="my-auto">
                                                            <h6 class="mb-0 text-sm">
                                                                <asp:Label runat="server" ID="lblListCoursesCodCurso"><%# Eval("CodCurso") %></asp:Label>
                                                            </h6>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbListCoursesNomeCurso" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                    <p class="text-sm font-weight-bold mb-0">
                                                        <asp:Label runat="server" ID="lblListCoursesNomeCurso"><%# Eval("Nome") %></asp:Label>
                                                    </p>
                                                </td>
                                                <td class="text-xs font-weight-bold">
                                                    <asp:TextBox ID="tbListCoursesCodRef" CssClass="form-control" runat="server" Text='<%# Bind("CodRef") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                    <asp:Label ID="lblListCoursesCodRef" runat="server">  <%# Eval("CodRef") %></asp:Label>
                                                </td>
                                                <td class="text-xs font-weight-bold">
                                                    <asp:TextBox ID="tbListCoursesCodQNQ" CssClass="form-control" runat="server" Text='<%# Bind("CodQNQ") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                    <asp:Label ID="lblListCoursesCodQNQ" runat="server"> Nível <%# Eval("CodQNQ") %></asp:Label>
                                                </td>
                                                <td class="align-middle font-weight-bold text-center">
                                                    <asp:LinkButton runat="server" ID="lbtEditEditCourse" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Eval("CodCurso") %>'
                                                        Text="Edit" class="text-secondary font-weight-bold text-xs" EnableViewState="True" AutoPostBack="true">
                                                    </asp:LinkButton>
                                                </td>
                                                <td class="align-middle text-center">
                                                    <asp:LinkButton runat="server" ID="lbtDeleteEditCourse" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                        Text="Delete" class="text-secondary font-weight-bold text-xs">
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
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="btnPreviousListCourses" CssClass="page-link" OnClick="btnPreviousListCourses_Click" CausesValidation="false" runat="server">
                                                <i class="fa fa-angle-left"></i>
                                                <span class="sr-only">Previous</span>
                                            </asp:LinkButton>
                                        </li>
                                        <li class="page-item">
                                            <asp:Label CssClass="sr-only" runat="server" ID="lblPageNumberListCourses"></asp:Label>
                                        </li>
                                        <li class="page-item">
                                            <asp:LinkButton ID="btnNextListCourses" CssClass="page-link" OnClick="btnNextListCourses_Click" CausesValidation="false" runat="server">
                                                <i class="fa fa-angle-right"></i>
                                                <span class="sr-only">Next</span>
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                    <!--AsyncPostBackTrigger dos botões a ser gerado no C# -->
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousListCourses" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextListCourses" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="editModulesCourse" class="hidden">
                            <asp:UpdatePanel ID="updatePanelEditModulesCourses" runat="server">
                                <ContentTemplate>
                                    <div class="page-header min-vh-30">
                                        <div class="container">
                                            <div class="row ">
                                                <div class="col-xl-8 col-lg-7 col-md-6">
                                                    <div class="card card-plain">
                                                        <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                                            <asp:Label runat="server" ID="Label1" CssClass="text-white"></asp:Label>
                                                        </div>
                                                        <div class="card card-plain">
                                                            <div class="card-header pb-0 text-left bg-transparent">
                                                                <h5 class="font-weight-bolder text-info text-gradient">Edição do curso:</h5>
                                                            </div>
                                                            <!-- Inserção de Dados de Curso - Textboxes e DDL's -->
                                                            <div class="card-body" id="editModulesCourseTextBoxes" runat="server">
                                                                <div role="form">
                                                                    <label>Nome do Curso</label>
                                                                    <asp:RequiredFieldValidator ID="rfvCourseNameEditCourse" ValidationGroup="EditForm" ErrorMessage="Nome do curso obrigatório" Text="*" runat="server" ControlToValidate="tbCourseNameEditCourse" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <div class="mb-2">
                                                                        <asp:TextBox ID="tbCourseNameEditCourse" CssClass="form-control" ValidationGroup="EditForm" placeholder="Nome do Curso" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <label>Tipo de Curso</label>
                                                                    <div class="mb-2">
                                                                        <asp:DropDownList ID="ddlTipoCursoEditCourse" CssClass="form-control" ValidationGroup="EditForm" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso"></asp:DropDownList>
                                                                    </div>

                                                                    <label>Área do Curso</label>
                                                                    <div class="mb-2">
                                                                        <asp:DropDownList ID="ddlAreaCursoEditCourse" ValidationGroup="EditForm" CssClass="form-control" runat="server" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                                                    </div>

                                                                    <label>Referencial n.º:</label>
                                                                    <asp:RequiredFieldValidator ID="rfvRefEditCourse" ValidationGroup="EditForm" runat="server" ErrorMessage="N.º de Referencial obrigatório" Text="*" ControlToValidate="tbRefEditCourse" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <div class="mb-2">
                                                                        <asp:TextBox ID="tbRefEditCourse" ValidationGroup="EditForm" CssClass="form-control" placeholder="Referencial n.º" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <label>Qualificação QNQ</label>
                                                                    <div class="mb-2">
                                                                        <asp:DropDownList ID="ddlQNQEditCourse" ValidationGroup="EditForm" CssClass="form-control" runat="server" DataSourceID="SQLDSQNQ" DataTextField="codQNQ" DataValueField="codQNQ"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <asp:Repeater ID="rptEditModulesCourse" runat="server" OnItemDataBound="rptEditModulesCourse_OnItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="row px-2" style="padding: 10px;">
                                                                            <!-- Editar Módulos de um Curso - Seleção dos Módulos -->
                                                                            <small class="text-uppercase font-weight-bold"></small>
                                                                            <p>Edite os módulos pertencentes a este curso</p>
                                                                            <asp:Label runat="server" ID="lbl_CursoModulo"></asp:Label>
                                                                            <div class="input-group mb-4">
                                                                                <asp:LinkButton runat="server" ID="lbtn_search" class="input-group-text text-body">
                                                                                <i class="fas fa-search" aria-hidden="true"></i>
                                                                                </asp:LinkButton>
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
                                                                            <td id="ForMyCkbEdit" class="text-xs font-weight-bold">
                                                                                <div class="stats">
                                                                                    <asp:HiddenField ID="hdnEditCourseModuleID" runat="server" Value='<%# Eval("CodModulo") %>' />
                                                                                    <asp:HiddenField ID="hdnEditCourseModuleName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                                    <div class="form-check">
                                                                                        <asp:CheckBox runat="server" ID="chkBoxEditModulesCourse" OnCheckedChanged="chkBoxEditModulesCourse_CheckedChanged" />
                                                                                        <asp:Label runat="server" ID="lblOrderEditModulesCourse">Selecione este módulo</asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                <asp:PlaceHolder runat="server" ID="footerPlaceHolder"></asp:PlaceHolder>
                                                                        </div>
                                                                </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                                <!-- Paginação dos Módulos -->
                                                                <ul class="pagination">
                                                                    <li class="page-item">
                                                                        <asp:LinkButton ID="btnPreviousEditModulesCourses" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousEditModulesCourses_OnClick" runat="server">
                                                                        <i class="fa fa-angle-left"></i>
                                                                        <span class="sr-only">Previous</span>
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <asp:Label CssClass="sr-only" runat="server" ID="lblPageNumberEditCoursesModules"></asp:Label>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <asp:LinkButton ID="btnNextEditModulesCourses" CssClass="page-link" CausesValidation="false" OnClick="btnNextEditModulesCourses_OnClick" runat="server">
                                                                        <i class="fa fa-angle-right"></i>
                                                                        <span class="sr-only">Next</span>
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                                <!-- Fim da Paginação dos Módulos -->

                                                                <asp:Button runat="server" CssClass="btn btn-primary" Text="Editar Curso" ID="btnEditCourse" ValidationGroup="EditForm" CausesValidation="true" AutoPostBack="true" OnClick="btnEditCourse_OnClick" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnEditCourse" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousEditModulesCourses" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextEditModulesCourses" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <!-- Fim da Listagem de Cursos -->
                    </div>
                </div>
                <!-- Inserção de Cursos -->
                <asp:HiddenField ID="hfInsertCoursesVisible" runat="server" Value="false" />
                <div id="insertCoursesDiv" class="hidden">
                    <asp:UpdatePanel runat="server" ID="updatePanelInsertCourses">
                        <ContentTemplate>
                            <div class="page-header min-vh-30">
                                <div class="container">
                                    <div class="row ">
                                        <div class="col-xl-8 col-lg-7 col-md-6">
                                            <div class="card card-plain">
                                                <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                                    <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                                </div>
                                                <div class="card card-plain">
                                                    <div class="card-header pb-0 text-left bg-transparent">
                                                        <h5 class="font-weight-bolder text-info text-gradient">Inserção de novo curso:</h5>
                                                    </div>
                                                    <!-- Inserção de Dados de Curso - Textboxes e DDL's -->
                                                    <div class="card-body">
                                                        <div role="form">
                                                            <label>Nome do Curso</label>
                                                            <asp:RequiredFieldValidator ID="rfvCourseName" ValidationGroup="insertForm" ErrorMessage="Nome do curso obrigatório" Text="*" runat="server" ControlToValidate="tbCourseName" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:TextBox ID="tbCourseName" CssClass="form-control" ValidationGroup="insertForm" placeholder="Nome do Curso" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label>Tipo de Curso</label>
                                                            <asp:RequiredFieldValidator ID="rfvTipoCurso" ValidationGroup="insertForm" runat="server" ErrorMessage="Tipo de curso obrigatório" Text="*" ControlToValidate="ddlTipoCurso" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:DropDownList ID="ddlTipoCurso" ValidationGroup="insertForm" CssClass="form-control" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso"></asp:DropDownList>
                                                            </div>

                                                            <label>Área do Curso</label>
                                                            <asp:RequiredFieldValidator ID="rvfarea" ValidationGroup="insertForm" runat="server" ErrorMessage="Área do curso obrigatória" Text="*" ControlToValidate="ddlAreaCurso" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:DropDownList ID="ddlAreaCurso" ValidationGroup="insertForm" CssClass="form-control" runat="server" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                                            </div>

                                                            <label>Referencial n.º:</label>
                                                            <asp:RequiredFieldValidator ID="rfvRef" ValidationGroup="insertForm" runat="server" ErrorMessage="N.º de Referencial obrigatório" Text="*" ControlToValidate="tbRef" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:TextBox ID="tbRef" CssClass="form-control" ValidationGroup="insertForm" placeholder="Referencial n.º" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label>Qualificação QNQ</label>
                                                            <asp:RequiredFieldValidator ID="rfvQNQ" runat="server" ValidationGroup="insertForm" ErrorMessage="Nível de Qualificação QNQ obrigatória" Text="*" ControlToValidate="ddlQNQ" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                            <div class="mb-2">
                                                                <asp:DropDownList ValidationGroup="insertForm" ID="ddlQNQ" CssClass="form-control" runat="server" DataSourceID="SQLDSQNQ" DataTextField="codQNQ" DataValueField="codQNQ"></asp:DropDownList>
                                                                <asp:SqlDataSource ID="SQLDSQNQ" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT CONCAT('Nível ', codQNQ) AS codQNQ FROM [nivelQNQ]"></asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <asp:Repeater ID="rptInsertCourses" runat="server" OnItemDataBound="rptInsertCourses_OnItemDataBound">
                                                            <HeaderTemplate>
                                                                <div class="row px-2" style="padding: 10px;">
                                                                    <!-- Inserção de Dados de Curso - Seleção dos Módulos -->
                                                                    <small class="text-uppercase font-weight-bold">Módulos do Curso:</small>
                                                                    <p>Seleccione os módulos pertencentes a este curso</p>
                                                                    <asp:Label runat="server" ID="lbl"></asp:Label>
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
                                                                    <td id="ForMyCkb" class="text-xs font-weight-bold">
                                                                        <div class="stats">
                                                                            <asp:HiddenField ID="hdnInsertModuleID" runat="server" Value='<%# Eval("CodModulo") %>' />
                                                                            <asp:HiddenField ID="hdnInsertModuleName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                            <div class="form-check">
                                                                                <asp:CheckBox runat="server" ID="chkBoxInsertModulesCourse" OnCheckedChanged="chkBoxInsertModulesCourse_CheckedChanged" AutoPostBack="True" EnableViewState="true" />
                                                                                <asp:Label runat="server" ID="lblOrderInsertModules">Selecione este módulo</asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                                    <asp:PlaceHolder runat="server" ID="footerPlaceHolder"></asp:PlaceHolder>
                                                                </div>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                                <!-- Paginação dos Módulos -->
                                                <ul class="pagination" style="padding-left: 15px;">
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnPreviousInsertCoursesModules" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousInsertCoursesModules_Click" runat="server">
                                                                <i class="fa fa-angle-left"></i>
                                                                <span class="sr-only">Previous</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:Label CssClass="page-item" runat="server" ID="lblPageNumberInsertCourses"></asp:Label>
                                                    </li>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnNextInsertCoursesModules" CssClass="page-link" CausesValidation="false" OnClick="btnNextInsertCoursesModules_Click" runat="server">
                                                                <i class="fa fa-angle-right"></i>
                                                                <span class="sr-only">Next</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                                <!-- Fim da Paginação dos Módulos -->
                                                <div class="row px-4" style="padding: 10px;">
                                                    <small class="text-uppercase font-weight-bold">Ordem dos Módulos:</small>
                                                    <asp:Label runat="server" ID="lblOrderOfModulesSelected">  </asp:Label>
                                                </div>
                                            </div>
                                            <div class="row flex-row">
                                                <div class="align-center text-center col-md-3" style="margin-bottom: 10px;">
                                                    <asp:Button ID="btnInsertCourse" ValidationGroup="insertForm" runat="server" Text="Inserir" CausesValidation="true" OnClick="btnInsertCourse_Click" AutoPostBack="true" class="btn bg-gradient-info w-100 mt-4 mb-0" />
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

                            <!-- Fim de Inserção de Cursos -->
                            <!--AsyncPostBackTrigger da CheckBox a ser gerado no C# -->
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnInsertCourse" />
                            <asp:AsyncPostBackTrigger ControlID="btnPreviousInsertCoursesModules" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNextInsertCoursesModules" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <script>
        $(function () {
            $(".draggable-item").draggable({
                revert: "invalid",
                cursor: "move",
                helper: "clone",
                zIndex: 100
            });

            $(".draggable-item").droppable({
                tolerance: "pointer",
                drop: function (event, ui) {
                    var draggedItem = ui.draggable;
                    var droppedOnItem = $(this);

                    // If dragged onto itself, do nothing
                    if (draggedItem.is(droppedOnItem)) {
                        return;
                    }

                    // Get the index of dragged and dropped items
                    var draggedIndex = draggedItem.index();
                    var droppedIndex = droppedOnItem.index();

                    // Calculate the direction of movement
                    var moveUp = draggedIndex > droppedIndex;

                    // Move the dragged item accordingly
                    if (moveUp) {
                        draggedItem.insertBefore(droppedOnItem);
                    } else {
                        draggedItem.insertAfter(droppedOnItem);
                    }

                    // Send AJAX request to update the order on the server
                    $.ajax({
                        type: "POST",
                        url: "YourPage.aspx/UpdateOrder",
                        data: JSON.stringify({ draggedItemId: draggedItem.attr("id"), droppedOnItemId: droppedOnItem.attr("id") }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Handle error
                        }
                    });
                }
            });
        });
    </script>

    <!-- Função de Javascript para Mostrar a Div de Inserir após click no Button Inserir Módulo -->
    <script>
        function showInsert() {
            var insertDiv = document.getElementById('insertCoursesDiv');
            var listDiv = document.getElementById('listCoursesDiv');
            var btnInsert = document.getElementById('<%= btn_insertCourse.ClientID %>');
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

    <!-- Função de Javascript para Mostrar a Div de Inserir após click no Button Inserir Curso -->
    <script>
        function showEditModules() {
            var editModulesDiv = document.getElementById('editModulesCourse');
            var listDiv = document.getElementById('listCoursesDiv');
            var btnInsert = document.getElementById('<%= btn_insertCourse.ClientID %>');
            var btnBack = document.getElementById('<%= btn_backEditModules.ClientID %>');
            var filterMenu = document.getElementById('filtermenu');

            if (editModulesDiv.classList.contains('hidden')) {
                editModulesDiv.classList.remove('hidden');
                listDiv.classList.add('hidden');
                btnInsert.classList.add('hidden');
                btnBack.classList.remove('hidden');
                filterMenu.classList.add('hidden');

            } else {
                editModulesDiv.classList.add('hidden');
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
                    url = '../ManageCourses.aspx?List';
                    document.getElementById('listCoursesDiv').classList.remove('hidden');
                    document.getElementById('insertCoursesDiv').classList.add('hidden');
                    title = "Listar Cursos";
                    break;
                case 'Edit':
                    url = '../ManageCourses.aspx?Edit';
                    document.getElementById('listCoursesDiv').classList.add('hidden');
                    document.getElementById('insertCoursesDiv').classList.add('hidden');
                    title = "Editar Cursos";
                    break;
                case 'Insert':
                    url = '../ManageCourses.aspx?Insert';
                    document.getElementById('insertCoursesDiv').classList.remove('hidden');
                    document.getElementById('listCoursesDiv').classList.add('hidden');
                    title = "Inserir Cursos";
                    break;
                default:
                    // Default URL or action if not recognized
                    url = '../ManageCourses.aspx';
                    title = "Gestão de Cursos";
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
                breadcrumbItems.push('<a href="../ManageCourses.aspx">Gestão de Cursos</a>');

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
