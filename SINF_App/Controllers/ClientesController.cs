using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Models;

namespace SINF_App.Controllers
{
    public class ClientesController : ApiController
    {
        //
        // GET: /Clientes/

        public IEnumerable<Cliente> Get(Login loginInfo)
        {
            return SINF_App.Lib_Primavera.Comercial.ListaClientes(loginInfo);
        }


        // GET api/cliente/5    
        public Cliente Get(Login loginInfo,string id)
        {
            Cliente cliente = SINF_App.Lib_Primavera.Comercial.GetCliente(loginInfo,id);
            if (cliente == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return cliente;
            }
        }

        public HttpResponseMessage PostCliente (Login loginInfo,Cliente cliente)
        {
            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.InsereClienteObj(loginInfo,cliente);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, cliente);
                string uri = Url.Link("DefaultApi", new { CodCliente = cliente.CodCliente });
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
                erro =SINF_App.Lib_Primavera.Comercial.UpdCliente(loginInfo,cliente);
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
