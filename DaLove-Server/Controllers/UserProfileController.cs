using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
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
        private IUserProfileAccessService _userProfileAccess;

        public UserProfileController(IUserProfileAccessService userProfileAccess) : base()
        {
            _userProfileAccess = userProfileAccess;
        }

        [HttpGet]
        public ActionResult GetUserProfile()
        {
            var userProfile = _userProfileAccess.GetUserProfile(CurrentUserId);
            if (userProfile == null)
            {
                return NoContent();
            }

            // Will add automapper later
            return Ok(new UserProfileGetDto()
            {
                DisplayUserName = userProfile.DisplayName,
                UniqueUserName = userProfile.UniqueUserName
            });
        }

        [HttpPost]
        public ActionResult CreateUserProfile([FromBody] UserProfileCreateDto newUserProfileDto)
        {
            try
            {
                // Will add automapper later
                var userProfile = new UserProfile()
                {
                    UniqueUserName = newUserProfileDto.UniqueUserName,
                    DisplayName = newUserProfileDto.DisplayUserName,
                    UserId = CurrentUserId
                };

                var createdUserProfile = _userProfileAccess.CreateUserProfile(userProfile);
                // Will add automapper later
                return Ok(new UserProfileGetDto()
                {
                    DisplayUserName = userProfile.DisplayName,
                    UniqueUserName = userProfile.UniqueUserName
                });
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
    }
}
