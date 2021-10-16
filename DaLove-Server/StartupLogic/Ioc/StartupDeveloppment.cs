using DaLove_Server.Services.Fakes;
using DaLove_Server.Services.RandomMemories;
using DaLove_Server.Services.RandomMemoriesAccess;
using DaLove_Server.Services.UserProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server
{
    public static class StartupDeveloppment
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<IRandomMemoryService, FakeRandomMemory>();
            services.AddTransient<IMemoryAccessService, FakeHttpLinkMemoryAccess>();
            services.AddTransient<IUserProfileAccessService, FakeUserProfile>();
        }
    }
}
