using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Data.Dtos;
using DaLove_Server.Options;
using DaLove_Server.Services.RandomMemories;
using DaLove_Server.Services.RandomMemoriesAccess;
using DaLove_Server.Services.UserProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class RandomMemoriesController : AuthorizedController
    {
        private readonly IMemoryContainerService _memoryContainer;
        private readonly IMemoryDomainService _randomDomainContext;
        private readonly IUserProfileAccessService _userProfileAccessService;

        public RandomMemoriesController(IMemoryContainerService memoryContainer, IMemoryDomainService memoryDomainContext, IUserProfileAccessService userProfileAccessService)
        {
            _memoryContainer = memoryContainer;
            _randomDomainContext = memoryDomainContext;
            _userProfileAccessService = userProfileAccessService;
        }

        [HttpGet]
        public ActionResult GetRandomMemories()
        {
            var memory = _randomDomainContext.GetRandomMemory(CurrentUserId);
            if(memory == null)
            {
                return NoContent();
            }

            var memoryUri = _memoryContainer.GetUriAccessToMemory(memory);

            return Ok(memoryUri);
        }

        [HttpPost]
        public ActionResult PostMemory([FromForm] IFormFile memory, [FromForm] string jsonDto)
        {
            var postMemoryDto = JsonConvert.DeserializeObject<PostMemoryDto>(jsonDto);
            var userProfile = _userProfileAccessService.GetUserProfile(CurrentUserId);
            if (userProfile == null)
            {
                return BadRequest("Unknown userId");
            }

            var guidFileName = Guid.NewGuid();
            _randomDomainContext.PostNewMemory(postMemoryDto, userProfile, CurrentUserId, guidFileName.ToString());
            var uri = _memoryContainer.PostNewMemory(memory.OpenReadStream(), guidFileName.ToString());

            return Ok(uri);
        }
    }
}
