<%@ Page Title="" Language="C#" MasterPageFile="~/CinelMP.Master" AutoEventWireup="true" CodeBehind="ManageSchedules.aspx.cs" Inherits="FinalProject.ManageSchedules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="row" style="margin-top: 15px">
        <div class="col-md-6 col-md-6 text-start" style="padding-left: 35px;">
            <asp:Button runat="server" CssClass="btn btn-primary" CausesValidation="False" Text="Voltar" ID="btnBack" OnClick="btnBack_Click"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <label class="col-form-label">Turma:</label>
            <asp:Label runat="server" ID="NrTurma" CssClass="form-control"></asp:Label>
            </div>
        <div class="col-md-3">
            <label class="col-form-label">Módulos:</label>
            <asp:DropDownList ID="ddlModulesForClassGroup" CssClass="form-control" AutoPostBack="true" runat="server" DataSourceID="SQLDSModules" DataTextField="nomeModulos" DataValueField="codModulos" OnSelectedIndexChanged="ddlModulesForClassGroup_OnSelectedIndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SQLDSModules" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="select distinct nomeModulos,codModulos from turma as t left join moduloFormadorTurma as MFT on t.codTurma=MFT.codTurma left join modulo as M on MFT.codModulo=M.codModulos WHERE t.codTurma=@CodTurma">
                <SelectParameters>
                      <asp:SessionParameter Name="CodTurma" SessionField="CodTurma" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div class="col-md-2">
            <label class="col-form-label">Formador:</label>
            <asp:Label runat="server" ID="lblFormador" CssClass="form-control"></asp:Label>
            <asp:HiddenField ID="hdfTeacherNome" runat="server"/>
            <asp:HiddenField ID="hdfTeacher" runat="server"/>
        </div>
        <div class="col-md-2">
            <label class="col-form-label">Salas:</label>
            <asp:DropDownList ID="ddlClassRoom" CssClass="form-control" AutoPostBack="true" runat="server" DataSourceID="SQLDSClassRoom" DataTextField="nrSala" DataValueField="codSala"></asp:DropDownList>
            <asp:SqlDataSource ID="SQLDSClassRoom" runat="server" ConnectionString="<%$ ConnectionStrings:projetofinalConnectionString %>" SelectCommand="SELECT S.codSala, CONCAT(nrSala,' | ', localSala) AS nrSala, S.isActive FROM sala AS S INNER JOIN localSala AS LS ON S.codLocalSala=LS.codLocalSala"></asp:SqlDataSource>
        </div>
        <div class="col-md-2">
             <label class="col-form-label">Horas do Módulo:</label>
             <label id="lblHoursModule" class="form-control"></label>
        </div>
        <div class="col-md-2">
             <label class="col-form-label">Data de Início:</label>
             <asp:Label runat="server" ID="lblDate" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-md-2">
            <label class="col-form-label">Cor do Evento:</label>
            <input class="form-control" type="color" id="colorPicker" value="000000">
        </div>
        <asp:HiddenField ID="hdfHorario" runat="server" />
        <asp:HiddenField ID="hdfDuration" runat="server" />
        <asp:HiddenField ID="hdfInitialDate" runat="server" />
    </div>
    <div class="col-md-4">
        <label id="lbl_mensagem" style="margin-top: 20px;"></label>
    </div>
    <div class="card calendar">
        <div class="card-body p-3">
            <div class="calendar" data-bs-toggle="calendar" id="calendar"></div>
        </div>
    </div>
    <br />
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Guardar" Visible="True" CausesValidation="False" ID="btnSave" />
    <asp:Button runat="server" CssClass="btn btn-primary" Text="Gerar" Visible="True" CausesValidation="False" ID="btnGenerate" />
    <asp:HiddenField ID="hdnJsonSchedule" runat="server" />
    <script>
        var CodUtilizador = '<%= Session["CodUtilizador"] %>';
        var CodTurma = '<%= Session["CodTurma"] %>';

        document.addEventListener('DOMContentLoaded', function () {
            var calendarData = []; // Array to store selected time slots
            var SundaysHolidays = []; // Array to store holidays and sundays
            var teacherAvailability = []; // Array to store availability of trainer
            var classroomAvailability = []; // Array to store availability of classrooms
            var minimumSlotDuration = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year
            var currentDate = new Date();
            var currentYear = currentDate.getFullYear();

            // Get the selected values from dropdown lists
            var selectedClassgroup = $('#<%= NrTurma.ClientID %> ').text();
            var selectedModule = $('#<%= ddlModulesForClassGroup.ClientID %> option:selected').text();
            var selectedModuleValue = $('#<%= ddlModulesForClassGroup.ClientID %> option:selected').val();
            var selectedTeacher = $('#<%= hdfTeacherNome.ClientID %> ').text();
            var selectedTeacherValue = $('#<%= hdfTeacher.ClientID %>').val();
            var selectedClassroom = $('#<%= ddlClassRoom.ClientID %> option:selected').text();
            var selectedClassroomValue = $('#<%= ddlClassRoom.ClientID %> option:selected').val();
            var nrTurma = $('#<%= NrTurma.ClientID %> ').text();
            var duracao = $('#<%= hdfDuration.ClientID %>').val();
            var dataInicio = $('#<%= hdfInitialDate.ClientID %>').val();
            console.log(dataInicio)

            // Function to check if the selected slot duration meets the minimum requirement and starts and ends on the hour
            function isSlotDurationValid(slot) {
                var slotDuration = slot.end.getTime() - slot.start.getTime();
                return slotDuration >= minimumSlotDuration && slot.start.getMinutes() === 0 && slot.end.getMinutes() === 0;
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

            function CalculateModuleHours(moduloId) {
                let totalHours = 0;
                calendarData.forEach(function (event) {
                    if (event.cod_modulo == moduloId) {
                        totalHours += Math.abs(new Date(event.end) - new Date(event.start)) / 36e5;
                    }
                });
                return totalHours;
                console.log(totalHours)
            }

            function UpdateHoursModule() {
                var totalHours = CalculateModuleHours(selectedModuleValue);
                $('#lblHoursModule').text(`${totalHours} horas de ${duracao} horas`);
                if (totalHours == duracao) {
                    $('#lblHoursModule').addClass('form-control border-danger');
                }
                else {
                    $('#lblHoursModule').removeClass('form-control border-danger');
                    $('#lblHoursModule').addClass('form-control');
                }
            }

            $(document).ready(function () {
                UpdateHoursModule();
            });

            //Function to format date
            function formatDateToISOString(dateString) {
                var date = new Date(dateString);
                var utcString = date.toUTCString();
                var isoString = new Date(utcString).toISOString();
                return isoString.slice(0, 10) + 'T' + isoString.slice(11, 19);
            }

            // Function to add holidays and Sundays to the calendarData array
            function addHolidaysAndSundaysToSelectedSlots() {
                holidays.forEach(function (holiday) {
                    SundaysHolidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#ff0000',
                        dataType: 'unselectable'
                    });
                });
            }

            // Function to add events to the calendarData array
            function addAvailabilityToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    teacherAvailability.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: event.color,
                        dataType: 'unselectable'
                    });
                });
            }

            // Function to add events to the calendarData array
            function addAvailabilityClassroomsToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    classroomAvailability.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: event.color,
                        dataType: 'unselectable'
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

                UpdateHoursModule();
            }



            //Disponibilidade do formador
            $.ajax({
                type: "POST",
                url: "/Scheduler.asmx/GetTeacherAvailabilityFromJson",
                data: JSON.stringify({ CodUtilizador: $('#<%= hdfTeacher.ClientID %>').val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d);

                    eventData.forEach(function (event) {
                        event.source = 'teacherAvailability';
                    });

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

                    eventData.forEach(function (event) {
                        event.source = 'classroomAvailability';
                    });

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
                data: JSON.stringify({ CodTurma: CodTurma }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d);

                    // Add a property to each event indicating its source
                    eventData.forEach(function (event) {
                        event.source = 'classGroupSchedule';
                    });

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

            //Gerador Automático
            document.getElementById('<%= btnGenerate.ClientID %>').addEventListener('click', function (event) {
                $.ajax({
                    type: "POST",
                    url: "/Scheduler.asmx/GetScheduleAutomaticJSON",
                    data: JSON.stringify({ CodTurma: CodTurma }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            var eventData = JSON.parse(response.d); // Extração do array baseado no ficheiro JSON

                            addEventsToSelectedSlots(eventData);
                            renderCalendar();

                            $('#messageModal .modal-body').text("Horário guardado com sucesso!");
                            $('#messageModal .modal-body').removeClass('alert-danger').addClass('alert-success');
                            $('#messageModal').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#messageModal').modal('hide');
                            }, 3000);
                        } else {
                            // Show error message in modal

                            $('#messageModal .modal-body').text("Ocorreu um erro ao guardar o horário inserida.");
                            $('#messageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#messageModal').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#messageModal').modal('hide');
                            }, 3000);

                            return;
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $('#lbl_mensagem').text("Erro ao extrair os dados da base de dados.");
                        $('#lbl_mensagem').addClass('alert alert-danger');
                        $('#lbl_mensagem').fadeIn();

                        setTimeout(function () {
                            $('#lbl_mensagem').fadeOut(function () {
                                $(this).removeClass('alert alert-danger');
                            });
                        }, 3000);

                        return;
                    }
                });
                event.preventDefault();
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
                    initialDate: dataInicio,
                    slotMinTime: '08:00:00',
                    slotMaxTime: '23:00:00',
                    allDaySlot: true,
                    selectable: true,
                    timeZone: 'UTC',
                    select: function (slot) {
                        var isAllDay = slot.allDay;
                        var currentDate = new Date();

                        if (slot.start < currentDate) {
                            console.log(slot.start)
                            console.log(currentDate)
                            $('#InvalidDate').modal('show');
                            return;
                        }

                        var selectedEventData = new Date(slot.start);
                        var selectedEventDay = selectedEventData.getDate();
                        var selectedEventMonth = selectedEventData.getMonth() + 1; // Os meses em JavaScript são base 0, então adicionamos 1 para obter o mês correto
                        var selectedEventYear = selectedEventData.getFullYear();

                        var dataInicioParts = dataInicio.split('/');
                        var dataInicioDay = parseInt(dataInicioParts[0], 10);
                        var dataInicioMonth = parseInt(dataInicioParts[1], 10);
                        var dataInicioYear = parseInt(dataInicioParts[2], 10);

                        if (selectedEventYear < dataInicioYear ||
                            (selectedEventYear === dataInicioYear && selectedEventMonth < dataInicioMonth) ||
                            (selectedEventYear === dataInicioYear && selectedEventMonth === dataInicioMonth && selectedEventDay < dataInicioDay)) {
                            $('#messageModal .modal-body').text("Só pode adicionar aulas a partir da data de início da turma!");
                            $('#messageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#messageModal').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#messageModal').modal('hide');
                            }, 3000);

                            return;
                        }

                        // Determine start and end times based on regime
                        var horario = $('#<%= hdfHorario.ClientID %>').val();
                        var selectedStartTime = horario === 'Laboral' ? '08:00:00' : '16:00:00';
                        var selectedEndTime = horario === 'Pós-Laboral' ? '16:00:00' : '23:00:00';

                        if (isAllDay) {
                            // If it's an all-day event, set the start and end times accordingly
                            var selectedDateStart = new Date(Date.UTC(slot.start.getUTCFullYear(), slot.start.getUTCMonth(), slot.start.getUTCDate(), selectedStartTime.split(':')[0], selectedStartTime.split(':')[1], 0));
                            var selectedDateEnd = new Date(Date.UTC(slot.start.getUTCFullYear(), slot.start.getUTCMonth(), slot.start.getUTCDate(), selectedEndTime.split(':')[0], selectedEndTime.split(':')[1], 0));

                            slot.start = selectedDateStart.toISOString();
                            slot.end = selectedDateEnd.toISOString();

                            slot.start = new Date(selectedDateStart);
                            slot.end = new Date(selectedDateEnd);
                        }

                        if (!isSlotDurationValid(slot)) {
                            $('#InvalidTimeModal .modal-body').text("Os eventos devem ser iniciados à hora certa. Poderá adicionar eventos com múltiplos de 1h.");
                            $('#InvalidTimeModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#InvalidTimeModal').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#InvalidTimeModal').modal('hide');
                            }, 3000);

                            return;
                        }

                        // Check if the selected event conflicts with any existing events
                        if (isEventConflict(slot, teacherAvailability) || isEventConflict(slot, SundaysHolidays) || isEventConflict(slot, classroomAvailability)) {
                            $('#InvalidDate .modal-body').text("O evento está em conflito com outros eventos!");
                            $('#InvalidDate .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#InvalidDate').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#InvalidDate').modal('hide');
                            }, 3000);

                            return;
                        }

                        // Calcular o total de horas do módulo seleccionado
                        var totalHours = CalculateModuleHours(selectedModuleValue);
                        console.log(totalHours)
                        console.log(duracao)

                        // Duração do evento seleccionado
                        var EventDuration  = Math.abs(new Date(slot.end) - new Date(slot.start)) / 36e5;

                        // Check se o total de horas inseridas nas aulas mais a duração do evento seleccionado se ultrapassa o máximo de horas do módulo
                        if (totalHours + EventDuration > duracao) {
                            // Mostrar mensagem de erro
                            $('#InvalidDate .modal-body').text("Está a exceder o número de horas que faltam deste módulo!");
                            $('#InvalidDate .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#InvalidDate').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#InvalidDate').modal('hide');
                            }, 3000);

                            return;
                        }

                        // Get the selected color from the color picker
                        var selectedColor = $('#colorPicker').val();

                        // Create the event title by concatenating the selected values
                        var eventTitle = nrTurma + " | " + selectedModule + " | " + selectedTeacher + " | " + selectedClassroom;
                        console.log(eventTitle)

                        var eventToAdd = {
                            title: eventTitle,
                            start: formatDateToISOString(slot.start),
                            end: formatDateToISOString(slot.end),
                            rendering: 'background',
                            cod_modulo: selectedModuleValue,
                            cod_formador: selectedTeacherValue,
                            cod_sala: selectedClassroomValue,
                            color: selectedColor,
                            cod_turma: selectedClassgroup,
                            dataType: 'selectable'
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

                        console.log(formattedSlots)

                        // Call the server-side method using AJAX
                        $.ajax({
                            type: "POST",
                            url: "/Scheduler.asmx/SetScheduleForClassGroup",
                            data: JSON.stringify({ calendarData: formattedSlots, CodModulo: selectedModuleValue, CodTurma: CodTurma, CodUtilizador: selectedTeacherValue, CodSala: selectedClassroomValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show success message in modal
                                    $('#messageModal .modal-body').text("Aula inserida com sucesso!");
                                    $('#messageModal .modal-body').removeClass('alert-danger').addClass('alert-success');
                                    $('#messageModal').modal('show');

                                    // Fade out the modal after 3 seconds
                                    setTimeout(function () {
                                        $('#messageModal').modal('hide');
                                    }, 3000);
                                } else {
                                    // Show error message in modal

                                    $('#messageModal .modal-body').text("Ocorreu um erro ao guardar a aula inserida.");
                                    $('#messageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                                    $('#messageModal').modal('show');

                                    // Fade out the modal after 3 seconds
                                    setTimeout(function () {
                                        $('#messageModal').modal('hide');
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                               
                                $('#errorMessageModal .modal-body').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                                $('#errorMessageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                                $('#errorMessageModal').modal('show');

                                // Fade out the modal after 3 seconds
                                setTimeout(function () {
                                    $('#errorMessageModal').modal('hide');
                                }, 3000);

                                return;
                            }
                        });

                        UpdateHoursModule();
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

                calendar.setOption('eventClick', function (slot) {
                    // Check if the clicked event is unselectable
                    if (slot.event.extendedProps.dataType === 'selectable') {
                        // Get the start time components of the clicked event
                        var start = slot.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        // Get the end time components of the clicked event
                        var end = slot.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        // Remove the event from the calendarData array
                        calendarData = calendarData.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        // Remover o evento do array teacherAvailability
                        teacherAvailability = teacherAvailability.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remover o evento do array classroomAvailabilitys
                        classroomAvailability = classroomAvailability.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remove the event from the calendar
                        slot.event.remove();


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
                            data: JSON.stringify({ calendarData: formattedSlots, CodModulo: selectedModuleValue, CodTurma: CodTurma, CodUtilizador: selectedTeacherValue, CodSala: selectedClassroomValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show success message in modal
                                   
                                    $('#messageModal .modal-body').text("Aula removida e horário guardado com sucesso.");
                                    $('#messageModal').modal('show');

                                    // Fade out the modal after 3 seconds
                                    setTimeout(function () {
                                        $('#messageModal').modal('hide');
                                    }, 3000);
                                } else {
                                    // Show error message in modal

                                    $('#messageModal .modal-body').text("Erro ao guardar o horário na base de dados.");
                                    $('#messageModal').modal('show');

                                    // Fade out the modal after 3 seconds
                                    setTimeout(function () {
                                        $('#messageModal').modal('hide');
                                    }, 3000);
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                               
                                $('#errorMessageModal .modal-body').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                                $('#errorMessageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                                $('#errorMessageModal').modal('show');

                                // Fade out the modal after 3 seconds
                                setTimeout(function () {
                                    $('#errorMessageModal').modal('hide');
                                }, 3000);

                                return;
                            }
                        });
                        UpdateHoursModule();

                    } else if (slot.event.extendedProps.dataType === 'unselectable') {
                        // Display custom alert modal
                        $('#UnselectableModal').modal('show');
                        return;
                    } else {
                       
                        $('#errorMessageModal .modal-body').text("Não pode remover este evento.");
                        $('#errorMessageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                        $('#errorMessageModal').modal('show');

                        // Fade out the modal after 3 seconds
                        setTimeout(function () {
                            $('#errorMessageModal').modal('hide');
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

                teacherAvailability.forEach(function (slot) {
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
                            dataType: 'unselectable'
                        });
                    }
                });

                classroomAvailability.forEach(function (slot) {
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
                            dataType: 'unselectable'
                        });
                    }
                });

                SundaysHolidays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'unselectable'
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

                    console.log(formattedSlots)
                    console.log(selectedModuleValue)
                    console.log(CodTurma)


                    // Call the server-side method using AJAX
                    $.ajax({
                        type: "POST",
                        url: "/Scheduler.asmx/SetScheduleForClassGroupAutomatic",
                        data: JSON.stringify({ CodTurma: CodTurma }), // Pass formattedSlots with all properties
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success response if needed
                            if (response.d) {
                                // Show success message in modal
                                $('#messageModal .modal-body').text("Horário guardado com sucesso!");
                                $('#messageModal').modal('show');

                                // Fade out the modal after 3 seconds
                                setTimeout(function () {
                                    $('#messageModal').modal('hide');
                                }, 3000);

                                return;
                            } else {
                                $('#messageModal .modal-body').text("Erro ao guardar o horário na base de dados.");
                                $('#messageModal').modal('show');

                                // Fade out the modal after 3 seconds
                                setTimeout(function () {
                                    $('#messageModal').modal('hide');
                                }, 3000);
                                return;
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            $('#errorMessageModal .modal-body').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                            $('#errorMessageModal .modal-body').removeClass('alert-success').addClass('alert-danger');
                            $('#errorMessageModal').modal('show');

                            // Fade out the modal after 3 seconds
                            setTimeout(function () {
                                $('#errorMessageModal').modal('hide');
                            }, 3000);

                            return;
                        }
                    });
                    UpdateHoursModule();

                    event.preventDefault(); // Prevent default button behavior
                });

            }
        });



    </script>
</asp:Content>
