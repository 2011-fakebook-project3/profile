using System;
using System.IO;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess.Services
{
    /// <summary>
    /// Interface for interacting with azure.
    /// </summary>
    public interface IBlobService
    {
        public Task<Uri> UploadToBlobAsync(Stream content, string contentType, string fileName, string blobContainerName = null);
    }
}
