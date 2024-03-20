<%@ Page Title="Salas" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClassrooms.aspx.cs" Inherits="FinalProject.ManageClassrooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="nav-wrapper position-relative end-0">
                        <ul class="nav nav-pills nav-fill p-1">
                            <li class="nav-item">
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="listClassrooms" href="../ManageClassrooms.aspx?List">Listar
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton class="nav-link mb-0 px-0 py-1" runat="server" ID="insertClassrooms" href="../ManageClassrooms.aspx?Insert">Inserir
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="editClassrooms" href="../ManageClassrooms.aspx?Edit"> Editar/Eliminar
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="listClassrooms" />
                <asp:AsyncPostBackTrigger ControlID="insertClassrooms" />
                <asp:AsyncPostBackTrigger ControlID="editClassrooms" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div class="container bg-secundary py-2">
                    <div id="listClassroomsDiv" class="pageDiv">
                        <div class="py-3 align-items-center row" style="padding-left: 28px;">
                            <div class="col-sm-3">
                                <small class="text-uppercase font-weight-bold">Cursos:</small>
                            </div>
                        </div>
                    </div>
                    <div id="insertClasseroomsDiv" class="pageDiv">

                        <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                            <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                        </div>

                        <div class="page-header min-vh-30">
                            <div class="container">
                                <div class="row ">
                                    <div class="col-xl-8 col-lg-7 col-md-6">
                                        <div class="card card-plain">
                                            <!-- Inserção de Dados de Curso - Textboxes e DDL's -->
                                            <div class="card-header pb-0 text-left bg-transparent">
                                                <h5 class="font-weight-bolder text-info text-gradient">Inserção de novo curso:</h5>
                                            </div>
                                            <div class="card-body">
                                                <div role="form">
                                                    <label>Nome do Curso</label>
                                                    <asp:RequiredFieldValidator ID="rfvCourseName" ErrorMessage="Nome do curso obrigatório" Text="*" runat="server" ControlToValidate="tbCourseName" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <div class="mb-2">
                                                        <asp:TextBox ID="tbCourseName" CssClass="form-control" placeholder="Nome do Curso" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label>Tipo de Curso</label>
                                                    <asp:RequiredFieldValidator ID="rfvTipoCurso" runat="server" ErrorMessage="Tipo de curso obrigatório" Text="*" ControlToValidate="ddlTipoCurso" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <div class="mb-2">
                                                        <asp:DropDownList ID="ddlTipoCurso" CssClass="form-control" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>

                                                    </div>
                                                    <label>Área do Curso</label>
                                                    <asp:RequiredFieldValidator ID="rvfarea" runat="server" ErrorMessage="Área do curso obrigatória" Text="*" ControlToValidate="ddlAreaCurso" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <div class="mb-2">
                                                        <asp:DropDownList ID="ddlAreaCurso" CssClass="form-control" runat="server" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                                    </div>
                                                    <label>Referencial n.º:</label>
                                                    <asp:RequiredFieldValidator ID="rfvRef" runat="server" ErrorMessage="N.º de Referencial obrigatório" Text="*" ControlToValidate="tbRef" ForeColor="#cc3a60"></asp:RequiredFieldValidator>

                                                    <div class="mb-2">
                                                        <asp:TextBox ID="tbRef" CssClass="form-control" placeholder="Referencial n.º" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label>Qualificação QNQ</label>
                                                    <asp:RequiredFieldValidator ID="rfvQNQ" runat="server" ErrorMessage="Nível de Qualificação QNQ obrigatória" Text="*" ControlToValidate="ddlQNQ" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <div class="mb-2">
                                                        <asp:DropDownList ID="ddlQNQ" CssClass="form-control" runat="server" DataSourceID="SQLDSQNQ" DataTextField="codQNQ" DataValueField="codQNQ"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSQNQ" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT CONCAT('Nível ', codQNQ) AS codQNQ FROM [nivelQNQ]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rpt_insertClassrooms" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="row px-2" style="padding: 10px;">
                                                            <!-- Inserção de Dados de Curso - Seleção dos Módulos -->
                                                            <small class="text-uppercase font-weight-bold">Módulos do Curso:</small>
                                                            <p>Seleccione os módulos pertencentes a este curso</p>
                                                            <asp:Label runat="server" ID="Label1"></asp:Label>
                                                            <div class="input-group mb-4">
                                                                <asp:LinkButton runat="server" ID="lbtn_search" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:TextBox runat="server" ID="tb_search" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <table class="table align-items-center justify-content-center mb-0">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Módulo</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">UFCD</th>
                                                                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Pertence</th>
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
                                                            <td id="formyckb" class="text-xs font-weight-bold">
                                                                <div class="stats">
                                                                    <small><%# Eval("CodModulo") %></small>
                                                                    <asp:HiddenField ID="hdnModuleID" runat="server" Value='<%# Eval("CodModulo") %>' />
                                                                    <asp:HiddenField ID="hdnModuleName" runat="server" Value='<%# Eval("Nome") %>' />
                                                                    <div class="form-check">
                                                                        <asp:CheckBox runat="server" ID="chckBox" />
                                                                        <asp:Label runat="server" ID="lbl_order">Selecione este módulo</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                        </div>
                                            </table>                                      
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="row px-4" style="padding: 10px;">
                                            <small class="text-uppercase font-weight-bold">Ordem dos Módulos:</small>
                                            <asp:Label runat="server" ID="lbl_selection">  </asp:Label>
                                        </div>
                                        <div class="text-center">
                                            <asp:Button ID="btn_insert" runat="server" Text="Inserir" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n12">
                                            <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="editClasseroomsDiv" class="pageDiv">
                        <div class="py-3 align-items-center row" style="padding-left: 28px;">
                            <button class="btn btn-icon btn-2 btn-primary" type="button">
                                <span class="btn-inner--icon"><i class="fa-solid fa-filter" aria-hidden="true"></i></span>
                            </button>
                            <div class="col-sm-3">
                                <small class="text-uppercase font-weight-bold">Edite os cursos</small>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="rpt_manageCourses" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
