<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="LoginFE.aspx.cs" Inherits="FinalProject.AppFrontEnd.LoginFE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section class="vh-100">
  <div class="container-fluid h-custom">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-md-9 col-lg-6 col-xl-5 justify-content-center">
        <img src="..\Images\cinel.png" class="img-fluid" alt="Cinel Logo">
      </div>

      <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
        <div>
          <div class="d-flex flex-row align-items-center justify-content-center justify-content-lg-start">
            <p class="lead fw-normal mb-0 me-3">Entrar com</p>&nbsp;
            <!--Facebook Button -->
            <asp:linkButton class="btn btn-danger btn-floating mx-1" runat="server" ID="btn_facebook" OnClick="btn_facebook_Click">
              <i class="fa fa-facebook-f"></i>
            </asp:linkButton>
            <!--Google Button -->
            <asp:linkButton class="btn btn-danger btn-floating mx-1" runat="server" ID="btn_google" OnClick="btn_google_Click">
              <i class="fa fa-google"></i>
            </asp:linkButton>
          </div>

          <div class="divider d-flex align-items-center my-4">
            <p class="text-center fw-bold mx-3 mb-0">OU</p>
          </div>

          <!-- Email input -->
          <div class="form-outline mb-4"> 
              <label class="form-label" for="tb_email">Utilizador ou E-mail</label>
              <asp:TextBox ID="tb_email" runat="server" class="form-control form-control-lg" placeholder="Introduza um e-mail ou utilizador válido"></asp:TextBox>

             
          </div>

          <!-- Password input -->
          <div class="form-outline mb-3">
              <label class="form-label" for="tb_pw">Password</label>
              <asp:TextBox ID="tb_pw" runat="server" class="form-control form-control-lg" placeholder="Introduza a password" ></asp:TextBox>
              
          </div>

          <div class="d-flex justify-content-between align-items-center">
            <!-- Checkbox -->
            <div class="form-check mb-0">
              <input class="form-check-input me-2" type="checkbox" value="" id="chkBoxRemember" runat="server" />
              <label class="form-check-label" for="chkBoxRemember">Lembrar-me</label>
            </div>
            <a href="#" data-target="#pwdModal" data-toggle="modal">Recuperar password</a>
          </div>
            
          <div class="text-center text-lg-start mt-4 pt-2">
            <asp:Button class="btn btn-danger btn-lg" ID="btnLoginBE" OnClick="btnLoginBE_Click" runat="server" Text="Login" style="padding-left: 2.5rem; padding-right: 2.5rem;"/>
            <p class="small fw-bold mt-2 pt-1 mb-0">Ainda não estás registado(a)? <asp:LinkButton class="link-danger" href="RegisterAccountFE.aspx" runat="server">Regista-te aqui!</asp:LinkButton>
            </p>
          </div>

<!--Modal-->
<div id="pwdModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
  <div class="modal-dialog">
  <div class="modal-content">
      <div class="modal-header">
           <h4 class="text-center">Esqueceste-te da tua password?</h4>
      </div>
      <div class="modal-body">
          <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="text-center">
                          
                          <p>Digita o teu e-mail para recuperar a palavra-passe.</p>
                            <div class="panel-body">
                                <fieldset>
                                    <div class="form-group">
                                        <asp:TextBox ID="tb_emailpwrecover" runat="server" class="form-control input-lg" placeholder="E-mail Address"></asp:TextBox>  
                                    </div>

                                    <asp:Button ID="btnRecoverPasswordFE" runat="server" class="btn btn-lg btn-danger btn-block" Text="Recuperar Password" OnClick="btnRecoverPasswordFE_Click" />
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      </div>
      <div class="modal-footer">
          <div class="col-md-12">
          <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
		  </div>	
      </div>
  </div>
  </div>
</div>
<!--Modal -->
        </div>
      </div>
    </div>
  </div>
</section>
</asp:Content>
