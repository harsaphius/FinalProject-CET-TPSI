<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="FinalProject.ManageCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-secundary py-2">
        <div class="row ">
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
        <div class="py-3 align-items-center row" style="padding-left: 28px;">
            <div class="col-sm-3">
                <small class="text-uppercase font-weight-bold">Módulos para o curso:</small>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rpt_manageCourses" runat="server" DataSourceID="SQLDSModulos">
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_course_insert" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="alert alert-primary text-white font-weight-bold" role="alert">
            <asp:Label runat="server" ID="lbl_selection"></asp:Label>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="input-group mb-3">                
                    <asp:TextBox runat="server" ID="tb_course" CssClass="form-control" placeholder="Nome do Curso" AutoPostBack="True"></asp:TextBox> 
                    <asp:Button runat="server" ID="btn_course_insert" class="btn btn-outline-primary mb-0" Text="Inserir" OnClick="btn_course_insert_Click" />   
                    <asp:RequiredFieldValidator runat="server" ID="rfvtbcourse" ControlToValidate="tb_course" ForeColor="#cc3a60" ErrorMessage=""></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
