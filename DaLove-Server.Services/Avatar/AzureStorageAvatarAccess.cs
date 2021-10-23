using Azure.Storage.Blobs;
using DaLove_Server.Options;
using System;
using System.IO;

namespace DaLove_Server.Services.Avatar
{
    public class AzureStorageAvatarAccess : IAvatarAccess
    {
        private readonly AzureBlobOptions _azureBlobOptions;

        public AzureStorageAvatarAccess(AzureBlobOptions azureBlobOptions)
        {
            _azureBlobOptions = azureBlobOptions;
        }


        public Uri GetAvatar(string avatarGuid)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.AvatarContainer);

            var blobClient = blobContainerClient.GetBlobClient(avatarGuid);
            var sasUri = blobClient.Uri;
            return sasUri;
        }

        public void RemoveAvatarFile(string avatarFileName)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.AvatarContainer);

            var blobClient = blobContainerClient.GetBlobClient(avatarFileName);
            blobClient.Delete();
        }

        public Uri StoreAvatar(string avatarFileName, Stream fileToUpload)
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.AvatarContainer);

            var blobClient = blobContainerClient.GetBlobClient(avatarFileName);
            blobClient.Upload(fileToUpload);
            return blobClient.Uri;
        }
    }
}
