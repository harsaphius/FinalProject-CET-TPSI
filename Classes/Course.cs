using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Course
    {
        public int CodCurso { get; set; }
        public string Nome { get; set; }
        public int CodTipoCurso { get; set; }
        public int CodArea { get; set; }
        public string CodRef { get; set; }
        public int CodQNQ { get; set; }

        public static int InsertCourse(List<string> values, List<int> modules)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@NameCourse", values[0]);
            myCommand.Parameters.AddWithValue("@CodTipoCurso", Convert.ToInt32(values[1]));
            myCommand.Parameters.AddWithValue("@CodArea", Convert.ToInt32(values[2]));
            myCommand.Parameters.AddWithValue("@CodRef", values[3]);
            myCommand.Parameters.AddWithValue("@CodQNQ", Convert.ToInt32(values[4]));
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter CourseRegister = new SqlParameter();
            CourseRegister.ParameterName = "@CourseRegisted";
            CourseRegister.Direction = ParameterDirection.Output;
            CourseRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseRegister);

            SqlParameter CourseNumber = new SqlParameter();
            CourseNumber.ParameterName = "@CourseNumber";
            CourseNumber.Direction = ParameterDirection.Output;
            CourseNumber.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseNumber);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "CourseRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery();

            int AnswCourseRegister = Convert.ToInt32(myCommand.Parameters["@CourseRegisted"].Value);
            int AnswCourseNumber = Convert.ToInt32(myCommand.Parameters["@CourseNumber"].Value);

            myCon.Close(); //Fechar a conexão


            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "CourseModulesRegister";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (int moduleID in modules)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodCourse", AnswCourseNumber);
                myCommand2.Parameters.AddWithValue("@CodMod", moduleID); // Set the value of CodMod parameter
                myCommand2.ExecuteNonQuery();//Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            }
           
            myCon.Close();

            return AnswCourseRegister;
        }

        public static List<Course> LoadCourses()
        {
            List<Course> Courses = new List<Course>();
            List<string> conditions = new List<string>();

            string query = "select * from curso";

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
                Course informacao = new Course();
                informacao.CodCurso = dr.GetInt32(0);
                informacao.Nome = dr.GetString(1);
                informacao.CodTipoCurso = dr.GetInt32(2);
                informacao.CodRef = dr.GetString(3);
                informacao.CodQNQ = dr.GetInt32(4);

                Courses.Add(informacao);
            }
            myConn.Close();

            return Courses;

        }

    }
}