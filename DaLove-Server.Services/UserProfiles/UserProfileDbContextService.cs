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

        public bool ExistsUserName(string uniqueUserName)
        {
            return _daLoveDbContext.UserProfiles.Any(u => u.UniqueUserName == uniqueUserName);
        }

        public UserProfile GetUserProfile(string userId)
        {
            return _daLoveDbContext.UserProfiles.SingleOrDefault(p => p.UserId == userId);
        }

    }
}
