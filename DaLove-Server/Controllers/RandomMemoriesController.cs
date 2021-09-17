using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Options;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomMemoriesController : ControllerBase
    {
        private readonly AzureBlobOptions _azureBlobOptions;

        public RandomMemoriesController(AzureBlobOptions azureBlobOptions)
        {
            _azureBlobOptions = azureBlobOptions;
        }

        [HttpGet]
        public ActionResult GetRandomMemories()
        {
            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient("DanielGotRaped.mp4");
            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return Ok(sasUri.AbsoluteUri);
        }
    }
}
