<%@ Page Title="Turmas" EnableEventValidation="false" EnableViewState="true"  Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClasses.aspx.cs" Inherits="FinalProject.ManageClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 15px">
                    <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Nova Turma" Visible="True"  CausesValidation="False" ID="btnInsertClassMain" OnClick="btnInsertClassMain_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="False" Visible="False" Text="Voltar" ID="btnBack" OnClick="btnBack_OnClick" />
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
                                    <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                        <span>Designação:</span>
                                        <div class="input-group mb-4">
                                            <asp:LinkButton runat="server" ID="lbtnSearchFilters" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                            <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Type here..." AutoPostBack="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-sm-3 mb-xl-0 mb-4">
                                        <span>Data de Início: </span>
                                        <div class="input-group mb-4">
                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                            <asp:TextBox runat="server" ID="tbDataInicioFilters" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-xl-3 col-sm-3 mb-xl-0 mb-4">
                                        <span>Data de Fim: </span>
                                        <div class="input-group mb-4">
                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                            <asp:TextBox runat="server" ID="tbDataFimFilters" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-2 col-lg-4 col-md-4 col-sm-4 mb-xl-0 mb-0">
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
                    <div class="container-fluid py-4">
                        <div class="row">
                            <div class="col-12">
                                <div class="card mb-4">
                                    <div class="container bg-secundary py-2">
                                        <div id="listClassesDiv" runat="server">
                                            <asp:UpdatePanel ID="updatePanelListClasses" runat="server">
                                                <ContentTemplate>
                                                    <div class="py-3 align-items-center row" style="padding-left: 28px;">
                                                        <div class="col-sm-3">
                                                            <small class="text-uppercase font-weight-bold">Turmas:</small>
                                                        </div>
                                                        <asp:Repeater ID="rptClasses" runat="server" OnItemDataBound="rptClasses_OnItemDataBound" OnItemCommand="rptClasses_OnItemCommand">
                                                            <HeaderTemplate>
                                                                <div class="card-body px-0 pt-0 pb-2">
                                                                    <div class="table-responsive p-0">
                                                                        <table class="table align-items-center mb-0">
                                                                            <thead>
                                                                                <colgroup>
                                                                                    <col style="width: 10%;" />
                                                                                    <col style="width: 50%;" />
                                                                                    <col style="width: 20%;" />
                                                                                    <col style="width: 10%;" />
                                                                                    <col style="width: 10%;" />
                                                                                </colgroup>
                                                                                <tr>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º da Turma</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome do Curso</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome da Turma</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Data de Início</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Data de Fim</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"></th>
                                                                                 <%--   <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Formadores</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Formandos</th>--%>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfCodTurma" Value='<%# Eval("CodTurma") %>' />
                                                                <tr>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <p class="mb-0 text-sm text-center">
                                                                            <asp:Label runat="server" Text='<%# Eval("CodTurma") %>'></asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <p class="mb-0 text-sm text-left">
                                                                            <asp:Label runat="server" Text='<%# Eval("NomeCurso") %>'></asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <div class="d-flex px-2">
                                                                            <div class="my-auto">
                                                                                <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("NomeTurma") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                                <p class="mb-0 text-sm">
                                                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("NomeTurma") %>' Visible="true"></asp:Label>
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <p class="mb-0 text-sm text-center">
                                                                            <asp:Label runat="server" Text='<%# String.Format("{0:d}", Eval("DataInicio")) %>'></asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <p class="mb-0 text-sm text-center">
                                                                            <asp:Label runat="server" Text='<%#  String.Format("{0:d}", Eval("DataFim")) %>'></asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td> <asp:LinkButton CausesValidation="False" CommandArgument='<%# Eval("CodTurma") %>' class="btn-md btn-outline-primary text-sm"
                                                                                        data-bs-toggle="tooltip" Text="Detalhes" data-bs-placement="bottom" CommandName="Details" runat="server">
                                                                                       
                                                                                    </asp:LinkButton></td>
                                                                    <!--Nota: opção desconsiderada porque não consegui ultrapassar o limite do maxJsonLength mesmo aumentando no WebConfig -->
                                                                    <%-- <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <div class="avatar-group mt-2">
                                                                            <asp:Repeater runat="server" ID="rptStudentsClassGroup" OnItemCommand="rptStudentsClassGroup_OnItemCommand">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CausesValidation="False" CommandArgument='<%# Eval("CodTurma") %>' ID="rptStudentDetail" class="avatar avatar-xs rounded-circle"
                                                                                        data-bs-toggle="tooltip" data-bs-placement="bottom" CommandName="DetailS" title='<%# Eval("Nome") %>' runat="server">
                                                                                        <img src='<%# Eval("Foto") %>' alt='<%# Eval("Nome") %>' style="width: 25px; height: 25px;">
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;">
                                                                        <div class="avatar-group mt-2">
                                                                            <asp:Repeater runat="server" ID="rptTeachersClassGroup" OnItemCommand="rptTeachersClassGroup_OnItemCommand">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CausesValidation="False" CommandArgument='<%# Eval("CodTurma") %>' ID="rptTeacherDetail" class="avatar avatar-xs rounded-circle" data-bs-toggle="tooltip" data-bs-placement="bottom"
                                                                                        CommandName="DetailT" title='<%# Eval("Nome") %>' runat="server">
                                                                                        <img src='<%# Eval("Foto") %>' alt='<%# Eval("Nome") %>' style="width: 25px; height: 25px;">
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </td>--%>
                                                                    <td style="width: auto; white-space: normal; padding: 2px;" class="align-middle text-center">
                                                                        <asp:LinkButton runat="server" ID="lbtSchedule" CausesValidation="false" CommandName="Schedule" Visible="true" CommandArgument='<%# Eval("CodTurma") %>'
                                                                            class="text-primary font-weight-bold text-sm"> <i class="fas fa-calendar" aria-hidden="true"></i>
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
                                                        <div class="col-12">
                                                            <ul class="pagination justify-content-center" style="padding: 2px;">
                                                                <li class="page-item">
                                                                    <asp:LinkButton ID="btnPreviousClasses" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousClasses_OnClick" runat="server">
                                                                    <i class="fa fa-angle-left"></i>
                                                                    <span class="sr-only">Previous</span>
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li class="page-item active">
                                                                    <span class="page-link">
                                                                        <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberClassGroups"></asp:Label></span>
                                                                </li>
                                                                <li class="page-item">
                                                                    <asp:LinkButton ID="btnNextClasses" CssClass="page-link" OnClick="btnNextClasses_OnClick" CausesValidation="false" runat="server">
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
                                        <div id="insertClassesDiv" runat="server" visible="False">
                                            <asp:UpdatePanel ID="updatePanelInsertClassGroup" runat="server">
                                                <ContentTemplate>
                                                    <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                                        <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                                    </div>
                                                    <div class="page-header min-vh-30">
                                                        <div class="container">
                                                            <div class="row ">
                                                                <div class="col-xl-8 col-lg-8 col-md-8">
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
                                                                                    <asp:SqlDataSource ID="SQLDSCurso" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM curso WHERE isActive=1"></asp:SqlDataSource>
                                                                                </div>
                                                                                <label>Nr.º da Turma</label>
                                                                                <div class="mb-2">
                                                                                    <asp:Label ID="lblTurma" CssClass="form-control" runat="server"></asp:Label>
                                                                                </div>
                                                                                <label>Regime</label>
                                                                                <div class="mb-2">
                                                                                    <asp:DropDownList ID="ddlRegime" OnSelectedIndexChanged="ddlRegime_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server" DataSourceID="SQLDSRegime" DataTextField="nomeRegime" DataValueField="codRegime"></asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="SQLDSRegime" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM regime"></asp:SqlDataSource>
                                                                                </div>
                                                                                <label>Horário</label>
                                                                                <div class="mb-2">
                                                                                    <asp:DropDownList ID="ddlHorarioTurma" OnSelectedIndexChanged="ddlHorarioTurma_OnSelectedIndexChanged" AutoPostBack="True" CssClass="form-control" runat="server" DataSourceID="SQLDSHorarioTurma" DataTextField="horarioTurma" DataValueField="codHorarioTurma"></asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="SQLDSHorarioTurma" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM horarioTurma"></asp:SqlDataSource>
                                                                                </div>
                                                                                <label>Data Prevista de Início</label>
                                                                                <asp:RequiredFieldValidator ID="rfvDataInicio" Text="*" ErrorMessage="Data Prevista de Início Obrigatória" runat="server" ControlToValidate="tbDataInicio" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="tbDataInicio" runat="server" Format="dd-mm-yyyy" AutoPostBack="True" OnTextChanged="tbDataInicio_OnTextChanged" CssClass="form-control datepicker" TextMode="Date"></asp:TextBox>
                                                                                <label>Data Prevista de Fim</label>
                                                                                <asp:Label ID="lblDataFim" runat="server" CssClass="form-control datepicker"></asp:Label>
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
                                                                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º de Formando</th>
                                                                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                                                            <th class=""></th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td style="width: auto; white-space: normal; padding: 2px;">
                                                                                                <p class="mb-0 text-sm text-center">
                                                                                                    <asp:Label runat="server" Text='<%# Eval("CodFormando") %>'></asp:Label>
                                                                                                </p>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="d-flex px-2" style="width: auto; white-space: normal; padding: 2px;">
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
                                                                                                <asp:Label runat="server" CssClass="text-white" ID="lblPageNumbersStudents"></asp:Label></span>
                                                                                        </li>
                                                                                        <li class="page-item">
                                                                                            <asp:LinkButton ID="btnNextStudents" CssClass="page-link" OnClick="btnNextStudents_Click" CausesValidation="false" runat="server">
                                                                                            <i class="fa fa-angle-right"></i>
                                                                                            <span class="sr-only">Next</span>
                                                                                            </asp:LinkButton>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                                <br />
                                                                                <div id="listBoxStudentsForCourse" runat="server">
                                                                                    <div class="card">
                                                                                        <div class="card-body">
                                                                                            <strong class="card-title">Listagem de Formandos:</strong>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <asp:ListBox ID="listBoxStudents" class="list-group" runat="server" Height="150" Width="250"></asp:ListBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-6">

                                                                                <div class="col-sm-4 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7" style="padding-bottom: 5px; padding-top: 15px;">Módulos:</div>
                                                                                <div class="dropdown">
                                                                                    <asp:DropDownList ID="ddlModulesOfCourse" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModulesOfCourse_OnSelectedIndexChanged" AutoPostBack="True" class="form-control" runat="server" DataTextField="nomeModulos" DataValueField="codModulo"></asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="SQLDSModulesForCourse" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
                                                                                </div>
                                                                                <div class="col-sm-4 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7" style="padding-bottom: 5px; padding-top: 10px">Formadores:</div>
                                                                                <div class="dropdown">
                                                                                    <asp:DropDownList ID="ddlTeacherForModules" AutoPostBack="True" class="form-control" runat="server" DataTextField="nome" DataValueField="codFormador"></asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="SQLDSTeachersForModules" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
                                                                                </div>
                                                                                <div class="text-center">
                                                                                    <div class="col-md-8" style="padding-top: 5px;">

                                                                                        <asp:Button ID="btnAddTeacherModuleClassGroup" AutoPostBack="True" runat="server" Text="Adicionar Módulo|Formador" CausesValidation="False" OnClick="btnAddTeacherModuleClassGroup_OnClick" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                                    </div>
                                                                                </div>
                                                                                <br />
                                                                                <div id="listBoxTeachersModules" runat="server">
                                                                                    <div class="card">
                                                                                        <div class="card-body">
                                                                                            <strong class="card-title">Listagem de Formadores por Módulos:</strong>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6">
                                                                                                    <asp:ListBox ID="listBoxTeachersForModules" class="list-group" Height="150" Width="250" runat="server"></asp:ListBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-md-8" style="padding-top: 5px;">
                                                                                    <asp:Button ID="btnRemoveTeacherModuleClassGroup" AutoPostBack="True" runat="server" Text="Remover Módulo|Formador" CausesValidation="False" OnClick="btnRemoveTeacherModuleClassGroup_OnClick" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="text-center">
                                                                            <asp:Button ID="btnInsertClass" AutoPostBack="True" runat="server" Text="Inserir" OnClick="btnInsertClass_OnClick" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                        </div>
                                                                        <div class="row px-4" style="padding: 10px;">
                                                                            <asp:Label runat="server" ID="lblMessageInsert" Style="display: flex; align-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                                            <asp:Timer ID="timerMessageInsert" runat="server" Interval="3000" OnTick="timerMessageInsert_OnTick" Enabled="false"></asp:Timer>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="oblique position-absolute top-0 h-100 d-md-block me-n12">
                                                                            <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnInsertClass" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNextStudents" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousStudents" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCurso" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModulesOfCourse" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTeacherForModules" />
                                                </Triggers>
                                            </asp:UpdatePanel>
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
            <asp:AsyncPostBackTrigger ControlID="btnInsertClassMain" />
            <asp:PostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
