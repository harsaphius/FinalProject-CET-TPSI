<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="FinalProject.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top: 15px">
        <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
            <asp:Button runat="server" CssClass="btn btn-primary" Text="Voltar" ID="btn_back" OnClick="btn_back_Click" />
        </div>
    </div>

    <div class="container-fluid py-4">
        <div class="row">
            <div class="col-md-12">
                <div class="card mb-4">
                    <div class="card-header pb-0 text-primary text-center" style="margin-bottom:2px;">
                        <h6>
                            <asp:Label ID="lblNome" runat="server"></asp:Label></h6>
                    </div>
                    <div class="card-body px-0 pt-0 pb-2">
                        <div style="padding-left: 25px;">
                            <div>
                                <h6 style="display: inline-block;">Área do Curso: </h6>
                                <asp:Label runat="server" ID="lblArea" Style="display: inline-block;"></asp:Label>
                            </div>
                            <div>
                                <h6 style="display: inline-block;">Tipo de Curso: </h6>
                                <asp:Label runat="server" ID="lblTipo" Style="display: inline-block;"></asp:Label>
                            </div>
                            <div>
                                <h6 style="display: inline-block;">Referencial n.º: </h6>
                                <asp:Label runat="server" ID="lblRef" Style="display: inline-block;"></asp:Label>
                            </div>
                            <div>
                                <h6 style="display: inline-block;">Nível</h6>
                                <asp:Label runat="server" ID="lblQNQ" Style="display: inline-block;"></asp:Label>
                                <h6 style="display: inline-block;">do QNQ</h6>
                            </div>
                        </div>
                        <div>
                            <asp:Repeater runat="server" ID="rptModules">
                                <HeaderTemplate>
                                    <div class="card-body px-0 pt-0 pb-2">
                                        <div class="table-responsive p-2 col-md-6">
                                            <table class="table align-items-center mb-0">
                                                <thead>
                                                    <tr>
                                                        <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                        <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td id="cellNome">
                                            <div class="d-flex px-2">
                                                <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("SVG") %>' />
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
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
