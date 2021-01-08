﻿using System;

using System.IO;

using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Fakebook.Profile.DataAccess.Services
{
    /// <summary>
    /// Gets data from/to azure.
    /// </summary>
    class BlobService : IBlobService
    {
        private readonly BlobServiceClient _client;
        private string _containerName;

        public BlobService(BlobServiceClient client, string containerName) {
            _client = client;
            _containerName = containerName;
        }

        public async Task<Uri> UploadToBlobAsync(Stream content, string contentType, string fileName, string blobContainerName = null)
        {
            return default;
        }

        private BlobContainerClient GetClient(string containerName)
        {
            return default;
        }
    }
}