using MsSqlRepoitory.Entities;
using Service.BackendService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Web.Areas.Admin.Controllers
{
    //public class LocationTagController : GPXBaseController
    public class LocationTagController : Controller 
    {
        public ILocationTagService LocationTagService { get; }

        public LocationTagController(ILocationTagService projectTagService)
        {
            LocationTagService = projectTagService;
        }
        public ActionResult Index()
        {
            //LocationTagService.Save(new LocationTag
            //{
            //    LocationName = "Test",
            //    Sequence = 99
            //});

            return View();
        }

        public ActionResult Get(int page, int rows,string sidx,string sord)
        {

            var result = LocationTagService.Query(page, rows, sidx, sord);
            int totalRecords = LocationTagService.CountAll();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = result
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Get(int? page, int? limit, string sortBy, string direction, string txtLocationName)
        //{
        //    var result = LocationTagService.GetPage(page, limit, sortBy, direction, txtLocationName);
        //    var records = result.Records.ToViewModelList();
        //    return Content(JsonConvert.SerializeObject(new { records = records, total = result.Total }), "application/json");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Save(LocationTagViewModel viewModel)
        //{
        //    LocationTagService.Save(new LocationTagDto
        //    {
        //        LocationTagId = viewModel.LocationTagId,
        //        LocationName = viewModel.LocationName,
        //        LocationShortName = viewModel.LocationShortName
        //    });
        //    return Json(new { result = true });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Delete(int locationTagId)
        //{
        //    LocationTagService.Remove(locationTagId);
        //    return Json(new { result = true });
        //}
    }
}