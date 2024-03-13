<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="FinalProject.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-secundary py-2">
        <div id="listCoursesDiv" class="hidden">
            <div class="py-3 align-items-center row" style="padding-left: 28px;">
                <div class="col-sm-3">
                    <small class="text-uppercase font-weight-bold">Cursos:</small>
                </div>
            </div>
        </div>
        <div id="insertCoursesDiv" class="">
            <div class="row px-4 justify-content-center">
                <div class="col-lg-4 col-md-3 col-sm-6">
                    <span>Área:</span>
                    <div class="dropdown">
                        <asp:DropDownList ID="ddl_area" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                        <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-lg-4 col-md-3 col-sm-6">
                    <span>Tipo:</span>
                    <div class="dropdown">
                        <asp:DropDownList ID="ddl_tipo" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso"></asp:DropDownList>
                        <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="row px-4">
                <div class="alert alert-primary text-white font-weight-bold px-4" role="alert">
                    <small class="text-uppercase font-weight-bold">Módulos:</small>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rpt_insertCourses" runat="server" DataSourceID="SQLDSModulos">
                        <HeaderTemplate>
                            <div class="container">
                                <div class="row">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="card-group col-lg-4 col-md-3 col-sm-6" style="padding-bottom: 20px; height: auto;">
                                <div class="card">
                                    <div class="card-header p-2 mx-3 mt-3 position-relative z-index-1">
                                    </div>
                                    <div class="card-body pt-2">
                                        <span class="text-gradient text-primary text-uppercase text-xs font-weight-bold my-2">UFCD <%# Eval("codUFCD") %></span>
                                        <a href="javascript:;" class="card-title h5 d-block text-darker"><%# Eval("nomeModulos") %>
                                        </a>
                                        <p class="card-description mb-4">
                                            <%# Eval("descricao") %>
                                        </p>
                                        <div class="author">
                                            <div class="name">
                                                <span>Duração: <%# Eval("duracao") %> h</span>
                                                <div class="stats">
                                                    <small><%# Eval("codModulos") %></small>
                                                    <asp:HiddenField ID="hdnModuleID" runat="server" Value='<%# Eval("codModulos") %>' />
                                                    <asp:HiddenField ID="hdnModuleName" runat="server" Value='<%# Eval("nomeModulos") %>' />
                                                    <div class="form-check">
                                                        <asp:CheckBox runat="server" class="form-check-input" ID="chkBoxMod" AutoPostBack="true" OnCheckedChanged="chkBoxMod_CheckedChanged"></asp:CheckBox>
                                                        <asp:Label runat="server" ID="lbl_order">Selecione este módulo</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                    </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource ID="SQLDSModulos" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [modulo]"></asp:SqlDataSource>
                    <div class="row px-4">
                        <div class="alert alert-primary text-white font-weight-bold" role="alert">
                            <small class="text-uppercase font-weight-bold">Ordem dos Módulos:</small>
                            <asp:Label runat="server" ID="lbl_selection"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_course_insert" />
                </Triggers>
            </asp:UpdatePanel>

            <div class="row px-3">
                <div class="form-group">
                    <div class="input-group mb-3">
                        <asp:TextBox runat="server" ID="tb_course" CssClass="form-control" placeholder="Nome do Curso" AutoPostBack="True"></asp:TextBox>
                        <asp:Button runat="server" ID="btn_course_insert" class="btn btn-outline-primary mb-0" Text="Inserir" OnClick="btn_course_insert_Click" />
                    </div>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ID="rfvtbcourse" ControlToValidate="tb_course" ForeColor="#cc3a60" ErrorMessage="Required"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div id="editCoursesDiv" class="hidden">
            <div class="py-3 align-items-center row" style="padding-left: 28px;">
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
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_course_insert" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <nav aria-label="Page navigation example">
        <ul id="pagination" class="pagination">
            <li class="page-item" id="previousPage">
                <a class="page-link" href="javascript:;" aria-label="Previous">
                    <i class="fa fa-angle-left"></i>
                    <span class="sr-only">Previous</span>
                </a>
            </li>
            <li id="paginationContainer" runat="server">
            <!-- Pagination will be dynamically generated here -->
            </li>
            <!-- Pagination items will be dynamically generated here -->
            <li class="page-item" id="nextPage">
                <a class="page-link" href="javascript:;" aria-label="Next">
                    <i class="fa fa-angle-right"></i>
                    <span class="sr-only">Next</span>
                </a>
            </li>
        </ul>
    </nav>

</asp:Content>
