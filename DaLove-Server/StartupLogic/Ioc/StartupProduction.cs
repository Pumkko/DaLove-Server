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
            services.AddTransient<IMemoryDomainService, RandomMemoryDbContextService>();
            services.AddTransient<IMemoryContainerService, AzureStorageMemoryAccessService>();
            services.AddTransient<IUserProfileAccessService, UserProfileDbContextService>();
            services.AddTransient<IAvatarAccessService, AzureStorageAvatarAccessService>();
        }
    }
}
