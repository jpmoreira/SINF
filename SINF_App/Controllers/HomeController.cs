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

        
        public ViewResult DocsCompra()
        {



            //RedirectToRouteResult resultRoute = RedirectToAction("PostLogin", "Login", loginInfo);
            //System.Console.WriteLine(resultRoute.ToString());
            //var temp = new { loginInfo = new { Username = "outro", Password = "belaflor2", Company = "BELAFLOR" }, descricaoFornecedor ="", idFornecedor="", idArtigo=""};
            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filtro = new DocCompraController.FiltroEncomendas();
            filtro.descricaoFornecedor = null;
            filtro.idArtigo = null;
            filtro.idFornecedor = null;

            filtro.loginInfo = new Login();
            filtro.loginInfo.Company = "BELAFLOR";
            filtro.loginInfo.Password = "belaflor2";
            filtro.loginInfo.Username = "outro";
            var ans = docCmpC.FetchEncomendas(filtro);

            //docCmpC.FetchEncomendas()




            return View(ans);
        }

    }
}
