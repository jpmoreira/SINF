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
                {
                    return Encomendas(loginInfo);
                }
            }
            ViewBag.ErrorMessage = "Invalid Login";
            return View();
        }


        public ViewResult Encomendas(Login loginInfo)
        {



            //RedirectToRouteResult resultRoute = RedirectToAction("PostLogin", "Login", loginInfo);
            //System.Console.WriteLine(resultRoute.ToString());
            //var temp = new { loginInfo = new { Username = "outro", Password = "belaflor2", Company = "BELAFLOR" }, descricaoFornecedor ="", idFornecedor="", idArtigo=""};
            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filtro = new DocCompraController.FiltroEncomendas();
            filtro.descricaoFornecedor = null;
            filtro.idArtigo = null;
            filtro.idFornecedor = null;
            filtro.loginInfo = loginInfo;
            ViewBag.loginInfo = filtro.loginInfo;
            var ans = docCmpC.FetchEncomendas(filtro);

            if (ans.GetType() != typeof(RespostaErro))
                return View("Encomendas", ans);

            ViewBag.ErrorMessage = "Invalid Login";
            return View("Login");
        }

        public ViewResult DetailOrder(Login loginInfo, string idDocument)
        {
            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filterOrder = new DocCompraController.FiltroEncomendas();
            filterOrder.descricaoFornecedor = null;
            filterOrder.idArtigo = null;
            filterOrder.idFornecedor = null;
            filterOrder.loginInfo = new Login();
            filterOrder.loginInfo = loginInfo;
            filterOrder.idDocument = idDocument;
            ViewBag.loginInfo = loginInfo;
            var ans = docCmpC.FetchEncomendas(filterOrder);

            return View(ans);
        }

    }
}
