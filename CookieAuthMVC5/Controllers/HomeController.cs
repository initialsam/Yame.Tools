using CookieAuthMVC5.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ㄎ.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Account != "fail")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Account+"asdID"),
                    new Claim(ClaimTypes.Name,  model.Account),
                    new Claim(ClaimTypes.Role, model.Account),
                    new Claim("TA", "測試")
                }, "MyCookieNameTODO");

                Request.GetOwinContext().Authentication.SignIn(identity);
            }

            return View(model);

        }

        [Authorize(Roles ="admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            Claim claim1 = claimsIdentity?.FindFirst(ClaimTypes.Name);
            Claim claim2 = claimsIdentity?.FindFirst("TA");
            return View();
        }

        [Authorize(Roles = "edit")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}