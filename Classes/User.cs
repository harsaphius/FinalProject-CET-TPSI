using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinalProject.Classes
{
    public class User
    {
        public int CodUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CodPerfil { get; set; }
        public bool Ativo { get; set; }
        public string Perfil { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int CodTipoDoc { get; set; }
        public string DocIdent { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodPrefix { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Sexo { get; set; }
        public string NIF { get; set; }
        public string Morada { get; set; }
        public string Localidade { get; set; }
        public int CodPais { get; set; }
        public string CodPostal { get; set; }
        public int CodEstadoCivil { get; set; }
        public string NrSegSocial { get; set; }
        public string IBAN { get; set; }
        public string Naturalidade { get; set; }
        public int CodNacionalidade { get; set; }
        public string Foto { get; set; }
        public int CodGrauAcademico { get; set; }
        public int CodSituacaoProf { get; set; }
        public string LifeMotto { get; set; }

        /// <summary>
        /// Função para determinar se o Login é permitido
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static List<int> IsLoginAllowed(string Username, string Password)
        {
            List<int> Users = new List<int>();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@User", Username); //Adicionar o valor da tb_user ao parâmetro @nome
            myCommand.Parameters.AddWithValue("@Pw", Security.EncryptString(Password));

            //Variável de Output para SP verificar se o utilizador e pw estão corretos
            SqlParameter UserExists = new SqlParameter();
            UserExists.ParameterName = "@UserExists";
            UserExists.Direction = ParameterDirection.Output;
            UserExists.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserExists);

            //Variável de Output para SP verificar o perfil
            SqlParameter AccountActive = new SqlParameter();
            AccountActive.ParameterName = "@AccountActive";
            AccountActive.Direction = ParameterDirection.Output;
            AccountActive.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(AccountActive);

            SqlParameter CodUtilizador = new SqlParameter();
            CodUtilizador.ParameterName = "@CodUtilizador";
            CodUtilizador.Direction = ParameterDirection.Output;
            CodUtilizador.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(CodUtilizador);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UserSignIn";

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswUserExist = Convert.ToInt32(myCommand.Parameters["@UserExists"].Value);
            Users.Add(AnswUserExist);
            int AnswAccountActive = Convert.ToInt32(myCommand.Parameters["@AccountActive"].Value);
            Users.Add(AnswAccountActive);
            int AnswNumiCodUser = Convert.ToInt32(myCommand.Parameters["@CodUtilizador"].Value);
            Users.Add(AnswNumiCodUser);

            myCon.Close(); //Fechar a conexão

            return Users;
        }

        /// <summary>
        /// Função para carregar um utilizador
        /// </summary>
        /// <param name="UserC"></param>
        /// <returns></returns>
        public static User LoadUser(string UserC)
        {
            string query = $"SELECT * FROM utilizador AS U LEFT JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador WHERE utilizador='{UserC}' OR email='{UserC}'";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();
            User informacao = new User();

            if (dr.Read())
            {
                informacao.CodUser = Convert.ToInt32(dr["codUtilizador"]);
                informacao.Username = dr["utilizador"].ToString();
                informacao.Email = dr["email"].ToString();
                informacao.Name = dr["nome"] == DBNull.Value ? " " : dr["nome"].ToString();
                informacao.CodTipoDoc = (int)(dr["codTipoDoc"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codTipoDoc"]));
                informacao.DocIdent = dr["docIdent"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                informacao.DataValidade = (DateTime)(dr["dataValidadeDocIdent"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(dr["dataValidadeDocIdent"]).Date);
                informacao.CodPrefix = (int)(dr["prefixo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["prefixo"]));
                informacao.Phone = dr["telemovel"] == DBNull.Value ? " " : dr["telemovel"].ToString();
                informacao.Sexo = (int)(dr["sexo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["sexo"]));
                informacao.DataNascimento = (DateTime)(dr["dataNascimento"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(dr["dataNascimento"]).Date);
                informacao.NIF = dr["nif"] == DBNull.Value ? " " : dr["nif"].ToString();
                informacao.Morada = dr["morada"] == DBNull.Value ? " " : dr["morada"].ToString();
                informacao.Localidade = dr["localidade"] == DBNull.Value ? " " : dr["localidade"].ToString();
                informacao.CodPais = (int)(dr["codPais"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codPais"]));
                informacao.CodPostal = dr["codPostal"] == DBNull.Value ? " " : dr["codPostal"].ToString();
                informacao.CodEstadoCivil = (int)(dr["codEstadoCivil"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codEstadoCivil"]));
                informacao.NrSegSocial = dr["nrSegSocial"] == DBNull.Value ? " " : dr["nrSegSocial"].ToString();
                informacao.IBAN = dr["IBAN"] == DBNull.Value ? " " : dr["IBAN"].ToString();
                informacao.Naturalidade = dr["naturalidade"] == DBNull.Value ? " " : dr["naturalidade"].ToString();
                informacao.CodNacionalidade = (int)(dr["codNacionalidade"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codNacionalidade"]));
                informacao.Foto = informacao.Foto = dr["foto"] == DBNull.Value ? "default_image_url" : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                informacao.CodGrauAcademico = (int)(dr["codGrauAcademico"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codGrauAcademico"]));
                informacao.CodSituacaoProf = (int)(dr["codSituacaoProfissional"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codSituacaoProfissional"]));
                informacao.LifeMotto = dr["lifemotto"] == DBNull.Value ? " " : dr["lifemotto"].ToString();
            }

            myConn.Close();

            return informacao;
        }

        /// <summary>
        /// Função para registar um utilizador
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static (int, int) RegisterUser(List<string> Values)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodePerfil", Values[0]);
            myCommand.Parameters.AddWithValue("@CompleteName", Values[1]);
            myCommand.Parameters.AddWithValue("@User", Values[2]);
            myCommand.Parameters.AddWithValue("@Email", Values[3]);
            myCommand.Parameters.AddWithValue("@Pw", Classes.Security.EncryptString(Values[4]));
            myCommand.Parameters.AddWithValue("@CodCardType", Values[5]);
            myCommand.Parameters.AddWithValue("@CardNumber", Values[6]);
            myCommand.Parameters.AddWithValue("@CardDate", Convert.ToDateTime(Values[7]));
            myCommand.Parameters.AddWithValue("@Prefix", Values[8]);
            myCommand.Parameters.AddWithValue("@PhoneNumber", Values[9]);

            SqlParameter UserRegister = new SqlParameter();
            UserRegister.ParameterName = "@UserRegister";
            UserRegister.Direction = ParameterDirection.Output;
            UserRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserRegister);

            SqlParameter UserCode = new SqlParameter();
            UserCode.ParameterName = "@UserCode";
            UserCode.Direction = ParameterDirection.Output;
            UserCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserCode);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UserRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswUserRegister = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);
            int AnswUserCode = Convert.ToInt32(myCommand.Parameters["@UserCode"].Value);


            myCon.Close(); //Fechar a conexão

            return (AnswUserRegister, AnswUserCode);
        }

        /// <summary>
        /// Função para completar o registo de um utilizador
        /// </summary>
        /// <param name="values"></param>
        /// <param name="anexos"></param>
        public static int CompleteRegisterUser(List<string> Values, List<FileControl> Anexos)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", Values[0]);
            myCommand.Parameters.AddWithValue("@Sexo", Convert.ToInt32(Values[1]));
            myCommand.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(Values[2]));
            myCommand.Parameters.AddWithValue("@NIF", Values[3]);
            myCommand.Parameters.AddWithValue("@Morada", Values[4]);
            myCommand.Parameters.AddWithValue("@CodPais", Convert.ToInt32(Values[5]));
            myCommand.Parameters.AddWithValue("@CodPostal", Values[6]);
            myCommand.Parameters.AddWithValue("@Freguesia", Values[7]);
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", Convert.ToInt32(Values[8]));
            myCommand.Parameters.AddWithValue("@NrSegSocial", Values[9]);
            myCommand.Parameters.AddWithValue("@IBAN", Values[10]);
            myCommand.Parameters.AddWithValue("@Naturalidade", Values[11]);
            myCommand.Parameters.AddWithValue("@CodNacionalidade", Convert.ToInt32(Values[12]));
            byte[] fotoBytes = Convert.FromBase64String(Values[13]);
            myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;
            myCommand.Parameters.AddWithValue("@CodGrauAcademico", Convert.ToInt32(Values[14]));
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", Convert.ToInt32(Values[15]));
            myCommand.Parameters.AddWithValue("@LifeMotto", Values[16]);

            SqlParameter UserRegister = new SqlParameter();
            UserRegister.ParameterName = "@UserRegister";
            UserRegister.Direction = ParameterDirection.Output;
            UserRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserRegister);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UserRegisterSecondaryData"; //Comando SQL Insert para inserir os dados acima na respetiva tabela
            int AnswUserRegisterComplete = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            myCon.Close(); //Fechar a conexão

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UserRegisterDocuments";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (FileControl anexo in Anexos)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodUtilizador", Convert.ToInt32(Values[0]));
                myCommand2.Parameters.AddWithValue("@NomeFicheiro", anexo.FileName);
                myCommand2.Parameters.AddWithValue("@ExtensaoFicheiro", anexo.ContentType);
                myCommand2.Parameters.AddWithValue("@Ficheiro", anexo.FileBytes);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();

            return AnswUserRegisterComplete;
        }

        public static (int, string) DetermineUtilizador(string User)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@User", User);

            SqlParameter UserCode = new SqlParameter();
            UserCode.ParameterName = "@UserCode";
            UserCode.Direction = ParameterDirection.Output;
            UserCode.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserCode);

            SqlParameter Username = new SqlParameter();
            Username.ParameterName = "@Username";
            Username.Direction = ParameterDirection.Output;
            Username.SqlDbType = SqlDbType.VarChar;
            Username.Size = 50;

            myCommand.Parameters.Add(Username);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DetermineUtilizador";

            myCommand.Connection = myCon;
            myCon.Open();

            myCommand.ExecuteNonQuery();

            int AnswUserCode = Convert.ToInt32(myCommand.Parameters["@UserCode"].Value);
            string AnswUsername = myCommand.Parameters["@Username"].Value.ToString();

            myCon.Close(); //Fechar a conexão

            return (AnswUserCode, AnswUsername);
        }

        public static (int, int) CheckEmail(string Email)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@GoogleEmail", Email);

            SqlParameter UserExists = new SqlParameter();
            UserExists.ParameterName = "@UserExists";
            UserExists.Direction = ParameterDirection.Output;
            UserExists.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserExists);

            SqlParameter AccountActive = new SqlParameter();
            AccountActive.ParameterName = "@AccountActive";
            AccountActive.Direction = ParameterDirection.Output;
            AccountActive.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(AccountActive);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "CheckEmailFromGoogle";

            myCommand.Connection = myCon;
            myCon.Open();

            myCommand.ExecuteNonQuery();

            int AnswUserExist = Convert.ToInt32(myCommand.Parameters["@UserExists"].Value);
            int AnswAccountActive = Convert.ToInt32(myCommand.Parameters["@AccountActive"].Value);

            myCon.Close(); //Fechar a conexão

            return (AnswUserExist, AnswAccountActive);
        }

        public static List<int> DetermineUserProfile(int UserCode)
        {
            List<int> UserProfiles = new List<int>();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@UserCode", UserCode);

            SqlParameter UserProfileListParam = new SqlParameter();
            UserProfileListParam.ParameterName = "@UserProfilesList";
            UserProfileListParam.Direction = ParameterDirection.Output;
            UserProfileListParam.SqlDbType = SqlDbType.NVarChar;
            UserProfileListParam.Size = -1;

            myCommand.Parameters.Add(UserProfileListParam);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DetermineUserProfile";

            myCommand.Connection = myCon;
            myCon.Open();

            myCommand.ExecuteNonQuery();

            string userProfileListString = (string)myCommand.Parameters["@UserProfilesList"].Value;

            if (!string.IsNullOrEmpty(userProfileListString))
            {
                string[] profileCodes = userProfileListString.Split(',');
                foreach (string code in profileCodes)
                {
                    UserProfiles.Add(int.Parse(code));
                }
            }

            myCon.Close(); //Fechar a conexão

            return UserProfiles;
        }
    }
}