using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemoriesAccess
{
    public class AzureStorageMemoryAccessService : IMemoryContainerService
    {
        private AzureBlobOptions _azureBlobOptions;

        public AzureStorageMemoryAccessService(AzureBlobOptions azureBlobOptions)
        {
            _azureBlobOptions = azureBlobOptions;
        }

        public Uri GetUriAccessToMemory(UserMemory memory)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient(memory.MemoryUniqueName);
            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return sasUri;
        }

        public Uri PostNewMemory(Stream stream, string memoryUniqueName)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient(memoryUniqueName);
            blobClient.Upload(stream);

            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return sasUri;
        }
    }
}
