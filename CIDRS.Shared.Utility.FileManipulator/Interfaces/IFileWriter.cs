using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.FileManipulator.Interfaces
{
    /// <summary>
    /// Contract of FileWriter
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="id"> entity id</param>
        /// <param name="folder">specified folder name for an entity</param>
        /// <param name="file">File</param>
        /// <returns></returns>
        Task<string> UploadImageAsync(string id, string folder, IFormFile file);
        Task<string> UploadBase64ImageAsync(string filename, string folder, string file);
        public bool DeleteFile(string relativeFilePath);
    }
}
