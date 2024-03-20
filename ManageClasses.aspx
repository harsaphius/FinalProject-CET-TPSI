<%@ Page Title="Turmas" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageClasses.aspx.cs" Inherits="FinalProject.ManageClasses" %>

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
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="listClasses" href="../ManageClasses.aspx?List">Listar
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton class="nav-link mb-0 px-0 py-1" runat="server" ID="insertClasses" href="../ManageClasses.aspx?Insert">Inserir
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="editClasses" href="../ManageClasses.aspx?Edit"> Editar/Eliminar
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="listClasses" />
                <asp:AsyncPostBackTrigger ControlID="insertClasses" />
                <asp:AsyncPostBackTrigger ControlID="editClasses" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div class="col-md-12 col-sm-6 text-end" style="padding-right: 20px; font-family: var(--bs-font-sans-serif)">
                    <a href="javascript:;" onclick="toggleFilters()">
                        <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                    </a>
                </div>
                <div id="filters" class="hidden">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Designação:</span>
                                    <div class="input-group mb-4">
                                        <asp:LinkButton runat="server" ID="lbtn_search" class="input-group-text text-body"><i class="fas fa-search" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:TextBox runat="server" ID="tb_search" CssClass="form-control" placeholder="Type here..." AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Área:</span>
                                    <div class="dropdown">
                                        <asp:DropDownList ID="ddl_area" runat="server" class="btn bg-gradient-secundary dropdown-toggle" DataSourceID="SQLDSArea" DataTextField="nomeArea" DataValueField="codArea"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLDSArea" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Tipo:</span>
                                    <div class="dropdown">
                                        <asp:DropDownList ID="ddl_tipo" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLDSTipo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Data de Início: </span>
                                    <div class="input-group mb-4">
                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                        <asp:TextBox runat="server" ID="tb_dataInicio" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Data de Fim: </span>
                                    <div class="input-group mb-4">
                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                        <asp:TextBox runat="server" ID="tb_dataFim" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>
                                        <br />
                                    </span>
                                    <div class="input-group mb-4">
                                        <asp:Button runat="server" ID="btn_clear" CssClass="btn btn-outline-primary mb-0" Text="Limpar" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_clear" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="container bg-secundary py-2">
                    <div id="listClassesDiv" class="pageDiv">
                        <div class="py-3 align-items-center row" style="padding-left: 28px;">
                            <div class="col-sm-3">
                                <small class="text-uppercase font-weight-bold">Cursos:</small>
                            </div>
                        </div>
                    </div>
                    <div id="insertClassesDiv" class="pageDiv">

                        <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                            <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
                        </div>

                        <div class="page-header min-vh-30">
                            <div class="container">
                                <div class="row ">
                                    <div class="col-xl-8 col-lg-7 col-md-6">
                                        <div class="card card-plain">
                                            <!-- Inserção de Nova Turma -->
                                            <div class="card-header pb-0 text-left bg-transparent">
                                                <h5 class="font-weight-bolder text-info text-gradient">Criação de Nova Turma:</h5>
                                            </div>
                                            <div class="card-body">
                                                <div role="form">
                                                    <label>Designação do Curso</label>
                                                    <div class="mb-2">
                                                        <asp:DropDownList ID="ddlCurso" CssClass="form-control" runat="server" DataSourceID="SQLDSCurso" DataTextField="nomeCurso" DataValueField="codCurso"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSCurso" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM curso"></asp:SqlDataSource>

                                                    </div>
                                                    <label>Nr.º da Turma</label>
                                                    <asp:RequiredFieldValidator ID="rfvTipoCurso" runat="server" ErrorMessage="N.º da Turma Obrigatório" Text="*" ControlToValidate="tbTurma" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <div class="mb-2">
                                                        <asp:TextBox ID="tbTurma" CssClass="form-control" placeholder="Turma n.º" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label>Data Prevista de Ínicio</label>
                                                    <asp:RequiredFieldValidator ID="rfvDataInicio" Text="*" ErrorMessage="Data Prevista de Início Obrigatória" runat="server" ControlToValidate="tbDataInicio" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbDataInicio" runat="server" CssClass="form-control datepicker" TextMode="Date"></asp:TextBox>
                                                    <label>Data Prevista de Fim</label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ErrorMessage="Data Prevista de Fim obrigatória" runat="server" ControlToValidate="tbDataFim" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="tbDataFim" runat="server" CssClass="form-control datepicker" TextMode="Date"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rpt_formandos" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="row px-2" style="padding: 10px;">
                                                            <!-- Inserção de Dados de Formandos Inscritos Para o Curso - Seleção de Formandos -->
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
                                                                    <div class="form-check">
                                                                        <asp:CheckBox runat="server" ID="chckBox" />
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="row px-2" style="padding: 10px;">
                                                            <!-- Selecção de Formadores para os módulos que compõe o curso -->
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
                                                                    <div class="form-check">
                                                                        <asp:CheckBox runat="server" ID="chckBox" />
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
                    <div id="editClassesDiv" class="pageDiv">
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
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Javascript para ativar/desativar a div dos filtros -->
    <script>
        function toggleFilters() {
            var filtersDiv = document.getElementById('filters');
            if (filtersDiv.classList.contains('hidden')) {
                filtersDiv.classList.remove('hidden');
            } else {
                filtersDiv.classList.add('hidden');
            }
        }
    </script>
</asp:Content>
