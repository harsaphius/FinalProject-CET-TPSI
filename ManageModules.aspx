<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageModules.aspx.cs" Inherits="FinalProject.ManageModules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="listModulesDiv" class="container-fluid pageDiv">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rpt_Modules" runat="server" OnItemDataBound="rpt_Modules_ItemDataBound" OnItemCommand="rpt_Modules_ItemCommand">
                    <HeaderTemplate>
                        <table class="table align-items-center justify-content-center mb-0">
                            <thead>
                                <tr>
                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Descrição</th>
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
                            <td class="text-xs font-weight-bold">
                                <%# Eval("Descricao") %>
                            </td>
                            <td class="align-middle text-center">
                                <asp:LinkButton runat="server" ID="lbt_edit" OnClientClick="handleEditButtonClick(); return false;" CommandName="Edit"
                                    CommandArgument='<%# Container.ItemIndex %>' Text="Edit" class="text-secondary font-weight-bold text-xs">
                                </asp:LinkButton>
                            </td>
                           
                        </tr>
                        <tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="insertModulesDiv" class="container-fluid pageDiv">
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
    <div id="editModulesDiv" class="container-fluid pageDiv">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rpt_editModules" runat="server" OnItemCommand="rpt_editModules_ItemCommand">
                    <HeaderTemplate>
                        <div class="table-responsive">
                            <table class="table align-items-center justify-content-center mb-0">
                                <thead>
                                    <tr>
                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Descrição</th>
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
                            <td class="text-xs font-weight-bold">
                                <%# Eval("Descricao") %>
                            </td>
                            <%--  <td class="align-middle text-center">
                                <asp:LinkButton runat="server" ID="lbtn_edit" OnClick="lbtn_edit_Click" Text="Edit" class="text-secondary font-weight-bold text-xs">
                                </asp:LinkButton>
                            </td>--%>
                            <%--     <td class="align-middle">
                                <button class="btn btn-link text-secondary mb-0">
                                    <i class="fa fa-ellipsis-v text-xs"></i>
                                </button>
                            </td>--%>
                        </tr>
                        <tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                            </table>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function handleEditButtonClick() {
            // Perform client-side actions such as showing/hiding divs
            document.getElementById("editModulesDiv").classList.add = "hidden";
            document.getElementById("listModulesDiv").classList.remove = "hidden";
        }
    </script>
</asp:Content>
