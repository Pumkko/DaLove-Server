using Azure.Security.KeyVault.Secrets;
using DaLove_Server.Data;
using DaLove_Server.Options;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DaLove_Server.StartupLogic
{
    public static class KeyVaultConfiguration
    {


        public static void AddAuthorization(IServiceCollection services, SecretClient keyVaultClient)
        {
            var auth0Authority = keyVaultClient.GetSecret("Auth0Authority").Value.Value;
            var auth0Audience = keyVaultClient.GetSecret("Auth0Audience").Value.Value;

            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = auth0Authority;
                options.Audience = auth0Audience;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:memories", policy => policy.Requirements.Add(new HasScopeRequirement("read:memories", auth0Authority)));
            });
        }

        public static void AddAzureStorageOptions(IServiceCollection services, SecretClient keyVaultClient)
        {
            var connectionString = keyVaultClient.GetSecret("AzureStorageConnectionString").Value.Value;
            var containerName = keyVaultClient.GetSecret("AzureStorageMemoryContainerName").Value.Value;
            var avatarContainerName = keyVaultClient.GetSecret("AzureStorageAvatarContainerName").Value.Value;

            var azureBlobOptions = new AzureBlobOptions()
            {
                ConnectionString = connectionString,
                MemoryContainer = containerName,
                AvatarContainer = avatarContainerName
            };

            services.AddSingleton(azureBlobOptions);
        }

        public static void AddGoogleFcmOptions(IServiceCollection services, SecretClient keyVaultClient)
        {
            var googleFcmOptionsCredentials = keyVaultClient.GetSecret("FcmGoogleJsonCredentials").Value.Value;

            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(googleFcmOptionsCredentials)
            });

            services.AddSingleton(app);
        }

        public static void AddSqlServer(IServiceCollection services, SecretClient keyVaultClient)
        {
            var connectionString = keyVaultClient.GetSecret("DaloveSqlServerConnectionString").Value.Value;
            services.AddDbContext<DaLoveDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

    }
}
