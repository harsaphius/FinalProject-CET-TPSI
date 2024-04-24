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
    public class Student
    {
        public int CodFormando { get; set; }
        public int CodInscricao { get; set; }
        public int CodTurma { get; set; }
        public string CodSituacao { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public byte[] FotoBytes { get; set; }
        public decimal Avaliacao { get; set; }
        public int CodModulo { get; set; }


        public List<Course> Courses { get; set; }

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
        public static List<Student> LoadStudents(List<string> conditions = null, string CodCourse = null, string querySub = null)
        {
            List<Student> Students = new List<Student>();

            string query = "SELECT DISTINCT F.codFormando, UD.nome, UDS.foto FROM formando AS F LEFT JOIN inscricao AS I ON F.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador";

            if (!String.IsNullOrEmpty(CodCourse))
            {
                query += $" WHERE I.codCurso={CodCourse}";
            }

            if (querySub != null)
            {
                query = querySub;
            }

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
                if (querySub != null) informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);

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
            List<Course> courses = new List<Course>();

            string query = $"SELECT DISTINCT * FROM formando AS F LEFT JOIN inscricao AS I ON F.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador LEFT JOIN tipoDocIdent AS TPI ON TPI.codTipoDoc=UD.codTipoDoc LEFT JOIN situacaoProfissional AS SP ON SP.codSituacaoProfissional=UDS.codSituacaoProfissional LEFT JOIN grauAcademico AS GA ON GA.codGrauAcademico=UDS.codGrauAcademico LEFT JOIN pais AS P ON P.codPais=UDS.codPais LEFT JOIN curso AS C ON I.codCurso = C.codCurso WHERE F.CodFormando={CodFormando}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                Student.CodFormando = Convert.ToInt32(dr["codFormando"]);
                //Student.CodInscricao = Convert.ToInt32(dr["codInscricao"]);
                Student.Nome = dr["nome"].ToString();

                User.CodUser = Convert.ToInt32(dr["codUtilizador"]);
                User.Username = dr["utilizador"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                User.Email = dr["email"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                User.Nome = dr["nome"] == DBNull.Value ? " " : dr["nome"].ToString();
                User.CodTipoDoc = (int)(dr["codTipoDoc"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codTipoDoc"]));
                User.TipoDoc = dr["tipoDocumentoIdent"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                User.DocIdent = dr["docIdent"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                User.DataValidade = (DateTime)(dr["dataValidadeDocIdent"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataValidadeDocIdent"]).Date);
                User.CodPrefix = (int)(dr["prefixo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["prefixo"]));
                User.Phone = dr["telemovel"] == DBNull.Value ? " " : dr["telemovel"].ToString();
                User.Sexo = (int)(dr["sexo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["sexo"]));
                User.DataNascimento = (DateTime)(dr["dataNascimento"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataNascimento"]).Date);
                User.NIF = dr["nif"] == DBNull.Value ? " " : dr["nif"].ToString();
                User.Morada = dr["morada"] == DBNull.Value ? " " : dr["morada"].ToString();
                User.Localidade = dr["localidade"] == DBNull.Value ? " " : dr["localidade"].ToString();
                User.CodPais = (int)(dr["codPais"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codPais"]));
                User.CodPostal = dr["codPostal"] == DBNull.Value ? " " : dr["codPostal"].ToString();
                User.CodEstadoCivil =
                    (int)(dr["codEstadoCivil"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codEstadoCivil"]));
                User.NrSegSocial = dr["nrSegSocial"] == DBNull.Value ? " " : dr["nrSegSocial"].ToString();
                User.IBAN = dr["IBAN"] == DBNull.Value ? " " : dr["IBAN"].ToString();
                User.Naturalidade = dr["naturalidade"] == DBNull.Value ? " " : dr["naturalidade"].ToString();
                User.CodNacionalidade = (int)(dr["codNacionalidade"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codNacionalidade"]));
                User.Nacionalidade = dr["nacionalidade"].ToString();

                string relativeImagePath = "~/assets/img/default.png";
                string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                User.Foto = dr["foto"] == DBNull.Value
                    ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData)
                    : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                User.CodGrauAcademico = (int)(dr["codGrauAcademico"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codGrauAcademico"]));
                User.GrauAcademico = dr["grauAcademico"].ToString();
                User.CodSituacaoProf = (int)(dr["codSituacaoProfissional"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codSituacaoProfissional"]));
                User.SituacaoProf = dr["situacaoProfissional"].ToString();
                User.LifeMotto = dr["lifemotto"] == DBNull.Value ? " " : dr["lifemotto"].ToString();
            }

            if (dr.HasRows)
            {
                do
                {
                    Course course = new Course();
                    course.CodCurso = Convert.ToInt32(dr["codCurso"]);
                    course.Nome = (dr["nomeCurso"].ToString());
                    course.IsChecked = true;

                    courses.Add(course);
                } while (dr.Read());
            }
            else
            {
                courses = new List<Course>();
            }

            Student.Courses = courses;

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

        public static int ChangeSituationForFormandoInTurma(int codFormando, int codTurma, int newSituationCode)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            string selectInscricaoQuery = "SELECT codInscricao FROM turmaFormando WHERE codFormando = @CodFormando AND codTurma = @CodTurma";

            int codInscricao = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand selectInscricaoCommand = new SqlCommand(selectInscricaoQuery, connection))
                {
                    selectInscricaoCommand.Parameters.AddWithValue("@CodFormando", codFormando);
                    selectInscricaoCommand.Parameters.AddWithValue("@CodTurma", codTurma);

                    connection.Open();

                    object result = selectInscricaoCommand.ExecuteScalar();

                    if (result != null)
                    {
                        codInscricao = Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine($"Não foi possível encontrar a inscrição do formando com código {codFormando} na turma com código {codTurma}.");
                        return -1;
                    }
                }
            }

            string updateSituationQuery = @"
                                                                    UPDATE inscricao 
                                                                    SET codSituacaoFormando = @NewSituationCode 
                                                                    WHERE codInscricao = @CodInscricao;

                                                                    UPDATE situacaoFormando 
                                                                    SET codSituacao = @NewSituationCode 
                                                                    WHERE codInscricao = @CodInscricao;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand updateSituationCommand = new SqlCommand(updateSituationQuery, connection))
                {
                    updateSituationCommand.Parameters.AddWithValue("@NewSituationCode", newSituationCode);
                    updateSituationCommand.Parameters.AddWithValue("@CodInscricao", codInscricao);

                    connection.Open();

                    int rowsAffected = updateSituationCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}