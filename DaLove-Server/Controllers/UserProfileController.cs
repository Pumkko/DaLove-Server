using AutoMapper;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using DaLove_Server.Services.Avatar;
using DaLove_Server.Services.UserProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class UserProfileController : AuthorizedController
    {
        private readonly IUserProfileAccessService _userProfileAccess;
        private readonly IMapper _mapper;

        public UserProfileController(IUserProfileAccessService userProfileAccess, IMapper mapper) : base()
        {
            _userProfileAccess = userProfileAccess;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetUserProfile()
        {
            var userProfile = _userProfileAccess.GetUserProfile(CurrentUserId);
            if (userProfile == null)
            {
                return NoContent();
            }

            var userProfileGetDto = _mapper.Map<UserProfileGetDto>(userProfile);
            return Ok(userProfileGetDto);
        }

        [HttpPost]
        public ActionResult CreateUserProfile([FromBody] UserProfileCreateDto newUserProfileDto)
        {
            try
            {
                var userProfile = _mapper.Map<UserProfile>(newUserProfileDto);

                var createdUserProfile = _userProfileAccess.CreateUserProfile(userProfile);

                var userProfileGetDto = _mapper.Map<UserProfileGetDto>(createdUserProfile);
                return Ok(userProfileGetDto);
            }
            catch (DuplicateNameException)
            {
                return BadRequest(newUserProfileDto.UniqueUserName);
            }
        }

        [HttpGet("{uniqueUserName}")]
        public ActionResult UniqueUserNameAvailable(string uniqueUserName)
        {
            var userProfile = _userProfileAccess.ExistsUserName(uniqueUserName);
            return Ok(userProfile);
        }

        [HttpGet("filter/{filter}")]
        public ActionResult GetPossibleRecipients(string filter)
        {
            IEnumerable<UserProfile> profiles = _userProfileAccess.GetPossibleRecipients(filter);
            var userProfileGetDto = _mapper.Map<IEnumerable<UserProfileGetDto>>(profiles);
            return Ok(userProfileGetDto);
        }
    }
}
