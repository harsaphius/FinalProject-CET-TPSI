using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

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
            myCommand.Parameters.AddWithValue("@Pw", Password);

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


    }

}