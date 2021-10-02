using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace DaLove_Server.Controllers
{
    [Authorize("read:memories")]
    [ApiController]
    [Route("[controller]")]
    public class RandomMemoriesController : ControllerBase
    {
        private readonly AzureBlobOptions _azureBlobOptions;
        private readonly DaLoveDbContext _daLoveDbContext;

        public RandomMemoriesController(AzureBlobOptions azureBlobOptions, DaLoveDbContext daLoveDbContext)
        {
            _azureBlobOptions = azureBlobOptions;
            _daLoveDbContext = daLoveDbContext;
        }

        [HttpGet]
        public ActionResult GetRandomMemories()
        {
            var rand = new Random();

            var userIdClaim = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = userIdClaim.Value;

            var allMemoriesForUsers = _daLoveDbContext.Memories.Where(m => m.UserId == userId).Select(m => m.MemoryName);
            var randomIndex = rand.Next(0, allMemoriesForUsers.Count() - 1);

            var memory = allMemoriesForUsers.AsEnumerable().ElementAt(randomIndex);

            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient(memory);
            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return Ok(sasUri.AbsoluteUri);
        }
    }
}
