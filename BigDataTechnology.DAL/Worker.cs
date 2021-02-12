using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.DAL.Repositories;
using BigDataTechnology.DATA;
using BigDataTechnology.Entities.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BigDataTechnology.DAL
{
    public class Worker : IWorker
    {
        private BigDataTechnologyDbContext _dbContext;

        public Worker()
        {
            //IServiceCollection services = new ServiceCollection();
            //var DbContextDIService = services.BuildServiceProvider();

            //_dbContext = DbContextDIService.GetService();

            _dbContext = new BigDataTechnologyDbContext().Create();
        }
        private IWeatherForecast _weatherForecast { get; set; }

        public IWeatherForecast WeatherForecast { get { return _weatherForecast ?? (_weatherForecast = new RepWeatherForecast(_dbContext)); } }

        public void SaveChanges()
        {
            try
            {/*Dbden önceki validationlar*/

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurs in Worker.SaveChanges()", ex);
            }
        }
        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && _dbContext != null)
                {

                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
