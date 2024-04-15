using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
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

        public static List<ClassGroup> LoadClassGroups(List<string> conditions = null, string order = null, string FromUser = null)
        {
            List<ClassGroup> ClassGroups = new List<ClassGroup>();
            List<Module> Modules = new List<Module>();

            string query = "SELECT * FROM turma AS T LEFT JOIN curso AS C ON T.codCurso=C.codCurso ";

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
                                query += " AND T.dataInicio >= '" + conditions[i] + "'";
                                break;
                            case 2:
                                query += " AND T.dataFim <= '" + conditions[i] + "'";
                                break;
                        }
                    }
                }
            }

            if (order != null)
            {
                query += order.Contains("ASC") ? " ORDER BY codTurma ASC" : order.Contains("DESC") ? " ORDER BY codTurma DESC" : order.Contains("nomeTurma") ? " ORDER BY nomeTurma ASC " : "";
            }

            if (FromUser != null)
            {
                query += $" LEFT JOIN moduloCurso AS MC ON C.codCurso=MC.codCurso LEFT JOIN modulo AS M ON MC.codModulo=M.codModulos LEFT JOIN moduloFormador AS MF ON M.codModulos=MF.codModulo INNER JOIN turmaFormando AS TF ON T.codTurma=TF.codTurma WHERE codFormando = {FromUser} OR codFormador= {FromUser}";
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
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

                if (FromUser != null && dr.HasRows)
                {
                    do
                    {
                        Module module = new Module();
                        module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                        module.UFCD = (dr["CodUFCD"].ToString());
                        module.Nome = dr["NomeModulos"].ToString();

                        Modules.Add(module);
                    } while (dr.Read());
                }
                else
                {
                    Modules = new List<Module>();
                }

                informacao.Modules = Modules;

                ClassGroups.Add(informacao);
            }

            myConn.Close();

            return ClassGroups;
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

                Module module = new Module();
                module.CodModulo = Convert.ToInt32(dr["codModulos"]);
                module.UFCD = (dr["codUFCD"].ToString());
                module.Nome = dr["nomeModulos"].ToString();
                Modules.Add(module);
            }

            informacao.Modules = Modules;
            myConn.Close();

            string queryFormando =
                $"SELECT DISTINCT T.codFormando, UD.nome, UDS.foto,S.situacao  FROM formando AS T  LEFT JOIN turmaFormando AS TF ON T.codFormando=TF.codFormando  LEFT JOIN inscricao AS I ON TF.codFormando = I.codUtilizador LEFT JOIN situacao AS S ON I.codSituacaoFormando=S.codSituacao LEFT JOIN utilizador AS U ON I.codUtilizador=U.codUtilizador  LEFT JOIN utilizadorData as UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary as UDS ON UD.codUtilizador=UDS.codUtilizador  WHERE TF.codTurma={CodTurma}";

            SqlCommand myCommand3 = new SqlCommand(queryFormando, myConn);
            myConn.Open();

            dr = myCommand3.ExecuteReader();

            while (dr.Read())
            {
                Student student = new Student();
                student.CodFormando = Convert.ToInt32(dr["codFormando"]);
                student.Nome = (dr["nome"].ToString());
                student.Foto = dr["foto"].ToString();
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
                teacher.Foto = dr["foto"].ToString();
                teacher.CodSituacao = dr["situacao"].ToString();

                Teachers.Add(teacher);
            }

            informacao.Teachers = Teachers;
            myConn.Close();



            return informacao;
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

        public static int GetCodCursoByCodTurma(int codTurma)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString;

            int codCurso = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT codCurso FROM turma WHERE codTurma = @CodTurma";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CodTurma", codTurma);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != DBNull.Value && result != null)
                    {
                        codCurso = Convert.ToInt32(result);
                    }
                }
            }

            return codCurso;
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

    }
}