<%@ Page Title="Gestão de Cursos" EnableEventValidation="false" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" EnableViewState="true" Inherits="FinalProject.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- FlatPickr -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 15px">
                    <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Curso" Visible="True" ID="btnInsertCourseMain" OnClick="btnInsertCourseMain_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" Visible="False" Text="Voltar" ID="btnBack" OnClick="btnBack_OnClick" />
                    </div>
                    <div class="col-md-6 col-sm-6 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
                        <asp:LinkButton ID="filtermenu" Visible="True" OnClick="filtermenu_OnClick" runat="server">
                    <i class="fas fa-filter text-primary text-lg" title="Filter" aria-hidden="true">Filtros</i>
                        </asp:LinkButton>
                    </div>
                    <div id="filters" class="col-md-12 col-md-6" visible="false" runat="server" style="padding-left: 30px;">
                        <asp:UpdatePanel ID="updatePanelFilters" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-xl-6 col-sm-6 mb-xl-0 mb-4">
                                        <span>Designação:</span>
                                        <div class="input-group mb-4">
                                            <asp:LinkButton runat="server" ID="lbtnSearchFilters" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Type here..." AutoPostBack="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                        <span>Área:</span>
                                        <div class="dropdown">
                                            <asp:DropDownList ID="ddlAreaCursoFilters" AutoPostBack="False" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea" AppendDataBoundItems="True"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                        <span>Tipo:</span>
                                        <div class="dropdown">
                                            <asp:DropDownList ID="ddlTipoCursoFilters" AutoPostBack="False" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso" AppendDataBoundItems="True"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-lg-4 col-md-4 col-sm-4 mb-xl-0 mb-0">
                                        <span>Ordenação:
                                        </span>
                                        <div class="input-group mb-0 text-end">
                                            <asp:DropDownList runat="server" ID="ddlOrderFilters" class="btn bg-gradient-secundary dropdown-toggle">
                                                <asp:ListItem Value="ASC">A-Z</asp:ListItem>
                                                <asp:ListItem Value="DESC">Z-A</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-sm-4 mb-xl-0 mb-4">
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
                <div class="container-fluid py-4">
                    <div class="row">
                        <div class="col-12">
                            <div class="card mb-4">
                                <div id="listCoursesDiv" runat="server">
                                    <asp:UpdatePanel ID="updatePanelListCourses" runat="server">
                                        <ContentTemplate>
                                            <div class="container row justify-content-center">
                                                <asp:Label runat="server" ID="lblMessageListCourses" Style="display: flex; justify-content: center; width: 70%; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                <asp:Timer ID="timerMessageListCourses" runat="server" Interval="3000" OnTick="timerMessageListCourses_OnTick" Enabled="false"></asp:Timer>
                                            </div>
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
                                                                    <colgroup>
                                                                        <col style="width: 5%;" />
                                                                        <col style="width: 50%;" />
                                                                        <col style="width: 12%;" />
                                                                        <col style="width: 12%;" />
                                                                        <col style="width: 12%;" />
                                                                        <col style="width: 10%;" />
                                                                        <col style="width: 10%;" />
                                                                    </colgroup>
                                                                    <tr>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Curso</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Referencial</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código QNQ</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Duração</th>
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
                                                        <td style="width: auto; white-space: normal; padding: 2px;">
                                                            <p class="text-sm font-weight-bold mb-0">
                                                                <asp:Label runat="server" ID="lblListCoursesNomeCurso"><%# Eval("Nome") %></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td class="text-xs font-weight-bold">
                                                            <asp:Label ID="lblListCoursesCodRef" runat="server">  <%# Eval("CodRef") %></asp:Label>
                                                        </td>
                                                        <td class="text-xs font-weight-bold">
                                                            <asp:Label ID="lblListCoursesCodQNQ" runat="server"> Nível <%# Eval("CodQNQ") %></asp:Label>
                                                        </td>
                                                        <td class="text-xs font-weight-bold">
                                                            <asp:Label ID="lblListCoursesDuracao" runat="server"> <%# Eval("Duracao") %></asp:Label>
                                                        </td>
                                                        <td class="align-middle font-weight-bold text-center">
                                                            <asp:LinkButton runat="server" ID="lbtEditEditCourse" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Eval("CodCurso") %>'
                                                                Text="Edit" class="text-secondary font-weight-bold text-xs" EnableViewState="True" AutoPostBack="true">
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td class="align-middle text-center">
                                                            <asp:LinkButton runat="server" ID="lbtDeleteEditCourse" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Eval("CodCurso") %>'
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
                                            <!-- Paginação dos Cursos -->
                                            <div class="col-12">
                                                <ul class="pagination justify-content-center" style="padding: 2px;">
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnPreviousListCourses" CssClass="page-link" OnClick="btnPreviousListCourses_Click" CausesValidation="false" runat="server">
                                                <i class="fa fa-angle-left"></i>
                                                <span class="sr-only">Previous</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li class="page-item active">
                                                        <span class="page-link">
                                                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberListCourses"></asp:Label></span>
                                                    </li>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnNextListCourses" CssClass="page-link" OnClick="btnNextListCourses_Click" CausesValidation="false" runat="server">
                                                <i class="fa fa-angle-right"></i>
                                                <span class="sr-only">Next</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                            <!--AsyncPostBackTrigger dos botões a ser gerado no C# -->
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnPreviousListCourses" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnNextListCourses" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="editModulesCourse" runat="server" visible="false">
                                    <asp:UpdatePanel ID="updatePanelEditModulesCourses" runat="server">
                                        <ContentTemplate>
                                            <div class="page-header min-vh-30">
                                                <div class="container">
                                                    <div class="row ">
                                                        <div class="col-xl-8 col-lg-8 col-md-6 col-sm-6">
                                                            <div class="card card-plain">
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
                                                                            <label>Duração do Curso</label>
                                                                            <div class="mb-2">
                                                                                <asp:Label runat="server" ID="lblDuracaoCursoEdit" CssClass="form-control"></asp:Label>
                                                                            </div>
                                                                            <label>Duração do Estágio</label>
                                                                            <div class="mb-2">
                                                                                <asp:TextBox runat="server" TextMode="Number" ID="tbDuracaoEstagioEdit" ValidationGroup="EditForm" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <br />

                                                                        <asp:Repeater ID="rptEditModulesCourse" runat="server" OnItemDataBound="rptEditModulesCourse_OnItemDataBound" EnableViewState="True">
                                                                            <HeaderTemplate>
                                                                                <div class="table-responsive">
                                                                                    <table class="table align-items-center justify-content-center mb-0">
                                                                                        <thead>
                                                                                            <colgroup>
                                                                                                <col style="width: 30%;" />
                                                                                                <col style="width: 10%;" />
                                                                                                <col style="width: 30%;" />
                                                                                            </colgroup>
                                                                                            <tr>
                                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Pertence</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                                        <div class="d-flex px-2">
                                                                                            <div>
                                                                                                <img src="<%# Eval("SVG") %>" class="avatar avatar-sm rounded-circle me-3">
                                                                                            </div>
                                                                                            <div class="my-auto">
                                                                                                <h6 class="mb-0 text-sm"><%# Eval("Nome") %></h6>
                                                                                            </div>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                                        <p class="text-sm font-weight-bold mb-0"><%# Eval("UFCD") %></p>
                                                                                    </td>
                                                                                    <td id="ForMyCkbEdit" class="text-xs font-weight-bold" style="width: auto; white-space: normal; padding: 2px;">
                                                                                        <asp:HiddenField ID="hdnEditCourseModuleID" runat="server" Value='<%# Eval("CodModulo") %>' />
                                                                                        <asp:HiddenField ID="hdnEditCourseModuleName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                                        <div class="form-check">
                                                                                            <asp:CheckBox runat="server" Checked='<%# Convert.ToBoolean(Eval("IsChecked")) %>' ID="chkBoxEditModulesCourse" OnCheckedChanged="chkBoxEditModulesCourse_CheckedChanged" AutoPostBack="true" EnableViewState="true" />
                                                                                            <asp:Label runat="server" ID="lblOrderEditModulesCourse">Seleccione este módulo</asp:Label>
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
                                                                        <div class="col-12">
                                                                            <ul class="pagination justify-content-center" style="padding: 2px;">
                                                                                <li class="page-item">
                                                                                    <asp:LinkButton ID="btnPreviousEditModulesCourses" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousEditModulesCourses_OnClick" runat="server">
                                                                        <i class="fa fa-angle-left"></i>
                                                                        <span class="sr-only">Previous</span>
                                                                                    </asp:LinkButton>
                                                                                </li>
                                                                                <li class="page-item active">
                                                                                    <span class="page-link">
                                                                                        <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberEditCoursesModules"></asp:Label></span>
                                                                                </li>

                                                                                <li class="page-item">
                                                                                    <asp:LinkButton ID="btnNextEditModulesCourses" CssClass="page-link" CausesValidation="false" OnClick="btnNextEditModulesCourses_OnClick" runat="server">
                                                                        <i class="fa fa-angle-right"></i>
                                                                        <span class="sr-only">Next</span>
                                                                                    </asp:LinkButton>
                                                                                </li>
                                                                            </ul>
                                                                        </div>
                                                                        <!-- Fim da Paginação dos Módulos -->
                                                                        <div class="row px-4" style="padding: 10px;">
                                                                            <small class="text-uppercase font-weight-bolder">Módulos seleccionados:</small>
                                                                            <asp:Label runat="server" ID="lblOrderOfModulesEditSelected"> </asp:Label>
                                                                        </div>
                                                                        <div class="row justify-content-center">
                                                                            <asp:Button runat="server" CssClass="btn btn-primary text-center col-md-6" Text="Editar Curso" ID="btnEditCourse" ValidationGroup="EditForm" CausesValidation="true" AutoPostBack="true" OnClick="btnEditCourse_OnClick" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="container row justify-content-center">
                                                                        <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; width: 70%; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                                        <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" OnTick="timerMessageEdit_OnTick" Enabled="false"></asp:Timer>
                                                                    </div>

                                                                </div>



                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n12">
                                                                <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnEditCourse" />
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
                        <div id="insertCoursesDiv" runat="server" visible="false">
                            <asp:UpdatePanel runat="server" ID="updatePanelInsertCourses">
                                <ContentTemplate>
                                    <div class="page-header min-vh-30">
                                        <div class="container">
                                            <div class="row ">
                                                <div class="col-xl-8 col-lg-8 col-md-6 col-sm-6">
                                                    <div class="card card-plain">
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
                                                                    <label>Duração do Curso</label>
                                                                    <div class="mb-2">
                                                                        <asp:Label runat="server" ID="lblDuracaoCurso" CssClass="form-control"></asp:Label>
                                                                    </div>
                                                                    <label>Duração do Estágio</label>
                                                                    <div class="mb-2">
                                                                        <asp:TextBox runat="server" ID="tbDuracaoEstagio" TextMode="Number" ValidationGroup="insertForm" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row px-2" style="padding: 10px;">
                                                                    <!-- Inserção de Dados de Curso - Seleção dos Módulos -->
                                                                    <small class="text-uppercase font-weight-bold">Módulos do Curso:</small>
                                                                    <p>Seleccione os módulos pertencentes a este curso</p>
                                                                    <div class="input-group mb-4">
                                                                        <asp:LinkButton runat="server" ID="lbtnSearch" OnClick="lbtnSearch_OnClick" class="input-group-text text-body" AutoPostBack="True"><i class="fas fa-search" aria-hidden="true" ></i></asp:LinkButton>
                                                                        <asp:TextBox runat="server" AutoPostBack="False" ID="tbSearchModules" CssClass="form-control" placeholder="Type here..."></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <asp:Repeater ID="rptInsertCourses" runat="server" OnItemDataBound="rptInsertCourses_OnItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <div class="table-responsive">
                                                                            <table class="table align-items-center justify-content-center mb-0">
                                                                                <thead>
                                                                                    <colgroup>
                                                                                        <col style="width: 35%;" />
                                                                                        <col style="width: 10%;" />
                                                                                        <col style="width: 25%;" />
                                                                                    </colgroup>
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
                                                                            <td style="width: auto; white-space: normal; padding: 2px;">
                                                                                <div class="d-flex px-2">
                                                                                    <div>
                                                                                        <img src="<%# Eval("SVG") %>" class="avatar avatar-sm rounded-circle me-3">
                                                                                    </div>
                                                                                    <div class="my-auto">
                                                                                        <h6 class="mb-0 text-sm"><%# Eval("Nome") %></h6>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: auto; white-space: normal; padding: 2px;">
                                                                                <p class="text-sm font-weight-bold mb-0"><%# Eval("UFCD") %></p>
                                                                            </td>
                                                                            <td id="ForMyCkb" class="text-xs font-weight-bold" style="width: auto; white-space: normal; padding: 2px;">
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
                                                        <div class="col-12">
                                                            <ul class="pagination pagination-info justify-content-center" style="padding: 15px;">
                                                                <li class="page-item">
                                                                    <asp:LinkButton ID="btnPreviousInsertCoursesModules" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousInsertCoursesModules_Click" runat="server">
                                                                <i class="fa fa-angle-left"></i>
                                                                <span class="sr-only">Previous</span>
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li class="page-item active">
                                                                    <span class="page-link">
                                                                        <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberInsertCourses"></asp:Label></span>
                                                                </li>
                                                                <li class="page-item">
                                                                    <asp:LinkButton ID="btnNextInsertCoursesModules" CssClass="page-link" CausesValidation="false" OnClick="btnNextInsertCoursesModules_Click" runat="server">
                                                                <i class="fa fa-angle-right"></i>
                                                                <span class="sr-only">Next</span>
                                                                    </asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <!-- Fim da Paginação dos Módulos -->

                                                        <div class="row px-4" style="padding: 10px;">
                                                            <small class="text-uppercase font-weight-bold">Ordem dos Módulos:</small>
                                                            <asp:Label runat="server" ID="lblOrderOfModulesInsertedSelected">  </asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row flex-row">
                                                        <div class="row justify-content-center">
                                                            <asp:Button ID="btnInsertCourse" ValidationGroup="insertForm" runat="server" Text="Inserir" CausesValidation="true" OnClick="btnInsertCourse_Click" AutoPostBack="true" class="btn bg-gradient-info text-center col-md-6" />
                                                        </div>
                                                    </div>
                                                    <div class="row px-4" style="padding: 10px;">
                                                        <asp:Label runat="server" ID="lblMessageInsert" Style="display: flex; align-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                        <asp:Timer ID="timerMessageInsert" runat="server" Interval="3000" OnTick="timerMessageInsert_OnTick" Enabled="false"></asp:Timer>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
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
                                    <asp:AsyncPostBackTrigger ControlID="btnInsertCourse" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousInsertCoursesModules" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextInsertCoursesModules" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsertCourseMain" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
