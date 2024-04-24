<%@ Page Title="Calendário" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="UserCalendar.aspx.cs" Inherits="FinalProject.UserCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>
   <script>
       var CodUtilizador = '<%= Session["CodUtilizador"] %>';
       var CodTurma = '<%= Session["CodTurma"] %>';

       // Function to initialize the calendar
       function initializeCalendar() {
           var calendarData = []; // Array to store selected time slots
           var Sundays_Holidays = []; // Array to store holidays and Sundays
           var Disponibilidade_Formador = []; // Array to store availability of trainers
           var Disponibilidade_Sala = []; // Array to store availability of classrooms
           var currentYear = new Date().getFullYear(); // Get the current year
           var currentDate = new Date();

           var numberOfYears = 1;

           // Initialize an empty array to store holidays
           var holidays = [];

           // Loop through each year
           for (var i = 0; i < numberOfYears; i++) {
               // Calculate the year based on the current year and the loop index
               var year = currentYear + i;

               // Add events for national holidays for each year
               holidays.push(
                   {
                       title: 'Ano Novo',
                       start: year + '-01-01T08:00:00',
                       end: year + '-01-01T23:00:00',
                   },
                   {
                       title: 'Dia da Liberdade',
                       start: year + '-04-25T08:00:00',
                       end: year + '-04-25T23:00:00',
                   },
                   {
                       title: 'Dia do Trabalhador',
                       start: year + '-05-01T08:00:00',
                       end: year + '-05-01T23:00:00',
                   },
                   {
                       title: 'Dia de Portugal',
                       start: year + '-06-10T08:00:00',
                       end: year + '-06-10T23:00:00',
                   },
                   {
                       title: 'Assunção de Nossa Senhora',
                       start: year + '-08-15T08:00:00',
                       end: year + '-08-15T23:00:00',
                   },
                   {
                       title: 'Implantação da República',
                       start: year + '-10-05T08:00:00',
                       end: year + '-10-05T23:00:00',
                   },
                   {
                       title: 'Dia de Todos os Santos',
                       start: year + '-11-01T08:00:00',
                       end: year + '-11-01T23:00:00',
                   },
                   {
                       title: 'Restauração da Independência',
                       start: year + '-12-01T08:00:00',
                       end: year + '-12-01T23:00:00',
                   },
                   {
                       title: 'Natal',
                       start: year + '-12-25T08:00:00',
                       end: year + '-12-25T23:00:00',
                   },
                   {
                       title: 'Véspera de Natal',
                       start: year + '-12-24T08:00:00',
                       end: year + '-12-24T23:00:00',
                   }
               );
           }


           // AJAX call to fetch availability of trainers
           $.ajax({
               type: "POST",
               url: "/Scheduler.asmx/GetTeacherAvailabilityFromJson",
               data: JSON.stringify({ CodUtilizador: CodUtilizador }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var eventData = JSON.parse(response.d);
                addAvailabilityOfTrainersToSelectedSlots(eventData);
                renderCalendar();
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error fetching teacher availability:", errorThrown);
                addHolidaysAndSundaysToSelectedSlots();
                renderCalendar();
            }
        });

        // AJAX call to fetch class group schedule
        $.ajax({
            type: "POST",
            url: "/Scheduler.asmx/GetScheduleOfClassGroupFromJson",
            data: JSON.stringify({ CodTurma: CodTurma }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var eventData = JSON.parse(response.d);
                addEventsToSelectedSlots(eventData);
                addHolidaysAndSundaysToSelectedSlots();
                renderCalendar();
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error fetching class group schedule:", errorThrown);
                addHolidaysAndSundaysToSelectedSlots();
                renderCalendar();
            }
        });

           // Function to render the calendar
           function renderCalendar() {
               var calendarEl = document.getElementById('calendar');
               var calendar = new FullCalendar.Calendar(calendarEl, {
                   locale: 'pt',
                   hiddenDays: [0],
                   initialView: 'dayGridMonth',
                   headerToolbar: {
                       left: 'prev,next today',
                       center: 'title',
                       right: 'dayGridMonth,timeGridWeek,timeGridDay'
                   },
                   buttonText: {
                       today: 'Hoje',
                       timeGridDay: 'Dia',
                       dayGridMonth: 'Mês',
                       timeGridWeek: 'Semana'
                   },
                   slotLabelFormat: {
                       hour: '2-digit',
                       minute: '2-digit',
                       hour12: false
                   },
                   firstDay: 1,
                   slotMinTime: '08:00:00',
                   slotMaxTime: '23:00:00',
                   allDaySlot: true,
                   selectable: true,
                   timeZone: 'UTC',
                   contentHeight: 'auto',
                   aspectRatio: 1.5,
                   initialDate: currentDate,
                   validRange: {
                       start: currentYear + '-01-01',
                       end: (currentYear + 2) + '-01-01'
                   },

                   events: calendarData.concat(Disponibilidade_Formador)
               });

               calendar.render();
           }

           // Function to add holidays and Sundays to the calendarData array
           function addHolidaysAndSundaysToSelectedSlots() {
               holidays.forEach(function (holiday) {
                   Sundays_Holidays.push({
                       title: holiday.title,
                       start: holiday.start,
                       end: holiday.end,
                       color: '#ff0000',
                       rendering: 'background'
                   });
               });
           }

           // Function to add availability of trainers to the Disponibilidade_Formador array
           function addAvailabilityOfTrainersToSelectedSlots(eventData) {
               eventData.forEach(function (event) {
                   Disponibilidade_Formador.push({
                       title: event.title,
                       start: event.start,
                       end: event.end,
                       color: event.color,
                       rendering: 'background'
                   });
               });
           }

           // Function to add availability of classrooms to the Disponibilidade_Sala array
           function addAvailabilityOfClassroomsToSelectedSlots(eventData) {
               eventData.forEach(function (event) {
                   console.log(event)
                   Disponibilidade_Sala.push({
                       title: event.title,
                       start: event.start,
                       end: event.end,
                       color: event.color,
                       rendering: 'background'
                   });
               });
           }

           // Function to add events to the calendarData array
           function addEventsToSelectedSlots(eventData) {
               eventData.forEach(function (event) {
                   calendarData.push({
                       title: event.title,
                       start: event.start,
                       end: event.end,
                       cod_modulo: event.cod_modulo,
                       cod_formador: event.cod_formador,
                       cod_sala: event.cod_sala,
                       color: event.color,
                       cod_turma: event.cod_turma
                   });
               });
           }
       }

       // Initialize calendar on page load
       $(document).ready(function () {
           initializeCalendar();
       });
   </script>

</asp:Content>
