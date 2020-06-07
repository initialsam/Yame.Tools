using MongoDbRepository;
using MsSqlRepoitory.Entities;
using MsSqlRepoitory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BackendService
{
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
            if(sord == "desc")
            {
                a = a.OrderByDescending(NewMethod());
            }
            else
            {
                a = a.OrderBy(x => x.Sequence);
            }


            return a.Skip((page - 1) * rows).Take(rows).ToList();
        }

        private static System.Linq.Expressions.Expression<Func<MongoDbRepository.LocationTag, int>> NewMethod()
        {
            return x => x.Sequence;
        }

        public int CountAll()
        {
            return LocationTagMongoDbRepo.AsQueryable().Count();
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
