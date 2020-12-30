﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Fakebook.Profile.DataAccess
{
    /// <summary>
    /// Gets data from/to azure.
    /// </summary>
    class BlobService : IBlobService
    {
        private readonly BlobServiceClient _Client;
        private string _ContainerName;

        public BlobService(BlobServiceClient client, string containerName){
            _Client = client;
        }

        //test
        public async Task<Uri> UploadToBlob(Stream content, string contentType, string fileName, string blobContainerName=null)
        {
            if(blobContainerName is not null)
            {
                blobContainerName = this._ContainerName;
            }

            BlobContainerClient blobclient = GetClient(blobContainerName);

            await blobclient.GetBlobClient(fileName).UploadAsync(content, new BlobHttpHeaders { ContentType = contentType});

            return blobclient.Uri;
        }

        private BlobContainerClient GetClient(string containerName)
        {
            BlobContainerClient containerClient = _Client.GetBlobContainerClient(containerName);
            return containerClient;
        }
    }
}