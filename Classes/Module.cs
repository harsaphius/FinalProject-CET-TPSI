using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    [Serializable]
    public class Module
    {
        public int CodModulo { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public string UFCD { get; set; }
        public string Descricao { get; set; }
        public decimal Creditos { get; set; }
        public string SVG { get; set; }
        public byte[] SVGBytes { get; set; }
        public bool IsChecked { get; set; }

        /// <summary>
        /// Função para inserir um módulo novo
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertModule(Module module)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@NameModule", module.Nome);
            myCommand.Parameters.AddWithValue("@Duration", module.Duracao);
            myCommand.Parameters.AddWithValue("@UFCD", module.UFCD);
            myCommand.Parameters.AddWithValue("@Description", module.Descricao);
            myCommand.Parameters.AddWithValue("@Credits", module.Creditos);
            //myCommand.Parameters.AddWithValue("@SVG", imageBytes);
            if (module.SVG != null)
            {
                myCommand.Parameters.AddWithValue("@SVG", module.SVG);
            }
            else
            {
                myCommand.Parameters.AddWithValue("@SVG", DBNull.Value);
            }
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter ModuleRegister = new SqlParameter();
            ModuleRegister.ParameterName = "@ModuleRegisted";
            ModuleRegister.Direction = ParameterDirection.Output;
            ModuleRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleRegister);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "ModuleRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswModuleRegister = Convert.ToInt32(myCommand.Parameters["@ModuleRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswModuleRegister;
        }

        /// <summary>
        /// Função para carregar os módulos
        /// </summary>
        /// <returns></returns>
        public static List<Module> LoadModules()
        {
            List<Module> Modules = new List<Module>();
            List<string> conditions = new List<string>();

            string query = "SELECT * FROM modulo";

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
                Module informacao = new Module();
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
                    informacao.SVG = null; // or set it to any default value you prefer
                }

                Modules.Add(informacao);
            }
            myConn.Close();
            return Modules;
        }

        public static List<Module> LoadModules(int CodCurso)
        {
            List<Module> Modules = new List<Module>();
            List<string> conditions = new List<string>();

            string query = $"SELECT * FROM modulo AS M INNER JOIN moduloCurso AS MC ON M.codModulo=MC.codModulo WHERE codCurso={CodCurso}";

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
                Module informacao = new Module();
                informacao.CodModulo = dr.GetInt32(0);
                informacao.Nome = dr.GetString(1);
                informacao.Duracao = dr.GetInt32(2);
                informacao.UFCD = dr.GetString(3);
                informacao.Descricao = dr.GetString(4);
                informacao.Creditos = dr.GetDecimal(5);

                informacao.SVG = "data:image/svg+xml;base64," + Convert.ToBase64String((byte[])dr["svg"]);

                Modules.Add(informacao);
            }
            myConn.Close();
            return Modules;
        }

        public static int UpdateModule(Module module)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@ModuleID", module.CodModulo);
            myCommand.Parameters.AddWithValue("@NameModule", module.Nome);
            myCommand.Parameters.AddWithValue("@Duration", module.Duracao);
            myCommand.Parameters.AddWithValue("@UFCD", module.UFCD);
            myCommand.Parameters.AddWithValue("@Description", module.Descricao);
            myCommand.Parameters.AddWithValue("@Credits", module.Creditos);
            if (module.SVGBytes != null && module.SVGBytes.Length > 0)
            {
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = module.SVGBytes;
            }
            else
            {
                myCommand.Parameters.Add("@SVG", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter ModuleUpdated = new SqlParameter();
            ModuleUpdated.ParameterName = "@ModuleUpdated";
            ModuleUpdated.Direction = ParameterDirection.Output;
            ModuleUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleUpdated);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UpdateModule"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswModuleUpdated = Convert.ToInt32(myCommand.Parameters["@ModuleUpdated"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswModuleUpdated;
        }

        public static int DeleteModule(int CodModulo)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@ModuleID", CodModulo);

            SqlParameter ModuleDeleted = new SqlParameter();
            ModuleDeleted.ParameterName = "@ModuleDeleted";
            ModuleDeleted.Direction = ParameterDirection.Output;
            ModuleDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ModuleDeleted);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "DeleteModule"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswModuleDeleted = Convert.ToInt32(myCommand.Parameters["@ModuleDeleted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswModuleDeleted;
        }

    }
}