using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DaLove_Server.Data;
using DaLove_Server.Options;
using DaLove_Server.StartupLogic;
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

            KeyVaultConfiguration.AddAzureStorageOptions(services, keyVaultClient);
            KeyVaultConfiguration.AddAuthorization(services, keyVaultClient);

            if (CurrentEnvironnement.IsProduction())
            {
                KeyVaultConfiguration.AddSqlServer(services, keyVaultClient);
                StartupProduction.ConfigureDependencies(services);
            }
            else
            {
                services.AddDbContext<DaLoveDbContext>(options =>
                    options.UseSqlServer("Data Source=localhost;Initial Catalog=dalove;Integrated Security=True"));

                StartupDeveloppment.ConfigureDependencies(services);
            }

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

            dbContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
