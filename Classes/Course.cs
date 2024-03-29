using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    [Serializable]
    public class Course
    {
        public int CodCurso { get; set; }
        public string Nome { get; set; }
        public int CodTipoCurso { get; set; }
        public int CodArea { get; set; }
        public string TipoCurso { get; set; }
        public string Area { get; set; }
        public string CodRef { get; set; }
        public int CodQNQ { get; set; }
        public List<Module> Modules { get; set; }

        public static int InsertCourse(Course Course, List<int> modules)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@NameCourse", Course.Nome);
            myCommand.Parameters.AddWithValue("@CodTipoCurso", Course.CodTipoCurso);
            myCommand.Parameters.AddWithValue("@CodArea", Course.CodArea);
            myCommand.Parameters.AddWithValue("@CodRef", Course.CodRef);
            myCommand.Parameters.AddWithValue("@CodQNQ", Course.CodQNQ);
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

            string query = "SELECT * FROM curso";

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
                informacao.CodCurso = Convert.ToInt32(dr["codCurso"]);
                informacao.Nome = dr["nomeCurso"].ToString();
                informacao.CodTipoCurso = Convert.ToInt32(dr["codArea"]);
                informacao.CodRef = dr["codRef"].ToString();
                informacao.CodQNQ = Convert.ToInt32(dr["codQNQ"]);

                Courses.Add(informacao);
            }

            myConn.Close();

            return Courses;
        }

        public static Course CompleteCourse(int CodCurso)
        {
            Course CompleteCourse = new Course();
            List<Module> ModulesCourse = new List<Module>();

            string query = $"SELECT * FROM curso AS C INNER JOIN tipoCurso AS TC ON C.codTipoCurso=TC.codTipoCurso INNER JOIN area AS A ON C.codArea=A.codArea INNER JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso INNER JOIN  modulo AS M ON MC.codModulo=M.codModulos WHERE C.codCurso={CodCurso}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                CompleteCourse.CodCurso = Convert.ToInt32(dr["codCurso"]);
                CompleteCourse.Nome = dr["nomeCurso"].ToString();
                CompleteCourse.CodArea = Convert.ToInt32(dr["codArea"]);
                CompleteCourse.Area = dr["nomeArea"].ToString();
                CompleteCourse.CodTipoCurso = Convert.ToInt32(dr["codTipoCurso"]);
                CompleteCourse.TipoCurso = dr["nomeTipoLongo"].ToString();
                CompleteCourse.CodRef = dr["codRef"].ToString();
                CompleteCourse.CodQNQ = Convert.ToInt32(dr["codQNQ"]);
            }

            do
            {
                Module module = new Module();
                module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                module.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);
                module.UFCD = (dr["CodUFCD"].ToString()); // Substitua "moduleID" pelo nome correto da coluna na sua tabela
                module.Nome = dr["NomeModulos"].ToString(); // Substitua "moduleName" pelo nome correto da coluna na sua tabela
                                                            // Adicione outros atributos do módulo conforme necessário
                ModulesCourse.Add(module);
            } while (dr.Read());

            CompleteCourse.Modules = ModulesCourse;

            myConn.Close();

            return CompleteCourse;
        }

        public static int UpdateCourse(Course Course, List<int> modules)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CourseID", Course.CodCurso);
            myCommand.Parameters.AddWithValue("@NameCourse", Course.Nome);
            myCommand.Parameters.AddWithValue("@CodTipoCurso", Course.CodTipoCurso);
            myCommand.Parameters.AddWithValue("@CodArea", Course.CodArea);
            myCommand.Parameters.AddWithValue("@CodRef", Course.CodRef);
            myCommand.Parameters.AddWithValue("@CodQNQ", Course.CodQNQ);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter CourseUpdated = new SqlParameter();
            CourseUpdated.ParameterName = "@CourseUpdated";
            CourseUpdated.Direction = ParameterDirection.Output;
            CourseUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseUpdated);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UpdateCourse"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery();

            int AnswCourseUpdated = Convert.ToInt32(myCommand.Parameters["@CourseUpdated"].Value);

            myCon.Close(); //Fechar a conexão

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UpdateCourseModules";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (int moduleID in modules)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodCourse", Course.CodCurso);
                myCommand2.Parameters.AddWithValue("@CodMod", moduleID); // Set the value of CodMod parameter
                myCommand2.ExecuteNonQuery();//Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            }

            myCon.Close();

            return AnswCourseUpdated;
        }

        public static Course ReturnCompleteCourse(int CodCurso)
        {
            Course CompleteCourse = new Course();
            List<Module> ModulesCourse = Classes.Module.LoadModules(CodCurso);

            string query = $"SELECT * FROM curso AS C INNER JOIN tipoCurso AS TC ON C.codTipoCurso=TC.codTipoCurso INNER JOIN area AS A ON C.codArea=A.codArea INNER JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso INNER JOIN  modulo AS M ON MC.codModulo=M.codModulos WHERE C.codCurso={CodCurso}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                CompleteCourse.CodCurso = Convert.ToInt32(dr["codCurso"]);
                CompleteCourse.Nome = dr["nomeCurso"].ToString();
                CompleteCourse.Area = dr["nomeArea"].ToString();
                CompleteCourse.TipoCurso = dr["nomeTipoLongo"].ToString();
                CompleteCourse.CodRef = dr["codRef"].ToString();
                CompleteCourse.CodQNQ = Convert.ToInt32(dr["codQNQ"]);

            }

            CompleteCourse.Modules = ModulesCourse;

            myConn.Close();

            return CompleteCourse;
        }
    }
}