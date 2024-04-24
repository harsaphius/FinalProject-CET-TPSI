using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    [Serializable]
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime TimeSlotBeginTime { get; set; }
        public DateTime TimeSlotEndTime { get; set; }
        public int TimeSlotID { get; set; }
        public string EventName { get; set; }
        public int CodTurma { get; set; }
        public string NrTurma { get; set; }
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
            myCommand.Parameters.AddWithValue("Titulo", "Formador: " + schedule.EventName);
            myCommand.Parameters.AddWithValue("CorDisponibilidade", schedule.EventColor);
            myCommand.Parameters.AddWithValue("Disponivel", schedule.Available);

            myCommand.ExecuteNonQuery();

            myCon.Close();

        }

        public static void DeleteTeacherAvailability(int CodUtilizador)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            string query = "DELETE FROM disponibilidadeFormador WHERE codFormador = @CodFormador";
            using (SqlCommand myCommand = new SqlCommand(query, myConn))
            {
                myCommand.Parameters.AddWithValue("@CodFormador", CodUtilizador);

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

        public static void InsertScheduleForClassGroup(Schedule schedule)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand("ClassGroupScheduleRegister", myCon);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCon.Open();

            myCommand.Parameters.AddWithValue("@CodTurma", schedule.CodTurma);
            myCommand.Parameters.AddWithValue("@CodModulo", schedule.CodModulo);
            myCommand.Parameters.AddWithValue("@CodFormador", schedule.CodFormador);
            myCommand.Parameters.AddWithValue("@CodSala", schedule.CodSala);
            myCommand.Parameters.AddWithValue("@TimeSlotID", schedule.TimeSlotID);
            myCommand.Parameters.AddWithValue("@DataDisponibilidade", schedule.TimeSlotBeginTime.Date);
            myCommand.Parameters.AddWithValue("@Titulo", "Turma: " + schedule.title);
            myCommand.Parameters.AddWithValue("@CorDisponibilidade", schedule.EventColor);

            myCommand.ExecuteNonQuery();

            myCon.Close();
        }

        public static void DeleteScheduleForClassGroup(int CodTurma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            string query = $"DELETE H FROM horario AS H LEFT JOIN moduloFormadorTurma AS MFC ON H.codModuloFormador=MFC.codModuloFormadorTurma LEFT JOIN turma AS T ON MFC.codTurma = T.codTurma WHERE T.codTurma = @CodTurma AND T.isActive=1";
            using (SqlCommand myCommand = new SqlCommand(query, myConn))
            {
                myCommand.Parameters.AddWithValue("@CodTurma", CodTurma);

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }
        public static void DeleteScheduleForClassGroupAll(int CodTurma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            // Delete schedules for the class group
            string deleteScheduleQuery = $"DELETE H FROM horario AS H INNER JOIN moduloFormadorTurma AS MFC ON H.codModuloFormador=MFC.codModuloFormadorTurma INNER JOIN turma AS T ON MFC.codTurma = T.codTurma WHERE T.codTurma = @CodTurma AND T.isActive=1";

            // Delete availability for classrooms associated with the class group
            string deleteClassroomAvailabilityQuery = $"DELETE FROM disponibilidadeSala WHERE codSala IN (SELECT codSala FROM horario WHERE codModuloFormador IN (SELECT codModuloFormadorTurma FROM moduloFormadorTurma WHERE codTurma = @CodTurma))";

            // Delete availability for teachers associated with the class group
            string deleteTeacherAvailabilityQuery = $"DELETE FROM disponibilidadeFormador WHERE codFormador IN (SELECT codFormador FROM moduloFormadorTurma WHERE codTurma = @CodTurma)";

            using (SqlCommand myCommand = new SqlCommand(deleteScheduleQuery, myConn))
            {
                myCommand.Parameters.AddWithValue("@CodTurma", CodTurma);

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                // Execute queries to delete availability
                using (SqlCommand deleteClassroomAvailabilityCommand = new SqlCommand(deleteClassroomAvailabilityQuery, myConn))
                {
                    deleteClassroomAvailabilityCommand.Parameters.AddWithValue("@CodTurma", CodTurma);
                    deleteClassroomAvailabilityCommand.ExecuteNonQuery();
                }

                using (SqlCommand deleteTeacherAvailabilityCommand = new SqlCommand(deleteTeacherAvailabilityQuery, myConn))
                {
                    deleteTeacherAvailabilityCommand.Parameters.AddWithValue("@CodTurma", CodTurma);
                    deleteTeacherAvailabilityCommand.ExecuteNonQuery();
                }

                myConn.Close();
            }
        }
        public static void DeleteClassRoomAvailability(int CodTurma)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            string query = $"DELETE D FROM disponibilidadeSala AS D INNER JOIN horario AS H ON D.codSala = H.codSala INNER JOIN moduloFormadorTurma AS MFT ON H.codModuloFormador = MFT.codModuloFormadorTurma WHERE MFT.codTurma = @CodTurma;";
            using (SqlCommand myCommand = new SqlCommand(query, myConn))
            {
                myCommand.Parameters.AddWithValue("@CodTurma", CodTurma);

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }
        public static void DeleteTeacherAvailabilityClassGroup(int CodTurma, int CodFormador)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            string query = $"  DELETE D FROM disponibilidadeFormador AS D INNER JOIN moduloFormadorTurma AS MFC ON D.codFormador = MFC.codFormador INNER JOIN turma AS T ON MFC.codTurma = T.codTurma WHERE T.codTurma = @CodTurma AND D.codFormador = @CodFormador AND T.isActive = 1;";
            using (SqlCommand myCommand = new SqlCommand(query, myConn))
            {
                myCommand.Parameters.AddWithValue("@CodTurma", CodTurma);
                myCommand.Parameters.AddWithValue("@CodFormador", CodFormador);

                myCommand.Connection = myConn;
                myConn.Open();

                myCommand.ExecuteNonQuery();

                myConn.Close();
            }
        }

    }
}

