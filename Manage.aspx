<%@ Page Title="Gestão" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="FinalProject.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <div class="h3">Gestão de Funcionalidades da Secretaria</div>
        <figure style="padding-right:20px; padding-top: 20px;">
            <blockquote class="blockquote text-center">
                <p class="ps-2">Innovation distinguishes between a leader and a follower.</p>
                <figcaption class="blockquote-footer ps-3 text-center">Steve Jobs
                </figcaption>
            </blockquote>
        </figure>
    </div>
    <div class="container-fluid py-4">
        <div class="row">
            <div class="col-12">
                <div class="card mb-4">
                    <div id="listUsersDiv" runat="server">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="container row justify-content-center">
                                    <asp:Label runat="server" ID="lblMessageEdit" Style="display: flex; justify-content: center; padding: 5px;" CssClass="hidden" role="alert"></asp:Label>
                                    <asp:Timer ID="timerMessageEdit" runat="server" Interval="3000" Enabled="False"></asp:Timer>
                                </div>
                                <div class="card-header pb-0">
                                    <h6>Utilizadores para ativação</h6>
                                </div>
                                <asp:Repeater ID="rptUsersForValidation" runat="server">
                                    <HeaderTemplate>
                                        <div class="card-body px-0 pt-0 pb-2">
                                            <div class="table-responsive p-0">
                                                <table class="table align-items-center mb-0" style="width: 100%;">
                                                    <colgroup>
                                                        <col style="width: 5%;" />
                                                        <col style="width: 25%;" />
                                                        <col style="width: 25%;" />
                                                        <col style="width: 5%;" />
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Utilizador</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">E-mail</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <p class="text-sm align-center">
                                                    <asp:HiddenField runat="server" ID="hdCodUser" Value='<%# Eval("CodUser") %>' />
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
                                            <td class="align-middle font-weight-bold text-center">
                                                <p>
                                                    <asp:LinkButton runat="server" ID="lbtEditUser" CausesValidation="false" OnClick="lbtEditUser_Click" CommandName="Activate" Visible="true" CommandArgument='<%# Bind("CodUser") %>' Text="Ativar" class="btn-md btn-outline-primary text-sm"></asp:LinkButton>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>

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
                                        <asp:Label runat="server" CssClass="text-white" ID="lblPageNumberUsers"></asp:Label>
                                    </span>
                                </li>
                                <li class="page-item">
                                    <asp:LinkButton ID="btnNextUser" CssClass="page-link" CausesValidation="false" runat="server">
                                                                                        <i class="fa fa-angle-right"></i>
                                                                                        <span class="sr-only">Next</span>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="text-center">
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Cursos" ID="btnManageCourses" href="./ManageCourses.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Turmas" ID="btnManageClassGroups" href="./ManageClasses.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Módulos" ID="btnManageModules" href="./ManageModules.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Formandos" ID="btnManageStudents" href="./ManageStudents.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Formadores" ID="btnManageTeachers" href="./ManageTeachers.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Salas" ID="btnManageClassrooms" href="./ManageClassrooms.aspx" />
        <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Gerir Utilizadores" ID="btnManageUsers" href="./ManageUsers.aspx" />
    </div>
</asp:Content>
