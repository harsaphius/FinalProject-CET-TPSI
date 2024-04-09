<%@ Page Title="Perfil" Language="C#"  EnableEventValidation="false" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="FinalProject.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <!-- Header Content -->
            <div class="container-fluid">
                <div class="page-header min-height-300 border-radius-xl mt-4" style="background-image: url('../assets/img/curved-images/curved0.jpg'); background-position-y: 50%;">
                    <span class="mask bg-gradient-primary opacity-6"></span>
                </div>
                <div class="card card-body blur shadow-blur mx-4 mt-n6 overflow-hidden">
                    <div class="row gx-4">
                        <div class="col-auto">
                            <div class="avatar avatar-xl position-relative">
                                <asp:FileUpload runat="server" ID="fuProfileImage" CssClass="hidden" />
                                <a id="UploadLink" type="file">
                                    <asp:Image ID="foto" runat="server" alt="profile_image" class="w-100 border-radius-lg shadow-sm"></asp:Image>
                                    <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="hidden" />
                                </a>
                            </div>
                        </div>
                        <div class="col-auto my-auto">
                            <div class="h-100">
                                <h5 class="mb-1">
                                    <a href="/UserProfile.aspx">
                                        <asp:Label runat="server" ID="profilename"></asp:Label>
                                    </a>
                                </h5>
                                <p class="mb-0 font-weight-bold text-sm">
                                    <asp:Label runat="server" ID="profileemail"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="col-lg-5 col-md-6 my-sm-auto ms-sm-auto me-sm-0 mx-auto mt-3">
                            <div class="nav-wrapper position-relative end-0">
                                <ul class="nav nav-pills nav-fill p-1 bg-transparent">
                                    <li class="nav-item">
                                        <asp:LinkButton class="btn btn-link pe-3 ps-0 mb-0 ms-auto" ID="lbtAvaliacoes" Visible="False" CausesValidation="False" runat="server">
                                    <i class="fas fa-grade"></i>
                                    <span class="ms-1">Avaliações</span>
                                        </asp:LinkButton>
                                    </li>
                                    <li class="nav-item">
                                        <asp:LinkButton class="btn btn-link pe-3 ps-0 mb-0 ms-auto" ID="lbtDisponibilidade" Visible="False" CausesValidation="False" runat="server">
                                            <i class="fas fa-time"></i>
                                            <span class="ms-1">Disponibilidades</span>
                                        </asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="alterarPW">
                                        <asp:LinkButton ID="lbtChangePW" class="btn btn-link pe-3 ps-0 mb-0 ms-auto" Visible="True" runat="server" CausesValidation="False" OnClick="lbtChangePW_OnClick">
                                           <i class="fas fa-lock"></i>
                                            <span class="ms-1">Alterar PW</span>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Profile Div -->
            <div id="profileSinapse" runat="server" visible="True" class="container-fluid py-4">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="card h-100">
                            <div class="card-header pb-0 p-3">
                                <div class="row">
                                    <div class="col-md-8 d-flex align-items-center">
                                        <h6 class="mb-0">Informações de Perfil</h6>
                                    </div>
                                    <div class="col-md-4 text-end">
                                        <asp:LinkButton runat="server" ID="editProfile" Visible="True" OnClick="editProfile_OnClick">
                                            <i class="fas fa-user-edit text-secondary text-sm" title="Edit Profile"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body p-3">

                                <hr class="horizontal gray-light my-2">
                                <ul class="list-group">

                                    <li class="list-group-item border-0 ps-0 pt-0 text-sm"><strong class="text-dark">Nome Completo:</strong> &nbsp;
                                <asp:Label runat="server" ID="infoname"></asp:Label></li>
                                    <li class="list-group-item border-0 ps-0 text-sm"><strong class="text-dark">Telemóvel:</strong> &nbsp;
                                <asp:Label runat="server" ID="infocell"></asp:Label></li>
                                    <li class="list-group-item border-0 ps-0 text-sm"><strong class="text-dark">E-mail:</strong> &nbsp;
                                <asp:Label runat="server" ID="infoemail"></asp:Label></li>

                                    <li class="list-group-item border-0 ps-0 pb-0">
                                        <p class="text-sm">
                                            <strong class="text-dark">Life Motto:</strong> &nbsp;<i>
                                                <asp:Label runat="server" ID="lbLifeMotto"></asp:Label></i>
                                        </p>
                                    </li>
                                    <li class="list-group-item border-0 ps-0 pb-0">
                                        <strong class="text-dark text-sm">Social:</strong> &nbsp;
                                <a class="btn btn-facebook btn-simple mb-0 ps-1 pe-2 py-0">
                                    <i class="fab fa-facebook fa-lg"></i>
                                </a>
                                        <a class="btn btn-twitter btn-simple mb-0 ps-1 pe-2 py-0">
                                            <i class="fab fa-twitter fa-lg"></i>
                                        </a>
                                        <a class="btn btn-instagram btn-simple mb-0 ps-1 pe-2 py-0">
                                            <i class="fab fa-instagram fa-lg"></i>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="card h-100">
                            <div class="card-header pb-0 p-3">
                                <h6 class="mb-0">Documentos Submetidos</h6>
                            </div>
                            <div class="card-body p-3">
                                <asp:Label runat="server" ID="lblSubmittedFiles"></asp:Label>
                                <asp:Repeater ID="fileRepeater" runat="server" OnItemCommand="fileRepeater_OnItemCommand" OnItemDataBound="fileRepeater_OnItemDataBound">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="downloadLink" runat="server"
                                            Text='<%# Eval("FileName") %>'
                                            CommandName="Download"
                                            CommandArgument='<%# Eval("FileID") %>'
                                            CausesValidation="false"
                                            AutoPostBack="true" />
                                        <br />
                                    </ItemTemplate>
                                    <FooterTemplate></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>


                    <!-- Fim de Profile Div -->



                </div>
            </div>
            <!-- Registration Completion -->
            <div id="registration" class="container-fluid mt-4 py-4" visible="False" runat="server">
                <!-- Begin Registration Page 1-->
                <div id="registerCompletionpage1" runat="server" visible="False">
                    <div class="card">
                        <div class="card-header pb-0 p-3">
                            <h6 class="mb-1">Completa o teu registo</h6>
                            <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="updatePanelRegistrationPage1" runat="server">
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
                                                        <asp:DropDownList ID="ddlSexo" runat="server" ValidationGroup="Page1" CssClass="form-control">
                                                            <asp:ListItem Value="0">Feminino</asp:ListItem>
                                                            <asp:ListItem Value="1">Masculino</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDataNascimento" runat="server" AssociatedControlID="tbDataNascimento">Data de Nascimento</asp:Label>
                                                        <div class="input-group">
                                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                                            <asp:TextBox ID="tbDataNascimento" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
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
                                                            <asp:TextBox ID="tbDataValidade" ValidationGroup="Page1" runat="server" CssClass="form-control datepicker" TextMode="DateTime"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-7">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblNrSegSocial" runat="server" AssociatedControlID="tbNrSegSocial">Número de Segurança Social</asp:Label>
                                                        <asp:TextBox ID="tbNrSegSocial" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="12345678910"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblNIF" runat="server" AssociatedControlID="tbNIF">NIF</asp:Label>
                                                        <asp:TextBox ID="tbNIF" ValidationGroup="Page1" runat="server" CssClass="form-control" placeholder="123456789"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblMorada" runat="server" AssociatedControlID="tbMorada">Morada</asp:Label>
                                                        <asp:TextBox ID="tbMorada" ValidationGroup="Page1" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Introduza a sua morada completa"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodPostal" runat="server" AssociatedControlID="tbCodPostal">Código-Postal</asp:Label>
                                                        <asp:TextBox runat="server" ID="tbCodPostal" CssClass="form-control" placeholder="0000-000"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblfreguesia" runat="server" AssociatedControlID="tbLocalidade">Localidade</asp:Label>
                                                        <asp:TextBox runat="server" ID="tbLocalidade" CssClass="form-control" ValidationGroup="Page1"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodPais" runat="server" AssociatedControlID="ddlCodPais">País</asp:Label>
                                                        <asp:DropDownList ID="ddlCodPais" ValidationGroup="Page1" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nomePT" DataValueField="codPais">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [pais] ORDER BY nomePT"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card card-footer">
                                                <div class="col-md-12 align-items-start">
                                                    <asp:Button runat="server" ID="btnBackMainPage" ValidationGroup="Page1" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" OnClick="btnBackMainPage_OnClick" />
                                                    &nbsp;
                                                    <asp:Button runat="server" ID="btnNextPage" ValidationGroup="Page1" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Seguinte" OnClick="btnNextPage_OnClick" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnBackMainPage" />
                                            <asp:AsyncPostBackTrigger ControlID="btnNextPage" />
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
                <div id="registerCompletionpage2" runat="server" visible="False">
                    <div class="card">
                        <div class="card-header pb-0 p-3">
                            <h6 class="mb-1">Completa o teu registo</h6>
                            <p class="text-sm">Preenche todos os campos da tabela abaixo</p>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="updatePanelRegistrationPage2" runat="server">
                                        <ContentTemplate>

                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodEstadoCivil" runat="server" AssociatedControlID="ddlCodEstadoCivil">Estado Civil</asp:Label>
                                                        <asp:DropDownList ID="ddlCodEstadoCivil" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSEstadoCivil" DataTextField="estadoCivil" DataValueField="codEstadoCivil">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SQLDSEstadoCivil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [estadoCivil]"></asp:SqlDataSource>
                                                    </div>
                                                </div>

                                                <div class="col-md-9">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblIBAN" runat="server" AssociatedControlID="tbIBAN">IBAN</asp:Label>
                                                        <asp:TextBox ID="tbIBAN" runat="server" ValidationGroup="Page2" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodNaturalidade" runat="server" AssociatedControlID="tbNaturalidade">Naturalidade</asp:Label>
                                                        <asp:TextBox runat="server" ID="tbNaturalidade" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodNacionalidade" runat="server" AssociatedControlID="ddlCodNacionalidade">Nacionalidade</asp:Label>
                                                        <asp:DropDownList ID="ddlCodNacionalidade" ValidationGroup="Page2" runat="server" CssClass="form-control" DataSourceID="SQLDSPais" DataTextField="nacionalidade" DataValueField="codPais">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCodSituacaoProfissional" runat="server" AssociatedControlID="ddlCodSituacaoProfissional">Situação Profissional</asp:Label>
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
                                                        <asp:FileUpload ID="fuAnexo" ValidationGroup="Page2" runat="server" CssClass="form-control" AllowMultiple="True" />
                                                        <small>CV, Documento de Identificação</small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row px-xl-5 px-sm-4 px-3">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblLifeMotto" runat="server" AssociatedControlID="tbCodPostal">Life Motto</asp:Label>
                                                        <asp:TextBox runat="server" ID="tbLifeMotto" CssClass="form-control" placeholder="Vini, Vidi, Vici"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card card-footer">
                                                <div class="col-md-12 align-items-center">
                                                    <asp:Button runat="server" ID="btnBackToPage1" ValidationGroup="Page2" CausesValidation="False" class="btn btn-outline-primary btn-sm mb-0" Text="Voltar" OnClick="btnBackToPage1_OnClick" />
                                                    &nbsp;

                                            <asp:Button runat="server" ID="btnSubmit" ValidationGroup="Page2" CausesValidation="True" class="btn btn-outline-primary btn-sm mb-0" Text="Submeter" OnClick="btnSubmit_OnClick"></asp:Button>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
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
            </div>
            <!-- End of Registration Completion -->
            <div id="ChangePw" runat="server" visible="False">
                <div class="row ">
                    <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                        <div class="card card-plain">
                            <div class="card-header pb-0 text-left bg-transparent">
                                <h3 class="font-weight-bolder text-primary text-gradient">Alterar a password</h3>
                                <p class="mb-0">Introduza a sua palavra-passe atual e a sua nova palavra-passe para alterar</p>
                            </div>
                            <div class="card-body">
                                <div role="form">
                                    <label>Password Atual</label>
                                    <asp:RequiredFieldValidator ID="rfvpwa" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwOld" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tbPwOld" CssClass="form-control" TextMode="Password" placeholder="Password Atual" runat="server"></asp:TextBox>
                                    </div>
                                    <label>Nova Password</label>
                                    <asp:RequiredFieldValidator ID="rfvpw" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwNew" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tbPwNew" CssClass="form-control" TextMode="Password" placeholder="Nova Password" runat="server"></asp:TextBox>
                                    </div>
                                    <label>Repetir a Nova Password</label>
                                    <asp:RequiredFieldValidator ID="rfvpwr" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tbPwNewRep" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tbPwNewRep" CssClass="form-control" TextMode="Password" placeholder="Repita a Password" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="text-center col-md-12">
                                        <asp:Button ID="btnChangePW" runat="server" Text="Alterar Password" class="btn bg-gradient-primary w-100 mt-4 mb-0" OnClick="btnChangePW_Click" />
                                        <asp:Button ID="btnBackFromPwChange" runat="server" Text="Voltar" CausesValidation="false" OnClick="btnBackFromPwChange_OnClick" class="btn bg-gradient-primary w-100 mt-4 mb-0" />
                                    </div>
                                    <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                        <asp:Label runat="server" ID="Label1" CssClass="text-white"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <asp:Label runat="server" ID="lblMessage" Style="display: flex; align-content: center; padding: 5px;" Visible="False" role="alert"></asp:Label>
            <asp:Timer ID="timerMessage" runat="server" Interval="3000" OnTick="timerMessage_OnTick" Enabled="False"></asp:Timer>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"/>
            <asp:AsyncPostBackTrigger ControlID="btnChangePW" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
