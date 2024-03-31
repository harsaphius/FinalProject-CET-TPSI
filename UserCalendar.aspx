<%@ Page Title="Calendário" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserCalendar.aspx.cs" Inherits="FinalProject.UserCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
            <span>Data de Início: </span>
            <div class="input-group mb-4">
                <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                <asp:TextBox runat="server" ID="tbDataInicioFilters" class="form-control datepicker" TextMode="Date"></asp:TextBox>
            </div>
        </div>

        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
            <span>Data de Fim: </span>
            <div class="input-group mb-4">
                <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                <asp:TextBox runat="server" ID="tbDataFimFilters" class="form-control datepicker" placeholder="Please select date" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <div class="col-xl-4 col-sm-6 mb-xl-0 mb-4">
            <span><br/></span>
            <asp:Button runat="server" ID="btnExportCalendar" CssClass="btn btn-danger" OnClick="btnExportCalendar_OnClick" Text="Export Calendar" />
        </div>
    </div>  
    <br/>
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>

</asp:Content>
