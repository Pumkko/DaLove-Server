using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.UserProfiles
{
    public class UserProfileEfCore : IUserProfileAccess
    {
        private readonly DaLoveDbContext _daLoveDbContext;

        public UserProfileEfCore(DaLoveDbContext daLoveDbContext)
        {
            _daLoveDbContext = daLoveDbContext;
        }

        public UserProfile GetUserProfile(string userId)
        {
            return _daLoveDbContext.UserProfiles.SingleOrDefault(p => p.UserId == userId);
        }
    }
}
