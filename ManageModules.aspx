<%@ Page Title="Módulos" Language="C#" MasterPageFile="~/CinelMP.Master" EnableViewState="true" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageModules.aspx.cs" Inherits="FinalProject.ManageModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 15px">
                    <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Inserir Novo Módulo" ID="btnInsertModuleMain" OnClick="btnInsertModuleMain_OnClick" />
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
                                    <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                        <span>Designação:</span>
                                        <div class="input-group mb-4">
                                            <asp:LinkButton runat="server" ID="lbtSearchFilters" class="input-group-text text-body" CausesValidation="False" AutoPostBack="True" OnClick="btnApplyFilters_OnClick">
                                        <i class="fas fa-search" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                            <asp:TextBox runat="server" ID="tbSearchFilters" CssClass="form-control" placeholder="Escreve aqui..." onkeypress="handleKeyPress(event)" CausesValidation="False" AutoPostBack="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-xl-2 col-sm-6 mb-xl-0 mb-4">
                                        <span>Nr.º de Horas:</span>
                                        <div class="dropdown">
                                            <asp:DropDownList ID="ddlNrHoras" runat="server" class="btn bg-gradient-secundary dropdown-toggle">
                                                <asp:ListItem Value="25">25 horas</asp:ListItem>
                                                <asp:ListItem Value="50">50 horas</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xl-3 col-lg-4 col-md-4 col-sm-4 mb-xl-0 mb-0">
                                        <span>Ordenação:</span>
                                        <div class="input-group mb-0 text-end">
                                            <asp:DropDownList runat="server" ID="ddlOrderFilters" class="btn bg-gradient-secundary dropdown-toggle">
                                                <asp:ListItem Value="ASC">A-Z</asp:ListItem>
                                                <asp:ListItem Value="DESC">Z-A</asp:ListItem>
                                                <asp:ListItem Value="codUFCD">UFCD</asp:ListItem>
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
                                <asp:AsyncPostBackTrigger ControlID="lbtSearchFilters" />
                                <asp:AsyncPostBackTrigger ControlID="tbSearchFilters" />
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
                                <div id="listModulesDiv" runat="server">
                                    <asp:UpdatePanel ID="updatePanelListModules" runat="server">
                                        <ContentTemplate>
                                            <div class="container row justify-content-center">
                                                <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" OnTick="timerMessageEdit_OnTick" Enabled="False"></asp:Timer>
                                            </div>
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
                                                                    <col style="width: 10%;" />
                                                                    <col style="width: 38%;" />
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
                                                        <td style="width: auto; white-space: normal; padding: 2px;">
                                                            <asp:HiddenField runat="server" ID="hdnModuleID" Value='<%# Eval("CodModulo") %>' />
                                                            <div class="d-flex px-1">    
                                                                <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("SVG") %>' OnClick="imgUpload_Click" />
                                                                <asp:Panel ID="pnlFileUpload" runat="server" Visible="false">
                                                                    <asp:FileUpload ID="fuUpload" runat="server" />
                                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpdateImage_Click" />
                                                                </asp:Panel>
                                                                <p class="text-sm">
                                                                    <asp:TextBox ID="tbNome" CssClass="form-control" runat="server" Text='<%# Bind("Nome") %>' Visible="false"></asp:TextBox>
                                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                                </p>
                                                            </div>
                                                            </div>
                                                        </td>
                                                        <td style="white-space: normal;">
                                                            <p class="text-sm">
                                                                <asp:TextBox ID="tbUFCD" CssClass="form-control" runat="server" Text='<%# Bind("UFCD") %>' Visible="false"></asp:TextBox>
                                                                <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("UFCD") %>' Visible="true"></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td>
                                                            <p class="text-sm">
                                                                <asp:DropDownList ID="ddlDuracaoEdit" runat="server" CssClass="form-control" Visible="false">
                                                                    <asp:ListItem Value="25">25 horas</asp:ListItem>
                                                                    <asp:ListItem Value="50">50 Horas</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblDuracao" class="text-sm" runat="server" Text='<%# Eval("Duracao") %>' Style="width: 100%;" Visible="true"></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td style="width: auto; white-space: normal; padding: 2px;">
                                                            <p class="text-xs" style="width: 100%; text-align: left;">
                                                                <script src="assets/js/ckeditor/ckeditor.js"></script>

                                                                <asp:TextBox ID="tbDescricao" title="Objetivos da UFCD" CssClass="form-control" Rows="2" runat="server" Text='<%# Bind("Descricao") %>' TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                                <asp:Literal ID="ltDescricao" runat="server" Text='<%# Eval("Descricao") %>' Visible="true"></asp:Literal>

                                                                <script type="text/javascript">
                                                                    CKEDITOR.replace('<%=tbDescricao.ClientID%>',
                                                                        {
                                                                            customConfig: 'custom/editor_config.js'
                                                                        });
                                                                </script>
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
                                            <!--Paginação List Modules -->
                                            <div class="col-12">
                                                <ul class="pagination justify-content-center" style="padding: 2px;">
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnPreviousModule" CssClass="page-link" CausesValidation="false" OnClick="btnPreviousModule_Click" runat="server">
                                                        <i class="fa fa-angle-left"></i>
                                                        <span class="sr-only">Previous</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li class="page-item active">
                                                        <span class="page-link">
                                                            <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberModules"></asp:Label></span>
                                                    </li>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="btnNextModule" CssClass="page-link" CausesValidation="false" OnClick="btnNextModule_Click" runat="server">
                                                        <i class="fa fa-angle-right"></i>
                                                        <span class="sr-only">Next</span>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>

                                            <!-- AsyncPostBackTrigger For Lbtn Gerados no C# -->
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="timerMessageEdit" />
                                            <asp:AsyncPostBackTrigger ControlID="btnPreviousModule" />
                                            <asp:AsyncPostBackTrigger ControlID="btnNextModule" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="insertModulesDiv" runat="server" visible="False">
                                    <asp:UpdatePanel runat="server" ID="updatePanelInsertModules" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="page-header min-vh-50">
                                                <div class="container-fluid">
                                                    <div class="row ">
                                                        <div class="col-xl-7 col-lg-7 col-md-6 flex-column">
                                                            <div class="card card-plain" style="max-width: 100%;">
                                                                <div class="card-header pb-0 text-left bg-transparent">
                                                                    <h5 class="font-weight-bolder text-info text-gradient">Inserção de novo módulo:</h5>
                                                                </div>
                                                                <div class="card-body">
                                                                    <div role="form">
                                                                        <label>Nome do Módulo</label>
                                                                        <asp:RequiredFieldValidator ID="rfvModuleName" ValidationGroup="InsertForm" ErrorMessage="Nome do módulo obrigatório" Text="*" runat="server" ControlToValidate="tbModuleName" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                        <div class="mb-3">
                                                                            <asp:TextBox ID="tbModuleName" CssClass="form-control" ValidationGroup="InsertForm" placeholder="Nome do Módulo" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <label>Duração</label>
                                                                        <div class="mb-3">
                                                                            <asp:DropDownList ID="ddlDuracao" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="25">25 horas</asp:ListItem>
                                                                                <asp:ListItem Value="50">50 horas</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <label>Código da UFCD</label>
                                                                        <asp:RequiredFieldValidator ID="rvfUFCD" ValidationGroup="InsertForm" runat="server" ErrorMessage="Código da UFCD obrigatório" Text="*" ControlToValidate="tbUFCD" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                        <div class="mb-3">
                                                                            <asp:TextBox ID="tbUFCD" ValidationGroup="InsertForm" CssClass="form-control" placeholder="Código da UFCD" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <label>Descrição</label>
                                                                        <div class="mb-3">
                                                                            <asp:TextBox ID="tbDescricao" CssClass="form-control" placeholder="Descrição" TextMode="MultiLine" runat="server" Rows="3"></asp:TextBox>
                                                                        </div>
                                                                        <label>Créditos</label>
                                                                        <asp:RequiredFieldValidator ValidationGroup="InsertForm" ID="rvfCredits" runat="server" ErrorMessage="Quantidade de créditos obrigatória" Text="*" ControlToValidate="tbCredits" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                        <div class="mb-3">
                                                                            <asp:TextBox ID="tbCredits" ValidationGroup="InsertForm" CssClass="form-control" placeholder="Créditos" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <label>SVG da UFCD</label>
                                                                        <div class="mb-3">
                                                                            <asp:FileUpload ID="fuSvgUFCDInsert" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="text-center">
                                                                            <asp:Button ID="btnInsertModule" runat="server" Text="Inserir" ValidationGroup="InsertForm" CausesValidation="True" OnClick="btnInsertModule_Click" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                                                        </div>
                                                                        <div>
                                                                            <br />
                                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="list-group" ValidationGroup="InsertForm" DisplayMode="List" />

                                                                            <asp:Label runat="server" ID="lblMessageInsert" Style="display: flex; align-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                                                            <asp:Timer ID="timerMessageInsert" runat="server" Interval="3000" OnTick="timerMessageInsert_OnTick" Enabled="False"></asp:Timer>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="oblique position-absolute top-0 h-100 d-md-block me-n10" style="max-width: 100%;">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnInsertModuleMain" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
