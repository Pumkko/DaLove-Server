using DaLove_Server.Services.RandomMemories;
using DaLove_Server.Services.RandomMemoriesAccess;
using DaLove_Server.Services.UserProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace DaLove_Server
{
    public static class StartupProduction
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<IRandomMemory, RandomMemoryEfCore>();
            services.AddTransient<IMemoryAccess, AzureStorageMemoryAccess>();
            services.AddTransient<IUserProfileAccess, UserProfileEfCore>();
        }
    }
}
