<%@ Page Title="Formadores" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageTeachers.aspx.cs" Inherits="FinalProject.ManageTeachers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row text-center">
            <div class="col-md-9 ">

                <div class="nav-wrapper position-relative end-0">
                    <ul class="nav nav-pills nav-fill p-1">
                        <li class="nav-item">
                            <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="listTeachers" OnClientClick="return handleLinkButtonClick('List');" href="javascript:void(0);">Listar
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link mb-0 px-0 py-1" runat="server" ID="insertTeachers" OnClientClick="return handleLinkButtonClick('Insert');" href="javascript:void(0);">Inserir
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton runat="server" class="nav-link mb-0 px-0 py-1" ID="editTeachers" OnClientClick="return handleLinkButtonClick('Edit');" href="javascript:void(0);"> Editar/Eliminar
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>


                <div class="container-fluid mt-4 py-4">
                    <div id="listTeachersDiv" class="hidden">
                        Listar Formadores

                    </div>
                    <div id="insertTeachersDiv" class="hidden">
                        <!-- Registration Completion -->
                        <div id="registration" class="container-fluid mt-4 py-4">
                            <!-- Begin Registration Page 1-->
                            <div id="registerCompletionpage1">
                                <div class="card">
                                    <div class="card-header pb-0 p-3">
                                        <h6 class="mb-1">Completa o teu registo</h6>
                                        <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="tbNome">Nome</asp:Label>
                                                                    <asp:RequiredFieldValidator ValidationGroup="Page1" ID="rfvNome" Text="*" ErrorMessage="Nome Completo Obrigatório" runat="server" ControlToValidate="tbNome" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbNome" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="Nome Completo"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo">Sexo</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" Text="*" ErrorMessage="Género Obrigatório" ValidationGroup="Page1" ControlToValidate="ddlSexo" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlSexo" runat="server" ValidationGroup="Page1" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Feminino</asp:ListItem>
                                                                        <asp:ListItem Value="1">Masculino</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="tbDataNascimento">Data de Nascimento</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvDataNascimento" Text="*" ValidationGroup="Page1" ErrorMessage="Data de Nascimento Obrigatória" runat="server" ControlToValidate="tbDataNascimento" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <div class="input-group">
                                                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>

                                                                        <asp:TextBox ID="tbDataNascimento" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="date"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lbltipoDocumento" runat="server" AssociatedControlID="ddlDocumentoIdent">Documento de Identificação</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvDocumentIdent" Text="*" ErrorMessage="Documento de Identificação Obrigatório" ValidationGroup="Page1" runat="server" ControlToValidate="ddlDocumentoIdent" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlDocumentoIdent" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCC" runat="server" AssociatedControlID="tbCC">Nr.º do Documento</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCC" Text="*" ValidationGroup="Page1" ErrorMessage="Nrº do Documento Obrigatório" runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbCC" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="e.g., 123456789 Z Z1Z"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDataValidade" runat="server" AssociatedControlID="tbDataValidade">Data de Validade</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvDataValidade" Text="*" ValidationGroup="Page1" ErrorMessage="Data de Validade Obrigatória" runat="server" ControlToValidate="tbDataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <div class="input-group">
                                                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                                        <asp:TextBox ID="tbDataValidade" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="date"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-7">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="tbNrSegSocial">Número de Segurança Social</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvNrSegSocial" Text="*" ValidationGroup="Page1" ErrorMessage="Nr.ª da Segurança Social Obrigatório" runat="server" ControlToValidate="tbNrSegSocial" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbNrSegSocial" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="12345678910"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNIF" runat="server" AssociatedControlID="tbNIF">NIF</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvNIF" Text="*" runat="server" ValidationGroup="Page1" ErrorMessage="NIF Obrigatório" ControlToValidate="tbNIF" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbNIF" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="123456789"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblMorada" runat="server" AssociatedControlID="tbMorada">Morada</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvMorada" Text="*" ValidationGroup="Page1" ErrorMessage="Morada Obrigatória" runat="server" ControlToValidate="tbMorada" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbMorada" ValidationGroup="Page1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Introduza a sua morada completa"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodPostal" runat="server" AssociatedControlID="tbCodPostal">Código-Postal</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvcodCodPostal" Text="*" ErrorMessage="Código-Postal Obrigatório" ValidationGroup="Page1" runat="server" ControlToValidate="tbCodPostal" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox runat="server" ID="tbCodPostal" CssClass="form-control" placeholder="0000-000"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblfreguesia" runat="server" AssociatedControlID="tbfreguesia">Localidade</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvfreguesia" Text="*" ErrorMessage="Freguesia Obrigatória" ValidationGroup="Page1" runat="server" ControlToValidate="tbfreguesia" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox runat="server" ID="tbfreguesia" CssClass="form-control" ValidationGroup="Page1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">País</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodPais" Text="*" ErrorMessage="País Obrigatório" ValidationGroup="Page1" runat="server" ControlToValidate="ddlCodPais" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlCodPais" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nomePT" DataValueField="codPais">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [pais] ORDER BY nomePT"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card card-footer">
                                                            <div class="col-md-12 align-items-start">
                                                                <asp:Button runat="server" ID="btn_back" ValidationGroup="Page1" OnClick="btn_back_Click" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" />
                                                                &nbsp;
                                            <asp:Button runat="server" ID="btn_next" ValidationGroup="Page1" OnClientClick="showNextDiv(); return false;" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Seguinte" />
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btn_next" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-3">
                                                <div>
                                                    <div class="alert text-white font-weight-bold" role="alert">
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Page1" ForeColor="#cc3a60" DisplayMode="List" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!-- End Registration Page 1-->

                            <!-- Begin Registration Page 2-->
                            <div id="registerCompletionpage2" class="hidden">
                                <div class="card">
                                    <div class="card-header pb-0 p-3">
                                        <h6 class="mb-1">Completa o teu registo</h6>
                                        <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>

                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Estado Civil</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodEstadoCivil" runat="server" ValidationGroup="Page2" ControlToValidate="ddlCodEstadoCivil" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlCodEstadoCivil" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSEstadoCivil" DataTextField="estadoCivil" DataValueField="codEstadoCivil">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSEstadoCivil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [estadoCivil]"></asp:SqlDataSource>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-9">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="tbIBAN">IBAN</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvIBAN" ValidationGroup="Page2" runat="server" ControlToValidate="tbIBAN" ForeColor="#cc3a60" ErrorMessage="IBAN obrigatório" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbIBAN" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="tbNaturalidade">Naturalidade</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodNaturalidade" ValidationGroup="Page2" runat="server" ControlToValidate="tbNaturalidade" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox runat="server" ID="tbNaturalidade"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Nacionalidade</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodNacionalidade" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodNacionalidade" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlCodNacionalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nacionalidade" DataValueField="codPais">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Situação Profissional</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodSituacaoProfissional" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodSituacaoProfissional" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlCodSituacaoProfissional" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSsituacaoProfissional" DataTextField="situacaoProfissional" DataValueField="codSituacaoProfissional">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSsituacaoProfissional" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [situacaoProfissional]"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodPrefixo" runat="server" AssociatedControlID="ddlprefixo">Prefixo</asp:Label>
                                                                    <asp:DropDownList ID="ddlprefixo" CssClass="form-control" runat="server" DataSourceID="SQLDSPrefixo" DataTextField="prefixo" DataValueField="codPais"></asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSPrefixo" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codPais,CONCAT(nomePT, ': ' , prefixo) AS prefixo FROM [pais] order by nomePT"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblTelemovel" runat="server" AssociatedControlID="tbTelemovel">Telemóvel</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvTelemovel" ValidationGroup="Page2" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60" Text="*" ErrorMessage="Telemóvel Obrigatório"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbTelemovel" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="tbEmail">Email</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" ErrorMessage="E-mail Obrigatório" ValidationGroup="Page2" runat="server" ControlToValidate="tbEmail" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="tbEmail" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row px-xl-5 px-sm-4 px-3">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCodGrauAcademico" runat="server" AssociatedControlID="ddlCodGrauAcademico">Grau Académico</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvCodGrauAcademico" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodGrauAcademico" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlCodGrauAcademico" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSGrauAcademico" DataTextField="grauAcademico" DataValueField="codGrauAcademico">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SQLDSGrauAcademico" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [grauAcademico]"></asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblFoto" runat="server" AssociatedControlID="fuFoto">Foto</asp:Label>
                                                                    <asp:FileUpload ID="fuFoto" ValidationGroup="Page2" runat="server" CssClass="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAnexo" runat="server" AssociatedControlID="fuAnexo">Anexo</asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvAnexo" ErrorMessage="Anexos Obrigatórios" ValidationGroup="Page2" runat="server" ControlToValidate="fuAnexo" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                                                    <asp:FileUpload ID="fuAnexo" ValidationGroup="Page2" runat="server" CssClass="form-control" AllowMultiple="True" />
                                                                    <small>CV, Documento de Identificação</small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card card-footer">
                                                            <div class="col-md-12 align-items-center">
                                                                <asp:Button runat="server" ID="Button1" ValidationGroup="Page2" OnClientClick="showPrevDiv(); return false;" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" />
                                                                &nbsp;

                                            <asp:Button runat="server" ID="btn_submit" ValidationGroup="Page2" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Submeter"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btn_submit" />
                                                    </Triggers>
                                                </asp:UpdatePanel>


                                            </div>
                                            <div class="col-md-3">
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Page2" ForeColor="#cc3a60" DisplayMode="List" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!-- End Registration Page 2-->
                            <div id="registrationMessage" class="hidden">
                                <div class="alert alert-primary text-white font-weight-bold" role="alert">
                                    <small class="text-uppercase font-weight-bold">
                                        <asp:Label runat="server" ID="lbl_message"></asp:Label></small>
                                </div>
                            </div>
                        </div>
                        <!-- End of Registration Completion -->


                    </div>


                    <div id="editTeachersDiv" class="hidden">Editar Formadores</div>
                </div>
    </div>
    </div>
        <!--Função de Javascript para mostrar o calendário do flatpickr nas TextBoxes -->
        <script>
            flatpickr('#<%= tbDataNascimento.ClientID %>', {
                // Options
                dateFormat: 'd-m-Y',
                theme: 'light'
            });

            flatpickr('#<%= tbDataValidade.ClientID %>', {
                // Options
                dateFormat: 'd-m-Y',
                theme: 'light'
            });
        </script>

        <!--Função de Javascript para esconder a primeira página do registo e mostrar a segunda -->
        <script>
            function showNextDiv() {
                // Check if all validators are valid
                var isValid = Page_ClientValidate('Page1');

                // If validators are valid, proceed to show next div
                if (isValid) {
                    // Remove 'show' class and add 'hide' class to div1
                    document.getElementById('registerCompletionpage1').classList.remove('card');
                    document.getElementById('registerCompletionpage1').classList.add('hidden');

                    // Remove 'hide' class and add 'show' class to div2
                    document.getElementById('registerCompletionpage2').classList.remove('hidden');
                    document.getElementById('registerCompletionpage2').classList.add('card');
                }
            }
        </script>

        <!--Função de Javascript para mostrar a mensagem do registo dos dados -->
        <script>
            function submitInfo() {
                // Check if all validators are valid
                var isValid = Page_ClientValidate('Page2');

                // If validators are valid, proceed to show next div
                if (isValid) {
                    // Remove 'hide' class and add 'show' class to div2
                    document.getElementById('registrationMessage').classList.remove('hidden');
                }
            }
        </script>

        <!-- Funções da Javascript para Atualizar o URL, as Divs consoante o click e atualizar o breadcrumb -->
        <script>
            function handleLinkButtonClick(action) {
                var url, title;
                switch (action) {
                    case 'List':
                        url = '../ManageTeachers.aspx?List';
                        document.getElementById('listTeachersDiv').classList.remove('hidden');
                        document.getElementById('editTeachersDiv').classList.add('hidden');
                        document.getElementById('insertTeachersDiv').classList.add('hidden');
                        title = "Listar Formadores";
                        break;
                    case 'Edit':
                        url = '../ManageTeachers.aspx?Edit';
                        document.getElementById('editTeachersDiv').classList.remove('hidden');
                        document.getElementById('listTeachersDiv').classList.add('hidden');
                        document.getElementById('insertTeachersDiv').classList.add('hidden');
                        title = "Editar Formadores";
                        break;
                    case 'Insert':
                        url = '../ManageTeachers.aspx?Insert';
                        document.getElementById('insertTeachersDiv').classList.remove('hidden');
                        document.getElementById('listTeachersDiv').classList.add('hidden');
                        document.getElementById('editTeachersDiv').classList.add('hidden');
                        title = "Inserir Formadores";
                        break;
                    default:
                        // Default URL or action if not recognized
                        url = '../ManageTeachers.aspx';
                        title = "Gestão de Formadores";
                        break;
                }

                //// Update URL
                window.history.replaceState(null, null, url);

                updateBreadcrumb(title, action);

                // Return false to prevent default postback behavior
                return false;
            }

            function updateBreadcrumb(title, action) {
                var breadcrumbContainer = document.getElementById('<%= Master.FindControl("SiteMapPath1").ClientID %>');
                var siteNode = document.getElementById('<%= Master.FindControl("siteNode").ClientID %>')
                if (breadcrumbContainer) {
                    breadcrumbContainer.innerHTML = '';
                    siteNode.innerHTML = '';

                    var breadcrumbItems = [];
                    breadcrumbItems.push('<a href="/">Home</a>');
                    breadcrumbItems.push('<a href="../Manage.aspx">Gestão</a>');
                    breadcrumbItems.push('<a href="../ManageTeachers.aspx">Gestão de Formadores</a>');

                    if (action === 'List') {
                        breadcrumbContainer.innerHTML = breadcrumbItems.join('>');
                        breadcrumbContainer.innerHTML += '>' + title;
                        siteNode.innerHTML = title;
                    }
                    if (action === 'Edit') {
                        breadcrumbContainer.innerHTML = breadcrumbItems.join(' > ');
                        breadcrumbContainer.innerHTML += '>' + title;
                        siteNode.innerHTML = title;
                    }
                    if (action === 'Insert') {
                        breadcrumbContainer.innerHTML = breadcrumbItems.join(' > ');
                        breadcrumbContainer.innerHTML += '>' + title;
                        siteNode.innerHTML = title;
                    }
                }
            }
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
</asp:Content>
