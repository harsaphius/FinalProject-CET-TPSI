using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        public List<Schedule> GetTeacherAvailabilityFromDatabase(int CodUtilizador)
        {

            List<Schedule> TeacherAvailabilities = new List<Schedule>();
            string query =
                $" WITH ContiguousSlots AS (SELECT DF.codIntervaloTempo, DF.dataDisponibilidade, DF.codFormador, DF.disponivel, IT.timeSlotBegin AS hora_inicio, IT.timeSlotEnd AS hora_fim, DF.titulo, DF.corDisponibilidade, ROW_NUMBER() OVER (ORDER BY DF.titulo, DF.dataDisponibilidade, IT.timeSlotBegin) AS rn FROM disponibilidadeFormador AS DF JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = DF.codIntervaloTempo WHERE DF.codFormador = {CodUtilizador} AND DF.disponivel = 1) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataDisponibilidade, titulo, corDisponibilidade, disponivel, codFormador FROM ContiguousSlots GROUP BY dataDisponibilidade, DATEADD(hour, -rn, hora_inicio), titulo, corDisponibilidade,disponivel, codFormador ORDER BY dataDisponibilidade, startTime;";


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
                            eventData.EventColor = dr["corDisponibilidade"] == DBNull.Value
                                ? " "
                                : dr["corDisponibilidade"].ToString();
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

            string query = $"WITH ContiguousSlots AS (SELECT codIntervaloTempo, dataHorario, codModuloFormador, codTurma, codModulos, codFormador, codSala, timeSlotBegin AS hora_inicio, timeSlotEnd AS hora_fim, tituloHorario, corHorario, ROW_NUMBER() OVER (ORDER BY tituloHorario, dataHorario, timeSlotBegin) AS rn FROM (SELECT H.codIntervaloTempo, H.dataHorario, H.codModuloFormador, H.codSala, H.tituloHorario, H.corHorario, IT.timeSlotBegin, IT.timeSlotEnd, T.codTurma, M.codModulos, F.codFormador FROM horario AS H INNER JOIN sala AS S ON S.codSala = H.codSala INNER JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = H.codIntervaloTempo INNER JOIN moduloFormadorTurma AS MFT ON MFT.codModuloFormadorTurma = H.codModuloFormador INNER JOIN turma AS T ON T.codTurma = MFT.codTurma INNER JOIN modulo AS M ON M.codModulos = MFT.codModulo INNER JOIN formador AS F ON F.codFormador = MFT.codFormador WHERE T.codTurma = {CodTurma}) AS Horario) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataHorario, tituloHorario, corHorario, codModulos, codFormador, codSala FROM ContiguousSlots GROUP BY dataHorario, DATEADD(hour, -rn, hora_inicio), tituloHorario, corHorario, codModulos, codFormador, codSala ORDER BY dataHorario, startTime;";


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
                            eventData.ScheduleDate = (dr["data"] != DBNull.Value
                                ? Convert.ToDateTime(dr["data"]).Date
                                : DateTime.Today);
                            eventData.TimeSlotBeginTime = (dr["horaInicio"] != DBNull.Value
                                ? Convert.ToDateTime(dr["horaInicio"]).Date
                                : DateTime.Today);
                            eventData.TimeSlotBeginTime = (dr["horaFim"] != DBNull.Value
                                ? Convert.ToDateTime(dr["horaFim"]).Date
                                : DateTime.Today);
                            eventData.EventName = dr["disponivel"] == DBNull.Value ? " " : dr["disponivel"].ToString();
                            eventData.EventColor = dr["corEvento"] == DBNull.Value ? " " : dr["corEvento"].ToString();
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

        public List<Schedule> GetScheduleOfClassRoomFromDatabase(int CodSala)
        {
            List<Schedule> ScheduleOfClassRoom = new List<Schedule>();

            string query = $"WITH ContiguousSlots AS (SELECT DS.codIntervaloTempo, DS.dataDisponibilidade, DS.codSala, DS.disponivel, IT.timeSlotBegin AS hora_inicio, IT.timeSlotEnd AS hora_fim, DS.titulo, DS.corDisponibilidade, ROW_NUMBER() OVER (ORDER BY DS.titulo, DS.dataDisponibilidade, IT.timeSlotBegin) AS rn FROM disponibilidadeSala AS DS JOIN intervaloTempo AS IT ON IT.codIntervaloTempo = DS.codIntervaloTempo WHERE DS.codSala = {CodSala} AND DS.disponivel = 1) SELECT MIN(hora_inicio) AS startTime, MAX(hora_fim) AS endTime, dataDisponibilidade, titulo, corDisponibilidade, disponivel, codSala FROM ContiguousSlots GROUP BY dataDisponibilidade, DATEADD(hour, -rn, hora_inicio), titulo, corDisponibilidade,disponivel, codSala ORDER BY dataDisponibilidade, startTime;";


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
                            eventData.ScheduleDate = (dr["data"] != DBNull.Value
                                ? Convert.ToDateTime(dr["data"]).Date
                                : DateTime.Today);
                            eventData.TimeSlotBeginTime = (dr["horaInicio"] != DBNull.Value
                                ? Convert.ToDateTime(dr["horaInicio"]).Date
                                : DateTime.Today);
                            eventData.TimeSlotBeginTime = (dr["horaFim"] != DBNull.Value
                                ? Convert.ToDateTime(dr["horaFim"]).Date
                                : DateTime.Today);
                            eventData.EventName = dr["disponivel"] == DBNull.Value ? " " : dr["disponivel"].ToString();
                            eventData.EventColor = dr["corEvento"] == DBNull.Value ? " " : dr["corEvento"].ToString();
                            eventData.CodSala =
                                (int)(dr["codSala"] == DBNull.Value ? 0000 : Convert.ToInt32(dr["codSala"]));
                            eventData.CodFormador = (int)(dr["codFormador"] == DBNull.Value
                                ? 0000
                                : Convert.ToInt32(dr["codFormador"]));
                            eventData.CodModulo = (int)(dr["codModulo"] == DBNull.Value
                                ? 0000
                                : Convert.ToInt32(dr["codModulo"]));
                            ScheduleOfClassRoom.Add(eventData);
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
                                    schedule.Available = true;
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
        public bool SetScheduleForClassGroup(Schedule[] calendarData, int CodUtilizador, int CodTurma, int CodSala)
        {
            try
            {
                Schedule.DeleteScheduleForClassGroup(CodTurma);
                Schedule.DeleteClassRoomAvailability(CodTurma);
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

    }
}

