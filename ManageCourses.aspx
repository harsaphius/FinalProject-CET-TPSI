<%@ Page Title="Gestão de Cursos" EnableEventValidation="false" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" EnableViewState="true" Inherits="FinalProject.ManageCourses" %>

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
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="listCourses" href="./ManageCourses.aspx?List">Listar
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton class="nav-link mb-0 px-0 py-1" runat="server" ID="insertCourses" href="./ManageCourses.aspx?Insert">Inserir
                                </asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="editCourses" href="./ManageCourses.aspx?Edit"> Editar/Eliminar
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="listCourses" />
                <asp:AsyncPostBackTrigger ControlID="insertCourses" />
                <asp:AsyncPostBackTrigger ControlID="editCourses" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="manageCoursesDiv" class="pageDiv">Gestão de Cursos </div>
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
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [area]"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
                                    <span>Tipo:</span>
                                    <div class="dropdown">
                                        <asp:DropDownList ID="ddl_tipo" class="dropdown-toggle btn bg-gradient-secundary" runat="server" DataSourceID="SQLDSTipo" DataTextField="nomeCurso" DataValueField="codTipoCurso">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codTipoCurso,CONCAT(nomeTipoCurto , ' - ' ,nomeTipoLongo) AS nomeCurso FROM tipoCurso"></asp:SqlDataSource>
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
                    <!-- Listagem de Cursos -->
                    <div id="listCoursesDiv" class="pageDiv">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Repeater ID="rpt_Courses" runat="server">
                                    <HeaderTemplate>
                                        <table class="table align-items-center justify-content-center mb-0">
                                            <thead>
                                                <tr>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Curso</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Referencial</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código QNQ</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="d-flex px-2">
                                                    <div class="my-auto">
                                                        <h6 class="mb-0 text-sm"><%# Eval("CodCurso") %></h6>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <p class="text-sm font-weight-bold mb-0"><%# Eval("Nome") %></p>
                                            </td>
                                            <td class="text-xs font-weight-bold">
                                                <%# Eval("CodRef") %>
                                            </td>
                                            <td class="text-xs font-weight-bold">Nível <%# Eval("CodQNQ") %>
                                            </td>
                                            <td class="align-middle text-center">
                                                <asp:LinkButton runat="server" ID="lbt_edit" Text="Edit" class="text-secondary font-weight-bold text-xs" OnClientClick="showEdit(); return true;">
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
                    <!-- Fim da Listagem de Cursos -->
                    <!-- Inserção de Cursos -->
                    <div id="insertCoursesDiv" class="pageDiv">

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
                                                <asp:Repeater ID="rpt_insertCourses" runat="server" OnItemDataBound="rpt_insertCourses_ItemDataBound">
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
                                                                        <asp:CheckBox runat="server" ID="chckBox" OnCheckedChanged="chkBoxMod_CheckedChanged" />
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
                                            <asp:Button ID="btn_insert" runat="server" Text="Inserir" OnClick="btn_insert_Click" class="btn bg-gradient-info w-100 mt-4 mb-0" />
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
                    <!-- Fim de Inserção de Cursos -->
                    <!-- Edição de Cursos -->
                    <div id="editCoursesDiv" class="pageDiv">
                        <div class="col-md-12 col-sm-6 text-end" style="padding-right: 20px; font-family: var(--bs-font-sans-serif)">
                            <a href="javascript:;" onclick="toggleFilters()">
                                <i class="fas fa-filter text-primary text-lg" data-bs-toggle="tooltip" data-bs-placement="top" title="Filter" aria-hidden="true">Filtros</i>
                            </a>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table align-items-center justify-content-center mb-0">
                                            <thead>
                                                <tr>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">Curso</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Nome</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Referencial</th>
                                                    <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2">Código QNQ</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="d-flex px-2">
                                                    <div class="my-auto">
                                                        <h6 class="mb-0 text-sm"><%# Eval("CodCurso") %></h6>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <p class="text-sm font-weight-bold mb-0"><%# Eval("Nome") %></p>
                                            </td>
                                            <td class="text-xs font-weight-bold">
                                                <%# Eval("CodRef") %>
                                            </td>
                                            <td class="text-xs font-weight-bold">Nível <%# Eval("CodQNQ") %>
                                            </td>
                                            <td class="align-middle text-center">
                                                <asp:LinkButton runat="server" ID="lbt_edit" Text="Edit" class="text-secondary font-weight-bold text-xs" OnClientClick="showEdit(); return true;">
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
                    <!-- Fim de Edição de Cursos -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"></script>
    <script>
        $(function () {
            $(".draggable-item").draggable({
                revert: "invalid",
                cursor: "move",
                helper: "clone",
                zIndex: 100
            });

            $(".draggable-item").droppable({
                tolerance: "pointer",
                drop: function (event, ui) {
                    var draggedItem = ui.draggable;
                    var droppedOnItem = $(this);

                    // If dragged onto itself, do nothing
                    if (draggedItem.is(droppedOnItem)) {
                        return;
                    }

                    // Get the index of dragged and dropped items
                    var draggedIndex = draggedItem.index();
                    var droppedIndex = droppedOnItem.index();

                    // Calculate the direction of movement
                    var moveUp = draggedIndex > droppedIndex;

                    // Move the dragged item accordingly
                    if (moveUp) {
                        draggedItem.insertBefore(droppedOnItem);
                    } else {
                        draggedItem.insertAfter(droppedOnItem);
                    }

                    // Send AJAX request to update the order on the server
                    $.ajax({
                        type: "POST",
                        url: "YourPage.aspx/UpdateOrder",
                        data: JSON.stringify({ draggedItemId: draggedItem.attr("id"), droppedOnItemId: droppedOnItem.attr("id") }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Handle error
                        }
                    });
                }
            });
        });
    </script>

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

    <!-- -->
    <script>
        function showEdit(event) {
            event.preventDefault();
            // Remove 'show' class and add 'hide' class to div1
            document.getElementById('listCoursesDiv').classList.add('hidden');

            // Remove 'hide' class and add 'show' class to div2
            document.getElementById('editCoursesDiv').classList.remove('hidden');
        }
    </script>

</asp:Content>
