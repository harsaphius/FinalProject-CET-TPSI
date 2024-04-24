<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="TeacherEvaluations.aspx.cs" Inherits="FinalProject.TeacherEvaluations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="listEvaluationsDiv" runat="server">
        <div class="container row justify-content-center">
            <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; width: 70%; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
            <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" Enabled="false" OnTick="timerMessageEdit_OnTick"></asp:Timer>
        </div>
        <asp:UpdatePanel ID="updatePanelEvaluations" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rptClassGroups" runat="server" OnItemDataBound="rptClassGroup_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="d-flex px-2 bg-gradient-faded-primary">
                            <div class="my-auto">
                                <h6 class="mb-0 text-md  text-white">
                                    <asp:Label runat="server" ID="lblNrTurma"><%# Eval("NomeTurma") %></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnCodTurma" Value='<%# Eval("CodTurma") %>' />
                                </h6>
                            </div>
                        </div>
                        <asp:Repeater ID="rptModules" runat="server" OnItemDataBound="rptModules_ItemDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="d-flex px-2">
                                    <div class="my-auto">
                                        <h6 class="mb-0 text-sm">
                                            <asp:Label runat="server" ID="lblModulo"><%# Eval("Nome") %></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdnCodModulo" Value='<%# Eval("CodModulo") %>' />
                                        </h6>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>

                                                <th>Formando</th>
                                                <th>Avaliação</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptStudents" runat="server" OnItemCommand="rptStudents_ItemCommand">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="imgPhoto" CssClass="avatar avatar-sm rounded-circle me-3" runat="server" ImageUrl='<%# Eval("Foto") %>' />
                                                            <asp:Label runat="server" ID="lblFormando"><%# Eval("Nome") %></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdnCodFormando" Value='<%# Eval("CodFormando") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbAvaliacao" CssClass="form-control" runat="server" Text='<%# Bind("Avaliacao") %>' Visible="false"></asp:TextBox>
                                                            <asp:Label ID="lblAvaliacao" Visible="true" runat="server" Text='<%# Eval("Avaliacao") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                        <td>
                                                            <asp:HiddenField runat="server" ID="hdnCodTurmaInner" Value='<%# Eval("CodTurma") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnCodModulo" Value='<%# Eval("CodModulo") %>' />

                                                            <asp:LinkButton CausesValidation="False" AutoPostBack="true" CommandArgument='<%# Eval("CodFormando") %>' class="btn-md btn-outline-primary text-sm"
                                                                data-bs-toggle="tooltip" Text="Avaliar" data-bs-placement="bottom" CommandName="Evaluate" runat="server" ID="lbtEvaluations">            
                                                            </asp:LinkButton>
                                                            <asp:LinkButton CausesValidation="False" AutoPostBack="true" CommandArgument='<%# Eval("CodFormando") %>' class="btn-md btn-outline-primary text-sm"
                                                                data-bs-toggle="tooltip" Text="Cancelar" data-bs-placement="bottom" CommandName="Cancel" runat="server" ID="lbtnCancel">            
                                                            </asp:LinkButton>
                                                            <asp:LinkButton CausesValidation="False" AutoPostBack="true" CommandArgument='<%# Eval("CodFormando") %>' class="btn-md btn-outline-primary text-sm"
                                                                data-bs-toggle="tooltip" Text="Gravar" data-bs-placement="bottom" CommandName="Commit" runat="server" ID="lbtnCommit">            
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <br />
                                <hr class="bg-white" />
                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
