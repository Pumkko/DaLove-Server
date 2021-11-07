using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;

namespace DaLove_Server.Services.RandomMemories
{
    public interface IMemoryDomainService
    {
        /// <summary>
        /// Return a rendom memory associated with the user
        /// </summary>
        /// <param name="userId">id of the user who wants a souvenir</param>
        /// <returns>null if nothing can be found associated with the user</returns>
        UserMemory GetRandomMemory(string userId);

        UserMemory PostNewMemory(PostMemoryDto postMemoryDto, UserProfile userProfile, string currentUserId, string uniqueName);
    }
}
