using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemoriesAccess
{
    public class AzureStorageMemoryAccess : IMemoryAccess
    {
        private AzureBlobOptions _azureBlobOptions;

        public AzureStorageMemoryAccess(AzureBlobOptions azureBlobOptions)
        {
            _azureBlobOptions = azureBlobOptions;
        }

        public Uri GetUriAccessToMemory(UserMemory memory)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient(memory.MemoryName);
            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return sasUri;
        }
    }
}
