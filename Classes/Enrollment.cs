﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    [Serializable]
    public class Enrollment
    {
        public int CodInscricao { get; set; }
        public int CodUtilizador { get; set; }
        public int CodSituacao { get; set; }
        public string Situacao { get; set; }
        public DateTime DataInscricao { get; set; }
        public int CodCurso { get; set; }
        public int CodModulo { get; set; }
        public string NomeCurso { get; set; }

        /// <summary>
        /// Função para inserir uma inscrição nova de formando
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static (int, int) InsertEnrollmentStudent(Enrollment enrollment)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", enrollment.CodUtilizador);
            myCommand.Parameters.AddWithValue("@CodSituacao", enrollment.CodSituacao);
            myCommand.Parameters.AddWithValue("@DataInscricao", DateTime.Now);
            myCommand.Parameters.AddWithValue("@CodCurso", enrollment.CodCurso);
            myCommand.Parameters.AddWithValue("@CodModulo", DBNull.Value);

            SqlParameter EnrollmentRegister = new SqlParameter();
            EnrollmentRegister.ParameterName = "@EnrollmentRegisted";
            EnrollmentRegister.Direction = ParameterDirection.Output;
            EnrollmentRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentRegister);

            SqlParameter EnrollmentCode = new SqlParameter();
            EnrollmentCode.ParameterName = "@EnrollmentCode";
            EnrollmentCode.Direction = ParameterDirection.Output;
            EnrollmentCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentCode);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "EnrollmentRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswEnrollmentRegister = Convert.ToInt32(myCommand.Parameters["@EnrollmentRegisted"].Value);
            int AnswEnrollmentCode = Convert.ToInt32(myCommand.Parameters["@EnrollmentCode"].Value);

            myCon.Close(); //Fechar a conexão

            return (AnswEnrollmentRegister, AnswEnrollmentCode);
        }

        /// <summary>
        /// Função para inserir uma inscrição nova de formador
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static (int, int) InsertEnrollmentTeacher(Enrollment enrollment)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", enrollment.CodUtilizador);
            myCommand.Parameters.AddWithValue("@CodSituacao", enrollment.CodSituacao);
            myCommand.Parameters.AddWithValue("@DataInscricao", DateTime.Now);
            myCommand.Parameters.AddWithValue("@CodCurso", DBNull.Value);
            myCommand.Parameters.AddWithValue("@CodModulo", enrollment.CodModulo);

            SqlParameter EnrollmentRegister = new SqlParameter();
            EnrollmentRegister.ParameterName = "@EnrollmentRegisted";
            EnrollmentRegister.Direction = ParameterDirection.Output;
            EnrollmentRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentRegister);

            SqlParameter EnrollmentCode = new SqlParameter();
            EnrollmentCode.ParameterName = "@EnrollmentCode";
            EnrollmentCode.Direction = ParameterDirection.Output;
            EnrollmentCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentCode);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "EnrollmentRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswEnrollmentRegister = Convert.ToInt32(myCommand.Parameters["@EnrollmentRegisted"].Value);
            int AnswEnrollmentCode = Convert.ToInt32(myCommand.Parameters["@EnrollmentCode"].Value);

            myCon.Close(); //Fechar a conexão

            return (AnswEnrollmentRegister, AnswEnrollmentCode);
        }

        public static void DeleteEnrollmentStudent(int CodUtilizador)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteEnrollment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
                    command.Parameters.AddWithValue("@IsTeacher", 0);
                    command.Parameters.AddWithValue("@CodModulo", DBNull.Value);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error deleting enrollments: " + ex.Message);
                    }
                }
            }
        }

        public static void DeleteEnrollmentTeacher(int CodUtilizador)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteEnrollment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    command.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
                    command.Parameters.AddWithValue("@IsTeacher", 1);
                    command.Parameters.AddWithValue("@CodCurso", DBNull.Value);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exception
                        throw new Exception("Error deleting enrollments: " + ex.Message);
                    }
                }
            }
        }

    }
}