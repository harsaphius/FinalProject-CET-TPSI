using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace FinalProject.Classes
{
    [Serializable]
    public class ClassGroup
    {
        public int CodTurma { get; set; }

        public int CodCurso { get; set; }

        public string NomeCurso { get; set; }

        public int CodRegime { get; set; }

        public string Regime { get; set; }

        public int CodHorarioTurma { get; set; }

        public string HorarioTurma { get; set; }

        public string NomeTurma { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public List<Student> Students { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Module> Modules { get; set; }

        public static string LoadClassGroups(List<string> conditions = null, string order = null, string FromUser = null)
        {
            List<ClassGroup> ClassGroups = new List<ClassGroup>();

            using (SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                string query = "SELECT * FROM turma AS T LEFT JOIN curso AS C ON T.codCurso=C.codCurso WHERE T.isActive=1";

                // Adicione as condições à consulta, se houver
                if (conditions != null)
                {
                    for (int i = 0; i < conditions.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(conditions[i]))
                        {
                            switch (i)
                            {
                                case 0:
                                    query += " AND (nomeTurma LIKE '%" + conditions[i] + "%' OR nomeCurso LIKE '%" +
                                             conditions[i] + "%')";
                                    break;
                                case 1:
                                    query += $" AND FORMAT(T.dataInicio, 'yyyy-MM-dd HH:mm') >=  CONVERT(DATETIME, '{conditions[i]}', 121) ";
                                    break;
                                case 2:
                                    query += $" AND FORMAT(T.dataFim, 'yyyy-MM-dd HH:mm') <=  CONVERT(DATETIME, '{conditions[i]}', 121) ";
                                    break;
                            }
                        }
                    }
                }

                // Adicione a ordem à consulta, se houver
                if (order != null)
                {
                    query += order.Contains("ASC") ? " ORDER BY nomeTurma ASC" : order.Contains("DESC") ? " ORDER BY nomeTurma DESC" : order.Contains("nomeTurma") ? " ORDER BY nomeTurma ASC " : "";
                }

                // Adicione a cláusula WHERE para filtrar os resultados por usuário, se aplicável
                if (FromUser != null)
                {
                    query += $" LEFT JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso LEFT JOIN modulo AS M ON MC.codModulo=M.codModulos LEFT JOIN moduloFormador AS MF ON M.codModulos=MF.codModulo INNER JOIN turmaFormando AS TF ON T.codTurma=TF.codTurma WHERE codFormando = {FromUser} OR codFormador= {FromUser}";
                }

                SqlCommand myCommand = new SqlCommand(query, myConn);
                myConn.Open();

                using (SqlDataReader dr = myCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ClassGroup informacao = new ClassGroup();
                        informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);
                        informacao.CodCurso = Convert.ToInt32(dr["codCurso"]);
                        informacao.NomeCurso = dr["nomeCurso"].ToString();
                        informacao.NomeTurma = dr["nomeTurma"].ToString();
                        informacao.DataFim = Convert.ToDateTime(dr["dataFim"]).Date;
                        informacao.DataInicio = Convert.ToDateTime(dr["dataInicio"]).Date;

                        if (FromUser != null && dr.HasRows)
                        {
                            // Chamar o método para ler os módulos
                            ReadModulesFromDataReader(dr, informacao);
                        }

                        ClassGroups.Add(informacao);
                    }
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string classGroupsJson = serializer.Serialize(ClassGroups);
            return classGroupsJson;
        }

        // Método separado para ler os módulos do DataReader
        private static void ReadModulesFromDataReader(SqlDataReader dr, ClassGroup classGroup)
        {
            List<Module> Modules = new List<Module>();

            do
            {
                Module module = new Module();
                module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                module.UFCD = (dr["codUFCD"].ToString());
                module.Nome = dr["nomeModulos"].ToString();

                if (dr.GetOrdinal("ordem") >= 0)
                {
                    module.Order = Convert.ToInt32(dr["ordem"]);
                }

                Modules.Add(module);
            } while (dr.Read());

            classGroup.Modules = Modules;
        }

        public static ClassGroup LoadClassGroup(string CodTurma = null)
        {
            ClassGroup informacao = new ClassGroup();
            List<Module> Modules = new List<Module>();
            List<Student> Students = new List<Student>();
            List<Teacher> Teachers = new List<Teacher>();

            string query = $"SELECT DISTINCT * FROM turma AS T LEFT JOIN curso AS C ON T.codCurso=C.codCurso LEFT JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso LEFT JOIN modulo AS M ON MC.codModulo=M.codModulos LEFT JOIN regime AS R ON T.codRegime=R.codRegime LEFT JOIN horarioTurma AS HT ON T.codHorarioTurma=HT.codHorarioTurma WHERE T.codTurma={CodTurma}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);
                informacao.CodCurso = Convert.ToInt32(dr["codCurso"]);
                informacao.NomeCurso = dr["nomeCurso"].ToString();
                informacao.NomeTurma = dr["nomeTurma"].ToString();
                informacao.Regime = dr["nomeRegime"].ToString();
                informacao.HorarioTurma = dr["horarioTurma"].ToString();
                informacao.DataInicio = Convert.ToDateTime(dr["dataInicio"]).Date;
                informacao.DataFim = Convert.ToDateTime(dr["dataFim"]).Date;
                informacao.CodRegime = Convert.ToInt32(dr["codRegime"]);
                informacao.CodHorarioTurma = Convert.ToInt32(dr["codHorarioTurma"]);

                Module module = new Module();
                module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                module.UFCD = (dr["codUFCD"].ToString());
                module.Nome = dr["nomeModulos"].ToString();
                Modules.Add(module);
            }

            informacao.Modules = Modules;
            myConn.Close();

            string queryFormando =
                $"SELECT DISTINCT T.codFormando, UD.nome, UDS.foto,S.situacao  FROM formando AS T  LEFT JOIN turmaFormando AS TF ON T.codFormando=TF.codFormando  LEFT JOIN inscricao AS I ON TF.codFormando = I.codUtilizador LEFT JOIN situacao AS S ON I.codSituacaoFormando=S.codSituacao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador  LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador  WHERE TF.codTurma={CodTurma} AND I.codSituacaoFormando=1";

            SqlCommand myCommand3 = new SqlCommand(queryFormando, myConn);
            myConn.Open();

            dr = myCommand3.ExecuteReader();

            while (dr.Read())
            {
                Student student = new Student();
                student.CodFormando = Convert.ToInt32(dr["codFormando"]);
                student.Nome = (dr["nome"].ToString());
                student.Foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                student.CodSituacao = dr["situacao"].ToString();
                Students.Add(student);

            }

            informacao.Students = Students;
            myConn.Close();

            string queryFormador =
                $"SELECT DISTINCT T.codTurma, MFT.codFormador, MFT.codModulo, M.nomeModulos, U.codUtilizador, UD.nome,UDS.foto,S.situacao FROM turma AS T LEFT JOIN moduloFormadorTurma AS MFT ON T.codTurma = MFT.codTurma  LEFT JOIN modulo AS M ON MFT.codModulo = M.codModulos  LEFT JOIN inscricao AS I ON MFT.codFormador = I.codUtilizador  LEFT JOIN situacao AS S ON I.codSituacaoFormando=S.codSituacao LEFT JOIN utilizador AS U ON MFT.codFormador = U.codUtilizador  LEFT JOIN utilizadorData AS UD ON U.codUtilizador = UD.codUtilizador  LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador WHERE T.codTurma={CodTurma}";

            SqlCommand myCommand2 = new SqlCommand(queryFormador, myConn);
            myConn.Open();

            dr = myCommand2.ExecuteReader();

            while (dr.Read())
            {
                Teacher teacher = new Teacher();
                teacher.CodFormador = Convert.ToInt32(dr["codFormador"]);
                teacher.Nome = (dr["nome"].ToString());
                teacher.Foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                teacher.CodSituacao = dr["situacao"].ToString();


                Teachers.Add(teacher);
            }

            informacao.Teachers = Teachers;
            myConn.Close();

            return informacao;
        }

        public static List<ClassGroup> LoadClassGroupsByFormador(string codFormador)
        {
            List<ClassGroup> ClassGroups = new List<ClassGroup>();
            List<Module> Modules = new List<Module>();
            Course course = new Course();
            string query = "  SELECT DISTINCT T.*,C.nomeCurso,M.codModulos,M.codUFCD, M.nomeModulos FROM turma AS T LEFT JOIN curso AS C ON T.codCurso = C.codCurso LEFT JOIN moduloFormadorTurma AS MFT ON T.codTurma = MFT.codTurma LEFT JOIN modulo AS M ON MFT.codModulo = M.codModulos WHERE T.isActive = 1 AND codFormador = @CodFormador";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myCommand.Parameters.AddWithValue("@CodFormador", codFormador);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                ClassGroup informacao = new ClassGroup();
                informacao.CodTurma = Convert.ToInt32(dr["codTurma"]);
                informacao.CodCurso = Convert.ToInt32(dr["codCurso"]);
                informacao.NomeCurso = dr["nomeCurso"].ToString();
                informacao.NomeTurma = dr["nomeTurma"].ToString();
                informacao.DataFim = Convert.ToDateTime(dr["dataFim"]).Date;
                informacao.DataInicio = Convert.ToDateTime(dr["dataInicio"]).Date;

                Module module = new Module();
                module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                module.UFCD = (dr["codUFCD"].ToString());
                module.Nome = dr["nomeModulos"].ToString();

                Modules.Add(module);

                informacao.Modules = Modules;

                ClassGroups.Add(informacao);
            }

            myConn.Close();

            return ClassGroups;
        }

        public static int InsertClassGroup(ClassGroup ClassGroup)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@NomeTurma", ClassGroup.NomeTurma);
            myCommand.Parameters.AddWithValue("@CodCurso", ClassGroup.CodCurso);
            myCommand.Parameters.AddWithValue("@CodRegime", ClassGroup.CodRegime);
            myCommand.Parameters.AddWithValue("@CodHorarioTurma", ClassGroup.CodHorarioTurma);
            myCommand.Parameters.AddWithValue("@DataInicio", ClassGroup.DataInicio);
            myCommand.Parameters.AddWithValue("@DataFim", ClassGroup.DataFim);
            myCommand.Parameters.AddWithValue("@CreationDate", DateTime.Now);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter ClassGroupRegister = new SqlParameter();
            ClassGroupRegister.ParameterName = "@ClassGroupRegisted";
            ClassGroupRegister.Direction = ParameterDirection.Output;
            ClassGroupRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ClassGroupRegister);

            SqlParameter ClassGroupCode = new SqlParameter();
            ClassGroupCode.ParameterName = "@ClassGroupCode";
            ClassGroupCode.Direction = ParameterDirection.Output;
            ClassGroupCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(ClassGroupCode);


            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ClassGroupRegister";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswClassGroupRegister = Convert.ToInt32(myCommand.Parameters["@ClassGroupRegisted"].Value);
            int AnswClassGroupCode = Convert.ToInt32(myCommand.Parameters["@ClassGroupCode"].Value);


            myCon.Close();

            if (AnswClassGroupRegister == 1)
            {
                SqlCommand myCommand2 = new SqlCommand();
                myCommand2.CommandType = CommandType.StoredProcedure;
                myCommand2.CommandText = "StudentCourseRegister";

                myCommand2.Connection = myCon;

                myCon.Open();

                foreach (Student student in ClassGroup.Students)
                {
                    myCommand2.Parameters.Clear();
                    myCommand2.Parameters.AddWithValue("@CodTurma", AnswClassGroupCode);
                    myCommand2.Parameters.AddWithValue("@CodFormando", student.CodFormando);
                    myCommand2.Parameters.AddWithValue("@CodInscricao", student.CodInscricao);
                    myCommand2.ExecuteNonQuery();
                }

                myCon.Close();


                SqlCommand myCommand3 = new SqlCommand();
                myCommand3.CommandType = CommandType.StoredProcedure;
                myCommand3.CommandText = "TeacherModuleClassGroupRegister";
                myCommand3.Connection = myCon;

                myCon.Open();

                foreach (Teacher teacher in ClassGroup.Teachers)
                {
                    foreach (Module module in ClassGroup.Modules)
                    {
                        if (teacher.TeachesModule(teacher.CodFormador, module.CodModulo))
                        {
                            myCommand3.Parameters.Clear();
                            myCommand3.Parameters.AddWithValue("@CodTurma", AnswClassGroupCode);
                            myCommand3.Parameters.AddWithValue("@CodModulo", module.CodModulo);
                            myCommand3.Parameters.AddWithValue("@CodFormador", teacher.CodFormador);
                            myCommand3.ExecuteNonQuery();
                        }
                    }

                }

                myCon.Close();
            }

            return AnswClassGroupRegister;
        }

        public static int GetCodInscricaoByCodUtilizador(int codUtilizador, int? codCurso = null, int? codModulo = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            int codInscricao = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetCodInscricaoByCodUtilizador", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CodUtilizador", codUtilizador);

                    if (codCurso.HasValue)
                    {
                        command.Parameters.AddWithValue("@CodCurso", codCurso.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CodCurso", DBNull.Value);
                    }

                    if (codModulo.HasValue)
                    {
                        command.Parameters.AddWithValue("@CodModulo", codModulo.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CodModulo", DBNull.Value);
                    }

                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != DBNull.Value && result != null)
                    {
                        codInscricao = Convert.ToInt32(result);
                    }
                }
            }

            return codInscricao;

        }

        public static string GetClassGroupNumber(string Course, string Regime, string Horario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString;
            string turmaNumber;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if a turma with the given course exists
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM turma WHERE codCurso = @Course AND codRegime = @Regime AND codHorarioTurma = @Horario", connection);
                command.Parameters.AddWithValue("@Course", Course);
                command.Parameters.AddWithValue("@Regime", Regime);
                command.Parameters.AddWithValue("@Horario", Horario);
                int existingTurmaCount = (int)command.ExecuteScalar();

                // If turma exists, increment count, otherwise set it to 01
                if (existingTurmaCount > 0)
                {
                    // Fetch existing count and increment by 1
                    command = new SqlCommand("SELECT MAX(CAST(RIGHT(nomeTurma, 2) AS INT)) FROM turma WHERE codCurso = @Course AND codRegime = @Regime AND codHorarioTurma = @Horario", connection);
                    command.Parameters.AddWithValue("@Course", Course);
                    command.Parameters.AddWithValue("@Regime", Regime);
                    command.Parameters.AddWithValue("@Horario", Horario);
                    int count = (int)command.ExecuteScalar() + 1;
                    turmaNumber = count.ToString("00");
                }
                else
                {
                    turmaNumber = "01";
                }
            }

            return turmaNumber;
        }

        public static List<Tuple<int, int>> GetTeacherModuleForClassGroup(int classGroupId)
        {
            List<Tuple<int, int>> teacherModuleList = new List<Tuple<int, int>>();

            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString;

            // SQL query to fetch teacher and module information for the specified class group
            string query = @"SELECT codFormador, codModulo
                     FROM moduloFormadorTurma
                     WHERE codTurma = @ClassGroupId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassGroupId", classGroupId);

                    connection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int teacherCode = Convert.ToInt32(dr["codFormador"]);
                            int moduleCode = Convert.ToInt32(dr["codModulo"]);

                            // Adiciona a tupla (código do formador, código do módulo) à lista
                            teacherModuleList.Add(Tuple.Create(teacherCode, moduleCode));
                        }
                    }
                }
            }

            return teacherModuleList;
        }

        public static List<Student> GetStudentsByTurmaModuloFormador(int codTurma, int codModulo, int codFormador)
        {
            List<Student> students = new List<Student>();
            string query = @" SELECT DISTINCT S.codFormando, UD.nome,UDS.foto,A.avaliacao,MT.codFormador,MT.codModulo,MT.codTurma FROM formando AS S LEFT JOIN turmaFormando AS TF ON S.codInscricao = TF.codInscricao LEFT JOIN moduloFormadorTurma AS MT ON TF.codTurma = MT.codTurma LEFT JOIN avaliacao AS A ON MT.codModuloFormadorTurma=A.codModuloFormador LEFT JOIN utilizador AS U ON S.codFormando = U.codUtilizador LEFT JOIN utilizadorData AS UD ON U.codUtilizador = UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador = UDS.codUtilizador WHERE TF.codTurma = @CodTurma AND MT.codModulo = @CodModulo AND MT.codFormador = @CodFormador GROUP BY S.codFormando, UD.nome,UDS.foto,A.avaliacao,MT.codFormador,MT.codModulo,MT.codTurma";

            // Configure a conexão com o banco de dados
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
            {
                // Configure o comando SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adicione os parâmetros à consulta
                    command.Parameters.AddWithValue("@CodTurma", codTurma);
                    command.Parameters.AddWithValue("@CodModulo", codModulo);
                    command.Parameters.AddWithValue("@CodFormador", codFormador);

                    // Abra a conexão com o banco de dados
                    connection.Open();

                    // Execute a consulta e leia os resultados
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Itere sobre os resultados e crie objetos Student
                        while (reader.Read())
                        {
                            Student student = new Student();
                            student.CodFormando = reader.GetInt32(reader.GetOrdinal("CodFormando"));
                            student.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                            student.CodTurma = reader.GetInt32(reader.GetOrdinal("CodTurma"));
                            student.CodModulo = reader.GetInt32(reader.GetOrdinal("CodModulo"));

                            string relativeImagePath = "~/assets/img/default.png";
                            string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                            byte[] imageData = File.ReadAllBytes(absoluteImagePath);
                            student.Foto = reader["foto"] == DBNull.Value ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData) : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])reader["foto"]);

                            student.Avaliacao = GetNotaByFormandoModuloTurma(student.CodFormando, codModulo, codTurma);

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        public static decimal GetNotaByFormandoModuloTurma(int codFormando, int codModulo, int codTurma)
        {
            decimal nota = 0;

            try
            {
                string query = "SELECT avaliacao FROM avaliacao AS A INNER JOIN moduloFormadorTurma AS MFT ON A.codModuloFormador=MFT.codModuloFormadorTurma  WHERE codFormando = @codFormando AND codModulo = @codModulo AND codTurma = @codTurma";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codFormando", codFormando);
                        command.Parameters.AddWithValue("@codModulo", codModulo);
                        command.Parameters.AddWithValue("@codTurma", codTurma);

                        connection.Open();

                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            nota = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                // Por exemplo, registre-a ou lance-a novamente
                // throw;
            }

            return nota;
        }


        public static void InserirAvaliacao(int codFormando, int codModuloFormador, int codTurma, decimal avaliacao)
        {
            try
            {
                string query = "INSERT INTO avaliacao (codFormando, codModuloFormador, avaliacao) VALUES (@codFormando, @codModuloFormador, @avaliacao)";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codFormando", codFormando);
                        command.Parameters.AddWithValue("@codModuloFormador", codModuloFormador); // Supondo que seja o código do módulo do formador
                        command.Parameters.AddWithValue("@codTurma", codTurma);
                        command.Parameters.AddWithValue("@avaliacao", avaliacao);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                // Por exemplo, registre-a ou lance-a novamente
                // throw;
            }
        }

        public static int ObterCodModuloFormador(int codTurma, int codModulo, int codFormador)
        {
            int codModuloFormador = 0;

            try
            {
                string query = "SELECT codModuloFormadorTurma FROM moduloFormadorTurma WHERE codTurma = @codTurma AND codModulo = @codModulo AND codFormador = @codFormador";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@codTurma", codTurma);
                        command.Parameters.AddWithValue("@codModulo", codModulo);
                        command.Parameters.AddWithValue("@codFormador", codFormador);

                        connection.Open();

                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            codModuloFormador = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                // Por exemplo, registre-a ou lance-a novamente
                // throw;
            }

            return codModuloFormador;
        }

    }
}