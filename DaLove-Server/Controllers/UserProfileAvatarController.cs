using DaLove_Server.Services.Avatar;
using DaLove_Server.Services.UserProfiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server.Controllers
{
    [Route("[controller]")]
    public class UserProfileAvatarController : AuthorizedController
    {
        private readonly IAvatarAccessService _avatarAccess;
        private readonly IUserProfileAccessService _userProfileAccessService;

        public UserProfileAvatarController(IAvatarAccessService avatarAccess, IUserProfileAccessService userProfileAccessService)
        {
            _avatarAccess = avatarAccess;
            _userProfileAccessService = userProfileAccessService;
        }


        [HttpPost]
        public ActionResult UploadAvatar(IFormFile file)
        {
            var userProfile = _userProfileAccessService.GetUserProfile(CurrentUserId);
            if (userProfile == null)
            {
                return BadRequest("Unknown userId");
            }

            if (!string.IsNullOrEmpty(userProfile.AvatarFileName))
            {
                _avatarAccess.RemoveAvatarFile(userProfile.AvatarFileName);
            }

            // Might rework later with a hash of the file and the user id
            var guidFileName = Guid.NewGuid();
            var uri = _avatarAccess.StoreAvatar(guidFileName.ToString(), file.OpenReadStream());
            _userProfileAccessService.SetAvatar(CurrentUserId, guidFileName.ToString());

            return Ok(uri);

        }
    }
}
