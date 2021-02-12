using BigDataTechnology.Entities;
using BigDataTechnology.Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BigDataTechnology.DATA
{
    public class BigDataTechnologyDbContext: DbContext
    {
        public BigDataTechnologyDbContext(DbContextOptions<BigDataTechnologyDbContext> options)
           : base(options)
        {


        }
        public BigDataTechnologyDbContext()
        {
            this.Database.Migrate();
        }

        public BigDataTechnologyDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BigDataTechnologyDbContext>();
            optionsBuilder.UseSqlServer(AppGlobal.ConnectionString);

            return new BigDataTechnologyDbContext(optionsBuilder.Options);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(AppGlobal.ConnectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppGlobal.ConnectionString, builder => builder.EnableRetryOnFailure());
                //optionsBuilder.UseSqlServer("Server=ARGESEMIHYILDIZ\\SQLSERVER2017DEV;Database=BigDataTechnology;User Id=sa;Password=1453;", builder => builder.EnableRetryOnFailure());

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().HasKey(c => c.Id);
            modelBuilder.Entity<WeatherForecast>().HasIndex(c => c.Location);

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<WeatherForecast> WeatherForecast { get; set; }

    }
}
