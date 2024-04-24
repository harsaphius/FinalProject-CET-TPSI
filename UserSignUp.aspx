<%@ Page Title="User Sign Up" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserSignUp.aspx.cs" Inherits="FinalProject.UserSignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="min-vh-100 mb-8">
        <asp:Label runat="server" ID="lbl_message" style="display:flex; align-content:center; padding: 5px;" CssClass="text-white" class="hidden" role="alert"></asp:Label>

        <div class="page-header align-items-start min-vh-50 pt-5 pb-11 m-3 border-radius-lg" style="background-image: url('../assets/img/curved-images/curved14.jpg');">
            <span class="mask bg-gradient-dark opacity-6"></span>
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-5 text-center mx-auto">
                        <h1 class="text-white mb-2 mt-5">Bem-vindo!</h1>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row mt-lg-n10 mt-md-n11 mt-n10">
                <div class="col-xl-8 col-lg-10 col-md-12 mx-auto">
                    <div class="card z-index-0">
                        <div class="registerBoard">
                            <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                                <asp:Label runat="server" ID="lblUserFromGoogle" CssClass="text-white"></asp:Label>
                            </div>
                      <%--      <div class="card-header text-center pt-4">
                                <h5>Registar com</h5>
                            </div>
                            <div class="row px-xl-5 px-sm-4 px-3 ">
                                <div class="col-3 ms-auto px-1">
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-light w-100" href="javascript:;">
                                    <svg width="24px" height="32px" viewBox="0 0 64 64" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink32">
                                        <g id="Artboard" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <g id="facebook-3" transform="translate(3.000000, 3.000000)" fill-rule="nonzero">
                                                <circle id="Oval" fill="#3C5A9A" cx="29.5091719" cy="29.4927506" r="29.4882047"></circle>
                                                <path d="M39.0974944,9.05587273 L32.5651312,9.05587273 C28.6886088,9.05587273 24.3768224,10.6862851 24.3768224,16.3054653 C24.395747,18.2634019 24.3768224,20.1385313 24.3768224,22.2488655 L19.8922122,22.2488655 L19.8922122,29.3852113 L24.5156022,29.3852113 L24.5156022,49.9295284 L33.0113092,49.9295284 L33.0113092,29.2496356 L38.6187742,29.2496356 L39.1261316,22.2288395 L32.8649196,22.2288395 C32.8649196,22.2288395 32.8789377,19.1056932 32.8649196,18.1987181 C32.8649196,15.9781412 35.1755132,16.1053059 35.3144932,16.1053059 C36.4140178,16.1053059 38.5518876,16.1085101 39.1006986,16.1053059 L39.1006986,9.05587273 L39.0974944,9.05587273 L39.0974944,9.05587273 Z" id="Path" fill="#FFFFFF"></path>
                                            </g>
                                        </g>
                                    </svg>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-3 me-auto px-1">
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-light w-100" href="javascript:;">
                                    <svg width="24px" height="32px" viewBox="0 0 64 64" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                                        <g id="Artboard" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                            <g id="google-icon" transform="translate(3.000000, 2.000000)" fill-rule="nonzero">
                                                <path d="M57.8123233,30.1515267 C57.8123233,27.7263183 57.6155321,25.9565533 57.1896408,24.1212666 L29.4960833,24.1212666 L29.4960833,35.0674653 L45.7515771,35.0674653 C45.4239683,37.7877475 43.6542033,41.8844383 39.7213169,44.6372555 L39.6661883,45.0037254 L48.4223791,51.7870338 L49.0290201,51.8475849 C54.6004021,46.7020943 57.8123233,39.1313952 57.8123233,30.1515267" id="Path" fill="#4285F4"></path>
                                                <path d="M29.4960833,58.9921667 C37.4599129,58.9921667 44.1456164,56.3701671 49.0290201,51.8475849 L39.7213169,44.6372555 C37.2305867,46.3742596 33.887622,47.5868638 29.4960833,47.5868638 C21.6960582,47.5868638 15.0758763,42.4415991 12.7159637,35.3297782 L12.3700541,35.3591501 L3.26524241,42.4054492 L3.14617358,42.736447 C7.9965904,52.3717589 17.959737,58.9921667 29.4960833,58.9921667" id="Path" fill="#34A853"></path>
                                                <path d="M12.7159637,35.3297782 C12.0932812,33.4944915 11.7329116,31.5279353 11.7329116,29.4960833 C11.7329116,27.4640054 12.0932812,25.4976752 12.6832029,23.6623884 L12.6667095,23.2715173 L3.44779955,16.1120237 L3.14617358,16.2554937 C1.14708246,20.2539019 0,24.7439491 0,29.4960833 C0,34.2482175 1.14708246,38.7380388 3.14617358,42.736447 L12.7159637,35.3297782" id="Path" fill="#FBBC05"></path>
                                                <path d="M29.4960833,11.4050769 C35.0347044,11.4050769 38.7707997,13.7975244 40.9011602,15.7968415 L49.2255853,7.66898166 C44.1130815,2.91684746 37.4599129,0 29.4960833,0 C17.959737,0 7.9965904,6.62018183 3.14617358,16.2554937 L12.6832029,23.6623884 C15.0758763,16.5505675 21.6960582,11.4050769 29.4960833,11.4050769" id="Path" fill="#EB4335"></path>
                                            </g>
                                        </g>
                                    </svg>
                                    </asp:LinkButton>
                                </div>
                                <div class="mt-2 position-relative text-center">
                                    <p class="text-sm font-weight-bold mb-2 text-secondary text-border d-inline z-index-2 bg-white px-3">
                                        OU
                                    </p>
                                </div>
                            </div>
                        </div>--%>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Perfil</label>
                                    <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" Text="*" ErrorMessage="Perfil Necessário" ControlToValidate="ddlPerfil" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:DropDownList class="dropdown-toggle btn bg-gradient-secundary" ID="ddlPerfil" runat="server" DataSourceID="SQLDSPerfil" DataTextField="perfil" DataValueField="codPerfil"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SQLDSPerfil" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [perfil] ORDER BY [codPerfil] OFFSET 1 ROWS"></asp:SqlDataSource>
                                    </div>
                                    <label>Nome Completo</label>
                                    <asp:RequiredFieldValidator ID="rfvname" Text="*" ErrorMessage="Nome Obrigatório" runat="server" ControlToValidate="tb_name" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tb_name" runat="server" oninput="validateUsername(this)" class="form-control" placeholder="Nome Completo"></asp:TextBox>
                                    </div>
                                    <label>Nome de Utilizador</label>
                                    <asp:RequiredFieldValidator ID="rfvusername" Text="*" ErrorMessage="Utilizador Obrigatório" runat="server" ControlToValidate="tb_username" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tb_username" runat="server" class="form-control" placeholder="Utilizador"></asp:TextBox>
                                    </div>
                                    <label>E-mail</label>
                                    <asp:RequiredFieldValidator ID="rfvemail" Text="*" ErrorMessage="E-mail Obrigatório" runat="server" ControlToValidate="tb_email" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tb_email" runat="server" class="form-control" placeholder="E-mail"></asp:TextBox>
                                    </div>
                                    <label>Palavra-passe</label>
                                    <asp:RequiredFieldValidator ID="rfvpw" Text="*" ErrorMessage="Palavra-passe obrigatória" runat="server" ControlToValidate="tb_pw" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox TextMode="Password" ID="tb_pw" runat="server" class="form-control" placeholder="Palavra-passe"></asp:TextBox>
                                    </div>
                                    <label>Repetição da Palavra-passe</label>
                                    <asp:RequiredFieldValidator ID="rfvpwr" runat="server" Text="*" ErrorMessage="Palavra-passe obrigatória" ControlToValidate="tb_pwr" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox TextMode="Password" ID="tb_pwR" runat="server" class="form-control" placeholder="Repetir a Palavra-passe"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="mb-3">
                                        <label>Tipo de Documento de Identificação</label>
                                        <div class="mb-0">
                                            <asp:DropDownList ID="ddl_tipoDocIdent" CssClass="form-control" runat="server" DataSourceID="SQLDSDocIdent" DataTextField="tipoDocumentoIdent" DataValueField="codTipoDoc"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLDSDocIdent" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [tipoDocIdent]"></asp:SqlDataSource>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <label>Nr.º do Documento de Identificação</label>
                                            <asp:RequiredFieldValidator ID="rfvCC" Text="*" ErrorMessage="Nr.º do Documento de Identificação Obrigatório" runat="server" ControlToValidate="tbCC" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                            <div>
                                                <asp:TextBox ID="tbCC" runat="server" CssClass="form-control" placeholder="Nr.º do Documento de Identificação"></asp:TextBox>
                                            </div>
                                            <label>Data de Validade</label>
                                            <asp:RequiredFieldValidator ID="rfvdataValidade" Text="*" ErrorMessage="Data de Validade obrigatória" runat="server" ControlToValidate="tbdataValidade" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tbdataValidade" runat="server" CssClass="form-control datepicker" placeholder="Data de Validade" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="mb-3">
                                        <label>Prefixo</label>
                                        <div class="mb-0 text-center">
                                            <asp:DropDownList ID="ddlprefixo" CssClass="form-control" runat="server" DataSourceID="SQLDSPais" DataTextField="prefixo" DataValueField="codPais"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SQLDSPais" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT codPais,CONCAT(nomePT, ': ' , prefixo) AS prefixo FROM [pais] order by nomePT"></asp:SqlDataSource>
                                        </div>
                                        <br />
                                        <label>Nr.º de Telemóvel</label>
                                        <asp:RequiredFieldValidator ID="rfvTelemovel" Text="*" ErrorMessage="Telemóvel Obrigatório" runat="server" ControlToValidate="tbTelemovel" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="tbTelemovel" runat="server" class="form-control" placeholder="Phone Number"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center">
                                    <div class="form-check form-check-info text-left">
                                        <input runat="server" class="form-check-input" type="checkbox" value="" id="flexCheckDefault" checked />
                                        <label class="form-check-label" for="flexCheckDefault">
                                            Concordo com <a href="javascript:;" class="text-dark font-weight-bolder">Termos e Condições</a>
                                        </label>
                                    </div>
                                    <div style="padding: 5px;" id="alert" role="alert">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                    </div>
                                    <asp:Button ID="btn_signup" OnClick="btn_signup_Click" runat="server" CssClass="btn bg-gradient-dark w-100 my-4 mb-2" Text="Sign Up" />
                                    <p class="text-sm mt-3 mb-0">Já tem uma conta? <a href="./UserSignIn.aspx" class="text-dark font-weight-bolder">Login</a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--Função de Javascript para mostrar o calendário do flatpickr nas TextBoxes -->
    <script>
        flatpickr('#<%= tbdataValidade.ClientID %>', {
            // Options
            dateFormat: 'd-m-Y',
            theme: 'light',
            minDate: new Date()
        });
    </script>
</asp:Content>