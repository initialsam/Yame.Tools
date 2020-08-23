using CoreWeb.MongoDbRepository;
using CoreWeb.MongoDbRepository.Entities;
using CoreWeb.Utility.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Core;

namespace CoreWeb.Service
{
    public class LoginService : ILoginService
    {
        public IMongoRepository<BackendAccount> BackendAccountRepo { get; }

        public LoginService(IMongoRepository<BackendAccount> backendAccountRepo)
        {
            BackendAccountRepo = backendAccountRepo;
        }

        public bool IsValidBackendAccount(string account, string watchword)
        {
            var sha1watchword = watchword.ToSha1();
            var backendAccount = BackendAccountRepo.FindOne(x => x.Account == account && x.Watchword == sha1watchword);

            if(backendAccount.IsNull()) return false;

            return true;
        }
        public void TestAggregate(ObjectId id)
        {
            var collection = BackendAccountRepo.GetMongoCollection();
            var a = collection.Aggregate()
                   .Unwind("Products");

            //.ToList();

            a = a.Match(new BsonDocument { { "Products.ProductId", id } })
                .Group(new BsonDocument
                    {
                            {"_id", new BsonDocument
                                {
                                 {"ProductId","$Products.ProductId" },
                                 {"Price","$Products.Price" }
                                }
                            },
                            { "total",new BsonDocument
                                {
                                    { "$sum",1}
                                }
                            }
                    })
                .Match(new BsonDocument { { "total", new BsonDocument { { "$gte", 50 } } } })
                .Sort(new BsonDocument { { "total", -1 } })
                .Limit(1)
                .Project(new BsonDocument
                    {
                            { "_id",0},
                            { "ProductId","$_id.ProductId"},
                            { "Price","$_id.Price"},
                            { "Total","$total"}
                    });
            var aa = a.ToList();

            //var b = aa.Select(x => BsonSerializer.Deserialize<LotteryModeDto>(x)).ToList().FirstOrDefault();
            //return b;

        }
        
    }
}
