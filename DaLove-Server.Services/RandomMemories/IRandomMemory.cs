using DaLove_Server.Data;
using DaLove_Server.Data.Domain;

namespace DaLove_Server.Services.RandomMemories
{
    public interface IRandomMemory
    {
        /// <summary>
        /// Return a rendom memory associated with the user
        /// </summary>
        /// <param name="userId">id of the user who wants a souvenir</param>
        /// <returns>null if nothing can be found associated with the user</returns>
        UserMemory GetRandomMemory(string userId);
    }
}
