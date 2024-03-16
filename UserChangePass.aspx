<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserChangePass.aspx.cs" Inherits="FinalProject.UserChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main-content mt-0">
        <section>
            <div style="padding: 5px;" id="alert" class="hidden" role="alert">
                <asp:Label runat="server" ID="lbl_message" CssClass="text-white"></asp:Label>
            </div>
            <div class="page-header min-vh-50">
                <div class="container">
                    <div class="row ">
                        <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                            <div class="card card-plain">
                                <div class="card-header pb-0 text-left bg-transparent">
                                    <h3 class="font-weight-bolder text-info text-gradient">Recuperar a password</h3>
                                    <p class="mb-0">Introduza o seu e-mail/nome de utilizador e a sua nova palavra-passe </p>
                                </div>
                                <div class="card-body">
                                    <div role="form">
                                        <label>Email</label>
                                        <asp:RequiredFieldValidator ID="rfvusername" ErrorMessage="E-mail Obrigatório" Text="*" runat="server" ControlToValidate="tbEmail" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tbEmail" oninput="validateUsername(this)" CssClass="form-control" placeholder="Utilizador ou E-mail" runat="server"></asp:TextBox>
                                        </div>
                                        <label>Password Atual</label>
                                        <asp:RequiredFieldValidator ID="rfvpwa" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tb_pwa" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pwa" oninput="validatePassword(this)" CssClass="form-control" placeholder="Password Atual" runat="server"></asp:TextBox>
                                        </div>
                                        <label>Nova Password</label>
                                        <asp:RequiredFieldValidator ID="rfvpw" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tb_pw" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pw" oninput="validatePassword(this)" CssClass="form-control" placeholder="Nova Password" runat="server"></asp:TextBox>
                                        </div>
                                        <label>Repetir a Nova Password</label>
                                        <asp:RequiredFieldValidator ID="rfvpwr" runat="server" ErrorMessage="Palavra-passe Obrigatória" Text="*" ControlToValidate="tb_pwr" ForeColor="#cc3a60"></asp:RequiredFieldValidator>
                                        <div class="mb-3">
                                            <asp:TextBox ID="tb_pwr" oninput="validatePassword(this)" CssClass="form-control" placeholder="Repita a Password" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="text-center">
                                            <asp:Button ID="btn_changepw" runat="server" Text="Alterar Password" class="btn bg-gradient-info w-100 mt-4 mb-0" OnClick="btn_changepw_Click" />
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

        </section>
    </main>
</asp:Content>
