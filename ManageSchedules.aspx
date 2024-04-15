<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageSchedules.aspx.cs" Inherits="FinalProject.ManageSchedules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <label class="col-form-label">Turma:</label>
        <asp:DropDownList ID="ddlClassGroup" OnSelectedIndexChanged="ddlClassGroup_OnSelectedIndexChanged" runat="server" AutoPostBack="True" DataSourceID="SQLDSClassGroup" DataTextField="nomeTurma" DataValueField="codTurma"></asp:DropDownList>
        <asp:SqlDataSource ID="SQLDSClassGroup" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT * FROM [turma]"></asp:SqlDataSource>
        <label class="col-form-label">Módulos:</label>
        <asp:DropDownList ID="ddlModulesForClassGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlModulesForClassGroup_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
        <asp:SqlDataSource ID="SQLDSModules" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
        <label class="col-form-label">Formadores:</label>
        <asp:DropDownList ID="ddlTeachersForModulesOfClassGroup" AutoPostBack="true" runat="server"></asp:DropDownList>
        <asp:SqlDataSource ID="SQLDSTeachers" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>"></asp:SqlDataSource>
        <label class="col-form-label">Salas:</label>
        <asp:DropDownList ID="ddlClassRoom" AutoPostBack="true" runat="server" DataSourceID="SQLDSClassRoom" DataTextField="nrSala" DataValueField="codSala"></asp:DropDownList>
        <asp:SqlDataSource ID="SQLDSClassRoom" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT S.codSala, CONCAT(nrSala,' | ', localSala) AS nrSala, S.isActive FROM sala AS S INNER JOIN localSala AS LS ON S.codLocalSala=LS.codLocalSala"></asp:SqlDataSource>
        <asp:HiddenField ID="hdfHorario" runat="server" />
        <label class="col-form-label">Cor do Evento</label>
        <input type="color" id="colorPicker" value="000000">
    </div>
    <div class="col-md-4">
        <label id="lbl_mensagem" style="margin-top: 20px;"></label>
    </div>
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Guardar" Visible="True" CausesValidation="False" ID="btnSave" />

    <script>
        var CodUtilizador = '<%= Session["CodUtilizador"] %>';
        var CodTurma = '<%= Session["CodTurma"] %>';

        document.addEventListener('DOMContentLoaded', function () {
            var calendarData = []; // Array to store selected time slots
            var Sundays_Holidays = []; // Array to store holidays and sundays
            var Disponibilidade_Formador = []; // Array to store availability of trainer
            var Disponibilidade_Sala = []; // Array to store availability of classrooms
            var MIN_SLOT_DURATION = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year
            var currentDate = new Date();

            // Extract the year, month, and day
            var currentYear = currentDate.getFullYear();
            var currentMonth = (currentDate.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 because getMonth() returns a zero-based index
            var currentDay = currentDate.getDate().toString().padStart(2, '0');

            // Construct the initialDate string in the format 'YYYY-MM-DD'
            var initialDate = currentYear + '-' + currentMonth + '-' + currentDay;

            // Get the selected values from dropdown lists
            var selectedTurma = $('#<%= ddlClassGroup.ClientID %> option:selected').val();
            var selectedModulo = $('#<%= ddlModulesForClassGroup.ClientID %> option:selected').text();
            var selectedModuloValue = $('#<%= ddlModulesForClassGroup.ClientID %> option:selected').val();
            var selectedTeacher = $('#<%= ddlTeachersForModulesOfClassGroup.ClientID %> option:selected').text();
            var selectedTeacherValue = $('#<%= ddlTeachersForModulesOfClassGroup.ClientID %> option:selected').val();
            var selectedSala = $('#<%= ddlClassRoom.ClientID %> option:selected').text();
            var selectedSalaValue = $('#<%= ddlClassRoom.ClientID %> option:selected').val();
            var nrTurma = $('#<%= ddlClassGroup.ClientID %> option:selected').text();


            // Function to check if the selected slot duration meets the minimum requirement and starts and ends on the hour
            function isSlotDurationValid(info) {
                var slotDuration = info.end.getTime() - info.start.getTime();
                return slotDuration >= MIN_SLOT_DURATION && info.start.getMinutes() === 0 && info.end.getMinutes() === 0;
            }

            // Define the number of years to include (current year + next two years)
            var numberOfYears = 5;


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
                        start: year + '-01-01T00:00:00',
                        end: year + '-01-01T23:59:00'
                    },
                    {
                        title: 'Dia da Liberdade',
                        start: year + '-04-25T00:00:00',
                        end: year + '-04-25T23:59:00'
                    },
                    {
                        title: 'Dia do Trabalhador',
                        start: year + '-05-01T00:00:00',
                        end: year + '-05-01T23:59:00'
                    },
                    {
                        title: 'Dia de Portugal',
                        start: year + '-06-10T00:00:00',
                        end: year + '-06-10T23:59:00'
                    },
                    {
                        title: 'Assunção de Nossa Senhora',
                        start: year + '-08-15T00:00:00',
                        end: year + '-08-15T23:59:00'
                    },
                    {
                        title: 'Implantação da República',
                        start: year + '-10-05T00:00:00',
                        end: year + '-10-05T23:59:00'
                    },
                    {
                        title: 'Dia de Todos os Santos',
                        start: year + '-11-01T00:00:00',
                        end: year + '-11-01T23:59:00'
                    },
                    {
                        title: 'Restauração da Independência',
                        start: year + '-12-01T00:00:00',
                        end: year + '-12-01T23:59:00'
                    },
                    {
                        title: 'Natal',
                        start: year + '-12-25T00:00:00',
                        end: year + '-12-25T23:59:00'
                    },
                    {
                        title: 'Véspera de Natal',
                        start: year + '-12-24T00:00:00',
                        end: year + '-12-24T23:59:00'
                    }
                );
            }

            function formatDateToISOString(dateString) {
                var date = new Date(dateString);
                var utcString = date.toUTCString();
                var isoString = new Date(utcString).toISOString();
                return isoString.slice(0, 10) + 'T' + isoString.slice(11, 19);
            }

            // Function to add holidays and Sundays to the calendarData array
            function addHolidaysAndSundaysToSelectedSlots() {
                holidays.forEach(function (holiday) {
                    Sundays_Holidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#ff0000',
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Function to add events to the calendarData array
            function addAvailabilityToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    Disponibilidade_Formador.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: event.color,
                        cod_turma: event.cod_turma,
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Function to add events to the calendarData array
            function addAvailabilityClassroomsToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    Disponibilidade_Sala.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: event.color,
                        cod_turma: event.cod_turma,
                        dataType: 'non_unselectable'
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
                        cod_turma: event.cod_turma,
                        dataType: 'selectable'
                    });
                });
            }

            //Disponibilidade do formador
            $.ajax({
                type: "POST",
                url: "/Scheduler.asmx/GetTeacherAvailabilityFromJson",
                data: JSON.stringify({ CodUtilizador: $('#<%= ddlTeachersForModulesOfClassGroup.ClientID %>').val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d);

                    addAvailabilityToSelectedSlots(eventData);

                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");

                    addHolidaysAndSundaysToSelectedSlots();

                    renderCalendar();
                }
            });

            //Disponibilidade das Salas
            $.ajax({
                type: "POST",
                url: "/Scheduler.asmx/GetScheduleOfClassRoomFromJson",
                data: JSON.stringify({ CodSala: $('#<%= ddlClassRoom.ClientID %>').val() }), // Retrieve the value of hf_cod_sala
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addAvailabilityClassroomsToSelectedSlots(eventData);

                    // Render calendar after adding events to calendarData array
                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    // If AJAX call fails, add holidays and Sundays to calendarData array
                    addHolidaysAndSundaysToSelectedSlots();

                    // Render calendar after adding events to calendarData array
                    renderCalendar();
                }
            });

            //Horário da Turma
            $.ajax({
                type: "POST",
                url: "/Scheduler.asmx/GetScheduleOfClassGroupFromJson",
                data: JSON.stringify({ CodTurma: $('#<%= ddlClassGroup.ClientID %>').val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d);

                    addEventsToSelectedSlots(eventData);
                    addHolidaysAndSundaysToSelectedSlots();

                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    addHolidaysAndSundaysToSelectedSlots();
                    renderCalendar();
                }
            });

            function renderCalendar() {
                var calendarEl = document.getElementById('calendar');
                var calendar = new FullCalendar.Calendar(calendarEl, {
                    locale: 'pt',
                    hiddenDays: [0],
                    initialView: 'timeGridWeek',
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
                    select: function (info) {
                        var isAllDay = info.allDay;
                        var currentDate = new Date();


                        if (info.start < currentDate) {

                            return;
                        }

                        // Determine start and end times based on regime
                        var horario = $('#<%= hdfHorario.ClientID %>').val();
                        var selectedStartTime = horario === 'Laboral' ? '08:00:00' : '16:00:00';
                        var selectedEndTime = horario === 'Pós-Laboral' ? '16:00:00' : '23:00:00';

                        if (isAllDay) {
                            // If it's an all-day event, set the start and end times accordingly
                            var selectedDateStart = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), selectedStartTime.split(':')[0], selectedStartTime.split(':')[1], 0));
                            var selectedDateEnd = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), selectedEndTime.split(':')[0], selectedEndTime.split(':')[1], 0));

                            info.start = selectedDateStart.toISOString();
                            info.end = selectedDateEnd.toISOString();

                            info.start = new Date(selectedDateStart);
                            info.end = new Date(selectedDateEnd);
                        }

                        if (!isSlotDurationValid(info)) {

                            return;
                        }

                        // Check if the selected event conflicts with any existing events
                        if (!isEventConflict(info, Disponibilidade_Formador) || isEventConflict(info, Sundays_Holidays) || isEventConflict(info, Disponibilidade_Sala)) {



                            return;
                        }

                        // Get the selected color from the color picker
                        var selectedColor = $('#colorPicker').val();

                        // Create the event title by concatenating the selected values
                        var eventTitle = nrTurma + " | " + selectedModulo + " | " + selectedTeacher + " | " + selectedSala;

                        var eventToAdd = {
                            title: eventTitle,
                            start: formatDateToISOString(info.start),
                            end: formatDateToISOString(info.end),
                            rendering: 'background',
                            cod_modulo: selectedModuloValue,
                            cod_formador: selectedTeacherValue,
                            cod_sala: selectedSalaValue,
                            color: selectedColor,
                            cod_turma: selectedTurma,
                            dataType: 'unselectable'
                        };

                        // Push the event object to calendarData array
                        calendarData.push(eventToAdd);

                        // Add the event to the calendar
                        calendar.addEvent(eventToAdd);

                        // Ensure calendarData array is properly structured
                        var formattedSlots = calendarData.map(function (slot) {
                            return {
                                title: slot.title,
                                start: slot.start,
                                end: slot.end,
                                cod_modulo: slot.cod_modulo,
                                cod_formador: slot.cod_formador,
                                cod_sala: slot.cod_sala,
                                color: slot.color
                            };
                        });

                        // Call the server-side method using AJAX
                        $.ajax({
                            type: "POST",
                            url: "/Scheduler.asmx/SetScheduleForClassGroup",
                            data: JSON.stringify({ calendarData: formattedSlots, CodTurma: selectedTurma, CodUtilizador: selectedTeacherValue, CodSala: selectedSalaValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Aula inserida com sucesso!");
                                    $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Ocorreu um erro ao guardar a aula inserida.");
                                    $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        });

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentDate,
                    validRange: {
                        start: currentYear + '-01-01',
                        end: (currentYear + 2) + '-01-01'
                    }
                });

                //// Function to check if there is a conflict between the selected event and existing events
                //function isEventConflict(selectedEvent, eventsArray) {
                //    for (var i = 0; i < eventsArray.length; i++) {
                //        var event = eventsArray[i];
                //        var selectedEventStartString = selectedEvent.start.toISOString().slice(0, -5); // Trim milliseconds
                //        var selectedEventEndString = selectedEvent.end.toISOString().slice(0, -5); // Trim milliseconds
                //        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                //            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                //            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                //            return true; // Conflict found
                //        }
                //    }
                //    return false; // No conflict
                //}

                // Function to update slotMinTime and slotMaxTime based on hf_regime value
                function updateSlotTimes(regime) {
                    if (regime === 'Laboral') {
                        calendar.setOption('slotMinTime', '08:00:00');
                        calendar.setOption('slotMaxTime', '16:00:00');
                    } else if (regime === 'Pós-Laboral') {
                        calendar.setOption('slotMinTime', '16:00:00');
                        calendar.setOption('slotMaxTime', '23:00:00');
                    }
                }

                // Initial call to update slot times based on hf_regime value
                updateSlotTimes($('#<%= hdfHorario.ClientID %>').val());

                // Event listener to update slot times when hf_regime value changes
                $('#<%= hdfHorario.ClientID %>').change(function () {
                    updateSlotTimes($(this).val());
                });

                calendar.setOption('eventClick', function (info) {
                    // Check if the clicked event is unselectable
                    if (info.event.extendedProps.dataType === 'selectable') {
                        // Get the start time components of the clicked event
                        var start = info.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        // Get the end time components of the clicked event
                        var end = info.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        // Remove the event from the calendarData array
                        calendarData = calendarData.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        // Remove the event from the calendar
                        info.event.remove();


                        // Ensure calendarData array is properly structured
                        var formattedSlots = calendarData.map(function (slot) {
                            return {
                                title: slot.title,
                                start: slot.start,
                                end: slot.end,
                                cod_modulo: slot.cod_modulo,
                                cod_formador: slot.cod_formador,
                                cod_sala: slot.cod_sala,
                                color: slot.color
                            };
                        });

                        // Call the server-side method using AJAX
                        $.ajax({
                            type: "POST",
                            url: "/Scheduler.asmx/SetScheduleForClassGroup",
                            data: JSON.stringify({ calendarData: formattedSlots, CodTurma: selectedTurma, CodUtilizador: selectedTeacherValue, CodSala: selectedSalaValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Aula removida e horário guardado com sucesso.");
                                    $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Erro ao guardar o horário na base de dados.");
                                    $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        });

                    } else if (info.event.extendedProps.dataType === 'unselectable') {
                        // Display custom alert modal
                        $('#UnselectableModal').modal('show');
                        return;
                    } else {
                        // Show error message in lbl_mensagem
                        $('#lbl_mensagem').text("Não pode remover este evento.");
                        $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                        $('#lbl_mensagem').fadeIn();

                        // Fade out the error message after 3 seconds
                        setTimeout(function () {
                            $('#lbl_mensagem').fadeOut(function () {
                                $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                            });
                        }, 3000);

                        return;
                    }
                });

                calendarData.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'unselectable'
                    });
                });

                Disponibilidade_Formador.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatDateToISOString(event.start) == slot.start && formatDateToISOString(event.end) == slot.end;
                    });

                    if (!existingEvent) {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            color: slot.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });

                Disponibilidade_Sala.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatDateToISOString(event.start) == slot.start && formatDateToISOString(event.end) == slot.end;
                    });

                    if (!existingEvent) {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            color: slot.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });

                Sundays_Holidays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'non_unselectable'
                    });
                });

                calendar.render();

                document.getElementById('<%= btnSave.ClientID %>').addEventListener('click', function (event) {
                    // Ensure calendarData array is properly structured
                    var formattedSlots = calendarData.map(function (slot) {
                        return {
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            cod_modulo: slot.cod_modulo,
                            cod_formador: slot.cod_formador,
                            cod_sala: slot.cod_sala,
                            color: slot.color
                        };
                    });

                    // Call the server-side method using AJAX
                    $.ajax({
                        type: "POST",
                        url: "/Scheduler.asmx/SetScheduleForClassGroup",
                        data: JSON.stringify({ calendarData: formattedSlots, CodTurma: selectedTurma, CodUtilizador: selectedTeacherValue, CodSala: selectedSalaValue }), // Pass formattedSlots with all properties
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success response if needed
                            if (response.d) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Horário guardado com sucesso!");
                                $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            } else {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Erro ao guardar o horário da turma.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }
                    });
                    event.preventDefault(); // Prevent default button behavior
                });

            }
        });

    </script>
</asp:Content>
