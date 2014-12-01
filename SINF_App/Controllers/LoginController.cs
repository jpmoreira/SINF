using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Models;
using System.Web.Mvc;
using System.Web;

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

                return new { Username = login.Username };
                
            }

            else
            {
                return erro;
            }
        }

         

         //public HttpResponseMessage Login()
         //{
             
         //    dynamic model = new { Name = "login" };
         //    var response = new HttpResponseMessage(HttpStatusCode.OK);
         //    string viewpath = HttpContext.Current.Server.MapPath("@~/Views/Login/Login.cshtml");
         //    var template = File
         //}
      
    }
}
