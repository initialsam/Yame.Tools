﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbRepository;
using MsSqlRepoitory.Entities;
using MsSqlRepoitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.BackendService
{
    public static class IQueryableExtension
    {
     
    }
    public class LocationTagService : ILocationTagService
    {
        public IRepository<MsSqlRepoitory.Entities.LocationTag> LocationTagRepo { get; }
        public IMongoRepository<MongoDbRepository.LocationTag> LocationTagMongoDbRepo{ get; }

        public LocationTagService(IRepository<MsSqlRepoitory.Entities.LocationTag> locationTagRepo,
            IMongoRepository<MongoDbRepository.LocationTag> locationTagMongoDbRepo)
        {
            LocationTagRepo = locationTagRepo;
            LocationTagMongoDbRepo = locationTagMongoDbRepo;
        }
        //public void Save(LocationTagDto dto)
        public void Save(MsSqlRepoitory.Entities.LocationTag dto)
        {
            //if (dto.LocationTagId == 0)
            //{
            //    LocationTagRepo.Create(dto.ToEntity());
            //}
            //else
            //{
            //    LocationTagRepo.Update(dto.ToEntity());
            //}
            if (dto.LocationTagId == 0)
            {
                LocationTagRepo.Create(dto);
            }
            else
            {
                LocationTagRepo.Update(dto);
            }
            LocationTagRepo.Save();

            var mongoLocationTag = new MongoDbRepository.LocationTag()
            {
                LocationName = dto.LocationName,
                Sequence = dto.Sequence
            };

            LocationTagMongoDbRepo.InsertOne(mongoLocationTag);
        }

        public void Remove(int locationTagId)
        {
            LocationTagRepo.Remove(LocationTagRepo.Query(x => x.LocationTagId == locationTagId).Single());
            LocationTagRepo.Save();
        }

        public List<MongoDbRepository.LocationTag> Query(int page, int rows, string sidx, string sord)
        {
            var a  = LocationTagMongoDbRepo.AsQueryable();
            //var b = LocationTagRepo.LookupAll().OrderBy(sidx, sord=="asc").ToList();
            if (sord == "desc")
            {
                a = a.OrderByDescending(s => s.GetType().GetProperty(sidx).GetValue(s));
            }
            else
            {
                a = a.OrderBy(s => s.GetType().GetProperty(sidx).GetValue(s));
            }


            return a.Skip((page - 1) * rows).Take(rows).ToList();
        }

        

        public int CountAll()
        {
            return LocationTagMongoDbRepo.AsQueryable().Count();
        }

        public void TestAggregate(ObjectId id)
        {
            var collection = LocationTagMongoDbRepo.GetMongoCollection();
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

        //public GridPageDto<LocationTagIndexDto> GetPage(int? page, int? limit, string sortBy, string direction, string txtLocationTagName)
        //{
        //    var query = LocationTagRepo.LookupAll();
        //    if (!string.IsNullOrWhiteSpace(txtLocationTagName))
        //    {
        //        query = query.Where(q => q.LocationName.Contains(txtLocationTagName));
        //    }

        //    var total = query.Count();
        //    query = query.ToPagedList(
        //                  sortBy,
        //                  DirectionHelper.DefaultDesc(direction),
        //                  nameof(LocationTagDto.LocationTagId),
        //                  page,
        //                  limit);

        //    var records = query.ToList().ToLocationTagIndexDtoList();

        //    return new GridPageDto<LocationTagIndexDto>
        //    {
        //        Total = total,
        //        Records = records
        //    };
        //}
    }
}
