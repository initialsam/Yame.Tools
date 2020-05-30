using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWeb.MongoDbRepository
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public IConfiguration Config { get; }
    public MongoDbSettings(IConfiguration config)
        {
            Config = config;
            ConnectionString = GetConnectionString();
            DatabaseName = GetDatabaseName();
        }

        private string GetConnectionString()
        {
            return Config["MongoDb:ConnectionString"]
                .Replace("{DB_NAME}", GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return Config["MongoDb:DatabaseName"];
           
        }
    }
}
