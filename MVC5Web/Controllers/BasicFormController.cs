using MVC5Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult GeneralForms()
        {
            return View();
        }

       


    }
}