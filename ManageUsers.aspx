<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="FinalProject.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 15px">
            <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Utilizador" ID="btnInsertUser" OnClientClick="showInsert(); return false;" />
                <asp:Button runat="server" CssClass="btn btn-primary hidden" Text="Voltar" ID="btnBack" OnClientClick="showInsert(); return false;" />
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
                                    <asp:Button runat="server" ID="btnApplyFilters" CausesValidation="False" CssClass="btn btn-outline-primary mb-0" Text="Aplicar" AutoPostBack="True" />
                                    <span>&nbsp; &nbsp;</span>
                                    <asp:Button runat="server" ID="btnClearFilters" CausesValidation="False" CssClass="btn btn-outline-primary mb-0" Text="Limpar" />
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
                        <div id="listUsersDiv">
                            <asp:UpdatePanel ID="updatePanelListUsers" runat="server">
                                <ContentTemplate>
                                    <div class="card-header pb-0">
                                        <h6>Utilizadores</h6>
                                    </div>
                                    <asp:Repeater ID="rptUsers" runat="server" OnItemDataBound="rptUsers_OnItemDataBound">
                                        <HeaderTemplate>
                                            <div class="card-body px-0 pt-0 pb-2">
                                                <div class="table-responsive p-0">
                                                    <table class="table align-items-center mb-0" style="width: 100%;">
                                                        <colgroup>
                                                            <col style="width: 5%;" />
                                                            <col style="width: 25%;" />
                                                            <col style="width: 25%;" />
                                                            <col style="width: 5%;" />
                                                            <col style="width: 5%;" />
                                                            <col style="width: 5%;" />
                                                            <col style="width: 5%;" />
                                                        </colgroup>
                                                        <thead>
                                                            <tr>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Utilizador</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">E-mail</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Ativo</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Formando</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Formador</th>
                                                                <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Funcionário</th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <p class="text-sm align-center">
                                                        <asp:Label runat="server" ID="lblCodUtilizador" Text='<%# Eval("CodUser") %>' Visible="True" />
                                                    </p>
                                                </td>
                                                <td>
                                                    <div class="d-flex px-1">
                                                        <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />

                                                        <p class="text-sm">
                                                            <asp:TextBox ID="tbUsername" CssClass="form-control" runat="server" Text='<%# Bind("Username") %>' Visible="false"></asp:TextBox>
                                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' Visible="true"></asp:Label>
                                                        </p>
                                                    </div>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:TextBox ID="tbEmail" CssClass="form-control" runat="server" Text='<%# Bind("Email") %>' Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' Visible="true"></asp:Label>
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:CheckBox ID="ckbAtivo" runat="server"></asp:CheckBox>
                                                    </p>
                                                </td>
                                                <td id="cellDescricao">
                                                    <p class="text-xs">
                                                        <asp:CheckBox ID="ckbFormando" runat="server"></asp:CheckBox>
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:CheckBox ID="ckbFormador" runat="server"></asp:CheckBox>
                                                    </p>
                                                </td>
                                                <td>
                                                    <p class="text-sm">
                                                        <asp:CheckBox ID="ckbFuncionario" runat="server"></asp:CheckBox>
                                                    </p>
                                                </td>
                                                <td class="align-middle font-weight-bold text-center">
                                                    <p>
                                                        <asp:LinkButton runat="server" ID="lbtEditUser" CausesValidation="false" CommandName="Edit" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Edit" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtCancelUser" CausesValidation="false" CommandName="Cancel" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Cancel" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                    </p>
                                                </td>
                                                <td class="align-middle text-center">
                                                    <p>
                                                        <asp:LinkButton runat="server" ID="lbtDeleteUser" CausesValidation="false" CommandName="Delete" Visible="true" CommandArgument='<%# Container.ItemIndex %>'
                                                            Text="Delete" class="text-secondary font-weight-bold text-sm">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbtConfirmUser" CausesValidation="false" CommandName="Confirm" Visible="false" CommandArgument='<%# Container.ItemIndex %>'
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

                                    <!--Paginação -->
                                    <div class="col-12">
                                        <ul class="pagination justify-content-center" style="padding: 2px;">
                                            <li class="page-item">
                                                <asp:LinkButton ID="btnPreviousUser" CssClass="page-link" CausesValidation="false" runat="server">
                                                    <i class="fa fa-angle-left"></i>
                                                    <span class="sr-only">Previous</span>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="page-item active">
                                                <span class="page-link">
                                                    <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberUsers"></asp:Label></span>
                                            </li>
                                            <li class="page-item">
                                                <asp:LinkButton ID="btnNextUser" CssClass="page-link" CausesValidation="false" runat="server">
                                                    <i class="fa fa-angle-right"></i>
                                                    <span class="sr-only">Next</span>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnPreviousUser" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNextUser" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="insertUserDiv" class="hidden">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Perfil</label>
                                        <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" Text="*" ErrorMessage="Perfil Necessário" ControlToValidate="ddlPerfil" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:DropDownList class="dropdown-toggle btn bg-gradient-secundary" ID="ddlPerfil" runat="server" DataSourceID="SQLDSPerfil" DataTextField="perfil" DataValueField="codPerfil"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLDSPerfil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [perfil] ORDER BY [codPerfil] OFFSET 1 ROWS"></asp:SqlDataSource>
                                        </div>
                                        <label>Nome Completo</label>
                                        <asp:RequiredFieldValidator ID="rfvname" Text="*" ErrorMessage="Nome Obrigatório" runat="server" ControlToValidate="tb_name" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_name" runat="server" oninput="validateUsername(this)" class="form-control" placeholder="Nome Completo"></asp:TextBox>
                                        </div>
                                        <label>Nome de Utilizador</label>
                                        <asp:RequiredFieldValidator ID="rfvusername" Text="*" ErrorMessage="Utilizador Obrigatório" runat="server" ControlToValidate="tb_username" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_username" runat="server" class="form-control" placeholder="Utilizador"></asp:TextBox>
                                        </div>
                                        <label>E-mail</label>
                                        <asp:RequiredFieldValidator ID="rfvemail" Text="*" ErrorMessage="E-mail Obrigatório" runat="server" ControlToValidate="tb_email" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_email" runat="server" class="form-control" placeholder="E-mail"></asp:TextBox>
                                        </div>
                                        <label>Palavra-passe</label>
                                        <asp:RequiredFieldValidator ID="rfvpw" Text="*" ErrorMessage="Palavra-passe obrigatória" runat="server" ControlToValidate="tb_pw" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pw" runat="server" class="form-control" placeholder="Palavra-passe"></asp:TextBox>
                                        </div>
                                        <label>Repetição da Palavra-passe</label>
                                        <asp:RequiredFieldValidator ID="rfvpwr" runat="server" Text="*" ErrorMessage="Palavra-passe obrigatória" ControlToValidate="tb_pwr" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pwR" runat="server" class="form-control" placeholder="Repetir a Palavra-passe"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div class="mb-3">
                                            <label>Tipo de Documento de Identificação</label>
                                            <div class="mb-0">
                                                <asp:DropDownList ID="ddl_tipoDocIdent" CssClass="form-control" runat="server" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc"></asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                            </div>
                                            <br />
                                            <div class="form-group">
                                                <label>Nr.º do Documento de Identificação</label>
                                                <asp:RequiredFieldValidator ID="rfvCC" Text="*" ErrorMessage="Nr.º do Documento de Identificação Obrigatório" runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <div>
                                                    <asp:TextBox ID="tbCC" runat="server" CssClass="form-control" placeholder="Nr.º do Documento de Identificação"></asp:TextBox>
                                                </div>
                                                <label>Data de Validade</label>
                                                <asp:RequiredFieldValidator ID="rfvdataValidade" Text="*" ErrorMessage="Data de Validade obrigatória" runat="server" ControlToValidate="tbdataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="tbdataValidade" runat="server" CssClass="form-control datepicker" placeholder="Data de Validade" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="mb-3">
                                            <label>Prefixo</label>
                                            <div class="mb-0 text-center">
                                                <asp:DropDownList ID="ddlprefixo" CssClass="form-control" runat="server" DataSourceID="SQLDSPais" DataTextField="prefixo" DataValueField="codPais"></asp:DropDownList>
                                                <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codPais,CONCAT(nomePT, ': ' , prefixo) AS prefixo FROM [pais] order by nomePT"></asp:SqlDataSource>
                                            </div>
                                            <br />
                                            <label>Nr.º de Telemóvel</label>
                                            <asp:RequiredFieldValidator ID="rfvTelemovel" Text="*" ErrorMessage="Telemóvel Obrigatório" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbTelemovel" runat="server" class="form-control" placeholder="Phone Number"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="text-center">

                                        <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                            <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                                        </div>
                                        <asp:Button ID="btnSignup" runat="server" CssClass="btn bg-gradient-dark w-100 my-4 mb-2" Text="Sign Up" />
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
            var insertDiv = document.getElementById('insertUserDiv');
            var listDiv = document.getElementById('listUsersDiv');
            var btnInsert = document.getElementById('<%= btnInsertUser.ClientID %>');
            var btnBack = document.getElementById('<%= btnBack.ClientID %>');
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
</asp:Content>
