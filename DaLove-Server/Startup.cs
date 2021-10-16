using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DaLove_Server.Data;
using DaLove_Server.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DaLove_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironnment)
        {
            Configuration = configuration;
            CurrentEnvironnement = currentEnvironnment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment CurrentEnvironnement { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var keyVaultOptions = new KeyVaultOptions();
            Configuration.GetSection(nameof(KeyVaultOptions)).Bind(keyVaultOptions);

            var keyVaultClient = new SecretClient(new Uri(keyVaultOptions.KeyVaultUri), new DefaultAzureCredential());

            AddSqlServer(services, keyVaultClient);
            AddAzureStorageOptions(services, keyVaultClient);

            var auth0Authority= keyVaultClient.GetSecret("Auth0Authority").Value.Value;
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

            if (CurrentEnvironnement.IsProduction())
            {
                StartupProduction.ConfigureDependencies(services);
            }
            else
            {
                StartupDeveloppment.ConfigureDependencies(services);
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:memories", policy => policy.Requirements.Add(new HasScopeRequirement("read:memories", auth0Authority)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DaLove_Server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DaLoveDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DaLove_Server v1"));
            }
            else
            {
                dbContext.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddAzureStorageOptions(IServiceCollection services, SecretClient keyVaultClient)
        {
            var connectionString = keyVaultClient.GetSecret("AzureStorageConnectionString").Value.Value;
            var containerName = keyVaultClient.GetSecret("AzureStorageMemoryContainerName").Value.Value;

            var azureBlobOptions = new AzureBlobOptions()
            {
                ConnectionString = connectionString,
                MemoryContainer = containerName
            };

            services.AddSingleton(azureBlobOptions);
        }

        private void AddSqlServer(IServiceCollection services, SecretClient keyVaultClient)
        {
            var connectionString = keyVaultClient.GetSecret("DaloveSqlServerConnectionString").Value.Value;
            services.AddDbContext<DaLoveDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

    }
}
