using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalProject.Classes
{
    public class FileControl
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }

        /// <summary>
        /// Função para processar o upload dos anexos
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static List<FileControl> ProcessUploadedFiles(FileUpload fileUpload)
        {
            List<FileControl> uploadedFiles = new List<FileControl>();

            if (fileUpload.HasFile)
            {
                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    FileControl fileData = new FileControl
                    {
                        FileName = Path.GetFileName(uploadedFile.FileName),
                        ContentType = uploadedFile.ContentType
                    };

                    using (BinaryReader reader = new BinaryReader(uploadedFile.InputStream))
                    {
                        fileData.FileBytes = reader.ReadBytes((int)uploadedFile.InputStream.Length);
                    }

                    uploadedFiles.Add(fileData);
                }
            }

            return uploadedFiles;
        }


        /// <summary>
        /// Função para carregar os ficheiros do utilizador no FileControl
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<FileControl> GetFilesForUser(int UserID)
        {
            List<FileControl> files = new List<FileControl>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString))
            {
                connection.Open();

                string query = $"SELECT codAnexo, nomeFicheiro, extensaoFicheiro, ficheiro FROM anexo WHERE codUtilizador = {UserID}";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", UserID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FileControl file = new FileControl
                        {
                            FileID = Convert.ToInt32(reader["codAnexo"]),
                            FileName = reader["nomeFicheiro"].ToString(),
                            ContentType = reader["extensaoFicheiro"].ToString(),
                            FileBytes = (byte[])(reader["ficheiro"])
                        };

                        files.Add(file);
                    }
                }
            }

            return files;
        }

        /// <summary>
        /// Função para processar o download do ficheiro clicado
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fileId"></param>
        public static void ProcessRequest(HttpContext context, int fileId)
        {
            if (!string.IsNullOrEmpty(context.Session["CodUtilizador"]?.ToString()))
            {
                int codUtilizador = Convert.ToInt32(context.Session["CodUtilizador"]);

                List<FileControl> files = FileControl.GetFilesForUser(codUtilizador);

                if (files != null && files.Count > 0)
                {
                    FileControl file = files.FirstOrDefault(f => f.FileID == fileId);
                    if (file != null && file.FileBytes != null)
                    {
                        context.Response.ContentType = file.ContentType;
                        context.Response.AddHeader("Content-Disposition", $"attachment; filename={file.FileName}");
                        context.Response.BinaryWrite(file.FileBytes);
                        context.Response.End();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Função para verificar se o ficheiro é uma imagem
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsImageFile(HttpPostedFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    string extension = fileExtension.ToLower();
                    return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
                }
            }
            return false;
        }

        /// <summary>
        /// Função para processar o ficheiro da fotografia
        /// </summary>
        /// <param name="photoFile"></param>
        /// <returns></returns>
        public static byte[] ProcessPhotoFile(HttpPostedFile photoFile)
        {
            if (photoFile != null && IsImageFile(photoFile))
            {
                byte[] fileBytes;
                using (BinaryReader reader = new BinaryReader(photoFile.InputStream))
                {
                    fileBytes = reader.ReadBytes(photoFile.ContentLength);
                }

                return fileBytes;
            }

            return null;
        }

    }
}