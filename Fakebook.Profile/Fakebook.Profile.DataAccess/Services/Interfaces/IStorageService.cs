using System;
using System.IO;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess.Services.Interfaces
{
    /// <summary>
    /// Interface for uploading an image
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Uploads a file to remote storage for the app.
        /// </summary>
        /// <param name="content">A file stream of an image file.</param>
        /// <param name="contentType">The content type. (possibly file format)</param>
        /// <param name="fileName">The name of the file to save</param>
        /// <param name="ContainerName">(Optional) The container in which to upload the file to.</param>
        /// <returns></returns>
        public Task<Uri> UploadFileContentAsync(Stream content, string contentType, string fileName, string ContainerName = null);
    }
}
