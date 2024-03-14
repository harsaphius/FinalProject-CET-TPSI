<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageTeachers.aspx.cs" Inherits="FinalProject.ManageTeachers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="insertTeachersDiv">
        <!-- Registration Completion -->
        <div id="registration" class="container-fluid mt-4 py-4">
            <div id="registerCompletionpage1" class="card">
                <div class="card">
                    <div class="card-header pb-0 p-3">
                        <h6 class="mb-1">Completa o teu registo</h6>
                        <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="tbNome">Nome</asp:Label>
                                    <asp:RequiredFieldValidator ValidationGroup="Page1" ID="rfvNome" Text="*" ErrorMessage="O campo Nome Completo é obrigatório." runat="server" ControlToValidate="tbNome" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbNome" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="Nome Completo"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo">Sexo</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" Text="*" ErrorMessage="O campo Sexo é obrigatório." ValidationGroup="Page1" ControlToValidate="ddlSexo" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSexo" runat="server" ValidationGroup="Page1" CssClass="form-control">
                                        <asp:ListItem>Feminino</asp:ListItem>
                                        <asp:ListItem>Masculino</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="tbDataNascimento">Data de Nascimento</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvDataNascimento" Text="*" ValidationGroup="Page1" ErrorMessage="O campo Data de Nascimento é obrigatório." runat="server" ControlToValidate="tbDataNascimento" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                        <asp:TextBox ID="tbDataNascimento" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" type="date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lbltipoDocumento" runat="server" AssociatedControlID="ddlDocumentoIdent">Tipo de Documento de Identificação</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvDocumentIdent" Text="*" ErrorMessage="O campo Tipo de Documento de Identificação é obrigatório." ValidationGroup="Page1" runat="server" ControlToValidate="ddlDocumentoIdent" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlDocumentoIdent" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCC" runat="server" AssociatedControlID="tbCC">Nr.º do Documento de Identificação</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCC" Text="*" ValidationGroup="Page1" ErrorMessage="O campo Nrº do Documento de Identificação é obrigatório." runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbCC" runat="server" ValidationGroup="Page1" CssClass="form-control" placeholder="e.g., 123456789 Z Z1Z"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDataValidade" runat="server" AssociatedControlID="tbDataValidade">Data de Validade</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvDataValidade" Text="*" ValidationGroup="Page1" ErrorMessage="O campo Data de Validade é obrigatório." runat="server" ControlToValidate="tbDataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                        <asp:TextBox ID="tbDataValidade" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" type="date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-5">
                                <div class="form-group">
                                    <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="tbNrSegSocial">Número de Segurança Social</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvNrSegSocial" Text="*" ValidationGroup="Page1" ErrorMessage="O campo Nr.ª da Segurança Social é obrigatório." runat="server" ControlToValidate="tbNrSegSocial" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbNrSegSocial" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="12345678910"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblNIF" runat="server" AssociatedControlID="tbNIF">NIF</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvNIF" Text="*" runat="server" ValidationGroup="Page1" ErrorMessage="O campo NIF é obrigatório." ControlToValidate="tbNIF" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbNIF" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="123456789"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:Label ID="lblMorada" runat="server" AssociatedControlID="tbMorada">Morada</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvMorada" Text="*" ValidationGroup="Page1" ErrorMessage="O campo Morada é obrigatório." runat="server" ControlToValidate="tbMorada" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbMorada" ValidationGroup="Page1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Introduza a sua morada completa"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblCodCodPostal" runat="server" AssociatedControlID="ddlCodCodPostal">Código-Postal</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvcodCodPostal" Text="*" ErrorMessage="O campo Código-Postal é obrigatório." ValidationGroup="Page1" runat="server" ControlToValidate="ddlCodCodPostal" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodCodPostal" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSCodCodPostal" DataTextField="nova_freguesia" DataValueField="id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSCodCodPostal" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [freguesias]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblfreguesia" runat="server" AssociatedControlID="tbfreguesia">Localidade</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvfreguesia" Text="*" ErrorMessage="O campo Nome Completo é obrigatório." ValidationGroup="Page1" runat="server" ControlToValidate="tbfreguesia" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbfreguesia" runat="server" CssClass="form-control" ValidationGroup="Page1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">País</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodPais" Text="*" ErrorMessage="O campo País é obrigatório." ValidationGroup="Page1" runat="server" ControlToValidate="ddlCodPais" InitialValue="" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodPais" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nomePT" DataValueField="codPais">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [pais]"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="card card-footer">
                            <div class="col-md-9 d-flex align-items-center justify-content-evenly">
                                <asp:Button runat="server" ID="btn_next" ValidationGroup="Page1" OnClientClick="showNextDiv(); return false;" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Seguinte" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_next" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div>
                <div class="alert text-white font-weight-bold" role="alert">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Page1" ForeColor="#cc3a60" DisplayMode="List" />
                </div>
            </div>

            <!-- Begin Registration Page 2-->
            <div id="registerCompletionpage2" class="hidden">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header pb-0 p-3">
                                <h6 class="mb-1">Completa o teu registo</h6>
                                <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Código do Estado Civil</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodEstadoCivil" runat="server" ValidationGroup="Page2" ControlToValidate="ddlCodEstadoCivil" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodEstadoCivil" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSEstadoCivil" DataTextField="estadoCivil" DataValueField="codEstadoCivil">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSEstadoCivil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [estadoCivil]"></asp:SqlDataSource>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="tbIBAN">IBAN</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvIBAN" ValidationGroup="Page2" runat="server" ControlToValidate="tbIBAN" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbIBAN" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="ddlCodNaturalidade">Código da Naturalidade</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodNaturalidade" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodNaturalidade" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodNaturalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSNaturalidade" DataTextField="titulo" DataValueField="id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSNaturalidade" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [freguesias] ORDER BY titulo"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Código da Nacionalidade</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodNacionalidade" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodNacionalidade" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodNacionalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nacionalidade" DataValueField="codPais">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Código da Situação Profissional</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodSituacaoProfissional" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodSituacaoProfissional" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodSituacaoProfissional" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSsituacaoProfissional" DataTextField="situacaoProfissional" DataValueField="codSituacaoProfissional">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSsituacaoProfissional" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [situacaoProfissional]"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Label ID="lblCodPrefixo" runat="server" AssociatedControlID="tbCodPrefixo">Prefixo</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodPrefixo" ValidationGroup="Page2" runat="server" ControlToValidate="tbCodPrefixo" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbCodPrefixo" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblTelemovel" runat="server" AssociatedControlID="tbTelemovel">Telemóvel</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvTelemovel" ValidationGroup="Page2" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbTelemovel" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="tbEmail">Email</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="Page2" runat="server" ControlToValidate="tbEmail" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="tbEmail" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row px-xl-5 px-sm-4 px-3">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCodGrauAcademico" runat="server" AssociatedControlID="ddlCodGrauAcademico">Código do Grau Académico</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvCodGrauAcademico" ValidationGroup="Page2" runat="server" ControlToValidate="ddlCodGrauAcademico" InitialValue="" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCodGrauAcademico" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSGrauAcademico" DataTextField="grauAcademico" DataValueField="codGrauAcademico">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SQLDSGrauAcademico" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [grauAcademico]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblFoto" runat="server" AssociatedControlID="fuFoto">Foto</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvFoto" runat="server" ValidationGroup="Page2" ControlToValidate="fuFoto" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="fuFoto" ValidationGroup="Page2" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblAnexo" runat="server" AssociatedControlID="fuAnexo">Anexo</asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvAnexo" ValidationGroup="Page2" runat="server" ControlToValidate="fuAnexo" ForeColor="#cc3a60" Text="*"></asp:RequiredFieldValidator>
                                    <asp:FileUpload ID="fuAnexo" ValidationGroup="Page2" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="card card-footer">
                            <div class="col-md-9 d-flex align-items-center justify-content-evenly">
                                <asp:Button runat="server" ID="btn_submit" ValidationGroup="Page2" CausesValidation="True" OnClientClick="submitInfo(); return false;" class="btn btn-outline-primary btn-sm mb-0" Text="Submeter"></asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_submit" />
                    </Triggers>
                </asp:UpdatePanel>

                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Page2" ForeColor="#cc3a60" />
            </div>
            <!-- End Registration Page 2-->
        </div>

        <div id="registrationMessage" class="hidden">
            <div class="alert alert-primary text-white font-weight-bold" role="alert">
                <small class="text-uppercase font-weight-bold">Submetido com sucesso</small>
            </div>
        </div>
        <!-- End of Registration Completion -->
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

        <script>
            function submitInfo() {
                // Check if all validators are valid
                var isValid = Page_ClientValidate('Page2');

                // If validators are valid, proceed to show next div
                if (isValid) {
                    //// Remove 'show' class and add 'hide' class to div1
                    //document.getElementById('registerCompletionpage2').classList.remove('card');
                    //document.getElementById('registerCompletionpage2').classList.add('hidden');

                    // Remove 'hide' class and add 'show' class to div2
                    document.getElementById('registrationMessage').classList.remove('hidden');
                }
            }
        </script>
    </div>

</asp:Content>
