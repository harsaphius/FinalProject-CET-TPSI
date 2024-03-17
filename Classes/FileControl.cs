using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FinalProject.Classes
{
    public class FileControl
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }

        public static List<FileControl> ProcessUploadedFiles(HttpFileCollection files)
        {
            List<FileControl> uploadedFiles = new List<FileControl>();
            // Check if files were uploaded
            if (files.Count > 0)
            {
                // Iterate through each uploaded file
                foreach (string fileKey in files)
                {
                    HttpPostedFile uploadedFile = files[fileKey];
                    FileControl fileData = new FileControl
                    {
                        FileName = Path.GetFileName(uploadedFile.FileName),
                        ContentType = uploadedFile.ContentType
                    };

                    // Read the file into a byte array
                    using (BinaryReader reader = new BinaryReader(uploadedFile.InputStream))
                    {
                        fileData.FileBytes = reader.ReadBytes(uploadedFile.ContentLength);
                    }

                    // Add the file data object to the list
                    uploadedFiles.Add(fileData);
                }
            }
            else
            {
                // Handle the case where no files were uploaded
            }

            return uploadedFiles;
        }

    }
}