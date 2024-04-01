using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace FinalProject.Classes
{
    public class Student
    {
        public int CodFormando { get; set; }
        public int CodInscricao { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }

        /// <summary>
        /// Função para inserir um formando
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertStudent(int StudentCode, int EnrollmentCode)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodFormando", StudentCode);
            myCommand.Parameters.AddWithValue("@CodInscricao", EnrollmentCode);

            SqlParameter StudentRegisted = new SqlParameter();
            StudentRegisted.ParameterName = "@StudentRegisted";
            StudentRegisted.Direction = ParameterDirection.Output;
            StudentRegisted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(StudentRegisted);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "StudentRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            int AnswStudentRegisted = Convert.ToInt32(myCommand.Parameters["@StudentRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswStudentRegisted;
        }

        /// <summary>
        /// Função para carregar os formandos
        /// </summary>
        /// <returns></returns>
        public static List<Student> LoadStudents(List<string> conditions = null)
        {
            List<Student> Students = new List<Student>();

            string query = "SELECT DISTINCT F.codFormando, UD.nome, UDS.foto FROM formando AS F LEFT JOIN inscricao AS I ON F.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador";

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
                Student informacao = new Student();
                informacao.CodFormando = Convert.ToInt32(dr["codFormando"]);
                //informacao.CodInscricao = Convert.ToInt32(dr["codInscricao"]);
                informacao.Nome = dr["nome"].ToString();

                string relativeImagePath = "~/assets/img/default.png";
                string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                informacao.Foto = dr["foto"] == DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData) : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                Students.Add(informacao);
            }
            myConn.Close();

            return Students;
        }

        /// <summary>
        /// Função para carregar os dados de um formando
        /// </summary>
        /// <param name="CodFormando"></param>
        /// <returns></returns>
        public static (Student, User) LoadStudent(int CodFormando)
        {
            Student Student = new Student();
            User User = new User();

            string query = $"SELECT DISTINCT * FROM formando AS F LEFT JOIN inscricao AS I ON F.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador WHERE F.CodFormando={CodFormando}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                Student.CodFormando = Convert.ToInt32(dr["codFormando"]);
                //Student.CodInscricao = Convert.ToInt32(dr["codInscricao"]);
                Student.Nome = dr["nome"].ToString();

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

            return (Student, User);
        }

        /// <summary>
        /// Função para eliminar um formando
        /// </summary>
        /// <param name="CodUtilizador"></param>
        /// <returns></returns>
        public static int DeleteStudent(int CodUtilizador)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter StudentDeleted = new SqlParameter();
            StudentDeleted.ParameterName = "@StudentDeleted";
            StudentDeleted.Direction = ParameterDirection.Output;
            StudentDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(StudentDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteStudent";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswStudentDeleted = Convert.ToInt32(myCommand.Parameters["@StudentDeleted"].Value);

            myCon.Close();

            return AnswStudentDeleted;
        }
    }
}