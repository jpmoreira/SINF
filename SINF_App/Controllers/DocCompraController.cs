using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Models;

namespace SINF_App.Controllers
{
    public class DocCompraController : ApiController
    {


        public IEnumerable<DocCompra> Get()
        {
            return SINF_App.Lib_Primavera.Comercial.VGR_List();
        }

        /*
        // GET api/cliente/5    
        public Lib_Primavera.Model.DocCompra Get(string id)
        {
            Lib_Primavera.Model.DocVenda doccompra = SINF_App.Lib_Primavera.Comercial.GR_List(id);
            if (docvenda == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return docvenda;
            }
        }
        */


        public HttpResponseMessage Post(DocCompra dc)
        {
            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.VGR_New(dc);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dc.id);
                string uri = Url.Link("DefaultApi", new { DocId = dc.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

    }
}
