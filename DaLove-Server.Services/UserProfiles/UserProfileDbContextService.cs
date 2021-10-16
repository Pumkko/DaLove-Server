using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
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

        public UserProfile GetUserProfile(string userId)
        {
            return _daLoveDbContext.UserProfiles.SingleOrDefault(p => p.UserId == userId);
        }
    }
}
