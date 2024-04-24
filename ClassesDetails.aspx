<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ClassesDetails.aspx.cs" Inherits="FinalProject.ClassesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top: 15px">
        <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
            <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="False" Text="Voltar" ID="btnBack" OnClick="btnBack_Click" />
        </div>
    </div>
    <div class="container row justify-content-center">
        <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; width: 70%; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
        <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" Enabled="false" OnTick="timerMessageEdit_OnTick"></asp:Timer>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-header pb-0 text-primary text-center" style="margin-bottom: 2px;">
                    <h6>
                        <asp:Label ID="lblNomeTurma" runat="server"></asp:Label></h6>
                </div>
                <div class="card-body px-0 pt-0 pb-2">
                    <div style="padding-left: 25px;">
                        <div>
                            <h6 style="display: inline-block;">Nr.º da Turma: </h6>
                            <asp:Label runat="server" ID="lblNrTurma" Style="display: inline-block;"></asp:Label>
                        </div>
                        <div>
                            <h6 style="display: inline-block;">Nome de Curso: </h6>
                            <asp:Label runat="server" ID="lblNomeCurso" Style="display: inline-block;"></asp:Label>
                        </div>
                        <div>
                            <h6 style="display: inline-block;">Regime:</h6>
                            <asp:Label runat="server" ID="lblRegime" Style="display: inline-block;"></asp:Label>
                        </div>
                        <div>
                            <h6 style="display: inline-block;">Horário da Turma:</h6>
                            <asp:Label runat="server" ID="lblHorario" Style="display: inline-block;"></asp:Label>
                        </div>
                        <div>
                            <h6 style="display: inline-block;">Data de Início: </h6>
                            <asp:Label runat="server" ID="lblDataInicioDetail" Style="display: inline-block;"></asp:Label>
                        </div>
                        <div>
                            <h6 style="display: inline-block;">Data de Fim:</h6>
                            <asp:Label runat="server" ID="lblDataFimDetail" Style="display: inline-block;"></asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:Repeater runat="server" ID="rptStudentsDetail">
                            <HeaderTemplate>
                                <div class="card-body px-0 pt-0 pb-2">
                                    <div class="table-responsive p-2 col-md-8">
                                        <table class="table align-items-center mb-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Formando</th>
                                                    <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Estado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <div class="d-flex px-2">
                                            <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />
                                            <div class="my-auto">
                                                <p class="mb-0 text-sm">
                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <p class="text-sm mb-0">
                                            <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("CodSituacao") %>' Visible="true" Style="width: 100%;"></asp:Label>
                                        </p>
                                    </td>
                                    <td>
                                        <p class="text-sm mb-0">
                                            <asp:LinkButton runat="server" ID="lbtDesistencia" CausesValidation="false" OnClick="lbtDesistencia_Click" AutoPostBack="True" CommandName="Desistir" Visible="true" CommandArgument='<%# Eval("CodFormando") %>'
                                                class="text-primary font-weight-bold text-sm"> Desistência</i>
                                            </asp:LinkButton>
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
                    <div>
                        <asp:Repeater runat="server" ID="rptTeachersDetail">
                            <HeaderTemplate>
                                <div class="card-body px-0 pt-0 pb-2">
                                    <div class="table-responsive p-2 col-md-8">
                                        <table class="table align-items-center mb-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Formador</th>
                                                    <th class="col-sm-2 text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Estado</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <div class="d-flex px-2">
                                            <asp:Image ID="imgUpload" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />
                                            <div class="my-auto">
                                                <p class="mb-0 text-sm">
                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <p class="text-sm mb-0">
                                            <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("CodSituacao") %>' Visible="true" Style="width: 100%;"></asp:Label>
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

</asp:Content>
