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
        public IRepository<LocationTag> LocationTagRepo { get; }

        public LocationTagService(IRepository<LocationTag> locationTagRepo)
        {
            LocationTagRepo = locationTagRepo;
        }
        //public void Save(LocationTagDto dto)
        public void Save(LocationTag dto)
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
        }

        public void Remove(int locationTagId)
        {
            LocationTagRepo.Remove(LocationTagRepo.Query(x => x.LocationTagId == locationTagId).Single());
            LocationTagRepo.Save();
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
