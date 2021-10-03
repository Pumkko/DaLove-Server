using DaLove_Server.Services.RandomMemories;
using DaLove_Server.Services.RandomMemoriesAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaLove_Server
{
    public static class StartupProduction
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<IRandomMemory, RandomMemoryEfCore>();
            services.AddTransient<IMemoryAccess, AzureStorageMemoryAccess>();
        }
    }
}
