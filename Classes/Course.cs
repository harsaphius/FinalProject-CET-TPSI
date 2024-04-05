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
        public DateTime DataCriacao { get; set; }
        public int Duracao { get; set; }
        public int DuracaoEstagio { get; set; }
        public int CodQNQ { get; set; }
        public List<Module> Modules { get; set; }

        /// <summary>
        ///     Função para inserir um curso completo
        /// </summary>
        /// <param name="Course"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static int InsertCourse(Course Course, List<int> modules)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString);

            var myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@NameCourse", Course.Nome);
            myCommand.Parameters.AddWithValue("@CodTipoCurso", Course.CodTipoCurso);
            myCommand.Parameters.AddWithValue("@CodArea", Course.CodArea);
            myCommand.Parameters.AddWithValue("@CodRef", Course.CodRef);
            myCommand.Parameters.AddWithValue("@CodQNQ", Course.CodQNQ);
            myCommand.Parameters.AddWithValue("@CreationDate", DateTime.Now);
            myCommand.Parameters.AddWithValue("@Duration", Course.Duracao);
            if (Course.DuracaoEstagio != null) myCommand.Parameters.AddWithValue("@Internship", Course.DuracaoEstagio);


            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var CourseRegister = new SqlParameter();
            CourseRegister.ParameterName = "@CourseRegisted";
            CourseRegister.Direction = ParameterDirection.Output;
            CourseRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseRegister);

            var CourseNumber = new SqlParameter();
            CourseNumber.ParameterName = "@CourseNumber";
            CourseNumber.Direction = ParameterDirection.Output;
            CourseNumber.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseNumber);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "CourseRegister";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            var AnswCourseRegister = Convert.ToInt32(myCommand.Parameters["@CourseRegisted"].Value);
            var AnswCourseNumber = Convert.ToInt32(myCommand.Parameters["@CourseNumber"].Value);

            myCon.Close();

            var myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "CourseModulesRegister";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (var moduleID in modules)
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
        ///     Função para carregar a listagem de cursos, podendo ou não conter filtros
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static List<Course> LoadCourses(List<string> conditions = null)
        {
            var Courses = new List<Course>();

            var query = "SELECT * FROM curso WHERE isActive=1";


            if (conditions != null)
                for (var i = 0; i < conditions.Count; i++)
                    if (!string.IsNullOrEmpty(conditions[i]))
                        switch (i)
                        {
                            case 0:
                                query += " AND nomeCurso LIKE '%" + conditions[i] + "%'";
                                break;
                            case 1:
                                query += " AND codRef =" + conditions[i];
                                break;
                        }

            //if (order != null)
            //{
            //    if (order.Contains("ASC"))
            //    {
            //        query += " ORDER BY nomeModulos ASC";
            //    }
            //    else if (order.Contains("DESC"))
            //    {
            //        query += " ORDER BY nomeModulos DESC";
            //    }
            //}

            //if (conditions != null)
            //{
            //    bool isFirstCondition = true;

            //    for (int i = 0; i < conditions.Count; i++)
            //    {
            //        if (!string.IsNullOrEmpty(conditions[i]))
            //        {
            //            if (isFirstCondition)
            //            {
            //                query += " WHERE ";
            //                isFirstCondition = false;
            //            }
            //            else
            //            {
            //                query += " AND ";
            //            }

            //            switch (i)
            //            {
            //                case 0:
            //                    query += "nomeCurso LIKE '%" + conditions[i] + "%'";
            //                    break;
            //                case 1:
            //                    query += "codArea =" + conditions[i];
            //                    break;
            //                case 2:
            //                    query += "codTipoCurso =" + conditions[i];
            //                    break;
            //                case 3:
            //                    query += "auditRow = '" + conditions[i] + "'";
            //                    break;
            //                default:
            //                    // Handle additional conditions if needed
            //                    break;
            //            }
            //        }
            //    }


            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                var informacao = new Course();
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
        ///     Função para carregar um curso com todas as suas características pelo se codCurso
        /// </summary>
        /// <param name="CodCurso"></param>
        /// <returns></returns>
        public static Course CompleteCourse(int CodCurso)
        {
            var CompleteCourse = new Course();
            var ModulesCourse = new List<Module>();

            var query =
                $"SELECT * FROM curso AS C LEFT JOIN tipoCurso AS TC ON C.codTipoCurso=TC.codTipoCurso LEFT JOIN area AS A ON C.codArea=A.codArea LEFT JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso LEFT JOIN modulo AS M ON MC.codModulo=M.codModulos WHERE C.codCurso={CodCurso}";

            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

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

            if (dr.HasRows)
                do
                {
                    var module = new Module();
                    module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                    if (module.SVG != null)
                    {
                        module.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);
                    }
                    else
                    {
                        var relativeImagePath = "~/assets/img/small-logos/default.svg";
                        var absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                        var imageData = File.ReadAllBytes(absoluteImagePath);

                        module.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String(imageData);
                    }

                    module.UFCD = dr["CodUFCD"].ToString();
                    module.Nome = dr["NomeModulos"].ToString();

                    ModulesCourse.Add(module);
                } while (dr.Read());
            else
                ModulesCourse = new List<Module>();

            CompleteCourse.Modules = ModulesCourse;

            myConn.Close();

            return CompleteCourse;
        }

        /// <summary>
        ///     Função para efetuar o update de um curso existente
        /// </summary>
        /// <param name="Course"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static int UpdateCourse(Course Course, List<int> modules)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString);

            var myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@CourseID", Course.CodCurso);
            myCommand.Parameters.AddWithValue("@NameCourse", Course.Nome);
            myCommand.Parameters.AddWithValue("@CodTipoCurso", Course.CodTipoCurso);
            myCommand.Parameters.AddWithValue("@CodArea", Course.CodArea);
            myCommand.Parameters.AddWithValue("@CodRef", Course.CodRef);
            myCommand.Parameters.AddWithValue("@CodQNQ", Course.CodQNQ);
            myCommand.Parameters.AddWithValue("@Duration", Course.Duracao);
            if (Course.DuracaoEstagio != null) myCommand.Parameters.AddWithValue("@Internship", Course.DuracaoEstagio);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var CourseUpdated = new SqlParameter();
            CourseUpdated.ParameterName = "@CourseUpdated";
            CourseUpdated.Direction = ParameterDirection.Output;
            CourseUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseUpdated);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateCourse";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            var AnswCourseUpdated = Convert.ToInt32(myCommand.Parameters["@CourseUpdated"].Value);

            myCon.Close();

            if (AnswCourseUpdated == 1)
            {
                //Deleting all modules already on the course
                var query = $"DELETE FROM moduloCurso WHERE codCurso = {Course.CodCurso}";
                var myCommand2 = new SqlCommand(query, myCon);
                myCon.Open();
                myCommand2.ExecuteNonQuery();
                myCon.Close();


                var myCommand3 = new SqlCommand();
                myCommand3.CommandType = CommandType.StoredProcedure;
                myCommand3.CommandText = "UpdateCourseModules";

                myCommand3.Connection = myCon;

                myCon.Open();

                foreach (var moduleID in modules)
                {
                    myCommand3.Parameters.Clear();
                    myCommand3.Parameters.AddWithValue("@CodCourse", Course.CodCurso);
                    myCommand3.Parameters.AddWithValue("@CodMod", moduleID);
                    myCommand3.ExecuteNonQuery();
                }

                myCon.Close();
            }

            return AnswCourseUpdated;
        }

        /// <summary>
        ///     Função para efetuar delete de um curso
        /// </summary>
        /// <param name="CodCurso"></param>
        /// <returns></returns>
        public static int DeleteCourse(int CodCurso)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CourseID", CodCurso);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var CourseDeleted = new SqlParameter();
            CourseDeleted.ParameterName = "@CourseDeleted";
            CourseDeleted.Direction = ParameterDirection.Output;
            CourseDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CourseDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteCourse";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            var AnswCourseDeleted = Convert.ToInt32(myCommand.Parameters["@CourseDeleted"].Value);

            myCon.Close();

            return AnswCourseDeleted;
        }
    }
}