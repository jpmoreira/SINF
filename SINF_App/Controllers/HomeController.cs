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
                    ViewBag.loginInfo = loginInfo;
                    if (loginInfo.Storage != null)
                        return Encomendas(loginInfo);
                    return View();
                }
            }
            ViewBag.LoginErrorMessage = "Login invalido";
            return View();
        }


        /**
         * View all orders
         */
        public ViewResult Encomendas(Login loginInfo)
        {

            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filtro = new DocCompraController.FiltroEncomendas();
            filtro.loginInfo = loginInfo;

            ViewBag.loginInfo = loginInfo;

            var ans = docCmpC.FetchEncomendas(filtro);
            if (ans.GetType() != typeof(RespostaErro))
                return View("Encomendas", ans);

            ViewBag.LoginErrorMessage = "Login Invalido";
            return View("Login");
        }



        /**
         *Detailed view of an order 
         */
        public ViewResult DetailOrder(Login loginInfo, string idDocument)
        {
            DocCompraController docCmpC = new DocCompraController();
            DocCompraController.FiltroEncomendas filterOrder = new DocCompraController.FiltroEncomendas();
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
            f.nrDocumentoExterno = Request.Form["nrDocExterno"];
            //

            f.quantidades = new Dictionary<int, double>();
            f.idArmazem = ViewBag.loginInfo.Storage;

            if (f.idArmazem == null || String.Compare(f.idArmazem, "") == 0)
            {
                ViewBag.ErrorMessage = "Armazem não seleccionado ou inválido";
                return View("GenericError");
            }
            
            f.tipoDestino = Request.Form["tipoDestino"];
            //if (f.idArmazem == null)
            //{
            //    ViewBag.ErrorMessage = "Armazem não seleccionado ou inválido";
            //    return View("GenericError");
            //}
            if (f.tipoDestino == null)
            {
                ViewBag.ErrorMessage = "Documento de destino inválido";
                return View("GenericError");
            }

            TipoDoc tipoDest;

            if (String.Compare(f.tipoDestino, "VFA") == 0)
                tipoDest = TipoDoc.Factura_Fornecedor;
            else
                tipoDest = TipoDoc.Guia_Transporte_Fornecedor;


            int nrLinha = 0;
            double quantidade = 0.0;
            foreach (string key in Request.Form.AllKeys)
            {
                if (!Int32.TryParse(key, out nrLinha) || !Double.TryParse(Request.Form[key], out quantidade))
                    continue;
                if (quantidade == 0)
                    continue;
                f.quantidades.Add(nrLinha, quantidade);
            }

            if(f.quantidades.Count == 0)
            {
                ViewBag.ErrorMessage=  "Nenhuma produto recebido.";
                return View("GenericError");
            }


            DocCompraController.FiltroEncomendas filterOrder = new DocCompraController.FiltroEncomendas();
            DocCompraController docCmpC = new DocCompraController();
            filterOrder.loginInfo = loginInfo;
            filterOrder.idDocument = idDocument;
            DocCompra ans = (DocCompra)docCmpC.FetchEncomenda(filterOrder);

            Object transfReturn = SINF_App.Lib_Primavera.Comercial.TransformaEncomenda(ans, tipoDest, loginInfo, f.quantidades, f.idArmazem, f.nrDocumentoExterno);

            if (transfReturn.GetType() == typeof(RespostaErro))
            {
                ViewBag.ErrorMessage = "Falhou a transformação de documentos";
                return View("GenericError");
            }

            return View(transfReturn);
        }


        public ActionResult Armazens(Login loginInfo)
        {
            var ans = SINF_App.Lib_Primavera.Comercial.ListaArmazens(loginInfo);
            ViewBag.loginInfo = loginInfo;
            return PartialView("Armazens", ans);
        }

    }
}
