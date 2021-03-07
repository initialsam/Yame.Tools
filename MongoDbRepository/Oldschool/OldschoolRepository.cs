using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Oldschool
{
    public class OldschoolRepository
    {
        private IMongoCollection<Oldschool> Collection { get; }
        public OldschoolRepository()
        {
            Collection = MongoHelper.GetMainDBColl<Oldschool>(nameof(Oldschool));
        }

        public bool Upsert1(Oldschool data)
        {
            var utcnow = DateTime.UtcNow;
            data.CreateDate = utcnow;
            var result = Collection.ReplaceOne(o => o.Id == data.Id,
                                  data,
                                  new ReplaceOptions() { IsUpsert = true });
            return result.IsAcknowledged;
        }

        public bool Upsert2(Oldschool data)
        {
            var utcnow = DateTime.UtcNow;

            FilterDefinition<Oldschool> filter =
                 Builders<Oldschool>.Filter.Eq(x => x.Id, data.Id);

            var updateBuilders = Builders<Oldschool>.Update
                .Set(x => x.Name, data.Name)
                .SetOnInsert(x => x.CreateDate, utcnow)
                .Set(x => x.LastUpdateDate, utcnow);
            var updateOption = new UpdateOptions
            {
                IsUpsert = true
            };

            var result = Collection.UpdateOne(filter, updateBuilders, updateOption);
            return result.IsAcknowledged;
        }

        public Oldschool Get1(ObjectId id)
        {
            return Collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Oldschool> Get2(string name,int count)
        {
            var builder = Builders<Oldschool>.Filter;
            FilterDefinition<Oldschool> filter = builder.And();
            if (!String.IsNullOrEmpty(name))
            {
                filter = filter & builder.Eq(x => x.Name, name);
            }

            if (count > 0 )
            {
                filter = filter & builder.Eq(x => x.SchoolList.Count, count);
            }

            return Collection.Find(filter).SortByDescending(x => x.CreateDate).ToEnumerable();
        }

        public IEnumerable<Oldschool> Get3(string name, int count)
        {
            FilterDefinition<Oldschool> filter =
               Builders<Oldschool>.Filter.Eq(x => x.Name, name) &
               Builders<Oldschool>.Filter.Gt(x => x.SchoolList.Count, count);
             

            return Collection.Find(filter).SortBy(x => x.LastUpdateDate).ToList();
        }

        public bool Delete(Oldschool data)
        {
            var result = Collection.DeleteOne(x => x.Id == data.Id);
            return result.IsAcknowledged;
        }

        public bool Update(Oldschool data)
        {
            FilterDefinition<Oldschool> filter =
                 Builders<Oldschool>.Filter.Eq(x => x.Id, data.Id);

            var updateBuilders = Builders<Oldschool>.Update
                .Set(x => x.Name, data.Name)
                .Set(x => x.LastUpdateDate, DateTime.UtcNow);
            var result = Collection.UpdateOne(filter, updateBuilders);
            return result.IsAcknowledged;
        }

        public bool UpdateSchool(ObjectId id,string school)
        {
            FilterDefinition<Oldschool> filter =
                 Builders<Oldschool>.Filter.Eq(x => x.Id, id);

            var updateBuilders = Builders<Oldschool>.Update
                .Push(x => x.SchoolList, school)
                .Set(x => x.LastUpdateDate, DateTime.UtcNow);

            var result = Collection.UpdateOne(filter, updateBuilders);
            return result.IsAcknowledged;
        }

        public bool Insert(Oldschool data)
        {
            try
            {
                Collection.InsertOne(data);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ReplaceSchoolList(Oldschool data)
        {
            FilterDefinition<Oldschool> filter =
                 Builders<Oldschool>.Filter.Eq(x => x.Id, data.Id);

            var updateBuilders = Builders<Oldschool>.Update
                .Set(x => x.SchoolList, data.SchoolList);

            var result = Collection.UpdateOne(filter, updateBuilders);
            return result.IsAcknowledged;
        }
    }
}
