using HackerNewsModule;
using HackerNewsModule.Domain;
using HackerNewsModule.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsDemo
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
            services.AddHttpClient<HackerNewsClient>();
            services.TryAddScoped<IHackerNewsService,HackerNewsService>();
            services.AddResponseCaching();
            services.AddAutoMapper(typeof(HackerNewsModule.MappingProfiles.AutoMapping));
            services.AddHostedService<TimedHostedService>();

            //services.AddDistributedMemoryCache();
            //services.AddMemoryCache();

            //services.AddMvc(options =>
            //{
            //    options.CacheProfiles.Add("Default5",
            //        new CacheProfile()
            //        {
            //            Location = ResponseCacheLocation.Any,
            //            Duration = 5
            //        });
            //});

            services.AddDbContext<HackerNewsDataContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HackerNewsDemo", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HackerNewsDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
