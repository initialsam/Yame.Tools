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
            return View();
        }
    }
}