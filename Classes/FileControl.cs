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
        public string FileUrl
        {
            get
            {
                return GenerateFileUrl(HttpContext.Current);
            }
        }
        public string GenerateFileUrl(HttpContext context)
        {
            ProcessRequest(context, FileID);

            return null;
        }

        public static List<FileControl> ProcessUploadedFiles(FileUpload fileUpload)
        {
            List<FileControl> uploadedFiles = new List<FileControl>();
            // Check if files were uploaded
            if (fileUpload.HasFile)
            {

                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    FileControl fileData = new FileControl
                    {
                        FileName = Path.GetFileName(uploadedFile.FileName),
                        ContentType = uploadedFile.ContentType
                    };

                    // Read the file into a byte array
                    using (BinaryReader reader = new BinaryReader(uploadedFile.InputStream))
                    {
                        fileData.FileBytes = reader.ReadBytes((int)uploadedFile.InputStream.Length);
                    }

                    // Add the file data object to the list
                    uploadedFiles.Add(fileData);

                }
            }

            return uploadedFiles;
        }

        public static bool IsImageFile(HttpPostedFile file)
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
            else
            {
                string imagePath = "~/assets/img/ivancik.jpg";
                string physicalPath = HttpContext.Current.Server.MapPath(imagePath);

                if (File.Exists(physicalPath))
                {
                    byte[] fileBytes;
                    using (FileStream fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read))
                    {
                        fileBytes = new byte[fileStream.Length];
                        fileStream.Read(fileBytes, 0, (int)fileStream.Length);
                    }
                    return fileBytes;
                }
            }

            return null; // Or handle error case as needed
        }

        public static List<FileControl> GetFilesForUser(int UserID)
        {
            List<FileControl> files = new List<FileControl>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["projetoFinalConnectionString"].ConnectionString))
            {
                connection.Open();

                string query = $"SELECT codAnexo, nomeFicheiro, extensaoFicheiro,ficheiro FROM anexo WHERE codUtilizador = {@UserID}";
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
        public static void ProcessRequest(HttpContext context, int fileId)
        {
            if (!string.IsNullOrEmpty(context.Session["CodUtilizador"]?.ToString()))
            {
                int codUtilizador = Convert.ToInt32(context.Session["CodUtilizador"]);

                List<FileControl> files = FileControl.GetFilesForUser(codUtilizador);

                if (files != null && files.Count > 0)
                {
                    // Assuming you want to download the first file in the list
                    FileControl file = files.FirstOrDefault(f => f.FileID == fileId);
                    if (file != null && file.FileBytes != null)
                    {
                        context.Response.ContentType = file.ContentType;
                        context.Response.AddHeader("Content-Disposition", $"attachment; filename={file.FileName}");
                        context.Response.BinaryWrite(file.FileBytes);
                        context.Response.Flush();
                        context.Response.End();
                        return;
                    }
                }
                else
                {
                    // Handle case where no files were found for the user
                    context.Response.StatusCode = 404; // File not found
                    context.Response.Write("No files found for the user");
                }
            }
            else
            {
                // Handle case where CodUtilizador is missing from session
                context.Response.StatusCode = 400; // Bad request
                context.Response.Write("CodUtilizador is missing from session");
            }
        }

        public string GenerateFileUrl()
        {
            // Assuming you have a page named DownloadFile.aspx where you handle file downloads
            // You can adjust this URL according to your application's structure
            string baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string downloadPageUrl = $"{baseUrl}/UserProfile.aspx"; // Adjust the page name as necessary

            // Construct the URL for downloading the file using the file ID
            return $"{downloadPageUrl}?fileId={FileID}";
        }
    }

}
