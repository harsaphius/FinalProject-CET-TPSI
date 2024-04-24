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
    public class Teacher
    {
        public int CodFormador { get; set; }
        public int CodInscricao { get; set; }
        public int CodTurma { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string CodSituacao { get; set; }
        public List<Module> Modules { get; set; }
        public byte[] FotoBytes { get; set; }

        /// <summary>
        /// Função para inserir um formador
        /// </summary>
        /// <param name="values"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static int InsertTeacher(int TeacherCode, int EnrollmentCode)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodFormador", TeacherCode);
            myCommand.Parameters.AddWithValue("@CodInscricao", EnrollmentCode);

            SqlParameter TeacherRegisted = new SqlParameter();
            TeacherRegisted.ParameterName = "@TeacherRegisted";
            TeacherRegisted.Direction = ParameterDirection.Output;
            TeacherRegisted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(TeacherRegisted);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "TeacherRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            int AnswTeacherRegisted = Convert.ToInt32(myCommand.Parameters["@TeacherRegisted"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswTeacherRegisted;
        }

        /// <summary>
        /// Função para carregar os formadores
        /// </summary>
        /// <returns></returns>
        public static List<Teacher> LoadTeachers(List<string> conditions = null, string CodCourse = null, string querySub = null)
        {
            List<Teacher> Teachers = new List<Teacher>();

            string query = "SELECT DISTINCT T.codFormador, UD.nome, UDS.foto FROM formador AS T LEFT JOIN inscricao AS I ON T.codInscricao=I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador";

            if (CodCourse != null)
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
                Teacher informacao = new Teacher();
                informacao.CodFormador = Convert.ToInt32(dr["codFormador"]);
                informacao.Nome = dr["nome"].ToString();
                if (querySub != null) informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);

                string relativeImagePath = "~/assets/img/default.png";
                string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                informacao.Foto = dr["foto"] == DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData) : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                Teachers.Add(informacao);
            }
            myConn.Close();

            return Teachers;
        }

        /// <summary>
        /// Função para carregar os dados de um formador
        /// </summary>
        /// <param name="CodFormando"></param>
        /// <returns></returns>
        public static (Teacher, User) LoadTeacher(int CodFormador)
        {
            Teacher teacher = new Teacher();
            List<Module> modules = new List<Module>();
            User User = new User();

            string query = $"SELECT DISTINCT *FROM formador AS T LEFT JOIN inscricao AS I ON T.codInscricao = I.codInscricao LEFT JOIN utilizador AS U ON I.codUtilizador = U.codUtilizador LEFT JOIN utilizadorData AS UD ON U.codUtilizador = UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador = UDS.codUtilizador LEFT JOIN tipoDocIdent AS TPI ON TPI.codTipoDoc=UD.codTipoDoc LEFT JOIN situacaoProfissional AS SP ON SP.codSituacaoProfissional=UDS.codSituacaoProfissional LEFT JOIN grauAcademico AS GA ON GA.codGrauAcademico=UDS.codGrauAcademico LEFT JOIN pais AS P ON P.codPais=UDS.codPais  LEFT JOIN modulo AS M ON I.codModulo = M.codModulos WHERE T.CodFormador = { CodFormador}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                teacher.CodFormador = Convert.ToInt32(dr["codFormador"]);
                teacher.Nome = dr["nome"].ToString();

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
                    Module module = new Module();
                    module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                    if (module.SVG != null)
                    {
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
                    module.IsChecked = true;

                    modules.Add(module);
                } while (dr.Read());
            }
            else
            {
                modules = new List<Module>();
            }

            teacher.Modules = modules;


            myConn.Close();

            return (teacher, User);
        }

        /// <summary>
        /// Função para eliminar um formador
        /// </summary>
        /// <param name="CodUtilizador"></param>
        /// <returns></returns>
        public static int DeleteTeacher(int CodUtilizador)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter TeacherDeleted = new SqlParameter();
            TeacherDeleted.ParameterName = "@TeacherDeleted";
            TeacherDeleted.Direction = ParameterDirection.Output;
            TeacherDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(TeacherDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteTeacher";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswTeacherDeleted = Convert.ToInt32(myCommand.Parameters["@TeacherDeleted"].Value);

            myCon.Close();

            return AnswTeacherDeleted;
        }

        public bool TeachesModule(int CodFormador, int CodModulo)
        {
            bool teachesModule = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString))
            {
                string query = @"SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END 
                             FROM formador AS F
                             JOIN moduloFormador AS MF ON F.codFormador = MF.codFormador
                             JOIN modulo AS M ON MF.codModulo = M.codModulos
                             WHERE F.codFormador = @CodFormador
                             AND MF.codModulo = @CodModulo;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CodFormador", CodFormador);
                    command.Parameters.AddWithValue("@CodModulo", CodModulo);

                    connection.Open();
                    teachesModule = Convert.ToBoolean(command.ExecuteScalar());
                }
            }

            return teachesModule;
        }

        public static (int, string) GetTeacherByModuleAndClass(int CodModulo, int CodTurma)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString))
            {
                string query = "SELECT UD.codUtilizador,UD.nome FROM utilizadorData AS UD INNER JOIN moduloFormadorTurma AS MFT ON UD.codUtilizador = MFT.codFormador WHERE MFT.codModulo = @CodModulo AND MFT.codTurma = @CodTurma";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CodModulo", CodModulo);
                command.Parameters.AddWithValue("@CodTurma", CodTurma);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int teacherId = Convert.ToInt32(reader["codUtilizador"]);
                            string teacherName = Convert.ToString(reader["nome"]);

                            return (teacherId, teacherName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao procurar pelo professor: " + ex.Message);
                }
            }
            return (-1, null);
        }

    }
}