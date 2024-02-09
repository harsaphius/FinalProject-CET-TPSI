<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="MainPageFE.aspx.cs" Inherits="FinalProject.AppFrontEnd.MainPageFE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Repeater ID="rpt_mainfe" runat="server">
        <HeaderTemplate>
              <div class="container">
                        <div class="row">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-lg-3 col-sm-6 card" style="background-color:lightcoral;">
                      <div><asp:Label ID="lbl_cod" runat="server" Text=""></asp:Label></div>
                      <div><b>Título </b><asp:LinkButton href="#!" ID="lbl_titulo" runat="server" Text=""></asp:LinkButton> </div><br />
                      <div><b>Data de Início: </b><asp:Label ID="lbl_data" runat="server"></asp:Label></div><br />
                      <div><b>Tipo: </b><asp:Label ID="lbl_tipo" runat="server"></asp:Label></div><br />
            </div>
        </ItemTemplate>
        <FooterTemplate>
                        </div>
              </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
