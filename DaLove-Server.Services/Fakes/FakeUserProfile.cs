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
    }
}
