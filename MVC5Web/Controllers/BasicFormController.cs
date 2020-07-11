using MVC5Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Web.Controllers
{
    public class BasicFormController : Controller
    {
        // GET: BasicForm
        public ActionResult Index()
        {
            var modelList = new List<Visit>()
            {
                new Visit{ PetName="aa" },
                new Visit{ PetName="bb" },
                new Visit{ PetName="cc" }
            };
            return View(modelList);
        }

        public ActionResult Create()
        {
            var viewModel = new Visit();
            
                var parentCategory = new List<string>() { "A1", "A2", "B1", "B2", "C1", "C2" };
            viewModel.Category = new List<string>() { "A1", "C2" };
          
            viewModel.CategoryList = new MultiSelectList(
               parentCategory.Select(x => new SelectListItem { Text = x, Value = x }),
               nameof(SelectListItem.Value),
               nameof(SelectListItem.Text),
               viewModel.Category);
            return View(viewModel);
        }

        public ActionResult CreateAjax()
        {
            return Create();
        }

        [HttpPost]
        public ActionResult CreateAjax(Visit viewModel)
        {
            if (!ModelState.IsValid) return ReturnError();
            try
            {
                ModelState.AddModelError("PetName", "PetName error set backend");
                throw new Exception("Test Ajax Fail");
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError(string.Empty, ex.Message);
                return ReturnError();
            }
            return View(viewModel);
        }

        public ActionResult GeneralForms()
        {
            return View();
        }

        private ActionResult ReturnError()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            Response.TrySkipIisCustomErrors = true;
            var returnData = new
            {
                ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0)
                                             .ToDictionary(k => k.Key,
                                                           k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            };
            return Json(returnData);
        }
    }
}