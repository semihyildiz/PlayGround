using BigDataTechnology.DAL;
using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.DATA;
using BigDataTechnology.Entities;
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

            /*Farklý Db contextler içeri girebilir*/
            services.AddDbContext<BigDataTechnologyDbContext>();

            services.AddSingleton<IWorker,Worker>();


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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ///test db connection
            //using (Worker worker = new Worker())
            //{
            //  var ds=  worker.WeatherForecast.Get(1);
            //}
        }
    }
}
