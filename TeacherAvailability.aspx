<%@ Page Title="Disponibilidade" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="TeacherAvailability.aspx.cs" Inherits="FinalProject.TeacherAvailability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Manhãs" Visible="True" CausesValidation="False" ID="btnMornings" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Tardes" Visible="True" CausesValidation="False" ID="btnAfternoons" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Sábados" Visible="True" CausesValidation="False" ID="btnSaturdays" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Sábados - Manhãs" Visible="True" CausesValidation="False" ID="btnSaturdaysMorning" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Sábados - Tarde" Visible="True" CausesValidation="False" ID="btnSaturdaysAfternoon" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Save" Visible="True" CausesValidation="False" ID="btnSave" />
    </div>
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>
    <br />
    <div>
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Limpar Manhãs" Visible="True" CausesValidation="False" ID="btnCleanMornings" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Limpar Tardes" Visible="True" CausesValidation="False" ID="btnCleanAfternoons" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Limpar Sábados" Visible="True" CausesValidation="False" ID="btnCleanSaturdays" />
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Limpar Disponibilidades" Visible="True" CausesValidation="False" ID="btnCleanAll" />
    </div>
    <script>
        var CodUtilizador = '<%= Session["CodUtilizador"] %>';
        document.addEventListener('DOMContentLoaded', function () {

            var calendarData = [];
            var holidaysAndSundays = [];
            var miniminumDuration = 60 * 60 * 1000; // Duração miníma em ms
            var currentDate = new Date();
            var currentYear = new Date().getFullYear(); // Get the current year
            var currentDay = currentDate; // Set currentDay to initialDate

            //Função que verifica se o tempo mínimo de intervalo está selecionado (apenas aceita intervalos de hora: 1h, 2h, 3h..)
            function isSlotDurationValid(slot) {
                var slotDuration = slot.end.getTime() - slot.start.getTime();
                return slotDuration >= miniminumDuration && slot.start.getMinutes() === 0 && slot.end.getMinutes() === 0;
            }

            //Array de feriados
            var holidays = [
                {
                    title: 'Ano Novo',
                    start: '2024-01-01T00:00:00',
                    end: '2024-01-01T23:59:00'
                },
                {
                    title: 'Dia da Liberdade',
                    start: '2024-04-25T00:00:00',
                    end: '2024-04-25T23:59:00'
                },
                {
                    title: 'Dia do Trabalhador',
                    start: '2024-05-01T00:00:00',
                    end: '2024-05-01T23:59:00'
                },
                {
                    title: 'Dia de Portugal',
                    start: '2024-06-10T00:00:00',
                    end: '2024-06-10T23:59:00'
                },
                {
                    title: 'Assunção de Nossa Senhora',
                    start: '2024-08-15T23:00:00',
                    end: '2024-08-15T23:59:00'
                },
                {
                    title: 'Implantação da República',
                    start: '2024-10-05T00:00:00',
                    end: '2024-10-05T23:59:00'
                },
                {
                    title: 'Dia de Todos os Santos',
                    start: '2024-11-01T00:00:00',
                    end: '2024-11-01T23:59:00'
                },
                {
                    title: 'Restauração da Independência',
                    start: '2024-12-01T00:00:00',
                    end: '2024-12-01T23:59:00'
                },
                {
                    title: 'Natal',
                    start: '2024-12-25T00:00:00',
                    end: '2024-12-25T23:59:00'
                },
                {
                    title: 'Véspera de Natal',
                    start: '2024-12-24T00:00:00',
                    end: '2024-12-24T23:59:00'
                },
            ];

            // Função para gerar os domingos do ano atual a partir da data atual
            function generateSundays() {
                var sundays = [];
                var currentDate = new Date();
                var currentYear = new Date().getFullYear();

                currentDate.setDate(currentDate.getDate() + (7 - currentDate.getDay()));

                while (currentYear === currentDate.getFullYear()) {
                    sundays.push({
                        title: 'Domingo',
                        start: currentDate.toISOString().slice(0, 10) + 'T00:00:00',
                        end: currentDate.toISOString().slice(0, 10) + 'T23:59:00',
                        color: '#cc3a60',
                        dataType: 'unselectable'
                    });
                    currentDate.setDate(currentDate.getDate() + 7);
                }
                return sundays;
            }

            // Função para adicionar os feriados e domingos ao calendarData array
            function addHolidaysAndSundaysToCalendarData() {
                holidays.forEach(function (holiday) {
                    holidaysAndSundays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#cc3a60',
                        dataType: 'unselectable'
                    });
                });

                var sundays = generateSundays();

                sundays.forEach(function (sunday) {
                    holidaysAndSundays.push({
                        title: sunday.title,
                        start: sunday.start,
                        end: sunday.end,
                        color: '#cc3a60',
                        dataType: 'unselectable'
                    });
                });
            }

            // Function to add events to the selectedSlots array
            function addEventsToCalendarData(eventData) {
                eventData.forEach(function (event) {
                    calendarData.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        cod_turma: event.cod_turma,
                        color: event.color,
                        dataType: 'selectable'
                    });
                    //} else {
                    //    calendarData.push({
                    //        title: event.title,
                    //        start: event.start,
                    //        end: event.end,
                    //        cod_turma: event.cod_turma,
                    //        color: event.color,
                    //        dataType: 'unselectable'
                    //    });
                    //}
                });
            }

            function slotAlreadyExists(slot) {
                // Check if any existing slot overlaps with the provided slot
                return calendarData.some(function (existingSlot) {
                    return (slot.start >= existingSlot.start && slot.start < existingSlot.end) ||
                        (slot.end > existingSlot.start && slot.end <= existingSlot.end);
                });
            }

            $.ajax({
                type: "POST",
                url: "/Scheduler.asmx/GetTeacherAvailabilityFromJson",
                data: JSON.stringify({ CodUtilizador: CodUtilizador }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addHolidaysAndSundaysToCalendarData();
                    addEventsToCalendarData(eventData);

                    // Render calendar after adding events to selectedSlots array
                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    // If AJAX call fails, add holidays and Sundays to selectedSlots array
                    addHolidaysAndSundaysToCalendarData();
                    // Render calendar even if AJAX call fails
                    renderCalendar();
                }
            });

            function renderCalendar() {
                var calendarEl = document.getElementById('calendar');
                var calendar = new FullCalendar.Calendar(calendarEl, {
                    initialView: 'dayGridMonth',
                    headerToolbar: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'dayGridMonth,timeGridWeek,timeGridDay'
                    },
                    buttonText: {
                        today: 'Hoje'
                    },
                    slotLabelFormat: {
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: false
                    },
                    locale: 'pt',
                    firstDay: 1,
                    slotLabelInterval: '00:30',
                    slotMinTime: '08:00:00',
                    slotMaxTime: '23:59:00',
                    allDaySlot: true,
                    selectable: true,
                    timeZone: 'UTC',
                    select: function (slot) {
                        var isAllDay = slot.allDay;

                        if (slot.start < currentDate) {
                            $('#InvalidDate').modal('show');
                            return;
                        }

                        if (isAllDay) {
                            var startDate = new Date(Date.UTC(slot.start.getUTCFullYear(), slot.start.getUTCMonth(), slot.start.getUTCDate(), 8, 0, 0));
                            var endDate = new Date(Date.UTC(slot.start.getUTCFullYear(), slot.start.getUTCMonth(), slot.start.getUTCDate(), 23, 0, 0));

                            slot.start = startDate.toISOString();
                            slot.end = endDate.toISOString();

                            slot.start = new Date(startDate);
                            slot.end = new Date(endDate);
                        }

                        if (!isSlotDurationValid(slot)) {
                            $('#InvalidTimeModal').modal('show');
                            return;
                        }

                        // Check if the selected event conflicts with any existing events
                        if (isEventConflict(slot, calendarData) || isEventConflict(slot, calendarData)) {
                            alert("Selected time conflicts with existing events.");
                            return;
                        }

                        var eventToAdd = {
                            title: 'Disponível',
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            color: '#82d616',
                            dataType: 'selectable'
                        };

                        if (!slotAlreadyExists(slot)) { // Push the event object to selectedSlots array
                            calendarData.push(eventToAdd);
                        }
                        else {
                            // The slot already exists, handle this scenario (e.g., show a message)
                            return;
                            console.log('Slot already exists:', slot);
                        }

                        // Add the event to the calendar
                        calendar.addEvent(eventToAdd);

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentDay, // Set initial date to the first day of the current month
                    validRange: {
                        start: currentYear + '-01-01',
                        end: (currentYear + 1) + '-01-01'
                    },
                    views: {
                        timeGridDay: {
                            buttonText: 'Dia'
                        },
                        dayGridMonth: {
                            buttonText: 'Mês'
                        },
                        timeGridWeek: {
                            buttonText: 'Semana'
                        }
                    }
                });

                // Function to check if there is a conflict between the selected event and existing events
                function isEventConflict(selectedEvent, eventsArray) {
                    for (var i = 0; i < eventsArray.length; i++) {
                        var event = eventsArray[i];
                        var selectedEventStartString = selectedEvent.start.toISOString().slice(0, -5); // Trim milliseconds
                        var selectedEventEndString = selectedEvent.end.toISOString().slice(0, -5); // Trim milliseconds
                        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                            return true; // Conflict found
                        }
                    }
                    return false; // No conflict
                }

                calendar.setOption('eventClick', function (slot) {
                    if (slot.event.extendedProps.dataType === 'selectable') {
                        // Get the start time components of the clicked event
                        var start = slot.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        // Get the end time components of the clicked event
                        var end = slot.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        // Remove the event from the selectedSlots array
                        calendarData = calendarData.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        // Remove the event from the calendar
                        slot.event.remove();
                    } else if (slot.event.extendedProps.dataType === 'unselectable') {
                        // Display custom alert modal
                        $('#UnselectableModal').modal('show');
                        return;
                    }
                });

                holidaysAndSundays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: '#cc3a60',
                        dataType: 'unselectable'
                    });
                });

                calendarData.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: '#82d616',
                        dataType: 'selectable'
                    });
                });

                calendar.render();

                function isHoliday(date, holidays) {
                    var dateString = date.toISOString().slice(0, 10); // Get the date string in YYYY-MM-DD format
                    return holidays.some(function (holiday) {
                        return holiday.start.slice(0, 10) === dateString; // Check if the date matches any holiday
                    });
                }

                function AllMornings() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear(); // Get the current year

                    // Set the time to 8:00 AM
                    currentDate.setHours(8, 0, 0, 0);

                    // Loop through each day of the current year
                    while (currentDate.getFullYear() === currentYear) {
                        // Check if the current day is not Saturday or Sunday
                        if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6 && !isHoliday(currentDate, holidays)) {
                            console.log("Marking:", currentDate);
                            // Add the time slot from 8:00 to 16:00 as "Disponível"
                            calendar.addEvent({
                                title: 'Manhã Disponível',
                                start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                rendering: 'background',
                                color: '#63B3ED'
                            });
                            calendarData.push({
                                title: 'Manhã Disponível',
                                start: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                                end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                color: '#63B3ED'// End time
                            });
                        }
                        // Move to the next day
                        currentDate.setDate(currentDate.getDate() + 1);
                    }
                }

                function AllAfternoons() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear(); // Get the current year

                    // Set the time to 8:00 AM
                    currentDate.setHours(8, 0, 0, 0);

                    // Loop through each day of the current year
                    while (currentDate.getFullYear() === currentYear) {
                        // Check if the current day is not Saturday or Sunday
                        if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6 && !isHoliday(currentDate, holidays)) {
                            console.log("Marking:", currentDate);
                            // Add the time slot from 16:00 to 23:00 as "Disponível"
                            calendar.addEvent({
                                title: 'Tarde Disponível',
                                start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                rendering: 'background',
                                color: '#fbcf33'
                            });
                            calendarData.push({
                                title: 'Tarde Disponível',
                                start: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // Start time
                                end: currentDate.toISOString().slice(0, 10) + 'T23:00:00', // End time
                                color: '#fbcf33'
                            });
                        }
                        // Move to the next day
                        currentDate.setDate(currentDate.getDate() + 1);
                    }
                }

                var allSaturdaysSelected = false;

                function AllSaturdays() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear(); // Get the current year
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday
                    allSaturdaysSelected = true;

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6 && !isHoliday(currentDate, holidays)) {
                        console.log("Marking:", currentDate);
                        calendar.addEvent({
                            title: 'Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                            end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                            rendering: 'background',
                            color: '#82d616'
                        });
                        calendarData.push({
                            title: 'Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                            end: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // End time
                            color: '#82d616'
                        });
                        currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                    }
                }

                function AllSaturdaysMorning() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear(); // Get the current year
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday
                    allSaturdaysSelected = true;

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6 && !isHoliday(currentDate, holidays)) {
                        console.log("Marking:", currentDate);
                        calendar.addEvent({
                            title: 'Manhã de Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                            end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                            color: '#82d616'


                        });
                        calendarData.push({
                            title: 'Manhã de Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                            end: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // End time
                            color: '#82d616'
                        });
                        currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                    }
                }

                function AllSaturdaysAfternoon() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear(); // Get the current year
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday

                    allSaturdaysSelected = true;

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6 && !isHoliday(currentDate, holidays)) {
                        console.log("Marking:", currentDate);
                        calendar.addEvent({
                            title: 'Tarde de Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                            end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                            color: '#82d616'
                        });
                        calendarData.push({
                            title: 'Tarde de Sábado Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // Start time
                            end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                            color: '#82d616'// End time
                        });
                        currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                    }
                }

                function CleanAvailability() {
                    // Remove events from the calendar
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Disponível') && event.extendedProps.dataType !== 'unselectable') {
                            event.remove();
                            event.preventDefault();
                        }
                    });

                    // Remove events from the selectedSlots array
                    calendarData = calendarData.filter(function (slot) {
                        event.preventDefault();
                        return !(slot.title.includes('Disponível') && slot.dataType !== 'unselectable');
                    });
                }

                function CleanAvailabilityMornings() {
                    // Remove events from the calendar
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Manhã Disponível') && event.extendedProps.dataType !== 'unselectable') {
                            event.remove();
                            event.preventDefault();
                        }
                    });

                    // Remove events from the selectedSlots array
                    calendarData = calendarData.filter(function (slot) {
                        event.preventDefault();
                        return !(slot.title.includes('Manhã Disponível') && slot.dataType !== 'unselectable');
                    });
                }

                function CleanAvailabilityAfternoons() {
                    // Remove events from the calendar
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Tarde Disponível') && event.extendedProps.dataType !== 'unselectable') {
                            event.remove();
                            event.preventDefault();
                        }
                    });

                    // Remove events from the selectedSlots array
                    calendarData = calendarData.filter(function (slot) {
                        event.preventDefault();
                        return !(slot.title.includes('Tarde Disponível') && slot.dataType !== 'unselectable');
                    });
                }

                function CleanAvailabilitySaturdays() {
                    // Remove events from the calendar
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Sábado Disponível') && event.extendedProps.dataType !== 'unselectable') {
                            event.remove();
                            event.preventDefault();
                        }
                    });

                    // Remove events from the selectedSlots array
                    calendarData = calendarData.filter(function (slot) {
                        event.preventDefault();
                        return !(slot.title.includes('Sábado Disponível') && slot.dataType !== 'unselectable');
                    });
                }



                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnMornings.ClientID %>').addEventListener('click', function (event) {
                    AllMornings();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnAfternoons.ClientID %>').addEventListener('click', function (event) {
                    AllAfternoons();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnSaturdays.ClientID %>').addEventListener('click', function (event) {
                    AllSaturdays();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnSaturdaysMorning.ClientID %>').addEventListener('click', function (event) {
                    if (allSaturdaysSelected) {
                        alert("You cannot select Saturday mornings if all Saturdays are already selected.");
                        event.preventDefault();
                        return;
                    }
                    AllSaturdaysMorning();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnSaturdaysAfternoon.ClientID %>').addEventListener('click', function (event) {
                    if (allSaturdaysSelected) {
                        alert("You cannot select Saturday mornings if all Saturdays are already selected.");
                        event.preventDefault();
                        return;
                    }
                    AllSaturdaysAfternoon();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnCleanAll.ClientID %>').addEventListener('click', function (event) {
                    CleanAvailability();
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnCleanMornings.ClientID %>').addEventListener('click', function (event) {
                    CleanAvailabilityMornings();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnCleanAfternoons.ClientID %>').addEventListener('click', function (event) {
                    CleanAvailabilityAfternoons();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });
                // Button click event handler to select all Saturdays
                document.getElementById('<%= btnCleanSaturdays.ClientID %>').addEventListener('click', function (event) {
                    CleanAvailabilitySaturdays();
                    event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
                });

                document.getElementById('<%= btnSave.ClientID %>').addEventListener('click', function (event) {
                    // Ensure selectedSlots array is properly structured
                    var formattedSlots = calendarData.map(function (slot) {
                        return {
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            color: slot.color,
                            cod_turma: slot.cod_turma
                        };
                    });

                    // Call the server-side method using AJAX
                    $.ajax({
                        type: "POST",
                        url: "/Scheduler.asmx/SetTimeSlotDataFromClient",
                        data: JSON.stringify({ calendarData: formattedSlots, CodUtilizador: CodUtilizador }), // Pass formattedSlots with all properties
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success response if needed
                            if (response.d) {
                                // Show success message
                                alert("Disponibilidade de Formador atualizada com sucesso! Será redirecionado para outra página.");
                                // Redirect to another page
                                window.location.href = "UserProfile.aspx"; // Change "new_page.aspx" to the desired page
                            } else {
                                // Show error message
                                alert("Ocorreu um erro ao atualizar a disponibilidade do formador.");
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Handle error
                            console.error("Error occurred while sending data to server.");
                        }
                    });
                    event.preventDefault(); // Prevent default button behavior
                });

            }
        });

    </script>

</asp:Content>
