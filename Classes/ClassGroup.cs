using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class ClassGroup
    {
        public int CodTurma { get; set; }

        public int CodCurso { get; set; }

        public string NomeTurma { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public static List<ClassGroup> LoadClassGroups(List<string> conditions = null)
        {
            List<ClassGroup> ClassGroups = new List<ClassGroup>();

            string query = "SELECT * FROM turma";

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
                ClassGroup informacao = new ClassGroup();
                informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);
                informacao.CodCurso = Convert.ToInt32(dr["codCurso"]);
                informacao.NomeTurma = dr["nomeTurma"].ToString();
                informacao.DataFim = Convert.ToDateTime(dr["dataFim"]);
                informacao.DataInicio = Convert.ToDateTime(dr["dataInicio"]);

                ClassGroups.Add(informacao);
            }
            myConn.Close();

            return ClassGroups;
        }

    }
}