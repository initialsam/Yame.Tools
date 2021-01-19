using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Oldschool
{
    public static class MongoHelper
    {
        /// <summary>
        /// 取得MainDB的表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collname"></param>
        /// <returns></returns>
        public static IMongoCollection<T> GetMainDBColl<T>(string collname)
        {
            MongoClient client = new MongoClient(GetConnectionString());
            IMongoDatabase database = client.GetDatabase(GetDatabaseName());
            return database.GetCollection<T>(collname);
        }

        /// <summary>
        /// 可以輸出Mongodb的語法
        /// Subscribe的輸出慢，測試用、正式環境勿用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collname"></param>
        /// <param name="subscribe"></param>
        /// <returns></returns>
        public static IMongoCollection<T> GetMainDBColl<T>(string collname, Action<string> subscribe)
        {
            /*
             var collection = AdapterHelper.GetMainDBColl<Order>(nameof(Order), (str) =>
                {
                    System.Diagnostics.Trace.WriteLine(str);
                });
            */
            var mongoConnectionUrl = new MongoUrl(GetConnectionString());
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            if (subscribe != null)
            {
                mongoClientSettings.ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        subscribe.Invoke($"{e.CommandName} - {e.Command.ToJson()}");
                    });
                };
            }
            var client = new MongoClient(mongoClientSettings);
            IMongoDatabase database = client.GetDatabase(GetDatabaseName());
            return database.GetCollection<T>(collname);
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings
                .Get("MongoDbConnectionString")
                .Replace("{ DB_NAME}", GetDatabaseName());
        }

        private static string GetDatabaseName()
        {
            return ConfigurationManager
            .AppSettings
            .Get("MongoDbDatabaseName");
        }
    }
}
