using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FinalProject.Classes
{
    public class Security
    {
        /// <summary>
        ///     Função de Encriptação de Dados MD5
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string EncryptString(string Message)
        {
            var Passphrase = "Patrícia Rocha";
            byte[] Results;
            var UTF8 = new UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            var HashProvider = new MD5CryptoServiceProvider();
            var TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            var DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                var Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string

            var enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }

        /// <summary>
        ///     Função de Desencritação de Dados MD5
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string DecryptString(string Message)
        {
            var Passphrase = "Patrícia Rocha";
            byte[] Results;
            var UTF8 = new UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            var HashProvider = new MD5CryptoServiceProvider();
            var TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]

            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");

            var DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                var Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

        /// <summary>
        ///     Função para validação de e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        ///     Função para validação de username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsValidUsername(string username)
        {
            var usernamePattern = @"^[a-zA-Z0-9_-]{3,30}$";
            return Regex.IsMatch(username, usernamePattern);
        }

        /// <summary>
        ///     Função para avaliar se é data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidDate(string input)
        {
            DateTime dateValue;
            return DateTime.TryParse(input, out dateValue);
        }

        /// <summary>
        ///     Função para avaliar se é decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(string value)
        {
            var isValidDecimal = value.All(c => char.IsDigit(c) || c == '.' || c == ',');

            return isValidDecimal;
        }

        /// <summary>
        ///     Função para avaliação da força da password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static (bool IsStrong, List<string> Failures) IsPasswordStrong(string password)
        {
            var maiusculas = new Regex("[A-Z]");
            var minusculas = new Regex("[a-z]");
            var numeros = new Regex("[0-9]");
            var especiais = new Regex("[^A-Za-z0-9]");
            var plica = new Regex("'");

            var failures = new List<string>();
            var strong = true;

            if (password.Length < 6)
            {
                failures.Add("Palavra-passe com pelo menos 6 caracteres!");
                strong = false;
            }
            else if (maiusculas.Matches(password).Count < 1)
            {
                failures.Add("Palavra-passe com pelo menos uma maíuscula!");
                strong = false;
            }
            else if (minusculas.Matches(password).Count < 1)
            {
                failures.Add("Palavra-passe com pelo menos uma minúscula!");
                strong = false;
            }
            else if (numeros.Matches(password).Count < 1)
            {
                failures.Add("Palavra-passe com pelo menos um número!");
                strong = false;
            }
            else if (especiais.Matches(password).Count < 1)
            {
                failures.Add("Palavra-passe com pelo menos 1 caracter especial!");
                strong = false;
            }
            else if (plica.Matches(password).Count > 1)
            {
                failures.Add("Palavra-passe sem plicas: \"'\" !");
                strong = false;
            }

            return (strong, failures);
        }

        /// <summary>
        ///     Função para alterar a password
        /// </summary>
        /// <param name="User"></param>
        /// <param name="OldPass"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int ChangePassword(string Email, string OldPass, string NewPass)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString); //Definir a conexão à base de dados

            var myCommand = new SqlCommand(); //Novo commando SQL
            myCommand.Parameters.AddWithValue("@Email", Email);
            myCommand.Parameters.AddWithValue("@PWAtual", EncryptString(OldPass));
            myCommand.Parameters.AddWithValue("@PWNova", EncryptString(NewPass));
            myCommand.Parameters.AddWithValue("@AuditRow", DateTime.Now);

            //Variável de Output para SP verificar se o utilizador e pw estão corretos
            var PasswordChanged = new SqlParameter();
            PasswordChanged.ParameterName = "@PasswordChanged";
            PasswordChanged.Direction = ParameterDirection.Output;
            PasswordChanged.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(PasswordChanged);

            myCommand.CommandType = CommandType.StoredProcedure; //Diz que o command type é uma SP
            myCommand.CommandText =
                "ChangePassword"; //Comando SQL Insert para inserir os dados acima na respetiva tabela

            myCommand.Connection =
                myCon; //Definição de que a conexão do meu comando é a minha conexão definida anteriormente
            myCon.Open(); //Abrir a conexão
            myCommand.ExecuteNonQuery(); //Executar o Comando Non Query dado que não devolve resultados - Não efetua query à BD - Apenas insere dados
            var AnswPasswordChanged = Convert.ToInt32(myCommand.Parameters["@PasswordChanged"].Value);

            myCon.Close(); //Fechar a conexão

            return AnswPasswordChanged;
        }

        /// <summary>
        ///     Função de recuperação da password
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="NovaPasse"></param>
        /// <returns></returns>
        public static (int AnswUserExists, int AnswAccountActive) RecoverPassword(string Email, string NovaPasse)
        {
            var myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projetofinalConnectionString"]
                .ConnectionString);

            var myCommand = new SqlCommand();
            myCommand.Parameters.AddWithValue("@Email", Email);
            myCommand.Parameters.AddWithValue("@PwNova", EncryptString(NovaPasse));

            var UserExist = new SqlParameter();
            UserExist.ParameterName = "@UserExist";
            UserExist.Direction = ParameterDirection.Output;
            UserExist.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(UserExist);

            var AccountActive = new SqlParameter();
            AccountActive.ParameterName = "@AccountActive";
            AccountActive.Direction = ParameterDirection.Output;
            AccountActive.SqlDbType = SqlDbType.Int;

            myCommand.Parameters.Add(AccountActive);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "RecoverPw";

            myCommand.Connection = myCon;
            myCon.Open();
            myCommand.ExecuteNonQuery();
            var AnswUserExist = Convert.ToInt32(myCommand.Parameters["@UserExist"].Value);
            var AnswAccountActive = Convert.ToInt32(myCommand.Parameters["@AccountActive"].Value);

            myCon.Close();

            return (AnswUserExist, AnswAccountActive);
        }

        /// <summary>
        ///     Função para comparação de datas
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool DataComparison(DateTime date1)
        {
            var isSup = true;
            if (date1 > DateTime.Now)
                return isSup = false;
            return isSup = true;
        }
    }
}