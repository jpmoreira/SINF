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
                LoginController logC = new LoginController();
                var response = logC.PostLogin(loginInfo);
                if (logC.PostLogin(loginInfo).GetType() != typeof(RespostaErro))
                {
                    return Encomendas(loginInfo);
                }
            }
            ViewBag.LoginErrorMessage = "Invalid Login";
            return View();
        }


        public ViewResult Encomendas(Login loginInfo)
        {

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

            ViewBag.LoginErrorMessage = "Invalid Login";
            return View("Login");
        }

        public ViewResult DetailOrder(Login loginInfo, string idDocument)
        {
            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filterOrder = new DocCompraController.FiltroEncomendas();
            filterOrder.descricaoFornecedor = null;
            filterOrder.idArtigo = null;
            filterOrder.idFornecedor = null;
            filterOrder.loginInfo = loginInfo;
            filterOrder.idDocument = idDocument;

            ViewBag.loginInfo = loginInfo;

            var ans = docCmpC.FetchEncomenda(filterOrder);

            if(ans.GetType() != typeof(RespostaErro))
                return View(ans);            

            return View("ErrorOrder", ans);

        }

    }
}
