using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace FinalProject
{


    /// <summary>
    /// Summary description for Scheduler
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Scheduler : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTeacherAvailabilityFromJson(int CodUtilizador)
        {
            List<Schedule> availabilities = GetTeacherAvailabilityFromDatabase(CodUtilizador);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string TeacherAvailabilities = serializer.Serialize(availabilities);

            return TeacherAvailabilities;
        }

        public static List<Schedule> GetTeacherAvailabilityFromDatabase(int CodUtilizador)
        {

            List<Schedule> TeacherAvailabilities = new List<Schedule>();
            string query =
                $" WITH ContiguousSlots AS (SELECT DF.codIntervaloTempo, DF.dataDisponibilidade, DF.codFormador, DF.disponivel, IT.timeSlotBegin AS hora_inicio, IT.timeSlotEnd AS hora_fim, DF.titulo, DF.corDisponibilidade, ROW_NUMBER() OVER (ORDER BY DF.titulo, DF.dataDisponibilidade, IT.timeSlotBegin) AS rn FROM disponibilidadeFormador AS DF JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = DF.codIntervaloTempo WHERE DF.codFormador = {CodUtilizador} AND DF.disponivel = 0) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataDisponibilidade, titulo, corDisponibilidade, disponivel, codFormador FROM ContiguousSlots GROUP BY dataDisponibilidade, DATEADD(hour, -rn, hora_inicio), titulo, corDisponibilidade,disponivel, codFormador ORDER BY dataDisponibilidade, startTime;";


            using (SqlConnection myConn = new SqlConnection(ConfigurationManager
                       .ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myConn.Open();

                    using (SqlDataReader dr = myCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Schedule eventData = new Schedule();
                            eventData.TimeSlotBeginTime = (dr["dataDisponibilidade"] != DBNull.Value
                                ? Convert.ToDateTime(dr["dataDisponibilidade"]).Date
                                : DateTime.Today);
                            eventData.EventName = dr["disponivel"] == DBNull.Value ? " " : dr["disponivel"].ToString();
                            eventData.CodFormador = (int)(dr["codFormador"] == DBNull.Value
                                ? 00000
                                : Convert.ToInt32(dr["codFormador"]));
                            eventData.start = (dr["startTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["startTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.end = (dr["endTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["endTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.title = dr["titulo"] == DBNull.Value
                                ? " "
                                : dr["titulo"].ToString();
                            eventData.color = dr["corDisponibilidade"] == DBNull.Value
                                ? " "
                                : dr["corDisponibilidade"].ToString();

                            TeacherAvailabilities.Add(eventData);
                        }
                    }
                }
            }

            return TeacherAvailabilities;
        }

        public static List<Schedule> GetTeacherAvailabilityFromDatabaseWithoutCTE(int CodUtilizador)
        {
            List<Schedule> TeacherAvailabilities = new List<Schedule>();
            string query = @"  SELECT CONVERT(date, DF.dataDisponibilidade) AS dataDisponibilidade, IT.timeSlotBegin AS startTime, IT.timeSlotEnd AS endTime, DF.titulo, DF.corDisponibilidade, DF.disponivel, DF.codFormador FROM disponibilidadeFormador AS DF JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = DF.codIntervaloTempo INNER JOIN horario AS H ON H.codIntervaloTempo=IT.codIntervaloTempo INNER JOIN moduloFormadorTurma AS MFT ON H.codModuloFormador=MFT.codModuloFormadorTurma WHERE DF.codFormador = @CodUtilizador AND DF.disponivel = 0 ORDER BY CONVERT(date, DF.dataDisponibilidade), startTime;";

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
                    myConn.Open();

                    using (SqlDataReader dr = myCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Schedule eventData = new Schedule();
                            eventData.TimeSlotBeginTime = (dr["dataDisponibilidade"] != DBNull.Value
                                ? Convert.ToDateTime(dr["dataDisponibilidade"]).Date
                                : DateTime.Today);
                            eventData.TimeSlotBeginTime = (dr["startTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["startTime"])
                                : DateTime.Today);
                            eventData.TimeSlotEndTime = (dr["endTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["endTime"])
                                : DateTime.Today);
                            eventData.EventName = dr["disponivel"] == DBNull.Value ? " " : dr["disponivel"].ToString();
                            eventData.EventColor = dr["corDisponibilidade"] == DBNull.Value
                                ? " "
                                : dr["corDisponibilidade"].ToString();
                            eventData.CodFormador = (int)(dr["codFormador"] == DBNull.Value
                                ? 00000
                                : Convert.ToInt32(dr["codFormador"]));
                            eventData.title = dr["titulo"] == DBNull.Value
                                ? " "
                                : dr["titulo"].ToString();
                            eventData.color = dr["corDisponibilidade"] == DBNull.Value
                                ? " "
                                : dr["corDisponibilidade"].ToString();

                            TeacherAvailabilities.Add(eventData);
                        }
                    }
                }
            }

            return TeacherAvailabilities;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetScheduleOfClassGroupFromJson(int CodTurma)
        {
            List<Schedule> ClassGroupSchedule = GetScheduleOfClassGroupFromDatabase(CodTurma);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string ScheduleOfClassGroup = serializer.Serialize(ClassGroupSchedule);

            return ScheduleOfClassGroup;
        }

        public List<Schedule> GetScheduleOfClassGroupFromDatabase(int CodTurma)
        {
            List<Schedule> ScheduleOfClassGroup = new List<Schedule>();

            string query = $"WITH ContiguousSlots AS (SELECT H.codIntervaloTempo, H.dataHorario, H.codModuloFormador, MFT.codFormador, MFT.codModulo, H.codSala, IT.timeSlotBegin AS hora_inicio, IT.timeSlotEnd AS hora_fim, H.tituloHorario, H.corHorario, ROW_NUMBER() OVER (ORDER BY H.tituloHorario, H.dataHorario, IT.timeSlotBegin) AS rn FROM horario AS H INNER JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = H.codIntervaloTempo INNER JOIN moduloFormadorTurma AS MFT ON MFT.codModuloFormadorTurma = H.codModuloFormador INNER JOIN turma AS T ON T.codTurma = MFT.codTurma INNER JOIN modulo AS M ON M.codModulos = MFT.codModulo WHERE T.codTurma = {CodTurma}) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataHorario, tituloHorario, codFormador, codModulo, corHorario, codSala FROM ContiguousSlots GROUP BY dataHorario, DATEADD(hour, -rn, hora_inicio), tituloHorario, corHorario, codSala, codFormador, codModulo ORDER BY tituloHorario, dataHorario, startTime;";


            using (SqlConnection myConn =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myConn.Open();

                    using (SqlDataReader dr = myCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Schedule eventData = new Schedule();
                            eventData.ScheduleDate = (dr["dataHorario"] != DBNull.Value
                                ? Convert.ToDateTime(dr["dataHorario"]).Date
                                : DateTime.Today);
                            eventData.start = (dr["startTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["startTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.end = (dr["endTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["endTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.title = dr["tituloHorario"] == DBNull.Value
                               ? " "
                               : dr["tituloHorario"].ToString();
                            eventData.color = dr["corHorario"] == DBNull.Value
                                ? " "
                                : dr["corHorario"].ToString();
                            eventData.CodSala =
                                (int)(dr["codSala"] == DBNull.Value ? 0000 : Convert.ToInt32(dr["codSala"]));
                            eventData.CodFormador = (int)(dr["codFormador"] == DBNull.Value
                                ? 0000
                                : Convert.ToInt32(dr["codFormador"]));
                            eventData.CodModulo = (int)(dr["codModulo"] == DBNull.Value
                                ? 0000
                                : Convert.ToInt32(dr["codModulo"]));
                            ScheduleOfClassGroup.Add(eventData);
                        }
                    }
                }
            }

            return ScheduleOfClassGroup;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetScheduleOfClassRoomFromJson(int CodSala)
        {
            List<Schedule> ClassRoomSchedule = GetScheduleOfClassRoomFromDatabase(CodSala);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string ScheduleOfClassRoom = serializer.Serialize(ClassRoomSchedule);

            return ScheduleOfClassRoom;
        }

        public static List<Schedule> GetScheduleOfClassRoomFromDatabase(int CodSala)
        {
            List<Schedule> ScheduleOfClassRoom = new List<Schedule>();

            string query = $"WITH ContiguousSlots AS (SELECT DS.codIntervaloTempo, DS.dataDisponibilidade, DS.codSala, DS.disponivel, IT.timeSlotBegin AS hora_inicio, IT.timeSlotEnd AS hora_fim, DS.titulo, DS.corDisponibilidade, ROW_NUMBER() OVER (ORDER BY DS.titulo, DS.dataDisponibilidade, IT.timeSlotBegin) AS rn FROM disponibilidadeSala AS DS JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = DS.codIntervaloTempo WHERE DS.codSala = {CodSala} AND DS.disponivel = 0) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataDisponibilidade, titulo, corDisponibilidade, disponivel, codSala FROM ContiguousSlots GROUP BY dataDisponibilidade, DATEADD(hour, -rn, hora_inicio), titulo, corDisponibilidade,disponivel, codSala ORDER BY dataDisponibilidade, startTime;";


            using (SqlConnection myConn =
                   new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myConn.Open();

                    using (SqlDataReader dr = myCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Schedule eventData = new Schedule();
                            eventData.ScheduleDate = (dr["dataDisponibilidade"] != DBNull.Value
                                ? Convert.ToDateTime(dr["dataDisponibilidade"]).Date
                                : DateTime.Today);
                            eventData.start = (dr["startTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["startTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.end = (dr["endTime"] != DBNull.Value
                                ? Convert.ToDateTime(dr["endTime"]).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"));
                            eventData.EventColor = dr["corDisponibilidade"] == DBNull.Value ? " " : dr["corDisponibilidade"].ToString();
                            eventData.title = dr["titulo"] == DBNull.Value
                               ? " "
                               : dr["titulo"].ToString();
                            eventData.color = dr["corDisponibilidade"] == DBNull.Value
                                ? " "
                                : dr["corDisponibilidade"].ToString();
                            eventData.EventName = dr["disponivel"] == DBNull.Value ? " " : dr["disponivel"].ToString();
                            eventData.EventColor = dr["corDisponibilidade"] == DBNull.Value ? " " : dr["corDisponibilidade"].ToString();
                            eventData.CodSala =
                                (int)(dr["codSala"] == DBNull.Value ? 0000 : Convert.ToInt32(dr["codSala"]));
                        }
                    }
                }
            }

            return ScheduleOfClassRoom;
        }

        [WebMethod]
        public bool SetTimeSlotDataFromClient(Schedule[] calendarData, int CodUtilizador)
        {
            try
            {
                Schedule.DeleteTeacherAvailability(CodUtilizador);

                foreach (var scheduleData in calendarData)
                {
                    if (scheduleData != null)
                    {
                        // Access the title property for each slot
                        string title = scheduleData.title;
                        string startDateString = scheduleData.start;
                        string endDateString = scheduleData.end;
                        string color = scheduleData.color;

                        if (title != null && startDateString != null && endDateString != null && color != null)
                        {
                            // Append default times if only date is provided
                            if (!startDateString.Contains("T"))
                                startDateString += "T08:00:00";
                            else if (startDateString.EndsWith("Z"))
                                startDateString = startDateString.Remove(startDateString.Length - 1);

                            if (!endDateString.Contains("T"))
                                endDateString += "T23:00:00";
                            else if (endDateString.EndsWith("Z"))
                                endDateString = endDateString.Remove(endDateString.Length - 1);

                            // Parse start and end date-time strings into DateTime objects
                            DateTime startDatetime, endDatetime;
                            if (DateTime.TryParse(startDateString, out startDatetime) &&
                                DateTime.TryParse(endDateString, out endDatetime))
                            {
                                TimeSpan timeSpan = endDatetime.TimeOfDay - startDatetime.TimeOfDay;

                                int diferença = timeSpan.Hours;

                                for (int i = 0; i < diferença; i++)
                                {
                                    int intervalHours = 1; // Define the interval duration in hours, e.g., 1 hour

                                    DateTime intervalStart = startDatetime.AddHours(i * intervalHours);
                                    DateTime intervalEnd = intervalStart.AddHours(intervalHours);

                                    int CodTimeSlot =
                                        Schedule.VerifyTimeslotAvailability(intervalStart, intervalEnd, CodUtilizador);

                                    // Create a new Schedule object
                                    Schedule schedule = new Schedule();
                                    schedule.CodFormador = CodUtilizador;
                                    schedule.ScheduleDate = startDatetime.Date;
                                    schedule.TimeSlotID = CodTimeSlot;
                                    schedule.TimeSlotBeginTime = intervalStart;
                                    schedule.TimeSlotEndTime = intervalEnd;
                                    schedule.Available = false;
                                    schedule.EventColor = color;
                                    schedule.EventName = title;

                                    Schedule.InsertTeacherAvailability(schedule);
                                }
                            }

                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string exx = ex.ToString();
                return false;
            }
        }

        [WebMethod]
        public bool SetScheduleForClassGroup(Schedule[] calendarData, int CodModulo, int CodUtilizador, int CodTurma, int CodSala)
        {
            try
            {
                Schedule.DeleteScheduleForClassGroup(CodTurma);
                Schedule.DeleteClassRoomAvailability(CodTurma);
                Schedule.DeleteTeacherAvailabilityClassGroup(CodUtilizador, CodTurma);

                foreach (var scheduleData in calendarData)
                {
                    if (scheduleData != null)
                    {
                        // Access the title property for each slot
                        string title = scheduleData.title;
                        string startDateString = scheduleData.start;
                        string endDateString = scheduleData.end;
                        string color = scheduleData.color;

                        if (title != null && startDateString != null && endDateString != null && color != null)
                        {
                            // Append default times if only date is provided
                            if (!startDateString.Contains("T"))
                                startDateString += "T08:00:00";
                            else if (startDateString.EndsWith("Z"))
                                startDateString = startDateString.Remove(startDateString.Length - 1);

                            if (!endDateString.Contains("T"))
                                endDateString += "T23:00:00";
                            else if (endDateString.EndsWith("Z"))
                                endDateString = endDateString.Remove(endDateString.Length - 1);

                            // Parse start and end date-time strings into DateTime objects
                            DateTime startDatetime, endDatetime;
                            if (DateTime.TryParse(startDateString, out startDatetime) &&
                                DateTime.TryParse(endDateString, out endDatetime))
                            {
                                TimeSpan timeSpan = endDatetime.TimeOfDay - startDatetime.TimeOfDay;

                                int diferença = timeSpan.Hours;

                                for (int i = 0; i < diferença; i++)
                                {
                                    int intervalHours = 1; // Define the interval duration in hours, e.g., 1 hour

                                    DateTime intervalStart = startDatetime.AddHours(i * intervalHours);
                                    DateTime intervalEnd = intervalStart.AddHours(intervalHours);

                                    int CodTimeSlot =
                                        Schedule.VerifyTimeslotAvailability(intervalStart, intervalEnd, CodUtilizador);

                                    // Create a new Schedule object
                                    Schedule schedule = new Schedule();
                                    schedule.CodTurma = CodTurma;
                                    schedule.CodSala = CodSala;
                                    schedule.CodModulo = CodModulo;
                                    schedule.CodFormador = CodUtilizador;
                                    schedule.ScheduleDate = startDatetime.Date;
                                    schedule.TimeSlotID = CodTimeSlot;
                                    schedule.TimeSlotBeginTime = intervalStart;
                                    schedule.TimeSlotEndTime = intervalEnd;
                                    schedule.Available = true;
                                    schedule.EventColor = color;
                                    schedule.EventName = title;
                                    schedule.CodSala = CodSala;

                                    Schedule.InsertScheduleForClassGroup(schedule);
                                }
                            }

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string exx = ex.ToString();
                return false;
            }
        }


        [WebMethod(EnableSession = true)]
        public bool SetScheduleForClassGroupAutomatic(int CodTurma)
        {
            try
            {
                Schedule.DeleteScheduleForClassGroupAll(CodTurma);
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["Automatic"] != null)
                    {
                        string automatico = HttpContext.Current.Session["Automatic"].ToString();

                        JavaScriptSerializer serializer = new JavaScriptSerializer();

                        List<Schedule> automaticSchedule = serializer.Deserialize<List<Schedule>>(automatico);

                        foreach (var automatic in automaticSchedule)
                        {
                            int CodTimeSlot =
                               Schedule.VerifyTimeslotAvailability(automatic.TimeSlotBeginTime, automatic.TimeSlotEndTime, automatic.CodFormador);
                            automatic.TimeSlotID = CodTimeSlot;
                            Schedule.InsertScheduleForClassGroup(automatic);
                        }
                        return true;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string exx = ex.ToString();
                return false;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetScheduleAutomaticJSON(int CodTurma)
        {

            ClassGroup classGroup = ClassGroup.LoadClassGroup(CodTurma.ToString());
            List<Classroom> classrooms = Classroom.LoadClassrooms();

            List<Schedule> scheduleList = GenerateSchedule(classGroup, classrooms);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string AutomaticSchedule = serializer.Serialize(scheduleList);
            HttpContext.Current.Session["Automatic"] = AutomaticSchedule;

            return AutomaticSchedule;
        }
        public List<Schedule> GenerateSchedule(ClassGroup classGroup, List<Classroom> classrooms)
        {
            List<Schedule> schedule = new List<Schedule>();
            List<Tuple<int, int>> teacherModules = ClassGroup.GetTeacherModuleForClassGroup(classGroup.CodTurma);
            List<Schedule> teacherNotAvailable = new List<Schedule>();
            DateTime startDate = classGroup.DataInicio.Date.AddHours((classGroup.CodHorarioTurma == 1) ? 8 : 16);
            DateTime classStartTime = startDate;
            DateTime classEndTime;
            int startHour = (classGroup.CodHorarioTurma == 1) ? 8 : 16;
            int endHour = (classGroup.CodHorarioTurma == 1) ? 16 : 23;

            foreach (var teacherModule in teacherModules)
            {
                int teacherId = teacherModule.Item1;
                int moduleId = teacherModule.Item2;
                Teacher teacher = classGroup.Teachers.FirstOrDefault(t => t.CodFormador == teacherId);
                Module module = classGroup.Modules.FirstOrDefault(m => m.CodModulo == moduleId);
                teacherNotAvailable = Scheduler.GetTeacherAvailabilityFromDatabaseWithoutCTE(teacher.CodFormador);
                module.Duracao = Module.GetUFCDDurationByModuleID(moduleId);
                string cor = GetRandomColor();
                int Duracao = module.Duracao;
                int xCont = 0;

                while (Duracao > 0)
                {
                    if (classStartTime.DayOfWeek == DayOfWeek.Saturday || classStartTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        classStartTime = classStartTime.AddDays(1);
                    }

                    int ano = DateTime.Today.Year;
                    List<DateTime> feriados = ObterFeriados(ano);

                    foreach (DateTime feriado in feriados)
                    {
                        if (feriado.Date == classStartTime)
                        {
                            classStartTime = classStartTime.AddDays(1);
                        }
                    }

                    if ((classStartTime.AddHours(Math.Min(module.Duracao, 4)) < classStartTime.Date.AddHours(endHour)) && (classStartTime.AddHours(Math.Min(module.Duracao, 4)) != DateTime.MinValue))
                    {
                        classEndTime = classStartTime.AddHours(Math.Min(module.Duracao, 4));
                    }
                    else
                    {
                        classEndTime = classStartTime.Date.AddHours(endHour);
                    }

                    List<Schedule> classroomNotAvailable = Scheduler.GetScheduleOfClassRoomFromDatabase(classrooms[xCont].CodSala);

                    while (IsTeacherUnavailable(classStartTime, classEndTime, teacherNotAvailable) || IsClassRoomUnavailable(classStartTime, classEndTime, classroomNotAvailable) || classStartTime == classEndTime)
                    {
                        if (classStartTime.DayOfWeek == DayOfWeek.Saturday || classStartTime.DayOfWeek == DayOfWeek.Sunday)
                        {
                            classStartTime.AddDays(1);
                        }

                        if (IsClassRoomUnavailable(classStartTime, classEndTime, classroomNotAvailable))
                        {
                            xCont++;
                            if (xCont < classrooms.Count)
                            {
                                classroomNotAvailable.Clear();
                                classroomNotAvailable = Scheduler.GetScheduleOfClassRoomFromDatabase(classrooms[xCont].CodSala);
                            }
                            else
                            {
                                xCont = 0;

                                if (classStartTime == classEndTime)
                                {
                                    classStartTime = classStartTime.AddDays(1).Date.AddHours(startHour);
                                    classEndTime = classStartTime.AddHours(4);

                                    if (classStartTime.DayOfWeek == DayOfWeek.Saturday || classStartTime.DayOfWeek == DayOfWeek.Sunday)
                                        classStartTime.AddDays(1);
                                }
                                else if (classStartTime.Hour < endHour)
                                {
                                    classStartTime = classStartTime.AddHours(1);
                                    if (classEndTime.Hour < endHour)
                                        classEndTime = classEndTime.AddHours(1);
                                }
                            }
                        }
                        else
                        {
                            if (classStartTime == classEndTime)
                            {
                                classStartTime = classStartTime.AddDays(1).Date.AddHours(startHour);
                                classEndTime = classStartTime.AddDays(4);

                                if (classStartTime.DayOfWeek == DayOfWeek.Saturday || classStartTime.DayOfWeek == DayOfWeek.Sunday)
                                    classStartTime.AddDays(1);
                            }
                            else if (classStartTime.Hour < endHour)
                            {
                                classStartTime = classStartTime.AddHours(1);
                                if (classEndTime.Hour < endHour)
                                    classEndTime = classEndTime.AddHours(1);
                            }
                        }
                    }

                    // Create a new Schedule object
                    Schedule newSchedule = new Schedule
                    {
                        ScheduleDate = classStartTime.Date, // Use only the date part
                        TimeSlotBeginTime = classStartTime,
                        TimeSlotEndTime = classEndTime,
                        EventName = module.Nome,
                        CodTurma = classGroup.CodTurma, // Use the class group's CodTurma
                        CodModulo = module.CodModulo,
                        CodSala = classrooms[xCont].CodSala,
                        CodFormador = teacher.CodFormador,
                        NrTurma = classGroup.NomeTurma,
                        EventColor = cor,
                        Available = true,
                        title = $"{classGroup.NomeTurma} - Sessão de {module.Nome}",
                        start = classStartTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end = classEndTime.ToString("yyyy-MM-ddTHH:mm:ss") // Assuming scheduled modules are available
                    };

                    // Add the new schedule to the list
                    schedule.Add(newSchedule);

                    // Subtract the remaining duration
                    Duracao -= (classEndTime - classStartTime).Hours;

                    if (classEndTime.Hour == endHour)
                        classStartTime = classStartTime.AddDays(1).Date.AddHours(startHour);
                    else
                        classStartTime = classEndTime;
                }
            }
            return schedule;
        }

        private static List<DateTime> ObterFeriados(int ano)
        {
            List<DateTime> feriados = new List<DateTime>();

            // Adiciona os feriados fixos
            feriados.Add(new DateTime(ano, 1, 1)); // Ano Novo
            feriados.Add(new DateTime(ano, 4, 25)); // Dia da Liberdade
            feriados.Add(new DateTime(ano, 5, 1)); // Dia do Trabalhador
            feriados.Add(new DateTime(ano, 6, 10)); // Dia de Portugal
            feriados.Add(new DateTime(ano, 8, 15)); // Assunção de Nossa Senhora
            feriados.Add(new DateTime(ano, 10, 5)); // Implantação da República
            feriados.Add(new DateTime(ano, 11, 1)); // Dia de Todos os Santos
            feriados.Add(new DateTime(ano, 12, 1)); // Restauração da Independência
            feriados.Add(new DateTime(ano, 12, 25)); // Natal
            feriados.Add(new DateTime(ano, 12, 24)); // Véspera de Natal

            return feriados;
        }


        private bool IsTeacherUnavailable(DateTime startTime, DateTime endTime, List<Schedule> teacherNotAvailable)
        {
            bool check = false;
            foreach (var schedule in teacherNotAvailable)
            {
                DateTime start = schedule.TimeSlotBeginTime;
                DateTime end = schedule.TimeSlotBeginTime;
                if ((startTime >= start && startTime < end) || (endTime > start && endTime < end) || (startTime <= start && endTime >= start))
                { check = true; }
            }
            return check;
        }

        private bool IsClassRoomUnavailable(DateTime startTime, DateTime endTime, List<Schedule> classroomNotAvailable)
        {
            bool check = false;
            foreach (var schedule in classroomNotAvailable)
            {
                DateTime start = schedule.TimeSlotBeginTime;
                DateTime end = schedule.TimeSlotBeginTime;
                if ((startTime >= start && startTime < end) || (endTime > start && endTime < end) || (startTime <= start && endTime >= start))
                { check = true; }
            }
            return check;
        }

        private string GetRandomColor()
        {
            // Create a Random object to generate random numbers
            Random random = new Random();

            // Generate random values for RGB components
            int r = random.Next(256); // Random value for red (0-255)
            int g = random.Next(256); // Random value for green (0-255)
            int b = random.Next(256); // Random value for blue (0-255)

            // Convert RGB values to hexadecimal format
            string hexR = r.ToString("X2"); // Convert red to hexadecimal and ensure two digits
            string hexG = g.ToString("X2"); // Convert green to hexadecimal and ensure two digits
            string hexB = b.ToString("X2"); // Convert blue to hexadecimal and ensure two digits

            // Concatenate hexadecimal values to form the color code
            string color = '#' + hexR + hexG + hexB; // Format: #RRGGBB

            return color;
        }


    }
}

