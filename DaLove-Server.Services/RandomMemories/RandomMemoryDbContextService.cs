using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaLove_Server.Services.RandomMemories
{
    public class RandomMemoryDbContextService : IMemoryDomainService
    {

        private readonly DaLoveDbContext _daLoveDbContext;

        public RandomMemoryDbContextService(DaLoveDbContext daLoveDbContext)
        {
            _daLoveDbContext = daLoveDbContext;
        }

        public UserMemory GetRandomMemory(string userId)
        {
            var rand = new Random();

            var currentUser = _daLoveDbContext.UserProfiles.SingleOrDefault(u => u.UserId == userId);
            if (currentUser == null)
            {
                return null;
            }

            var allMemoriesForUsers = currentUser.Memories;

            if (allMemoriesForUsers == null || !allMemoriesForUsers.Any())
            {
                return null;
            }

            var randomIndex = rand.Next(0, allMemoriesForUsers.Count() - 1);

            var memory = allMemoriesForUsers.AsEnumerable().ElementAt(randomIndex);

            return memory;
        }


        public UserMemory PostNewMemory(PostMemoryDto postMemoryDto, UserProfile userProfile, string currentUserId, string uniqueName)
        {
            var recipients = _daLoveDbContext.UserProfiles.Where(p => postMemoryDto.Recipients.Contains(p.UniqueUserName)).ToList();


            if (!postMemoryDto.Recipients.Contains(userProfile.UniqueUserName))
            {
                recipients.Add(userProfile);
            }

            var newUserMemory = new UserMemory()
            {
                MemoryUniqueName = uniqueName,
                MemoryFriendlyName = postMemoryDto.Caption,
                UserId = currentUserId,
                Recipients = recipients
            };

            _daLoveDbContext.Add(newUserMemory);
            _daLoveDbContext.SaveChanges();

            return newUserMemory;
        }
    }
}
