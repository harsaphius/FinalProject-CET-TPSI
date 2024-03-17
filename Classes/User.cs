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
            myCommand.Parameters.AddWithValue("@CardDate", values[7]);
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
            myCommand.Parameters.AddWithValue("@CodUser", values[0]);
            myCommand.Parameters.AddWithValue("@Sexo", Convert.ToInt32(values[10]));
            myCommand.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(values[11]));
            myCommand.Parameters.AddWithValue("@NIF", values[12]);
            myCommand.Parameters.AddWithValue("@Morada", values[13]);
            myCommand.Parameters.AddWithValue("@CodPais", Convert.ToInt32(values[14]));
            myCommand.Parameters.AddWithValue("@CodCodPostal", Convert.ToInt32(values[15]));
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", Convert.ToInt32(values[16]));
            myCommand.Parameters.AddWithValue("@NrSegSocial", values[17]);
            myCommand.Parameters.AddWithValue("@IBAN", values[18]);
            myCommand.Parameters.AddWithValue("@CodNaturalidade", Convert.ToInt32(values[19]));
            myCommand.Parameters.AddWithValue("@CodNacionalidade", Convert.ToInt32(values[20]));
            myCommand.Parameters.AddWithValue("@Foto", values[21]);
            myCommand.Parameters.AddWithValue("@CodGrauAcademico", Convert.ToInt32(values[22]));
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", Convert.ToInt32(values[23]));

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText = "UserRegisterSecondaryData"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

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