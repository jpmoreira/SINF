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
                var response = logC.PostLogin(loginInfo);
                if (logC.PostLogin(loginInfo).GetType() != typeof(RespostaErro))                   
                    return View("Index");
            }

            return View();
        }

        [HttpPost]
        public ViewResult DocsCompra()
        {

            try
            {
                Login user = new Login();
                user.Company = Session["Company"].ToString();
                user.Password = Session["Password"].ToString();
                user.Username = Session["Username"].ToString();
            }
            catch (Exception e)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                //RedirectToRouteResult resultRoute = RedirectToAction("PostLogin", "Login", loginInfo);
                //System.Console.WriteLine(resultRoute.ToString()); 
                DocCompraController docCmpC = new DocCompraController();

                //docCmpC.FetchEncomendas()


            }

            return View();
        }

    }
}
