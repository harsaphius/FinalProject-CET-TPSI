using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Student
    {
        public int CodFormando { get; set; }
        public int CodInscricao { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }

        /// <summary>
        /// Função para inserir um formando
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertStudent(int StudentCode, int EnrollmentCode)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodFormando", StudentCode);
            myCommand.Parameters.AddWithValue("@CodInscricao", EnrollmentCode);

            SqlParameter StudentRegisted = new SqlParameter();
            StudentRegisted.ParameterName = "@StudentRegisted";
            StudentRegisted.Direction = ParameterDirection.Output;
            StudentRegisted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(StudentRegisted);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "StudentRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            int AnswStudentRegisted = Convert.ToInt32(myCommand.Parameters["@StudentRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswStudentRegisted;
        }

        /// <summary>
        /// Função para carregar os formandos
        /// </summary>
        /// <returns></returns>
        public static List<Student> LoadStudents()
        {
            List<Student> Students = new List<Student>();
            List<string> conditions = new List<string>();

            string query = "SELECT DISTINCT codFormando,nome,foto FROM formando AS F INNER JOIN inscricao AS I ON F.codInscricao=I.codInscricao INNER JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador INNER JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador INNER JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador";

            //// Decisões para colocar ou não os filtros dentro da string query
            //if (!string.IsNullOrEmpty(search_string))
            //{
            //    conditions.Add($"moeda.nome LIKE '%{search_string}%'");
            //}
            //if (grade != 0)
            //{
            //    conditions.Add($"moeda_estado.cod_estado = {grade}");
            //}
            //if (coin_type != 0)
            //{
            //    conditions.Add($"moeda.cod_tipo = {coin_type}");
            //}
            //if (status == 0)
            //{
            //    conditions.Add($"moeda_estado.deleted = {status}");
            //}
            //else if (status == 1)
            //{
            //    conditions.Add($"moeda_estado.deleted = {status}");
            //}
            //if (conditions.Count > 0)
            //{
            //    query += " WHERE " + string.Join(" AND ", conditions);
            //}
            //if (!string.IsNullOrEmpty(sort_order))
            //{
            //    query += " ORDER BY moeda_estado.valor_atual " + sort_order;
            //}

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Student informacao = new Student();
                informacao.CodFormando = Convert.ToInt32(dr["codFormando"]);
                //informacao.CodInscricao = Convert.ToInt32(dr["codInscricao"]);
                informacao.Nome = dr["nome"].ToString();

                informacao.Foto = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                Students.Add(informacao);
            }
            myConn.Close();

            return Students;
        }
    }
}