using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace BigDataTechnology.Entities
{
    public static class AppGlobal
    {
        private  static string _ConnectionString;

        public  static string ConnectionString
        {
            get {
                if (string.IsNullOrEmpty(_ConnectionString) == true) 
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                    var root = builder.Build();
                    _ConnectionString = root.GetConnectionString("DefaultConnection");
                }
                return _ConnectionString; 
            
            
            }
            set { _ConnectionString = value; }
        }


    }
}
