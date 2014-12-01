using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SINF_App.Models;

namespace SINF_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(Login loginInfo)
        {
            if (ModelState.IsValid)
            {
                //RedirectToRouteResult resultRoute = RedirectToAction("PostLogin", "Login", loginInfo);
                //System.Console.WriteLine(resultRoute.ToString()); 
                LoginController logC = new LoginController();
                if (logC.PostLogin(loginInfo).IsSuccessStatusCode)
                {
                    Session["Username"] = loginInfo.Username;
                    Session["Password"] = loginInfo.Password;
                    Session["Company"] = loginInfo.Company;
                    return View("Index");
                }
                    

            }

            return View();
        }

    }
}
