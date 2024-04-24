<%@ Page Title="Formandos" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="FinalProject.ManageStudents" %>

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
                        <asp:Button runat="server" CssClass="btn btn-primary" Visible="true" Text="Inserir Novo Formando" ID="btnInsertStudentMain" OnClick="btnInsertStudentMain_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" Visible="true" Text="Inserir da Lista de Utilizadores" ID="btnInsertStudentFromList" OnClick="btnInsertStudentFromList_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" Visible="false" Text="Voltar" ID="btnBack" OnClick="btnBack_OnClick" />
                    </div>
                    <div class="col-md-6 col-sm-6 text-end" style="padding-right: 35px; font-family: 'Sans Serif Collection'">
                        <asp:LinkButton ID="filtermenu" Visible="True" OnClick="filtermenu_OnClick" runat="server">
                            <i class="fas fa-filter text-primary text-lg" title="Filter" aria-hidden="true">Filtros</i>
                        </asp:LinkButton>
                    </div>
                    <div id="filters" runat="server" class="col-md-12 col-md-6" visible="False" style="padding-left: 30px;">
                        <asp:UpdatePanel ID="updatePanelFilters" runat="server">
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
                <div class="container row justify-content-center">
                    <asp:Label runat="server" ID="lblMessage" Style="display: flex; justify-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                    <asp:Timer ID="timerMessage" runat="server" Interval="3000" OnTick="timerMessage_OnTick" Enabled="False"></asp:Timer>
                </div>
                <div class="container-fluid py-4">
                    <div class="row">
                        <div class="col-12">
                            <div class="card mb-4">
                                <div id="listStudentsDiv" runat="server">
                                    <asp:UpdatePanel ID="updatePanelListStudent" runat="server">
                                        <ContentTemplate>
                                            <div class="card-header pb-0">
                                                <h6>Formandos</h6>
                                            </div>
                                            <asp:Repeater ID="rptListStudents" runat="server" OnItemCommand="rptListStudents_OnItemCommand">
                                                <HeaderTemplate>
                                                    <div class="card-body px-0 pt-0 pb-2">
                                                        <div class="table-responsive p-0">
                                                            <table class="table align-items-center mb-0">
                                                                <thead>
                                                                    <colgroup>
                                                                        <col style="width: 5%;" />
                                                                        <col style="width: 25%;" />
                                                                        <col style="width: 25%;" />
                                                                        <col style="width: 5%;" />
                                                                        <col style="width: 5%;" />
                                                                    </colgroup>
                                                                    <tr>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Nr.º de Formando</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Cursos</th>
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
                                                                <asp:Label runat="server" Text='<%# Eval("CodFormando") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnStudentID" runat="server" Value='<%# Eval("CodFormando") %>' />
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
                                                        <td class="align-middle font-weight-bold text-center">
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
                                                        <asp:LinkButton ID="btnPreviousListStudents" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousListStudents_Click" runat="server">
                                                    <i class="fa fa-angle-left"></i>
                                                    <span class="sr-only">Previous</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li class="page-item active">
                                                        <span class="page-link">
                                                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberListStudents"></asp:Label></span>
                                                    </li>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnNextListStudents" CssClass="page-link" CausesValidation="false" OnClick="btnNextListStudents_Click" runat="server">
                                                    <i class="fa fa-angle-right"></i>
                                                    <span class="sr-only">Next</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="listUsersDiv" visible="false" runat="server">
                                    <div class="card mb-4">
                                        <asp:UpdatePanel ID="updatePanelUsersForStudents" runat="server">
                                            <ContentTemplate>
                                                <div class="card-header pb-0 p-3">
                                                    <h6 class="mb-1">Utilizadores disponíveis</h6>
                                                    <p class="text-sm">Escolher os utilizadores para registar</p>
                                                </div>
                                                <div class="card-body">
                                                    <div class="card mb-4">
                                                        <!-- Listagem de Utilizador Disponíveis -->
                                                        <asp:Repeater ID="rptUserForStudents" runat="server" OnItemCommand="rptUserForStudents_OnItemCommand">
                                                            <HeaderTemplate>
                                                                <div class="card-body px-0 pt-0 pb-2">
                                                                    <div class="table-responsive p-0">
                                                                        <table class="table align-items-center mb-0">
                                                                            <thead>
                                                                                <colgroup>
                                                                                    <col style="width: 5%;" />
                                                                                    <col style="width: 20%;" />
                                                                                    <col style="width: 5%;" />
                                                                                    <col style="width: 5%;" />
                                                                                    <col style="width: 5%;" />
                                                                                </colgroup>
                                                                                <tr>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Utilizador n.º </th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Registar como Formando</th>
                                                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Cursos</th>
                                                                                    <th></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <p class="mb-0 text-sm text-center">
                                                                            <asp:Label runat="server" Text='<%# Eval("CodUser") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Eval("CodUser") %>' />

                                                                        </p>
                                                                    </td>
                                                                    <td>
                                                                        <div class="d-flex px-2">
                                                                            <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />
                                                                            <div class="my-auto">
                                                                                <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                                <p class="mb-0 text-sm">
                                                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="align-middle font-weight-bold text-center">
                                                                        <asp:LinkButton runat="server" ID="lbtSelectCoursesForUser" CausesValidation="false" CommandName="EditCoursesUsers" Visible="true" CommandArgument='<%# Eval("CodUser") %>'
                                                                            Text="Escolher Cursos" class="text-secondary font-weight-bold text-xs">
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
                                                                    <asp:LinkButton ID="btnPreviousUsersForStudents" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousUsersForStudents_OnClick" runat="server">
                                                                                <i class="fa fa-angle-left"></i>
                                                                                <span class="sr-only">Previous</span>
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li class="page-item active">
                                                                    <span class="page-link">
                                                                        <asp:Label runat="server" CssClass="text-white" ID="lblPageNumbersUsersForStudents"></asp:Label></span>
                                                                </li>
                                                                <li class="page-item">
                                                                    <asp:LinkButton ID="btnNextsUsersForStudents" CssClass="page-link" CausesValidation="false" OnClick="btnNextsUsersForStudents_OnClick" runat="server">
                                                                                <i class="fa fa-angle-right"></i>
                                                                                <span class="sr-only">Next</span>
                                                                    </asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div id="insertStudentsDiv" visible="false" runat="server">
                                    <asp:UpdatePanel ID="updatePanelInsertStudent" runat="server">
                                        <ContentTemplate>
                                            <div class="page-header min-vh-50">
                                                <div class="container-fluid">
                                                    <div class="row ">
                                                        <div class="col-md-12">
                                                            <!-- Registration Completion -->
                                                            <div id="registration" runat="server">
                                                                <!-- Begin Registration Page 1-->
                                                                <div id="registerCompletionpage1" runat="server">
                                                                    <div class="card">
                                                                        <div class="card-header pb-0 p-3">
                                                                            <h6 class="mb-1">Registo de novo formando</h6>
                                                                            <p class="text-sm">Preencha todos os campos da tabela abaixo</p>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <asp:UpdatePanel ID="updatePanelInsertStudentPage1" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblNome" runat="server" AssociatedControlID="tbNome">Nome</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ValidationGroup="Page1" ID="rfvNome" Text="*" ErrorMessage="Nome Completo Obrigatório" runat="server" ControlToValidate="tbNome" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                                                        <asp:TextBox ID="tbNome" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="Nome Completo"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-2">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo">Sexo</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlSexo" runat="server" ValidationGroup="Page1" CssClass="form-control">
                                                                                                            <asp:ListItem Value="0">Feminino</asp:ListItem>
                                                                                                            <asp:ListItem Value="1">Masculino</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="tbDataNascimento">Data de Nascimento</asp:Label>
                                                                                                        <div class="input-group">
                                                                                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                                                                            <asp:TextBox ID="tbDataNascimento" ValidationGroup="Page1" runat="server" placeholder="Pick a date" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lbltipoDocumento" runat="server" AssociatedControlID="ddlDocumentoIdent">Documento de Identificação</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ID="rfvDocumentIdent" Text="*" ErrorMessage="Documento de Identificação Obrigatório" ValidationGroup="Page1" runat="server" ControlToValidate="ddlDocumentoIdent" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                                                        <asp:DropDownList ID="ddlDocumentoIdent" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCC" runat="server" AssociatedControlID="tbCC">Nr.º do Documento</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ID="rfvCC" Text="*" ValidationGroup="Page1" ErrorMessage="Nrº do Documento Obrigatório" runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                                                        <asp:TextBox ID="tbCC" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="e.g., 123456789 Z Z1Z"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblDataValidade" runat="server" AssociatedControlID="tbDataValidade">Data de Validade</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ID="rfvDataValidade" Text="*" ValidationGroup="Page1" ErrorMessage="Data de Validade Obrigatória" runat="server" ControlToValidate="tbDataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                                                        <div class="input-group">
                                                                                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                                                                            <asp:TextBox ID="tbDataValidade" ValidationGroup="Page1" placeholder="Pick a date" runat="server" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-7">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="tbNrSegSocial">Número de Segurança Social</asp:Label>
                                                                                                        <asp:TextBox ID="tbNrSegSocial" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="12345678910"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-5">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblNIF" runat="server" AssociatedControlID="tbNIF">NIF</asp:Label>
                                                                                                        <asp:TextBox ID="tbNIF" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="123456789"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblMorada" runat="server" AssociatedControlID="tbMorada">Morada</asp:Label>
                                                                                                        <asp:TextBox ID="tbMorada" ValidationGroup="Page1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Introduza a sua morada completa"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodPostal" runat="server" AssociatedControlID="tbCodPostal">Código-Postal</asp:Label>
                                                                                                        <asp:TextBox runat="server" ID="tbCodPostal" CssClass="form-control" placeholder="0000-000"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblfreguesia" runat="server" AssociatedControlID="tbLocalidade">Localidade</asp:Label>
                                                                                                        <asp:TextBox runat="server" ID="tbLocalidade" CssClass="form-control" ValidationGroup="Page1"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">País</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlCodPais" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nomePT" DataValueField="codPais">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [pais] ORDER BY nomePT"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="card card-footer">
                                                                                                <div class="col-md-12 align-items-start">
                                                                                                    <asp:Button runat="server" ID="btnNextPage" ValidationGroup="Page1" CausesValidation="True" OnClick="btnNextPage_OnClick" CssClass="btn btn-outline-primary btn-sm mb-0" Text="Seguinte" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="btnNextPage" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="font-weight-bold list-group" role="alert">
                                                                                        <asp:ValidationSummary CssClass="list-group" ID="ValidationSummary1" runat="server" ValidationGroup="Page1" ForeColor="#cc3a60" DisplayMode="List" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!-- End Registration Page 1-->
                                                                <!-- Begin Registration Page 2-->
                                                                <div id="registerCompletionpage2" runat="server" visible="false">
                                                                    <div class="card">
                                                                        <div class="card-header pb-0 p-3">
                                                                            <h6 class="mb-1">Completa o teu registo</h6>
                                                                            <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <asp:UpdatePanel ID="updatePanelInsertStudentPage2" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-3">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Estado Civil</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlCodEstadoCivil" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSEstadoCivil" DataTextField="estadoCivil" DataValueField="codEstadoCivil">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSEstadoCivil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [estadoCivil]"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-9">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="tbIBAN">IBAN</asp:Label>
                                                                                                        <asp:TextBox ID="tbIBAN" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-3">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="tbNaturalidade">Naturalidade</asp:Label>
                                                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="tbNaturalidade"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Nacionalidade</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlCodNacionalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nacionalidade" DataValueField="codPais">
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-5">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Situação Profissional</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlCodSituacaoProfissional" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSsituacaoProfissional" DataTextField="situacaoProfissional" DataValueField="codSituacaoProfissional">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSsituacaoProfissional" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [situacaoProfissional]"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodPrefixo" runat="server" AssociatedControlID="ddlPrefixo">Prefixo</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlPrefixo" CssClass="form-control" runat="server" DataSourceID="SQLDSPrefixo" DataTextField="prefixo" DataValueField="codPais"></asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSPrefixo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codPais,CONCAT(nomePT, ': ' , prefixo) AS prefixo FROM [pais] order by nomePT"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblTelemovel" runat="server" AssociatedControlID="tbTelemovel">Telemóvel</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ID="rfvTelemovel" ValidationGroup="Page2" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60" Text="*" ErrorMessage="Telemóvel Obrigatório"></asp:RequiredFieldValidator>
                                                                                                        <asp:TextBox ID="tbTelemovel" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-5">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="tbEmail">Email</asp:Label>
                                                                                                        <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="E-mail Obrigatório" ValidationGroup="Page2" runat="server" ControlToValidate="tbEmail" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                                                        <asp:TextBox ID="tbEmail" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblCodGrauAcademico" runat="server" AssociatedControlID="ddlCodGrauAcademico">Grau Académico</asp:Label>
                                                                                                        <asp:DropDownList ID="ddlCodGrauAcademico" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSGrauAcademico" DataTextField="grauAcademico" DataValueField="codGrauAcademico">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:SqlDataSource ID="SQLDSGrauAcademico" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [grauAcademico]"></asp:SqlDataSource>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblFoto" runat="server" AssociatedControlID="fuFoto">Foto</asp:Label>
                                                                                                        <asp:FileUpload ID="fuFoto" ValidationGroup="Page2" runat="server" CssClass="form-control" AutoPostBack="false" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-4">
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="lblAnexo" runat="server" AssociatedControlID="fuAnexo">Anexo</asp:Label>
                                                                                                        <asp:FileUpload ID="fuAnexo" ValidationGroup="Page2" runat="server" CssClass="form-control" AutoPostBack="false" AllowMultiple="True" />
                                                                                                        <small>CV, Documento de Identificação</small>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="card card-footer">
                                                                                                <div class="col-md-12 align-items-center">
                                                                                                    <asp:Button runat="server" ID="btnBackPageOne" ValidationGroup="Page2" OnClick="btnBackPageOne_OnClick" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" />
                                                                                                    &nbsp;
                                                                                                    <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" ValidationGroup="Page2" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Submeter"></asp:Button>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="font-weight-bold list-group" role="alert">
                                                                                        <asp:ValidationSummary CssClass="list-group" ID="ValidationSummary2" runat="server" ValidationGroup="Page2" ForeColor="#cc3a60" DisplayMode="List" />
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!-- End Registration Page 2-->
                                                                <!-- Begin Registration Page 3 -->

                                                                <!-- End Registration Page 3 -->
                                                                <div class="row px-4" style="padding: 10px;">
                                                                    <asp:Label runat="server" ID="lblMessageInsert" Style="display: flex; align-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                                    <asp:Timer ID="timerMessageInsert" runat="server" Interval="3000" OnTick="timerMessageInsert_OnTick"></asp:Timer>
                                                                </div>
                                                            </div>
                                                            <!-- End of Registration Completion -->
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="CourseRegisterForStudent" visible="false" runat="server">
                                    <div class="card mb-4">
                                        <asp:UpdatePanel ID="updatePanelCoursesForStudent" runat="server">
                                            <ContentTemplate>
                                                <div class="page-header min-vh-50">
                                                    <div class="container-fluid">
                                                        <div class="row ">
                                                            <div class="col-md-12">
                                                                <div class="card">
                                                                    <div class="card-header pb-0 p-3">
                                                                        <h6 class="mb-1">Cursos disponíveis</h6>
                                                                        <p class="text-sm">Escolher os cursos para registar</p>
                                                                    </div>
                                                                    <div class="card-body">
                                                                        <div class="card mb-4">
                                                                            <!-- Listagem de Cursos -->
                                                                            <asp:Repeater ID="rptListCoursesForStudents" runat="server" OnItemCommand="rptListCoursesForStudents_OnItemCommand">
                                                                                <HeaderTemplate>
                                                                                    <div class="card-body px-0 pt-0 pb-2">
                                                                                        <div class="table-responsive p-0">
                                                                                            <table class="table align-items-center mb-0">
                                                                                                <thead>
                                                                                                    <colgroup>
                                                                                                        <col style="width: 20%;" />
                                                                                                        <col style="width: 25%;" />
                                                                                                        <col style="width: 25%;" />
                                                                                                        <col style="width: 20%;" />
                                                                                                        <col style="width: 5%;" />
                                                                                                    </colgroup>
                                                                                                    <tr>
                                                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Curso</th>
                                                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Referencial</th>
                                                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código QNQ</th>
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
                                                                                                        <asp:Label runat="server" ID="lblCod"><%# Eval("CodCurso") %></asp:Label></h6>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false" Style="width: 100%;"></asp:LinkButton>
                                                                                            <p class="text-sm font-weight-bold mb-0">
                                                                                                <asp:Label runat="server" ID="lblNome"><%# Eval("Nome") %></asp:Label>
                                                                                            </p>
                                                                                        </td>
                                                                                        <td class="text-xs font-weight-bold">
                                                                                            <asp:TextBox ID="tbCodRef" CssClass="form-control" runat="server" Text='<%# Bind("CodRef") %>' Visible="false" Style="width: 100%;"></asp:TextBox>
                                                                                            <asp:Label ID="lblCodRef" runat="server"> <%# Eval("CodRef") %></asp:Label>
                                                                                        </td>
                                                                                        <td class="text-xs font-weight-bold">
                                                                                            <asp:HiddenField ID="hdnCourseID" runat="server" Value='<%# Eval("CodCurso") %>' />
                                                                                            <asp:HiddenField ID="hdnCourseName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                                            <div class="form-check">
                                                                                                <asp:CheckBox runat="server"  Checked='<%# Convert.ToBoolean(Eval("IsChecked")) %>'  ID="chckBoxCourses" OnCheckedChanged="chckBoxCourses_CheckedChanged" />
                                                                                                <asp:Label runat="server" ID="lblOrder">Selecione este curso</asp:Label>
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
                                                                                        <asp:LinkButton ID="btnPreviousListCoursesForStudents" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousListCoursesForStudents_Click" runat="server">
                                                                                <i class="fa fa-angle-left"></i>
                                                                                <span class="sr-only">Previous</span>
                                                                                        </asp:LinkButton>
                                                                                    </li>
                                                                                    <li class="page-item active">
                                                                                        <span class="page-link">
                                                                                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberListCoursesForStudents"></asp:Label></span>
                                                                                    </li>
                                                                                    <li class="page-item">
                                                                                        <asp:LinkButton ID="btnNextListCoursesForStudents" CssClass="page-link" CausesValidation="false" OnClick="btnNextListCoursesForStudents_Click" runat="server">
                                                                                <i class="fa fa-angle-right"></i>
                                                                                <span class="sr-only">Next</span>
                                                                                        </asp:LinkButton>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                        <div>
                                                                            <asp:Button runat="server" ID="btnEnroll" class="btn btn-outline-primary btn-sm mb-0 text-end" CausesValidation="false" OnClick="btnEnroll_OnClick" CommandName="Edit" CommandArgument='<%# Eval("CodUtilizador") %>' Text="Inscrever" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hdnSourceDiv" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsertStudentMain" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<!-- Javascript para o Flatpicker -->
    <script>
        function initializeDatePickers() {
            flatpickr('#<%= tbDataNascimento.ClientID %>',
                {
                    dateFormat: 'd-m-Y',
                    theme: 'light',
                    maxDate: new Date(),
                    onChange: function (selectedDates, dateStr, instance) {
                        console.log("onClose chamada"); // Adicione este console.log para verificar se a função está sendo chamada
                        instance.redraw();
                    }

                });

            flatpickr('#<%= tbDataValidade.ClientID %>', {
                dateFormat: 'd-m-Y',
                theme: 'light',
                minDate: new Date(),
                onChange: function (selectedDates, dateStr, instance) {
                    // Always redraw the date picker
                    instance.redraw();
                }

            });
        }

        document.addEventListener('DOMContentLoaded', function () {
            initializeDatePickers(); // Initialize date pickers when DOM content is loaded
        });

        document.addEventListener('change', function (event) {
            var target = event.target;
            if (target.matches('#<%= tbDataNascimento.ClientID %>') || target.matches('#<%= tbDataValidade.ClientID %>')) {
                initializeDatePickers();
            }
        });

    </script>--%>

</asp:Content>
