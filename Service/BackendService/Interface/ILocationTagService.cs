using MsSqlRepoitory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BackendService
{
    public interface ILocationTagService
    {
        /// <summary>
        /// 儲存 (新增或修改)
        /// </summary>
        /// <param name="dto"></param>
        //void Save(LocationTagDto dto);
        void Save(LocationTag dto);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="projectTagId"></param>
        void Remove(int locationTagId);

        int CountAll();

        List<MongoDbRepository.LocationTag> Query(int page, int rows, string sidx, string sord);
        /// <summary>
        /// 取得分頁資料
        /// </summary>
        /// <param name="page">第幾頁</param>
        /// <param name="limit">幾筆</param>
        /// <param name="sortBy">排序欄位</param>
        /// <param name="direction">排序方式</param>
        /// <param name="txtLocationTagName">搜尋條件</param>
        /// <returns></returns>
        //GridPageDto<LocationTagIndexDto> GetPage(int? page, int? limit, string sortBy, string direction, string txtLocationTagName);


    }
}
