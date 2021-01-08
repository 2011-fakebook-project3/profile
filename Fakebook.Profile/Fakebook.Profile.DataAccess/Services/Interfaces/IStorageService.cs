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
        public Task<Uri> UploadFileContentAsync(Stream content, string contentType, string fileName, string ContainerName = null);
    }
}
