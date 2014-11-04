using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Lib_Primavera.Model;


namespace MvcApplication1.Controllers
{
    public class ArtigosController : ApiController
    {
        //
        // GET: /Artigos/

        public IEnumerable<Artigo> Get()
        {
            return SINF_App.Lib_Primavera.Comercial.ListaArtigos();
        }


        // GET api/artigo/5    
        public Artigo Get(string id)
        {
            Artigo artigo = SINF_App.Lib_Primavera.Comercial.GetArtigo(id);
            if (artigo == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }

    }
}

