using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Enrollment
    {
        public int CodInscricao { get; set; }
        public int CodUtilizador { get; set; }
        public int CodSituacao { get; set; }
        public string Situacao { get; set; }
        public DateTime DataInscricao { get; set; }
        public int CodCurso { get; set; }
        public string NomeCurso { get; set; }

        /// <summary>
        ///     Função para inserir uma inscrição nova
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static (int, int) InsertEnrollment(Enrollment enrollment)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", enrollment.CodUtilizador);
            myCommand.Parameters.AddWithValue("@CodSituacao", enrollment.CodSituacao);
            myCommand.Parameters.AddWithValue("@DataInscricao", DateTime.Now);
            myCommand.Parameters.AddWithValue("@CodCurso", enrollment.CodCurso);

            var EnrollmentRegister = new SqlParameter();
            EnrollmentRegister.ParameterName = "@EnrollmentRegisted";
            EnrollmentRegister.Direction = ParameterDirection.Output;
            EnrollmentRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentRegister);

            var EnrollmentCode = new SqlParameter();
            EnrollmentCode.ParameterName = "@EnrollmentCode";
            EnrollmentCode.Direction = ParameterDirection.Output;
            EnrollmentCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(EnrollmentCode);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText =
                "EnrollmentRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection =
                myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            var AnswEnrollmentRegister = Convert.ToInt32(myCommand.Parameters["@EnrollmentRegisted"].Value);
            var AnswEnrollmentCode = Convert.ToInt32(myCommand.Parameters["@EnrollmentCode"].Value);

            myCon.Close(); //Fechar a conexão

            return (AnswEnrollmentRegister, AnswEnrollmentCode);
        }
    }
}