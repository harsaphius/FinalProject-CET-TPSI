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


    }

}