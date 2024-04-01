using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

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

        /// <summary>
        /// Função para inserir um curso completo
        /// </summary>
        /// <param name="Course"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static int InsertCourse(Course Course, List<int> modules)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
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

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "CourseRegister";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            int AnswCourseRegister = Convert.ToInt32(myCommand.Parameters["@CourseRegisted"].Value);
            int AnswCourseNumber = Convert.ToInt32(myCommand.Parameters["@CourseNumber"].Value);

            myCon.Close();

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "CourseModulesRegister";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (int moduleID in modules)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodCourse", AnswCourseNumber);
                myCommand2.Parameters.AddWithValue("@CodMod", moduleID);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();

            return AnswCourseRegister;
        }

        /// <summary>
        /// Função para carregar a listagem de cursos, podendo ou não conter filtros
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static List<Course> LoadCourses(List<string> conditions = null)
        {
            List<Course> Courses = new List<Course>();

            string query = "SELECT * FROM curso WHERE isActive=1";
            if (conditions != null)
            {
                bool isFirstCondition = true;

                for (int i = 0; i < conditions.Count; i++)
                {
                    if (!string.IsNullOrEmpty(conditions[i]))
                    {
                        if (isFirstCondition)
                        {
                            query += " WHERE ";
                            isFirstCondition = false;
                        }
                        else
                        {
                            query += " AND ";
                        }

                        switch (i)
                        {
                            case 0:
                                query += "nomeCurso LIKE '%" + conditions[i] + "%'";
                                break;
                            case 1:
                                query += "codArea =" + conditions[i];
                                break;
                            case 2:
                                query += "codTipoCurso =" + conditions[i];
                                break;
                            case 3:
                                query += "auditRow = '" + conditions[i] + "'";
                                break;
                            default:
                                // Handle additional conditions if needed
                                break;
                        }
                    }
                }
            }

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

        /// <summary>
        /// Função para carregar um curso com todas as suas características pelo se codCurso
        /// </summary>
        /// <param name="CodCurso"></param>
        /// <returns></returns>
        public static Course CompleteCourse(int CodCurso)
        {
            Course CompleteCourse = new Course();
            List<Module> ModulesCourse = new List<Module>();

            string query = $"SELECT * FROM curso AS C INNER JOIN tipoCurso AS TC ON C.codTipoCurso=TC.codTipoCurso INNER JOIN area AS A ON C.codArea=A.codArea INNER JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso INNER JOIN modulo AS M ON MC.codModulo=M.codModulos WHERE C.codCurso={CodCurso}";

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
                if (module.SVG != null){
                    module.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);
                }
                else
                {
                    string relativeImagePath = "~/assets/img/small-logos/default.svg";
                    string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                    byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                    module.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String(imageData);
                }
                module.UFCD = (dr["CodUFCD"].ToString());
                module.Nome = dr["NomeModulos"].ToString();

                ModulesCourse.Add(module);
            } while (dr.Read());

            CompleteCourse.Modules = ModulesCourse;

            myConn.Close();

            return CompleteCourse;
        }

        /// <summary>
        /// Função para efetuar o update de um curso existente
        /// </summary>
        /// <param name="Course"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static int UpdateCourse(Course Course, List<int> modules)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
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

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateCourse";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            int AnswCourseUpdated = Convert.ToInt32(myCommand.Parameters["@CourseUpdated"].Value);

            myCon.Close();

            //Deleting all modules already on the course
            string query = $"DELETE FROM moduloCurso WHERE codCurso = {Course.CodCurso}";
            SqlCommand myCommand2 = new SqlCommand(query, myCon);
            myCon.Open();
            myCommand2.ExecuteNonQuery();
            myCon.Close();


            SqlCommand myCommand3 = new SqlCommand();
            myCommand3.CommandType = CommandType.StoredProcedure;
            myCommand3.CommandText = "UpdateCourseModules";

            myCommand3.Connection = myCon;

            myCon.Open();

            foreach (int moduleID in modules)
            {
                myCommand3.Parameters.Clear();
                myCommand3.Parameters.AddWithValue("@CodCourse", Course.CodCurso);
                myCommand3.Parameters.AddWithValue("@CodMod", moduleID);
                myCommand3.ExecuteNonQuery();
            }

            myCon.Close();

            return AnswCourseUpdated;
        }

    }
}