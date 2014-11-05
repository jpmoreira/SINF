using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Models;

namespace SINF_App.Controllers
{
    public class LoginController : ApiController
    {
         public HttpResponseMessage PostLogin(Login login)
        {
            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.Login(login);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, login.UserName);
                string uri = Url.Link("DefaultApi", new { UserName = login.UserName});
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.Created,erro);
            }
        }
    }
}
