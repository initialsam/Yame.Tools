using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public MongoDbSettings()
        {
            ConnectionString = GetConnectionString();
            DatabaseName = GetDatabaseName();
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings
                .Get("MongoDbConnectionString")
                .Replace("{ DB_NAME}", GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return ConfigurationManager
            .AppSettings
            .Get("MongoDbDatabaseName");
        }
    }
}
