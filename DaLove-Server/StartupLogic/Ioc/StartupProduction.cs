using DaLove_Server.Services.Avatar;
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
            services.AddTransient<IRandomMemoryService, RandomMemoryDbContextService>();
            services.AddTransient<IMemoryAccessService, AzureStorageMemoryAccessService>();
            services.AddTransient<IUserProfileAccessService, UserProfileDbContextService>();
            services.AddTransient<IAvatarAccess, AzureStorageAvatarAccess>();
        }
    }
}
