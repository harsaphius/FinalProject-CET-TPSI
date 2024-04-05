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
    public class Module
    {
        public int CodModulo { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UFCD { get; set; }
        public string Descricao { get; set; }
        public decimal Creditos { get; set; }
        public string SVG { get; set; }
        public byte[] SVGBytes { get; set; }
        public bool IsChecked { get; set; }


        /// <summary>
        ///     Função para inserir um módulo novo
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertModule(Module module)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@NameModule", module.Nome);
            myCommand.Parameters.AddWithValue("@Duration", module.Duracao);
            myCommand.Parameters.AddWithValue("@CreationDate", DateTime.Now);
            myCommand.Parameters.AddWithValue("@UFCD", module.UFCD);
            myCommand.Parameters.AddWithValue("@Description", module.Descricao);
            myCommand.Parameters.AddWithValue("@Credits", module.Creditos);
            if (module.SVG != null)
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = module.SVG;
            else
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var ModuleRegister = new SqlParameter();
            ModuleRegister.ParameterName = "@ModuleRegisted";
            ModuleRegister.Direction = ParameterDirection.Output;
            ModuleRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleRegister);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText =
                "ModuleRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection =
                myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            var AnswModuleRegister = Convert.ToInt32(myCommand.Parameters["@ModuleRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswModuleRegister;
        }

        /// <summary>
        ///     Função para carregar os módulos
        /// </summary>
        /// <returns></returns>
        public static List<Module> LoadModules(List<string> conditions = null, string order = null)
        {
            var Modules = new List<Module>();

            var query = "SELECT * FROM modulo WHERE isActive=1";

            if (conditions != null)
                for (var i = 0; i < conditions.Count; i++)
                    if (!string.IsNullOrEmpty(conditions[i]))
                        switch (i)
                        {
                            case 0:
                                query += " AND (nomeModulos LIKE '%" + conditions[i] + "%' OR codUFCD LIKE '%" +
                                         conditions[i] + "%')";
                                break;
                            case 1:
                                query += " AND duracao =" + conditions[i];
                                break;
                        }

            if (order != null)
            {
                if (order.Contains("ASC"))
                    query += " ORDER BY nomeModulos ASC";
                else if (order.Contains("DESC")) query += " ORDER BY nomeModulos DESC";
            }

            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                var informacao = new Module();
                informacao.CodModulo = dr.GetInt32(0);
                informacao.Nome = dr.GetString(1);
                informacao.Duracao = dr.GetInt32(2);
                informacao.UFCD = dr.GetString(3);
                informacao.Descricao = dr.GetString(4);
                informacao.Creditos = dr.GetDecimal(5);

                if (dr["svg"] != DBNull.Value)
                {
                    informacao.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);
                }
                else
                {
                    var relativeImagePath = "~/assets/img/small-logos/default.svg";
                    var absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                    var imageData = File.ReadAllBytes(absoluteImagePath);

                    informacao.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String(imageData);
                }

                Modules.Add(informacao);
            }

            myConn.Close();
            return Modules;
        }

        /// <summary>
        ///     Funnção para carregar os módulos de um curso específico
        /// </summary>
        /// <param name="CodCurso"></param>
        /// <returns></returns>
        public static List<Module> LoadModules(int CodCurso)
        {
            var Modules = new List<Module>();

            var query =
                $"SELECT * FROM modulo AS M INNER JOIN moduloCurso AS MC ON M.codModulos=MC.codModulo WHERE codCurso={CodCurso}";

            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                var informacao = new Module();
                informacao.CodModulo = dr.GetInt32(0);
                informacao.Nome = dr.GetString(1);
                informacao.Duracao = dr.GetInt32(2);
                informacao.UFCD = dr.GetString(3);
                informacao.Descricao = dr.GetString(4);
                informacao.Creditos = dr.GetDecimal(5);
                if (!dr.IsDBNull(dr.GetOrdinal("svg")))
                {
                    informacao.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);
                }
                else
                {
                    var relativeImagePath = "~/assets/img/small-logos/default.svg";
                    var absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                    var imageData = File.ReadAllBytes(absoluteImagePath);

                    informacao.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String(imageData);
                }

                informacao.IsChecked = CheckIfModuleIsInCourse(CodCurso, informacao.CodModulo);

                Modules.Add(informacao);
            }

            myConn.Close();

            return Modules;
        }

        public static bool CheckIfModuleIsInCourse(int CodCurso, int moduleID)
        {
            var completeCourse = Course.CompleteCourse(CodCurso);

            foreach (var module in completeCourse.Modules)
                if (module.CodModulo == moduleID)
                    return true;
            return false;
        }

        public static int CalculateTotalCourseDuration(List<int> moduleIDs)
        {
            var totalDuration = 0;

            foreach (var moduleID in moduleIDs) totalDuration += GetUFCDDurationByModuleID(moduleID);

            return totalDuration;
        }

        public static int GetUFCDDurationByModuleID(int moduleID)
        {
            var duration = 0;

            var connectionString =
                ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT duracao FROM modulo WHERE codModulos = @ModuleID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModuleID", moduleID);

                    connection.Open();

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value) duration = Convert.ToInt32(result);
                }
            }

            return duration;
        }

        /// <summary>
        ///     Função para update de um módulo
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static int UpdateModule(Module module)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@ModuleID", module.CodModulo);
            myCommand.Parameters.AddWithValue("@NameModule", module.Nome);
            myCommand.Parameters.AddWithValue("@Duration", module.Duracao);
            myCommand.Parameters.AddWithValue("@UFCD", module.UFCD);
            myCommand.Parameters.AddWithValue("@Description", module.Descricao);
            myCommand.Parameters.AddWithValue("@Credits", module.Creditos);
            if (module.SVGBytes != null && module.SVGBytes.Length > 0)
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = module.SVGBytes;
            else
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var ModuleUpdated = new SqlParameter();
            ModuleUpdated.ParameterName = "@ModuleUpdated";
            ModuleUpdated.Direction = ParameterDirection.Output;
            ModuleUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleUpdated);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateModule";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            var AnswModuleUpdated = Convert.ToInt32(myCommand.Parameters["@ModuleUpdated"].Value);

            myCon.Close();

            return AnswModuleUpdated;
        }

        /// <summary>
        ///     Função para efetuar delete de um módulo
        /// </summary>
        /// <param name="CodModulo"></param>
        /// <returns></returns>
        public static int DeleteModule(int CodModulo)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@ModuleID", CodModulo);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var ModuleDeleted = new SqlParameter();
            ModuleDeleted.ParameterName = "@ModuleDeleted";
            ModuleDeleted.Direction = ParameterDirection.Output;
            ModuleDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteModule";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            var AnswModuleDeleted = Convert.ToInt32(myCommand.Parameters["@ModuleDeleted"].Value);

            myCon.Close();

            return AnswModuleDeleted;
        }
    }
}