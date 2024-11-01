﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace FinalProject.Classes
{
    [Serializable]
    public class User
    {
        public int CodUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CodPerfil { get; set; }
        public bool Ativo { get; set; }
        public string Perfil { get; set; }
        public string Nome { get; set; }
        public string Phone { get; set; }
        public int CodTipoDoc { get; set; }
        public string TipoDoc { get; set; }
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
        public string Nacionalidade { get; set; }
        public string Foto { get; set; }
        public byte[] FotoBytes { get; set; }
        public int CodGrauAcademico { get; set; }
        public string GrauAcademico { get; set; }
        public int CodSituacaoProf { get; set; }
        public string SituacaoProf { get; set; }
        public string LifeMotto { get; set; }
        public List<int> UserProfiles { get; set; }

        /// <summary>
        /// Função para determinar se o Login é permitido
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static List<int> IsLoginAllowed(string Username, string Password)
        {
            List<int> Users = new List<int>();

            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["ProjetoFinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

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

            myCommand.Connection =
                myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
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
        public static User LoadUser(string UserC = null, string CodUser = null)
        {
            string query =
                $"SELECT * FROM utilizador AS U LEFT JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador LEFT JOIN tipoDocIdent AS TPI ON TPI.codTipoDoc=UD.codTipoDoc LEFT JOIN situacaoProfissional AS SP ON SP.codSituacaoProfissional=UDS.codSituacaoProfissional LEFT JOIN grauAcademico AS GA ON GA.codGrauAcademico=UDS.codGrauAcademico LEFT JOIN pais AS P ON P.codPais=UDS.codPais WHERE utilizador='{UserC}' OR email='{UserC}'";

            if (CodUser != null)
            {
                query =
                $"SELECT * FROM utilizador AS U LEFT JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador INNER JOIN tipoDocIdent AS TPI ON TPI.codTipoDoc=UD.codTipoDoc INNER JOIN situacaoProfissional AS SP ON SP.codSituacaoProfissional=UDS.codSituacaoProfissional INNER JOIN grauAcademico AS GA ON GA.codGrauAcademico=UDS.codGrauAcademico INNER JOIN pais AS P ON P.codPais=UDS.codPais WHERE U.codUtilizador={CodUser}";
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();
            User informacao = new User();

            if (dr.Read())
            {
                informacao.CodUser = Convert.ToInt32(dr["codUtilizador"]);
                informacao.Username = dr["utilizador"] == DBNull.Value ? " " : dr["utilizador"].ToString();
                informacao.Email = dr["email"] == DBNull.Value ? " " : dr["email"].ToString();
                informacao.Nome = dr["nome"] == DBNull.Value ? " " : dr["nome"].ToString();
                informacao.CodTipoDoc = (int)(dr["codTipoDoc"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codTipoDoc"]));
                informacao.TipoDoc = dr["tipoDocumentoIdent"] == DBNull.Value ? " " : dr["tipoDocumentoIdent"].ToString();
                informacao.DocIdent = dr["docIdent"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                informacao.DataValidade = (DateTime)(dr["dataValidadeDocIdent"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataValidadeDocIdent"]).Date);
                informacao.CodPrefix = (int)(dr["prefixo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["prefixo"]));
                informacao.Phone = dr["telemovel"] == DBNull.Value ? " " : dr["telemovel"].ToString();
                informacao.Sexo = (int)(dr["sexo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["sexo"]));
                informacao.DataNascimento = (DateTime)(dr["dataNascimento"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataNascimento"]).Date);
                informacao.NIF = dr["nif"] == DBNull.Value ? " " : dr["nif"].ToString();
                informacao.Morada = dr["morada"] == DBNull.Value ? " " : dr["morada"].ToString();
                informacao.Localidade = dr["localidade"] == DBNull.Value ? " " : dr["localidade"].ToString();
                informacao.CodPais = (int)(dr["codPais"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codPais"]));
                informacao.CodPostal = dr["codPostal"] == DBNull.Value ? " " : dr["codPostal"].ToString();
                informacao.CodEstadoCivil =
                    (int)(dr["codEstadoCivil"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codEstadoCivil"]));
                informacao.NrSegSocial = dr["nrSegSocial"] == DBNull.Value ? " " : dr["nrSegSocial"].ToString();
                informacao.IBAN = dr["IBAN"] == DBNull.Value ? " " : dr["IBAN"].ToString();
                informacao.Naturalidade = dr["naturalidade"] == DBNull.Value ? " " : dr["naturalidade"].ToString();
                informacao.CodNacionalidade = (int)(dr["codNacionalidade"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codNacionalidade"]));
                informacao.Nacionalidade = dr["nacionalidade"].ToString();

                string relativeImagePath = "~/assets/img/default.png";
                string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                informacao.Foto = dr["foto"] == DBNull.Value
                    ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData)
                    : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                informacao.FotoBytes = dr["foto"] == DBNull.Value
                    ? imageData
                    : (byte[])dr["foto"];

                informacao.CodGrauAcademico = (int)(dr["codGrauAcademico"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codGrauAcademico"]));
                informacao.GrauAcademico = dr["grauAcademico"].ToString();
                informacao.CodSituacaoProf = (int)(dr["codSituacaoProfissional"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codSituacaoProfissional"]));
                informacao.SituacaoProf = dr["situacaoProfissional"].ToString();

                informacao.LifeMotto = dr["lifemotto"] == DBNull.Value ? " " : dr["lifemotto"].ToString();

            }

            myConn.Close();

            return informacao;
        }

        /// <summary>
        /// Função para carregar todos os utilizadores
        /// </summary>
        /// <returns></returns>
        public static List<User> LoadUsers(string condition = null, string FromUser = null)
        {
            List<User> Users = new List<User>();
            List<string> conditions = new List<string>();

            string query =
                "SELECT * FROM utilizador AS U LEFT JOIN utilizadorData AS UD ON U.codUtilizador=UD.codUtilizador LEFT JOIN utilizadorDataSecondary AS UDS ON UD.codUtilizador=UDS.codUtilizador";

            if (condition != null) query += condition;

            if (FromUser != null) query = FromUser;

            SqlConnection myConn = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetofinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myConn);
            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                User informacao = new User();
                informacao.CodUser = Convert.ToInt32(dr["codUtilizador"]);
                informacao.Username = dr["utilizador"].ToString();
                informacao.Email = dr["email"].ToString();
                informacao.Ativo = Convert.ToBoolean(dr["ativo"]);
                informacao.Nome = dr["nome"] == DBNull.Value ? " " : dr["nome"].ToString();
                informacao.CodTipoDoc = (int)(dr["codTipoDoc"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codTipoDoc"]));
                informacao.DocIdent = dr["docIdent"] == DBNull.Value ? " " : dr["docIdent"].ToString();
                informacao.DataValidade = (DateTime)(dr["dataValidadeDocIdent"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataValidadeDocIdent"]).Date);
                informacao.CodPrefix = (int)(dr["prefixo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["prefixo"]));
                informacao.Phone = dr["telemovel"] == DBNull.Value ? " " : dr["telemovel"].ToString();
                informacao.Sexo = (int)(dr["sexo"] == DBNull.Value ? 1 : Convert.ToInt32(dr["sexo"]));
                informacao.DataNascimento = (DateTime)(dr["dataNascimento"] == DBNull.Value
                    ? DateTime.Today
                    : Convert.ToDateTime(dr["dataNascimento"]).Date);
                informacao.NIF = dr["nif"] == DBNull.Value ? " " : dr["nif"].ToString();
                informacao.Morada = dr["morada"] == DBNull.Value ? " " : dr["morada"].ToString();
                informacao.Localidade = dr["localidade"] == DBNull.Value ? " " : dr["localidade"].ToString();
                informacao.CodPais = (int)(dr["codPais"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codPais"]));
                informacao.CodPostal = dr["codPostal"] == DBNull.Value ? " " : dr["codPostal"].ToString();
                informacao.CodEstadoCivil =
                    (int)(dr["codEstadoCivil"] == DBNull.Value ? 1 : Convert.ToInt32(dr["codEstadoCivil"]));
                informacao.NrSegSocial = dr["nrSegSocial"] == DBNull.Value ? " " : dr["nrSegSocial"].ToString();
                informacao.IBAN = dr["IBAN"] == DBNull.Value ? " " : dr["IBAN"].ToString();
                informacao.Naturalidade = dr["naturalidade"] == DBNull.Value ? " " : dr["naturalidade"].ToString();
                informacao.CodNacionalidade = (int)(dr["codNacionalidade"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codNacionalidade"]));

                string relativeImagePath = "~/assets/img/default.png";
                string absoluteImagePath = HttpContext.Current.Server.MapPath(relativeImagePath);
                byte[] imageData = File.ReadAllBytes(absoluteImagePath);

                informacao.Foto = dr["foto"] == DBNull.Value
                    ? "data:image/jpeg;base64," + Convert.ToBase64String(imageData)
                    : "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                informacao.CodGrauAcademico = (int)(dr["codGrauAcademico"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codGrauAcademico"]));
                informacao.CodSituacaoProf = (int)(dr["codSituacaoProfissional"] == DBNull.Value
                    ? 1
                    : Convert.ToInt32(dr["codSituacaoProfissional"]));
                informacao.LifeMotto = dr["lifemotto"] == DBNull.Value ? " " : dr["lifemotto"].ToString();
                informacao.UserProfiles = DetermineUserProfile(informacao.CodUser);


                Users.Add(informacao);
            }

            myConn.Close();

            return Users;
        }

        /// <summary>
        /// Função para registar um utilizador
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static (int, int) RegisterUser(User user)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@CodePerfil", user.CodPerfil);
            myCommand.Parameters.AddWithValue("@CompleteName", user.Nome);
            myCommand.Parameters.AddWithValue("@User", user.Username);
            myCommand.Parameters.AddWithValue("@Email", user.Email);
            myCommand.Parameters.AddWithValue("@Pw", Classes.Security.EncryptString(user.Password));
            myCommand.Parameters.AddWithValue("@CodCardType", user.CodTipoDoc);
            myCommand.Parameters.AddWithValue("@CardNumber", user.DocIdent);
            myCommand.Parameters.AddWithValue("@CardDate", user.DataValidade);
            myCommand.Parameters.AddWithValue("@Prefix", user.CodPrefix);
            myCommand.Parameters.AddWithValue("@PhoneNumber", user.Phone);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

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

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UserRegister";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswUserRegister = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);
            int AnswUserCode = Convert.ToInt32(myCommand.Parameters["@UserCode"].Value);

            myCon.Close();

            int AnswUserRegisterCont = 0;
            int AnswUserCodeCont = 0;

            if (AnswUserRegister == 3)
            {
                SqlCommand myCommand3 = new SqlCommand();
                myCommand3.Parameters.AddWithValue("@CodePerfil", user.CodPerfil);
                myCommand3.Parameters.AddWithValue("@CompleteName", user.Nome);
                myCommand3.Parameters.AddWithValue("@User", user.Username);
                myCommand3.Parameters.AddWithValue("@Email", user.Email);
                myCommand3.Parameters.AddWithValue("@Pw", Classes.Security.EncryptString(user.Password));
                myCommand3.Parameters.AddWithValue("@CodCardType", user.CodTipoDoc);
                myCommand3.Parameters.AddWithValue("@CardNumber", user.DocIdent);
                myCommand3.Parameters.AddWithValue("@CardDate", user.DataValidade);
                myCommand3.Parameters.AddWithValue("@Prefix", user.CodPrefix);
                myCommand3.Parameters.AddWithValue("@PhoneNumber", user.Phone);
                myCommand3.Parameters.AddWithValue("@AuditRow", DateTime.Now);

                SqlParameter UserRegisterCont = new SqlParameter();
                UserRegisterCont.ParameterName = "@UserRegisterCont";
                UserRegisterCont.Direction = ParameterDirection.Output;
                UserRegisterCont.SqlDbType = SqlDbType.Int;

                myCommand3.Parameters.Add(UserRegisterCont);

                SqlParameter UserCodeCont = new SqlParameter();
                UserCodeCont.ParameterName = "@UserCodeCont";
                UserCodeCont.Direction = ParameterDirection.Output;
                UserCodeCont.SqlDbType = SqlDbType.Int;

                myCommand3.Parameters.Add(UserCodeCont);

                myCommand3.CommandType = CommandType.StoredProcedure;
                myCommand3.CommandText = "UserRegisterCont";

                myCommand3.Connection = myCon;
                myCon.Open();
                myCommand3.ExecuteNonQuery();

                AnswUserRegisterCont = Convert.ToInt32(myCommand3.Parameters["@UserRegisterCont"].Value);
                AnswUserCodeCont = Convert.ToInt32(myCommand3.Parameters["@UserCodeCont"].Value);

                myCon.Close();

            }

            SqlCommand myCommand2 = new SqlCommand();

            if (AnswUserRegister == 1)
            {
                myCommand2.Parameters.AddWithValue("@CodePerfil", user.CodPerfil);
                myCommand2.Parameters.AddWithValue("@UserCode", AnswUserCode);

                myCommand2.CommandType = CommandType.StoredProcedure;
                myCommand2.CommandText = "UserRegisterProfile";

                myCommand2.Connection = myCon;
                myCon.Open();
                myCommand2.ExecuteNonQuery();
                myCon.Close();
            }
            else if (AnswUserRegisterCont == 1)
            {
                myCommand2.Parameters.AddWithValue("@CodePerfil", user.CodPerfil);
                myCommand2.Parameters.AddWithValue("@UserCode", AnswUserCodeCont);

                myCommand2.CommandType = CommandType.StoredProcedure;
                myCommand2.CommandText = "UserRegisterProfile";

                myCommand2.Connection = myCon;
                myCon.Open();
                myCommand2.ExecuteNonQuery();
                myCon.Close();
            }

            if (AnswUserRegister == 1)
                return (AnswUserRegister, AnswUserCode);
            else if (AnswUserRegister == 3)
                return (AnswUserRegisterCont, AnswUserCodeCont);
            else
                return (AnswUserRegister, AnswUserCode);
        }

        /// <summary>
        /// Função para completar o registo de um utilizador
        /// </summary>
        /// <param name="values"></param>
        /// <param name="anexos"></param>
        public static int CompleteRegisterUser(User user, List<FileControl> Anexos)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@CodUtilizador", user.CodUser);
            myCommand.Parameters.AddWithValue("@Sexo", user.Sexo);
            myCommand.Parameters.AddWithValue("@DataNascimento", user.DataNascimento);
            myCommand.Parameters.AddWithValue("@NIF", user.NIF);
            myCommand.Parameters.AddWithValue("@Morada", user.Morada);
            myCommand.Parameters.AddWithValue("@CodPais", user.CodPais);
            myCommand.Parameters.AddWithValue("@CodPostal", user.CodPostal);
            myCommand.Parameters.AddWithValue("@Freguesia", user.Localidade);
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", user.CodEstadoCivil);
            myCommand.Parameters.AddWithValue("@NrSegSocial", user.NrSegSocial);
            myCommand.Parameters.AddWithValue("@IBAN", user.IBAN);
            myCommand.Parameters.AddWithValue("@Naturalidade", user.Naturalidade);
            myCommand.Parameters.AddWithValue("@CodNacionalidade", user.CodNacionalidade);

            if (user.Foto != null)
            {
                byte[] fotoBytes = Convert.FromBase64String(user.Foto);
                myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;
            }
            else
            {
                myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }

            myCommand.Parameters.AddWithValue("@CodGrauAcademico", user.CodGrauAcademico);
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", user.CodSituacaoProf);
            myCommand.Parameters.AddWithValue("@LifeMotto", user.LifeMotto);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);


            SqlParameter UserRegister = new SqlParameter();
            UserRegister.ParameterName = "@UserRegister";
            UserRegister.Direction = ParameterDirection.Output;
            UserRegister.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserRegister);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UserRegisterSecondaryData";
            int AnswUserRegisterComplete = Convert.ToInt32(myCommand.Parameters["@UserRegister"].Value);

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            myCon.Close();

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UserRegisterDocuments";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (FileControl anexo in Anexos)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodUtilizador", user.CodUser);
                myCommand2.Parameters.AddWithValue("@NomeFicheiro", anexo.FileName);
                myCommand2.Parameters.AddWithValue("@ExtensaoFicheiro", anexo.ContentType);
                myCommand2.Parameters.AddWithValue("@Ficheiro", anexo.FileBytes);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();

            return AnswUserRegisterComplete;
        }

        /// <summary>
        /// Função para determinar CodUtilizador e Username
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static (int, string) DetermineUtilizador(string User)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

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

            myCon.Close();

            return (AnswUserCode, AnswUsername);
        }

        /// <summary>
        /// Função para determinar se o utilizador que entrou com o Google existe e se tem conta ativa - Funcionalidade Login Google
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static (int, int) CheckEmail(string Email)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

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

            myCon.Close();

            return (AnswUserExist, AnswAccountActive);
        }

        /// <summary>
        /// Função para determinar os profiles afetos a um utilizador
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public static List<int> DetermineUserProfile(int UserCode)
        {
            List<int> UserProfiles = new List<int>();

            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

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

            myCon.Close();

            return UserProfiles;
        }

        public static int UpdateUsernameEmailAndProfiles(int UserCode, string Username, string Email, List<int> UserProfiles)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@UserCode", UserCode);
            myCommand.Parameters.AddWithValue("@Username", Username);
            myCommand.Parameters.AddWithValue("@Email", Email);
            myCommand.Parameters.AddWithValue("Ativo", UserProfiles[0]);
            myCommand.Parameters.AddWithValue("@CodFormando", UserProfiles[1] == 0 ? "0" : "2");
            myCommand.Parameters.AddWithValue("@CodFormador", UserProfiles[2] == 0 ? "0" : "3");
            myCommand.Parameters.AddWithValue("@CodFuncionario", UserProfiles[3] == 0 ? "0" : "4");
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter UserUpdated = new SqlParameter();
            UserUpdated.ParameterName = "@UserUpdated";
            UserUpdated.Direction = ParameterDirection.Output;
            UserUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserUpdated);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateUserProfiles";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswUserUpdated = Convert.ToInt32(myCommand.Parameters["@UserUpdated"].Value);

            myCon.Close();

            return AnswUserUpdated;
        }

        public static string GetEmailFromDatabase(int userCode)
        {
            string email = null;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString))
            {
                string query = "SELECT email FROM utilizador WHERE codUtilizador = @UserCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserCode", userCode);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        email = result.ToString();
                    }
                }
            }
            return email;
        }

        public static int DeleteUser(int UserCode)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@UserCode", UserCode);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            SqlParameter UserDeleted = new SqlParameter();
            UserDeleted.ParameterName = "@UserDeleted";
            UserDeleted.Direction = ParameterDirection.Output;
            UserDeleted.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserDeleted);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "DeleteUser";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            int AnswUserDeleted = Convert.ToInt32(myCommand.Parameters["@UserDeleted"].Value);

            myCon.Close();

            return AnswUserDeleted;
        }

        public static int ChangeProfilePic(User User)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@UserCode", User.CodUser);
            byte[] fotoBytes = Convert.FromBase64String(User.Foto);
            myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;

            SqlParameter UserPhotoUpdated = new SqlParameter();
            UserPhotoUpdated.ParameterName = "@UserPhotoUpdated";
            UserPhotoUpdated.Direction = ParameterDirection.Output;
            UserPhotoUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserPhotoUpdated);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "ChangeProfilePicture";

            myCommand.Connection = myCon;
            myCon.Open();

            myCommand.ExecuteNonQuery();

            int AnswUserPhotoUpdated = Convert.ToInt32(myCommand.Parameters["@UserPhotoUpdated"].Value);

            myCon.Close();

            return AnswUserPhotoUpdated;
        }

        public static int UpdateUser(User User, List<FileControl> Anexos)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager
                .ConnectionStrings["projetoFinalConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@CodUtilizador", User.CodUser);
            myCommand.Parameters.AddWithValue("@CompleteName", User.Nome);
            myCommand.Parameters.AddWithValue("@Email", User.Email);
            myCommand.Parameters.AddWithValue("@CodCardType", User.CodTipoDoc);
            myCommand.Parameters.AddWithValue("@CardNumber", User.DocIdent);
            myCommand.Parameters.AddWithValue("@CardDate", User.DataValidade);
            myCommand.Parameters.AddWithValue("@Prefix", User.CodPrefix);
            myCommand.Parameters.AddWithValue("@PhoneNumber", User.Phone);
            myCommand.Parameters.AddWithValue("@Sexo", User.Sexo);
            myCommand.Parameters.AddWithValue("@DataNascimento", User.DataNascimento);
            myCommand.Parameters.AddWithValue("@NIF", User.NIF);
            myCommand.Parameters.AddWithValue("@Morada", User.Morada);
            myCommand.Parameters.AddWithValue("@CodPais", User.CodPais);
            myCommand.Parameters.AddWithValue("@CodPostal", User.CodPostal);
            myCommand.Parameters.AddWithValue("@Freguesia", User.Localidade);
            myCommand.Parameters.AddWithValue("@CodEstadoCivil", User.CodEstadoCivil);
            myCommand.Parameters.AddWithValue("@NrSegSocial", User.NrSegSocial);
            myCommand.Parameters.AddWithValue("@IBAN", User.IBAN);
            myCommand.Parameters.AddWithValue("@Naturalidade", User.Naturalidade);
            myCommand.Parameters.AddWithValue("@CodNacionalidade", User.CodNacionalidade);

            if (User.Foto != null)
            {
                byte[] fotoBytes = Convert.FromBase64String(User.Foto);
                myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = fotoBytes;
            }
            else
            {
                myCommand.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = DBNull.Value;
            }

            myCommand.Parameters.AddWithValue("@CodGrauAcademico", User.CodGrauAcademico);
            myCommand.Parameters.AddWithValue("@CodSituacaoProf", User.CodSituacaoProf);
            myCommand.Parameters.AddWithValue("@LifeMotto", User.LifeMotto);
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);


            SqlParameter UserUpdated = new SqlParameter();
            UserUpdated.ParameterName = "@UserUpdated";
            UserUpdated.Direction = ParameterDirection.Output;
            UserUpdated.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserUpdated);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateUser";
            int AnswUserRegisterComplete = Convert.ToInt32(myCommand.Parameters["@UserUpdated"].Value);

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();

            myCon.Close();

            SqlCommand myCommand2 = new SqlCommand();
            myCommand2.CommandType = CommandType.StoredProcedure;
            myCommand2.CommandText = "UserRegisterDocuments";

            myCommand2.Connection = myCon;

            myCon.Open();

            foreach (FileControl anexo in Anexos)
            {
                myCommand2.Parameters.Clear();
                myCommand2.Parameters.AddWithValue("@CodUtilizador", User.CodUser);
                myCommand2.Parameters.AddWithValue("@NomeFicheiro", anexo.FileName);
                myCommand2.Parameters.AddWithValue("@ExtensaoFicheiro", anexo.ContentType);
                myCommand2.Parameters.AddWithValue("@Ficheiro", anexo.FileBytes);
                myCommand2.ExecuteNonQuery();
            }

            myCon.Close();

            return AnswUserRegisterComplete;
        }

        public static int AtivarUtilizador(int idUtilizador)
        {
            // Conexão com o banco de dados
            string connectionString = ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString;

            // Instrução SQL para atualizar o status do usuário para ativo
            string sql = "UPDATE utilizador SET ativo = 1 WHERE codUtilizador = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Adiciona o parâmetro para o ID do usuário
                    command.Parameters.AddWithValue("@ID", idUtilizador);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return 1;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ocorreu um erro ao ativar o utilizador: " + ex.Message);
                        return -1;
                    }
                }
            }
        }

        public static bool CheckSecondaryData(int CodUtilizador)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString;
            string query = "IF EXISTS (SELECT 1 FROM [projetofinal].[dbo].[utilizadorDataSecondary] WHERE codUtilizador = @CodUtilizador AND (sexo IS NOT NULL OR dataNascimento IS NOT NULL OR nif IS NOT NULL OR morada IS NOT NULL OR codPais IS NOT NULL OR codPostal IS NOT NULL OR codEstadoCivil IS NOT NULL OR nrSegSocial IS NOT NULL OR IBAN IS NOT NULL OR naturalidade IS NOT NULL OR codNacionalidade IS NOT NULL OR foto IS NOT NULL OR codGrauAcademico IS NOT NULL OR codSituacaoProfissional IS NOT NULL OR localidade IS NOT NULL OR lifeMotto IS NOT NULL OR auditRow IS NOT NULL))" +
                           "    SELECT CAST(1 AS BIT)" +
                           "ELSE" +
                           "    SELECT CAST(0 AS BIT)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CodUtilizador", CodUtilizador);
                connection.Open();
                return (bool)command.ExecuteScalar();
            }
        }
    }
}