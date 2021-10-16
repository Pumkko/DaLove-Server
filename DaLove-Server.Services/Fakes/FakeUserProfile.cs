using DaLove_Server.Data.Domain;
using DaLove_Server.Services.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.Fakes
{
    public class FakeUserProfile : IUserProfileAccessService
    {
        public UserProfile GetUserProfile(string userId)
        {
            return new()
            {
                UserId = userId,
                DisplayName = "FakeDisplayName",
                UniqueUserName = "FakeUniqueUserId"
            };
        }
    }
}
