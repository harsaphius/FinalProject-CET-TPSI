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

        public static List<int> IsLoginAllowed(string Username, string Password)
        {
            List<int> Users = new List<int>();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@User", Username); //Adicionar o valor da tb_user ao parâmetro @nome
            if (Username == "SuAdmin")
            {
                myCommand.Parameters.AddWithValue("@Pw", Password);
            }
            else
            {
                myCommand.Parameters.AddWithValue("@Pw", Security.EncryptString(Password));
            }

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

            //Variável de Output para SP verificar o perfil
            SqlParameter NumiAdmin = new SqlParameter();
            NumiAdmin.ParameterName = "@SuAdmin";
            NumiAdmin.Direction = ParameterDirection.Output;
            NumiAdmin.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(NumiAdmin);

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
            int AnswNumiAdmin = Convert.ToInt32(myCommand.Parameters["@SuAdmin"].Value);
            Users.Add(AnswNumiAdmin);
            int AnswNumiCodUser = Convert.ToInt32(myCommand.Parameters["@CodUtilizador"].Value);
            Users.Add(AnswNumiCodUser);

            myCon.Close(); //Fechar a conexão

            return Users;

        }
        public static User LoadUser(string UserC)
        {
            string query = $"SELECT * FROM utilizador AS U LEFT JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador WHERE utilizador='{UserC}'";

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
                informacao.DataValidade = (DateTime)(dr["dataValidadeDocIdent"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(dr["dataValidadeDocIdent"]));
                informacao.CodPrefix = (int)(dr["prefixo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["prefixo"]));
                informacao.Phone = dr["telemovel"] == DBNull.Value ? " " : dr["telemovel"].ToString();
                informacao.Sexo = (int)(dr["sexo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["sexo"]));
                informacao.DataNascimento = (DateTime)(dr["dataNascimento"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(dr["dataNascimento"]));
                informacao.NIF = dr["nif"] == DBNull.Value ? " " : dr["nif"].ToString();
                informacao.Morada = dr["morada"] == DBNull.Value ? " " : dr["morada"].ToString();
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
            }


            myConn.Close();

            return informacao;
        }
        public static int registerUser(List<string> values)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@CodePerfil", values[0]);
            myCommand.Parameters.AddWithValue("@CompleteName", values[1]);
            myCommand.Parameters.AddWithValue("@User", values[2]);
            myCommand.Parameters.AddWithValue("@Email", values[3]);
            myCommand.Parameters.AddWithValue("@Pw", Classes.Security.EncryptString(values[4]));
            myCommand.Parameters.AddWithValue("@CodCardType", values[5]);
            myCommand.Parameters.AddWithValue("@CardNumber", values[6]);
            myCommand.Parameters.AddWithValue("@CardDate", Convert.ToDateTime(values[7]));
            myCommand.Parameters.AddWithValue("@Prefix", values[8]);
            myCommand.Parameters.AddWithValue("@PhoneNumber", values[9]);

            SqlParameter UserRegister = new SqlParameter();
            UserRegister.ParameterName = "@UserRegister";
            UserRegister.Direction = ParameterDirection.Output;
            UserRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserRegister);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UserRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            int AnswUserRegister = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswUserRegister;

        }
        public static void completeRegisterUser(List<string> values, List<FileControl> anexos)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@CodUtilizador", values[0]);
            myCommand.Parameters.AddWithValue("@Sexo", Convert.ToInt32(values[8]));
            myCommand.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(values[9]));
            myCommand.Parameters.AddWithValue("@NIF", values[10]);
            myCommand.Parameters.AddWithValue("@Morada", values[11]);
            myCommand.Parameters.AddWithValue("@CodPais", Convert.ToInt32(values[12]));
            myCommand.Parameters.AddWithValue("@CodPostal", values[13]);
            myCommand.Parameters.AddWithValue("@Freguesia", values[14]);
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", Convert.ToInt32(values[15]));
            myCommand.Parameters.AddWithValue("@NrSegSocial", values[16]);
            myCommand.Parameters.AddWithValue("@IBAN", values[17]);
            myCommand.Parameters.AddWithValue("@Naturalidade", values[18]);
            myCommand.Parameters.AddWithValue("@CodNacionalidade", Convert.ToInt32(values[19]));
            byte[] fotoBytes = Convert.FromBase64String(values[20]);
            myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;
            myCommand.Parameters.AddWithValue("@CodGrauAcademico", Convert.ToInt32(values[21]));
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", Convert.ToInt32(values[22]));

            SqlParameter UserRegister = new SqlParameter();
            UserRegister.ParameterName = "@UserRegister";
            UserRegister.Direction = ParameterDirection.Output;
            UserRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserRegister);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UserRegisterSecondaryData"; //Comando SQL Insert para inserir os dados acima na respetiva tabela
            int AnswUserRegister = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            myCon.Close(); //Fechar a conexão

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UserRegisterDocuments";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (FileControl anexo in anexos)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodUtilizador", Convert.ToInt32(values[0]));
                myCommand2.Parameters.AddWithValue("@NomeFicheiro", anexo.FileName);
                myCommand2.Parameters.AddWithValue("@ExtensaoFicheiro", anexo.ContentType);
                myCommand2.Parameters.AddWithValue("@Ficheiro", anexo.FileBytes);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();
        }

        public static void updateRegisterUser(List<string> values, List<FileControl> anexos)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString); //Definir a conexão à base de dados

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL 
            myCommand.Parameters.AddWithValue("@CodUtilizador", values[0]);
            myCommand.Parameters.AddWithValue("@CompleteName", values[1]);
            myCommand.Parameters.AddWithValue("@Email", values[2]);
            myCommand.Parameters.AddWithValue("@CodCardType", values[3]);
            myCommand.Parameters.AddWithValue("@CardNumber", values[4]);
            myCommand.Parameters.AddWithValue("@CardDate", Convert.ToDateTime(values[5]));
            myCommand.Parameters.AddWithValue("@Prefix", values[6]);
            myCommand.Parameters.AddWithValue("@PhoneNumber", values[7]);
            myCommand.Parameters.AddWithValue("@Sexo", Convert.ToInt32(values[8]));
            myCommand.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(values[9]));
            myCommand.Parameters.AddWithValue("@NIF", values[10]);
            myCommand.Parameters.AddWithValue("@Morada", values[11]);
            myCommand.Parameters.AddWithValue("@CodPais", Convert.ToInt32(values[12]));
            myCommand.Parameters.AddWithValue("@CodPostal", values[13]);
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", Convert.ToInt32(values[14]));
            myCommand.Parameters.AddWithValue("@NrSegSocial", values[15]);
            myCommand.Parameters.AddWithValue("@IBAN", values[16]);
            myCommand.Parameters.AddWithValue("@Naturalidade", (values[17]));
            myCommand.Parameters.AddWithValue("@CodNacionalidade", Convert.ToInt32(values[18]));
            byte[] fotoBytes = Convert.FromBase64String(values[19]);
            myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;
            myCommand.Parameters.AddWithValue("@CodGrauAcademico", Convert.ToInt32(values[20]));
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", Convert.ToInt32(values[21]));

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UpdateUserRegister"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection = myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados

            myCon.Close(); //Fechar a conexão

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UserRegisterDocuments";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (FileControl anexo in anexos)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodUtilizador", Convert.ToInt32(values[0]));
                myCommand2.Parameters.AddWithValue("@NomeFicheiro", anexo.FileName);
                myCommand2.Parameters.AddWithValue("@ExtensaoFicheiro", anexo.ContentType);
                myCommand2.Parameters.AddWithValue("@Ficheiro", anexo.FileBytes);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();
        }
    }

}