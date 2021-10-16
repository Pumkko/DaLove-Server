using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemories
{
    public class RandomMemoryDbContextService : IRandomMemoryService
    {

        private readonly DaLoveDbContext _daLoveDbContext;

        public RandomMemoryDbContextService(DaLoveDbContext daLoveDbContext)
        {
            _daLoveDbContext = daLoveDbContext;
        }

        public UserMemory GetRandomMemory(string userId)
        {
            var rand = new Random();

            var allMemoriesForUsers = _daLoveDbContext.Memories.Where(m => m.UserId == userId);

            if (!allMemoriesForUsers.Any())
            {
                return null;
            }

            var randomIndex = rand.Next(0, allMemoriesForUsers.Count() - 1);

            var memory = allMemoriesForUsers.AsEnumerable().ElementAt(randomIndex);

            return memory;
        }
    }
}
