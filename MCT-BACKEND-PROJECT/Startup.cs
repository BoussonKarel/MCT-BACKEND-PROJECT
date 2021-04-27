using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCT_BACKEND_PROJECT.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MCT_BACKEND_PROJECT.Data;
using MCT_BACKEND_PROJECT.Repositories;

namespace MCT_BACKEND_PROJECT
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

            // Context
            services.AddDbContext<SpellenContext>();

            services.AddControllers();

            // Context
            services.AddTransient<ISpellenContext,SpellenContext>();
            // Repositories
            services.AddTransient<ISpelRepository, SpelRepository>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}