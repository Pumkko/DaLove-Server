using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DaLove_Server.Data;
using DaLove_Server.Data.Dtos;
using DaLove_Server.Options;
using DaLove_Server.Services.DeviceNotification;
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
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class RandomMemoriesController : AuthorizedController
    {
        private readonly IMemoryContainerService _memoryContainer;
        private readonly IMemoryDomainService _randomDomainContext;
        private readonly IUserProfileAccessService _userProfileAccessService;
        private readonly IDeviceNotificationService _deviceNotificationService;
        private readonly IMapper _mapper;

        public RandomMemoriesController(IMemoryContainerService memoryContainer, 
            IMemoryDomainService memoryDomainContext, 
            IUserProfileAccessService userProfileAccessService,
            IDeviceNotificationService deviceNotificationService,
            IMapper mapper)
        {
            _memoryContainer = memoryContainer;
            _randomDomainContext = memoryDomainContext;
            _userProfileAccessService = userProfileAccessService;
            _deviceNotificationService = deviceNotificationService;
            _mapper = mapper;
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

            var creatorUserProfile = _userProfileAccessService.GetUserProfile(memory.UserId);

            var userProfileGetDto = _mapper.Map<UserProfileGetDto>(creatorUserProfile);

            var dto = new GetUserMemoryDto()
            {
                Creator = userProfileGetDto,
                MemoryFriendlyName = memory.MemoryFriendlyName,
                MemoryUri = memoryUri
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> PostMemory([FromForm] IFormFile memory, [FromForm] string jsonDto)
        {
            var postMemoryDto = JsonConvert.DeserializeObject<PostMemoryDto>(jsonDto);
            var userProfile = _userProfileAccessService.GetUserProfile(CurrentUserId);
            if (userProfile == null)
            {
                return BadRequest("Unknown userId");
            }

            var guidFileName = Guid.NewGuid();
            var newmemory = _randomDomainContext.PostNewMemory(postMemoryDto, userProfile, CurrentUserId, guidFileName.ToString());
            var uri = _memoryContainer.PostNewMemory(memory.OpenReadStream(), guidFileName.ToString());

            await _deviceNotificationService.PushNotificationNewMemory(userProfile, newmemory.Recipients);

            return Ok(uri);
        }
    }
}
