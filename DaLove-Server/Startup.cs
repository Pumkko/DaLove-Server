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
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DaLove_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DaLoveDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            AddOptionsForType<AzureBlobOptions>(services);
            AddOptionsForType<KeyVaultOptions>(services);
           

            services.Configure<Auth0Options>(
                Configuration.GetSection(nameof(Auth0Options)));

            var auth0Options = new Auth0Options();
            Configuration.GetSection(nameof(Auth0Options)).Bind(auth0Options);


            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = auth0Options.Authority;
                options.Audience = auth0Options.Audience;
            });



            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:memories", policy => policy.Requirements.Add(new HasScopeRequirement("read:memories", auth0Options.Authority)));
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

        private void AddOptionsForType<T>(IServiceCollection services) where T : class
        {
            services.Configure<T>(
                Configuration.GetSection(typeof(T).Name));

            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<T>>().Value);
        }
    }
}
