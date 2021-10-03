using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Options;
using DaLove_Server.Services.RandomMemories;
using DaLove_Server.Services.RandomMemoriesAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class RandomMemoriesController : AuthorizedController
    {
        private readonly IMemoryAccess _memoryAccess;
        private readonly IRandomMemory _randomMemory;

        public RandomMemoriesController(IMemoryAccess memoryAccess, IRandomMemory randomMemory)
        {
            _memoryAccess = memoryAccess;
            _randomMemory = randomMemory;
        }

        [HttpGet]
        public ActionResult GetRandomMemories()
        {
            var memory = _randomMemory.GetRandomMemory(CurrentUserId);
            if(memory == null)
            {
                return NoContent();
            }

            var memoryUri = _memoryAccess.GetUriAccessToMemory(memory);

            return Ok(memoryUri);
        }
    }
}
