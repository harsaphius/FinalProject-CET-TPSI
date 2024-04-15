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
            <span>
                <br />
            </span>
            <asp:Button runat="server" ID="btnExportCalendar" CssClass="btn btn-danger" OnClick="btnExportCalendar_OnClick" Text="Export Calendar" />
        </div>
    </div>
    <br />
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>
    <!--Javascript do FullCalendar -->
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
            var currentDate = new Date();
            var currentDayOfWeek = currentDate.getDay();

            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    start: 'today',
                    center: 'title',
                    end: 'timeGridWeek agendaDay prev,next'
                },
                selectable: true,
                editable: true,
                events: [
                    // Include your static events here if needed
                    {
                        title: 'Call with Dave',
                        start: '2024-03-18',
                        end: '2024-03-18',
                        className: 'bg-gradient-danger'
                    },
                    {
                        title: 'Lunch meeting',
                        start: '2024-03-21',
                        end: '2024-03-22',
                        className: 'bg-gradient-warning'
                    },
                    {
                        title: 'All day conference',
                        start: '2024-03-29',
                        end: '2024-03-29',
                        className: 'bg-gradient-success'
                    },

                    {
                        title: 'Meeting with Mary',
                        start: '2024-03-01',
                        end: '2024-03-01',
                        className: 'bg-gradient-info'
                    },

                    {
                        title: 'Winter Hackaton',
                        start: '2024-03-03',
                        end: '2024-03-03',
                        className: 'bg-gradient-danger'
                    },

                    {
                        title: 'Digital event',
                        start: '2024-03-07',
                        end: '2024-03-09',
                        className: 'bg-gradient-warning'
                    },

                    {
                        title: 'Marketing event',
                        start: '2024-04-10',
                        end: '2024-04-10',
                        className: 'bg-gradient-primary'
                    },

                    {
                        title: 'Dinner with Family',
                        start: '2024-04-19',
                        end: '2024-04-19',
                        className: 'bg-gradient-danger'
                    },

                    {
                        title: 'Black Friday',
                        start: '2024-04-23',
                        end: '2024-04-23',
                        className: 'bg-gradient-info'
                    },

                    {
                        title: 'Cyber Week',
                        start: '2024-04-02',
                        end: '2024-04-02',
                        className: 'bg-gradient-warning'
                    },

                ],
                views: {
                    month: {
                        titleFormat: {
                            month: "long",
                            year: "numeric"
                        }
                    },
                    agendaWeek: {
                        titleFormat: {
                            month: "long",
                            year: "numeric",
                            day: "numeric"
                        }
                    },
                    agendaDay: {
                        titleFormat: {
                            month: "short",
                            year: "numeric",
                            day: "numeric"
                        },
                        type: 'timeGrid',
                        duration: { days: 1 },
                        buttonText: 'Day'
                    },
                    timeGridWeek: {
                        type: 'timeGrid',
                        duration: { days: 7 },
                        buttonText: 'Week',
                        firstDay: currentDayOfWeek
                    }
                }
            });

            calendar.render();
        });
    </script>
</asp:Content>
