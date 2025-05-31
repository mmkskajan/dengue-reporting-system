using CIDRS.Shared.Utility.FileManipulator.Helper;
using CIDRS.Shared.Utility.FileManipulator.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.FileManipulator
{
    /// <summary>
    /// The class that contains Uplod file methods
    /// </summary>
    public class FileWriter : IFileWriter
    {
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(string id, string folder, IFormFile file)
        {
            // Check the file is Image or not
            if (CheckIfImageFile(file))
            {
                return await WriteFile(id, folder, file);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return FileWriterHelper.GetImageFormat(fileBytes) != FileWriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(string fileRefName, string folder, IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = fileRefName + extension; //Create a filename with entity id
                                           //for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\{folder}", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }

        public bool DeleteFile(string relativeFilePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot", relativeFilePath);

            if (File.Exists(path))
                File.Delete(path);
            else
                return false;
            return true;
        }
        public async Task<string> UploadBase64ImageAsync(string fileName, string folderName, string avatar)
        {
            string fileNameWithExt;
            try
            {
                string[] avatarsplit = avatar.Split(',');
                var bytes = Convert.FromBase64String(avatarsplit[1]);

                var extensionSplit = avatarsplit[0].Split('/');
                var extension = extensionSplit[1].Split(';')[0];

                if (!(new[] { "jpg", "png", "jpeg" }.Contains(extension)))
                    return "Invalid image file";

                fileNameWithExt = $"{fileName}.{extension}";
                // fileName = $"{Guid.NewGuid()}.{extension}"; //Create a filename with entity id
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Images\\{folderName}", fileNameWithExt);

                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileNameWithExt;
        }
    }
}
