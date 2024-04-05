using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace FinalProject.Classes
{
    public class Teacher
    {
        public int CodFormador { get; set; }
        public int CodInscricao { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }

        /// <summary>
        ///     Função para inserir um formador
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertTeacher(int TeacherCode, int EnrollmentCode)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodFormando", TeacherCode);
            myCommand.Parameters.AddWithValue("@CodInscricao", EnrollmentCode);

            var TeacherRegisted = new SqlParameter();
            TeacherRegisted.ParameterName = "@TeacherRegisted";
            TeacherRegisted.Direction = ParameterDirection.Output;
            TeacherRegisted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(TeacherRegisted);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText =
                "TeacherRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection =
                myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            var AnswTeacherRegisted = Convert.ToInt32(myCommand.Parameters["@TeacherRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswTeacherRegisted;
        }

        /// <summary>
        ///     Função para carregar os formadores
        /// </summary>
        /// <returns></returns>
        public static List<Teacher> LoadTeachers(List<string> conditions = null)
        {
            var Teachers = new List<Teacher>();

            var query =
                "SELECT DISTINCT T.codFormador, UD.nome, UDS.foto FROM formador AS T LEFT JOIN inscricao AS I ON T.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador";

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

            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                var informacao = new Teacher();
                informacao.CodFormador = Convert.ToInt32(dr["codFormador"]);
                //informacao.CodInscricao = Convert.ToInt32(dr["codInscricao"]);
                informacao.Nome = dr["nome"].ToString();

                var relativeImagePath = "~/assets/img/default.png";
                var absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                var imageData = File.ReadAllBytes(absoluteImagePath);

                informacao.Foto = dr["foto"] == DBNull.Value
                    ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData)
                    : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                Teachers.Add(informacao);
            }

            myConn.Close();

            return Teachers;
        }

        /// <summary>
        ///     Função para carregar os dados de um formador
        /// </summary>
        /// <param name="CodFormando"></param>
        /// <returns></returns>
        public static (Teacher, User) LoadTeacher(int CodFormador)
        {
            var Teacher = new Teacher();
            var User = new User();

            var query =
                $"SELECT DISTINCT * FROM formador AS T LEFT JOIN inscricao AS I ON T.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador WHERE T.CodFormador={CodFormador}";

            var myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            var dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                Teacher.CodFormador = Convert.ToInt32(dr["codFormador"]);
                Teacher.Nome = dr["nome"].ToString();

                User.CodTipoDoc = Convert.ToInt32(dr["codTipoDoc"]);
                User.DocIdent = dr["docIdent"].ToString();
                User.DataValidade = Convert.ToDateTime(dr["dataValidadeDocIdent"]).Date;
                User.Email = dr["email"].ToString();
                User.Phone = dr["telemovel"].ToString();
                User.CodPrefix = Convert.ToInt32(dr["prefixo"]);
                User.Sexo = Convert.ToInt32(dr["sexo"]);
                User.DataNascimento = Convert.ToDateTime(dr["dataNascimento"]).Date;
                User.NIF = dr["nif"].ToString();
                User.Morada = dr["morada"].ToString();
                User.CodPais = Convert.ToInt32(dr["codPais"]);
                User.CodPostal = dr["codPostal"].ToString();
                User.CodEstadoCivil = Convert.ToInt32(dr["codEstadoCivil"]);
                User.NrSegSocial = dr["nrSegSocial"].ToString();
                User.IBAN = dr["IBAN"].ToString();
                User.Naturalidade = dr["naturalidade"].ToString();
                User.CodNacionalidade = Convert.ToInt32(dr["codNacionalidade"]);
                if (User.Foto != null)
                    User.Foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                User.CodGrauAcademico = Convert.ToInt32(dr["codGrauAcademico"]);
                User.CodSituacaoProf = Convert.ToInt32(dr["codSituacaoProfissional"]);
                User.Localidade = dr["localidade"].ToString();
                User.LifeMotto = dr["lifeMotto"].ToString();
            }

            myConn.Close();

            return (Teacher, User);
        }

        /// <summary>
        ///     Função para eliminar um formador
        /// </summary>
        /// <param name="CodUtilizador"></param>
        /// <returns></returns>
        public static int DeleteTeacher(int CodUtilizador)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"]
                .ConnectionString);
            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            var TeacherDeleted = new SqlParameter();
            TeacherDeleted.ParameterName = "@TeacherDeleted";
            TeacherDeleted.Direction = ParameterDirection.Output;
            TeacherDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(TeacherDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteTeacher";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            var AnswTeacherDeleted = Convert.ToInt32(myCommand.Parameters["@TeacherDeleted"].Value);

            myCon.Close();

            return AnswTeacherDeleted;
        }
    }
}