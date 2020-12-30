using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess
{
    /// <summary>
    /// Interface for interacting with azure.
    /// </summary>
    public interface IBlobService
    {
        public Task<Uri> UploadToBlobAsync(Stream content, string contentType, string fileName, string blobContainerName = null);
    }
}
