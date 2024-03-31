<%@ Page Title="User Sign In" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserSignIn.aspx.cs" Inherits="FinalProject.UserSignIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main-content mt-0">
        <section>
            <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
            </div>
            <div class="page-header min-vh-50" id="Login">
                <div class="container">
                    <div class="row ">
                        <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                            <div class="card card-plain">
                                <div class="card-header pb-0 text-left bg-transparent">
                                    <h3 class="font-weight-bolder text-info text-gradient">Bem-vindo de volta!</h3>
                                    <p class="mb-0">Introduza o seu e-mail/nome de utilizador e a palavra-passe </p>
                                </div>
                                <div class="card-body">
                                    <div role="form">
                                        <label>Email/Utilizador</label>
                                        <asp:RequiredFieldValidator ID="rfvusername" ErrorMessage="Nome de Utilizador Obrigatório" Text="*" runat="server" ControlToValidate="tb_username" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_username" ValidationGroup="MainForm" oninput="validateUsername(this)" CssClass="form-control" placeholder="Utilizador ou E-mail" runat="server"></asp:TextBox>
                                        </div>
                                        <label>Password</label>
                                        <asp:RequiredFieldValidator ID="rfvpw" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tb_pw" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pw" ValidationGroup="MainForm" oninput="validatePassword(this)" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                        </div>

                                        <p class="mb-4 text-sm mx-auto">
                                            <a class="nav-link" href="javascript:;" id="openModalLink" data-bs-toggle="tab" role="tab" aria-selected="true" onclick="showRecoverPassword(event); return false">Recuperar password</a>
                                        </p>
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" id="rememberMe" checked runat="server" />
                                            <label class="form-check-label" for="rememberMe">Lembrar-me?</label>
                                        </div>

                                        <div class="text-center">
                                            <asp:Button ID="btn_signin" ValidationGroup="MainForm" runat="server" Text="Login" class="btn bg-gradient-info w-100 mt-4 mb-0" OnClick="btn_signin_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="mt-2 position-relative text-center" style="padding: 10px;">
                                    <p class="text-sm font-weight-bold mb-2 text-secondary text-border d-inline z-index-2 px-3">
                                        OU
                                    </p>
                                </div>
                                <div class="card z-index-0">
                                    <p class="mb-0 text-center">Login com</p>
                                    <div class="row px-xl-5 px-sm-4 px-3" style="padding-bottom: 10px;">
                                        <div class="col-3 ms-auto px-1">
                                            <asp:LinkButton runat="server" class="btn btn-outline-light w-100" OnClick="btn_facebook_Click" CausesValidation="False" ID="btn_facebook">
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
                                            <asp:LinkButton runat="server" class="btn btn-outline-light w-100" OnClick="btn_google_Click" CausesValidation="False" ID="btn_google">
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
                                    </div>
                                </div>
                                <div class="card-footer text-center pt-0 px-lg-2 px-1">
                                    <p class="mb-4 text-sm mx-auto">
                                        Não tem acesso?
                                        <asp:LinkButton runat="server" ID="lbtn_signup" href="./UserSignUp.aspx" class="text-info text-gradient font-weight-bold">Registe-se aqui!</asp:LinkButton>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n8">
                                <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Div para recuperar Password -->
        <div id="RecoverPassword" class="page-header hidden">
            <div class="container">
                <div class="row ">
                    <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                        <div class="card card-plain">
                            <div class="card-header pb-0 text-left bg-transparent">
                                <h3 class="font-weight-bolder text-info text-gradient">Recuperação de Palavra-passe</h3>
                                <p class="mb-0">Introduza o seu e-mail para recuperar a sua palavra-passe </p>
                            </div>
                            <div class="card-body">
                                <div role="form">
                                    <label>Email</label>
                                    <asp:RequiredFieldValidator ID="rfvEmailRecover" ErrorMessage="E-mail Obrigatório" Text="*" runat="server" ControlToValidate="tbEmailRecover" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                    <div class="mb-3">
                                        <asp:TextBox ID="tbEmailRecover" ValidationGroup="ModalForm" CssClass="form-control" placeholder="E-mail" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        <div class="text-center">
                                            <asp:Button ID="btn_recover" ValidationGroup="ModalForm" runat="server" Text="Recuperar" class="btn bg-gradient-info w-100 mt-4 mb-0" OnClick="btn_recuperarPW_Click" />
                                            <asp:Button ID="btn_back" ValidationGroup="ModalForm" runat="server" Text="Voltar" class="btn bg-gradient-info w-100 mt-4 mb-0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n8">
                            <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <!--Javascript para validar se username e password estão vazios e mostrar cores -->
    <script>
        function validateUsername(element) {
            if (element.value.trim() === "") {
                element.classList.add("is-invalid");
            } else {
                element.classList.remove("is-invalid");
            }
        }
        function validatePassword(element) {
            if (element.value.trim() === "") {
                element.classList.add("is-invalid");
            } else {
                element.classList.remove("is-invalid");
            }
        }
    </script>
    
    <!--Javascript para mostrar a div de Recover Password -->
    <script>
        function showRecoverPassword(event) {
            event.preventDefault();

            document.getElementById('Login').classList.add('hidden');

            document.getElementById('RecoverPassword').classList.remove('hidden');
        }
    </script>
</asp:Content>
