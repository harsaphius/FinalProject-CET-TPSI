using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Module
    {
        public int CodModulo { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public string UFCD { get; set; }
        public string Descricao { get; set; }
        public decimal Creditos { get; set; }
        public string SVG { get; set; }

        public static int InsertModule(List<string> values, byte[] imageBytes)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@NameModule", values[0]);
            myCommand.Parameters.AddWithValue("@Duration", Convert.ToInt32(values[1]));
            myCommand.Parameters.AddWithValue("@UFCD", values[2]);
            myCommand.Parameters.AddWithValue("@Description", values[3]);
            myCommand.Parameters.AddWithValue("@Credits", Convert.ToDecimal(values[4]));
            myCommand.Parameters.AddWithValue("@SVG", imageBytes);

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

        public static List<Module> LoadModules()
        {
            List<Module> Modules = new List<Module>();
            List<string> conditions = new List<string>();

            string query = "select * from modulo";

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

    }
}