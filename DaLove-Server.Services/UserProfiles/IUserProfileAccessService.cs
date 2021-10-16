using DaLove_Server.Data.Domain;


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
    }
}
