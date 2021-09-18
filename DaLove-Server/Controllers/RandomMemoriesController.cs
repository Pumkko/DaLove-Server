using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DaLove_Server.Controllers
{
    [Authorize("read:memories")]
    [ApiController]
    [Route("[controller]")]
    public class RandomMemoriesController : ControllerBase
    {
        private readonly AzureBlobOptions _azureBlobOptions;

        private static readonly string[] _videos = {
            "DanielGotRaped.mp4",
            "DanielIsHappyToSeeUs.mp4",
            "DanielLovesKevin.mp4",
            "DanielNeedsToTakeAShit.mp4",
            "DanielSliding.mp4",
            "DanielWillDie.mp4",
            "IWantYouInMyRoom.mp4",
            "JulienWeirdSexuality.mp4",
            "KevinFitsInALocker.mp4",
            "Randelo.mp4",
            "Revenge.mp4",
            "SexierLeon.mp4",
            "Tattoos.mp4",
            "TryScorpions.mp4",
            "WomenToilets.mp4",
            "julianCatcher.mp4",
            "julianShakira.mp4",
            "julianTitanic.mp4",
            "sexyJulian.mp4"
        };


        public RandomMemoriesController(AzureBlobOptions azureBlobOptions)
        {
            _azureBlobOptions = azureBlobOptions;
        }

        [HttpGet]
        public ActionResult GetRandomMemories()
        {
            var rand = new Random();

            var randomIndex = rand.Next(0, _videos.Length - 1);

            var videoName = _videos[randomIndex];

            var blobContainerClient = new BlobContainerClient(_azureBlobOptions.ConnectionString, _azureBlobOptions.MemoryContainer);

            var blobClient = blobContainerClient.GetBlobClient(videoName);
            var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(10));
            return Ok(sasUri.AbsoluteUri);
        }
    }
}
