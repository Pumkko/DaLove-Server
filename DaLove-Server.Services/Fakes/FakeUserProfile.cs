using DaLove_Server.Data.Domain;
using DaLove_Server.Services.UserProfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.Fakes
{
    public class FakeUserProfile : IUserProfileAccessService
    {
        static private UserProfile _fakeUserProfile = null;

        public UserProfile CreateUserProfile(UserProfile newUserProfile)
        {
            _fakeUserProfile = newUserProfile;
            return _fakeUserProfile;
        }

        public UserProfile GetUserProfile(string userId)
        {
            return _fakeUserProfile;
        }

        public bool ExistsUserName(string uniqueUserName)
        {
            return _fakeUserProfile?.UniqueUserName == uniqueUserName;
        }

        public void SetAvatar(string currentUserId, string avatarFileName)
        {
            if(_fakeUserProfile == null)
            {
                throw new NoNullAllowedException(nameof(UserProfile));
            }

            _fakeUserProfile.AvatarFileName = avatarFileName;
        }

        public bool ExistsUserId(string userId)
        {
            return _fakeUserProfile?.UserId == userId;
        }
    }
}
