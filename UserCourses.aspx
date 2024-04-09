<%@ Page Title="Meus Cursos" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserCourses.aspx.cs" Inherits="FinalProject.UserCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Style to make LinkButtons display horizontally */
        .horizontal-link-buttons .nav-link {
            display: inline-block; /* Display LinkButtons inline */
            margin-right: 10px; /* Add some right margin between LinkButtons */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-4">
        <div>
            <p>
                <asp:Label runat="server" ID="lblEnrollment"></asp:Label>
            </p>
        </div>
        <div id="panelLinkButton">
            <asp:Panel ID="panelContainer" runat="server" CssClass="horizontal-link-buttons"></asp:Panel>
        </div>
        <div id="listCoursesDiv" runat="server">
            <asp:UpdatePanel runat="server" ID="updatePanelCoursesStudent">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-12">
                            <div class="card mb-4">
                                <div class="card-header pb-0">
                                    <h6>Cursos frequentados</h6>
                                </div>
                                <asp:Repeater runat="server">
                                    <HeaderTemplate>
                                        <div class="card-body px-0 pt-0 pb-2">
                                            <div class="table-responsive p-0">
                                                <table class="table align-items-center mb-0">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Turma</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Curso</th>
                                                            <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Data de Início</th>
                                                            <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Data de Fim</th>
                                                            <th class="text-secondary opacity-7"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="d-flex px-2 py-1">
                                                    <div>
                                                        <img src="../assets/img/team-2.jpg" class="avatar avatar-sm me-3" alt="user1">
                                                    </div>
                                                    <div class="d-flex flex-column justify-content-center">
                                                        <h6 class="mb-0 text-sm">John Michael</h6>
                                                        <p class="text-xs text-secondary mb-0">john@creative-tim.com</p>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <p class="text-xs font-weight-bold mb-0">Manager</p>
                                                <p class="text-xs text-secondary mb-0">Organization</p>
                                            </td>
                                            <td class="align-middle text-center text-sm">
                                                <span class="badge badge-sm bg-gradient-success">Online</span>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="text-secondary text-xs font-weight-bold">23/04/18</span>
                                            </td>
                                            <td class="align-middle">
                                                <button class="btn btn-link text-secondary mb-0">
                                                    <i class="fa fa-ellipsis-v text-xs"></i>
                                                </button>
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="listEvaluationsDiv" runat="server" Visible="False">
            <asp:UpdatePanel runat="server" ID="updatePanelEvaluationStudent">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-12">
                            <div class="card mb-4">
                                <div class="card-header pb-0">
                                    <h6>Avaliações</h6>
                                    <p>
                                        Curso:<asp:Label ID="lblCurso" runat="server" Text='<%# Eval("Curso") %>' Visible="true"></asp:Label>
                                    </p>
                                </div>
                                <asp:Repeater runat="server">
                                    <HeaderTemplate>
                                        <div class="card-body px-0 pt-0 pb-2">
                                            <div class="table-responsive p-0">
                                                <table class="table align-items-center justify-content-center mb-0">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código UFCD</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">N.º de Horas</th>
                                                            <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Avaliação</th>
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
                                                    <p class="text-sm">
                                                        <asp:Label ID="lblNome" runat="server" Text='<%# Eval("Nome") %>' Visible="true"></asp:Label>
                                                    </p>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUFCD" runat="server" Text='<%# Eval("UFCD") %>' Visible="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDuracao" runat="server" Text='<%# Eval("Duracao") %>' Visible="true"></asp:Label>

                                            </td>
                                            <td class="align-middle text-center">
                                                <asp:Label ID="lblAvaliacao" runat="server" Text='<%# Eval("UFCD") %>' Visible="true"></asp:Label>
                                            </td>
                                            <td class="align-middle">
                                                <button class="btn btn-link text-secondary mb-0">
                                                    <i class="fa fa-ellipsis-v text-xs"></i>
                                                </button>
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
