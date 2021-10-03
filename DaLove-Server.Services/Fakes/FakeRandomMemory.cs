using DaLove_Server.Data;
using DaLove_Server.Services.RandomMemories;
using System;

namespace DaLove_Server.Services.Fakes
{
    public class FakeRandomMemory : IRandomMemory
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
