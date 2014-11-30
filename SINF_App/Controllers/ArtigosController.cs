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
    public class ArtigosController : ApiController
    {
        //
        // GET: /Artigos/

        public IEnumerable<Artigo> Get(Login loginInfo)
        {
            return SINF_App.Lib_Primavera.Comercial.ListaArtigos(loginInfo);
        }


        // GET api/artigo/5    
        public Artigo Get(Login loginInfo,string id)
        {
            Artigo artigo = SINF_App.Lib_Primavera.Comercial.GetArtigo(loginInfo,id);
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

