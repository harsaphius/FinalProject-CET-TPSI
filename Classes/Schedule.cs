using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime TimeSlotBeginTime { get; set; }
        public DateTime TimeSlotEndTime { get; set; }
        public int TimeSlotID { get; set; }
        public string EventName { get; set; }
        public int CodTurma { get; set; }
        public int CodModulo { get; set; }
        public int CodSala { get; set; }
        public int CodFormador { get; set; }
        public string EventColor { get; set; }
        public Boolean Available { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }

        public static int VerifyTimeslotAvailability(DateTime StartTime, DateTime EndTime, int CodFormador)
        {
            SqlConnection myConn =
                new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@timeSlotBegin", StartTime);
                myCommand.Parameters.AddWithValue("@timeSlotEnd", EndTime);
                myCommand.Parameters.AddWithValue("@CodFormador", CodFormador);

                SqlParameter CodTimeSlot = new SqlParameter();
                CodTimeSlot.ParameterName = "@CodTimeSlot";
                CodTimeSlot.Direction = ParameterDirection.Output;
                CodTimeSlot.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(CodTimeSlot);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "VerifyTimeslotAvailability";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int AnswCodTimeSlot = Convert.ToInt32(myCommand.Parameters["@CodTimeSlot"].Value);

                myConn.Close();

                return AnswCodTimeSlot;
            }
        }

        public static void InsertTeacherAvailability(Schedule schedule)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand("TeacherAvailabilityRegister", myCon);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCon.Open();

            myCommand.Parameters.AddWithValue("@CodFormador", schedule.CodFormador);
            myCommand.Parameters.AddWithValue("@CodIntervaloTempo", schedule.TimeSlotID);
            myCommand.Parameters.AddWithValue("@DataDisponibilidade", schedule.TimeSlotBeginTime.Date);
            myCommand.Parameters.AddWithValue("@TimeSlotInicio", schedule.TimeSlotBeginTime);
            myCommand.Parameters.AddWithValue("@TimeSlotFim", schedule.TimeSlotEndTime);
            myCommand.Parameters.AddWithValue("Titulo", schedule.EventName);
            myCommand.Parameters.AddWithValue("CorDisponibilidade", schedule.EventColor);
            myCommand.Parameters.AddWithValue("Disponivel", schedule.Available);

            myCommand.ExecuteNonQuery();

            myCon.Close();

        }

        public static void DeleteTeacherAvailability(int CodUtilizador)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@CodFormador", CodUtilizador);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "DeleteTeacherAvailability";

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void InsertClassRoomAvailability(Schedule schedule)
        {

        }

        public static void DeleteClassRoomAvailability(int CodSala)
        {

        }

        public static void InsertScheduleForClassGroup(Schedule schedule)
        {

        }

        public static void DeleteScheduleForClassGroup(int CodTurma)
        {

        }



    }
}