using DaLove_Server.Data.Domain;
using System;

namespace DaLove_Server.Services.UserProfiles
{
    public interface IUserProfileAccessService
    {
        /// <summary>
        /// Return the profile of the given user, null if none can be found
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <returns>A Userprofile, null if none can be found</returns>
        public UserProfile GetUserProfile(string userId);

        /// <summary>
        /// Create a new user profile, warning : if the unique username is not unique the method should return an exception
        /// </summary>
        /// <exception cref="System.Data.DuplicateNameException"></exception>
        /// <param name="newUserProfile"></param>
        public UserProfile CreateUserProfile(UserProfile newUserProfile);

        /// <summary>
        /// Returns true if the username already exists
        /// </summary>
        /// <param name="uniqueUserName">the username to check</param>
        /// <returns>a boolean value</returns>
        public bool ExistsUserName(string uniqueUserName);

        /// <summary>
        /// Returns true if the user with the given id has a profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ExistsUserId(string userId);

        /// <summary>
        /// Set the avatar for the given user 
        /// </summary>
        /// <param name="userId">User ID, the user id must already have a created userprofile</param>
        /// <param name="avatarFileName">guid of a known profile</param>
        void SetAvatar(string userId, string avatarFileName);
    }
}
