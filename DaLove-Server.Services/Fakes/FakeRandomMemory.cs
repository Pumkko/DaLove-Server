using DaLove_Server.Data;
using DaLove_Server.Data.Domain;
using DaLove_Server.Services.RandomMemories;
using System;

namespace DaLove_Server.Services.Fakes
{
    public class FakeRandomMemory : IRandomMemoryService
    {
        public UserMemory GetRandomMemory(string userId)
        {
            return new UserMemory
            {
                UserId = userId,
                MemoryName = "FakeMemoryName",
                Id = 1
            };
        }
    }
}
