using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spellen.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Spellen.API.Data;
using Spellen.API.Repositories;
using Spellen.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Spellen.API
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
            // Configuratie voor connection strings
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            // EF Core: Context
            services.AddDbContext<SpellenContext>();

            services.AddControllers();

            // Auth0
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://karelb.eu.auth0.com/";
                options.Audience = "http://SpellenAPI";
            });

            // Context
            services.AddTransient<ISpellenContext,SpellenContext>();
            // Repositories
            services.AddTransient<ISpelRepository, SpelRepository>();
            services.AddTransient<IMateriaalRepository, MateriaalRepository>();
            services.AddTransient<ICategorieRepository, CategorieRepository>();
            // Services
            services.AddTransient<ISpellenService, SpellenService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MCT_BACKEND_PROJECT", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MCT_BACKEND_PROJECT v1"));
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
    }
}
