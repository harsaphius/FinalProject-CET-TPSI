<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="RegisterAccountFE.aspx.cs" Inherits="FinalProject.AppFrontEnd.RegisterAccountFE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="vh-100" style="background-color: #eee;">
  <div class="container h-100">
    <div class="row justify-content-center align-items-center h-100">
      <div class="col-lg-12 col-xl-11">
        <div class="card text-black" style="border-radius: 25px;">
          <div class="card-body p-md-5">
            <div class="row justify-content-center">
              <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
                <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Registar</p>
                <div class="mx-1 mx-md-4">

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fa fa-user fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <asp:TextBox ID="tb_username" runat="server" class="form-control" placeholder="Utilizador"></asp:TextBox>
                      <label class="form-label" for="tb_username">Introduza o utilizador</label>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fa fa-envelope fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <asp:TextBox ID="tb_email" runat="server" class="form-control" TextMode="Email" placeholder="E-mail"></asp:TextBox>
                      <label class="form-label" for="tb_email">Introduza o e-mail</label>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fa fa-lock fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <asp:TextBox ID="tb_password" runat="server" class="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                      <label class="form-label" for="tb_password">Introduza a password</label>
                    </div>
                  </div>

                  <div class="d-flex flex-row align-items-center mb-4">
                    <i class="fa fa-key fa-lg me-3 fa-fw"></i>
                    <div class="form-outline flex-fill mb-0">
                      <asp:TextBox ID="tb_passwordn" runat="server" class="form-control" TextMode="Password" placeholder="Repita a Password"></asp:TextBox>
                      <label class="form-label" for="tb_passwordn">Repita a Password</label>
                    </div>
                  </div>

                
                  <div class="form-check justify-content-center mb-5">
                    <input class="form-check-input me-2" type="checkbox" value="" id="form2Example3c" />
                    <label class="form-check-label" for="form2Example3">
                      I agree all statements in <a href="#!">Terms of service</a>
                    </label>
                  </div>


                  <div class="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                      <asp:Button ID="btn_registar" runat="server" Text="Registar" class="btn btn-danger btn-lg" OnClick="btn_registar_Click" />
                  </div>
                </div>

                </div>
              <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
                <img src="..\Images\cinel.png" class="img-fluid" alt="Cinel Logo">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>

</asp:Content>
