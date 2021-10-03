using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.UserProfiles
{
    public interface IUserProfileAccess
    {
        /// <summary>
        /// Return the profile of the given user, null if none can be found
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <returns>A Userprofile, null if none can be found</returns>
        public UserProfile GetUserProfile(string userId);
    }
}
