<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserCalendar.aspx.cs" Inherits="FinalProject.UserCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="card calendar">
    <div class="card-body p-3">
      <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
    </div>
  </div>
</asp:Content>
