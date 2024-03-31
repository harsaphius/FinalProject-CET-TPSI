using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class Classroom
    {
        public int CodSala { get; set; }
        public string NrSala { get; set; }
        public int CodTipoSala { get; set; }
        public int CodLocalSala { get; set; }
        public string TipoSala { get; set; }
        public string LocalSala { get; set; }

        /// <summary>
        /// Função para inserir uma sala nova
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int InsertClassroom(Classroom Classroom)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@NrSala", Classroom.NrSala);
            myCommand.Parameters.AddWithValue("@CodTipoSala", Classroom.CodTipoSala);
            myCommand.Parameters.AddWithValue("@CodLocalSala", Classroom.CodLocalSala);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter ClassroomRegister = new SqlParameter();
            ClassroomRegister.ParameterName = "@ClassroomRegisted";
            ClassroomRegister.Direction = ParameterDirection.Output;
            ClassroomRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ClassroomRegister);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ClassroomRegister";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswClassroomRegister = Convert.ToInt32(myCommand.Parameters["@ClassroomRegisted"].Value);

            myCon.Close();

            return AnswClassroomRegister;
        }

        /// <summary>
        /// Função para carregar as salas
        /// </summary>
        /// <returns></returns>
        public static List<Classroom> LoadClassrooms()
        {
            List<Classroom> Classrooms = new List<Classroom>();
            List<string> conditions = new List<string>();

            string query = "SELECT * FROM sala AS S INNER JOIN tipoSala AS TS ON S.codTipoSala=TS.codTipoSALA INNER JOIN localSala AS LS ON S.codLocalSala=LS.codLocalSala";

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
                Classroom informacao = new Classroom();
                informacao.CodSala = dr.GetInt32(0);
                informacao.NrSala = dr.GetString(1);
                informacao.TipoSala = dr["tipoSala"].ToString();
                informacao.LocalSala = dr["localSala"].ToString();

                Classrooms.Add(informacao);
            }
            myConn.Close();

            return Classrooms;
        }




    }
}