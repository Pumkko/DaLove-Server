using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.UserProfiles
{
    public class UserProfileDbContextService : IUserProfileAccessService
    {
        private readonly DaLoveDbContext _daLoveDbContext;

        public UserProfileDbContextService(DaLoveDbContext daLoveDbContext)
        {
            _daLoveDbContext = daLoveDbContext;
        }

        public UserProfile CreateUserProfile(UserProfile newUserProfile)
        {

            if(_daLoveDbContext.UserProfiles.Any(u => u.UniqueUserName == newUserProfile.UniqueUserName))
            {
                throw new DuplicateNameException(nameof(UserProfile.UniqueUserName));
            }

            var userProfile = _daLoveDbContext.UserProfiles.Add(newUserProfile);
            _daLoveDbContext.SaveChanges();
            return newUserProfile;
        }

        public bool ExistsUserId(string userId)
        {
            return _daLoveDbContext.UserProfiles.Any(u => u.UserId == userId);
        }

        public bool ExistsUserName(string uniqueUserName)
        {
            return _daLoveDbContext.UserProfiles.Any(u => u.UniqueUserName == uniqueUserName);
        }

        public IEnumerable<UserProfile> GetPossibleRecipients(string filter, string currentUserId)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _daLoveDbContext.UserProfiles
                    .Where(u => u.UserId != currentUserId).Take(30);
            }

            return _daLoveDbContext.UserProfiles
                .Where(u => u.UserId != currentUserId)
                .Where(u => u.UniqueUserName.Contains(filter) || u.DisplayName.Contains(filter)).Take(30);
        }

        public UserProfile GetUserProfile(string userId)
        {
            return _daLoveDbContext.UserProfiles.SingleOrDefault(p => p.UserId == userId);
        }

        public void SetAvatar(string userId, string avatarFileName)
        {
            var profile  = GetUserProfile(userId);
            if(profile == null)
            {
                throw new NoNullAllowedException(nameof(profile));
            }

            profile.AvatarFileName = avatarFileName;
            _daLoveDbContext.SaveChanges();
        }

        public void SetNewFcmDeviceToken(UserProfile userProfile, string newToken)
        {
            userProfile.LastKnownFcmDeviceToken = newToken;
            _daLoveDbContext.Update(userProfile);
            _daLoveDbContext.SaveChanges();
        }
    }
}
