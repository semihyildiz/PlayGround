using BigDataTechnology.DAL;
using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.DATA;
using BigDataTechnology.Entities;
using BigDataTechnology.WebApi.Middleware;
using BigDataTechnoloy.Business;
using BigDataTechnoloy.Business.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataTechnology.WebApi
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
            AppGlobal.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllers();
            services.AddSignalR();
            services.AddSingleton<IChatHubDispatcher, ChatHubDispatcher>();

            ///*Migration create için*/
            //services.AddDbContext<BigDataTechnologyDbContext>();

            services.AddSingleton<Worker>();
            InMemory.Initial();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
 
            app.UseWhen(context => context.Request.Path.Value.Contains("weatherforecast"), appBuilder =>
            {/*weatherforecast controllerine gelen requestleri middleware'ye yönlendiriyoruz*/
                appBuilder.UseMiddleware<RequestResponseMiddleware>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });

        }
    }
}
