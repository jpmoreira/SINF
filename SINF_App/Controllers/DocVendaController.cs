﻿using System;
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
    public class DocVendaController : ApiController
    {
        //
        // GET: /Clientes/

        public IEnumerable<DocCompra> Get(Login loginInfo)
        {
            return SINF_App.Lib_Primavera.Comercial.Encomendas_List(loginInfo);
        }


        // GET api/cliente/5    
        public DocCompra Get(Login loginInfo,string id)
        {
            DocCompra docvenda = SINF_App.Lib_Primavera.Comercial.Encomenda_Get(loginInfo,id);
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


        public HttpResponseMessage Post(Login info,DocVenda dv)
        {
            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.Encomendas_New(info,dv);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dv.id);
                string uri = Url.Link("DefaultApi", new {DocId = dv.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        public HttpResponseMessage Put(Login loginInfo,Cliente cliente)
        {

            RespostaErro erro = new RespostaErro();

            try
            {
                erro = SINF_App.Lib_Primavera.Comercial.UpdCliente(loginInfo,cliente);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }



        public HttpResponseMessage Delete(Login loginInfo,string id)
        {


            RespostaErro erro = new RespostaErro();

            try
            {

                erro = SINF_App.Lib_Primavera.Comercial.DelCliente(loginInfo,id);

                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }

            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);

            }

        }
    }
}
