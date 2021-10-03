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
    [Authorize("read:memories")]
    [ApiController]
    [Route("[controller]")]
    public class RandomMemoriesController : ControllerBase
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
            var userIdClaim = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = userIdClaim.Value;

            var memory = _randomMemory.GetRandomMemory(userId);
            if(memory == null)
            {
                return NoContent();
            }

            var memoryUri = _memoryAccess.GetUriAccessToMemory(memory);

            return Ok(memoryUri);
        }
    }
}
