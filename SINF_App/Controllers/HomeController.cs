using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using SINF_App.Models;

namespace SINF_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Login Default View
         */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        /**
         * Do Login View
         */
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


        /**
         * View all orders
         */
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



        /**
         *Detailed view of an order 
         */
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

            if (ans.GetType() != typeof(RespostaErro))
                return View(ans);

            return View("ErrorOrder", ans);
        }

        /**
         * Validate order fields and adds supplier delivery doc
         */
        public ActionResult ConfirmOrder(Login loginInfo, string idDocument)
        {

            ViewBag.loginInfo = loginInfo;
           // SINF_App.Controllers.DocCompraController.FiltroConversao f = new SINF_App.Controllers.DocCompraController.FiltroConversao();
            SINF_App.Models.FiltroConversao f = new SINF_App.Models.FiltroConversao();

            f.idDocument = idDocument;
            f.loginInfo = loginInfo;
            
            //todo documento externo
            Random rnd = new Random();
            f.nrDocumentoExterno = idDocument + rnd.Next(0, 100); 
            //

            f.quantidades = new Dictionary<int, double>();
            f.idArmazem = Request.Form["armazem"];
            f.tipoDestino = Request.Form["tipoDestino"];
            if (f.idArmazem == null)
            {
                ViewBag.ErrorMessage = "Armazem não seleccionado ou inválido";
                return View("GenericError");
            }
            if(f.tipoDestino == null)
            {
                ViewBag.ErrorMessage = "Documento de destino inválido";
                return View("GenericError");
            }



            int nrLinha = 0;
            double quantidade = 0.0;
            foreach(string key in Request.Form.AllKeys)
            {
                if (!Int32.TryParse(key, out nrLinha) || !Double.TryParse(Request.Form[key], out quantidade))
                    continue;
                if (quantidade == 0)
                    continue;
                f.quantidades.Add(nrLinha, quantidade);
            }

            string fQuantidadesStr = f.quantidades.ToString();



            return View(f);
        }


        public ActionResult Armazens(Login loginInfo)
        {
            var ans = SINF_App.Lib_Primavera.Comercial.ListaArmazens(loginInfo);
            return PartialView("Armazens", ans);
        }

    }
}
