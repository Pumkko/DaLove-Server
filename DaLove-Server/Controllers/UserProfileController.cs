using DaLove_Server.Services.UserProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class UserProfileController : AuthorizedController
    {
        private IUserProfileAccess _userProfileAccess;

        public UserProfileController(IUserProfileAccess userProfileAccess): base()
        {
            _userProfileAccess = userProfileAccess;
        }

        [HttpGet]
        public ActionResult GetUserProfile()
        {
            var userProfile = _userProfileAccess.GetUserProfile(CurrentUserId);
            if(userProfile == null)
            {
                return NoContent();
            }

            return Ok(userProfile);
        }
    }
}
