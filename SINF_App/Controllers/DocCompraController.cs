using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SINF_App.Models;
using Newtonsoft.Json;



namespace SINF_App.Controllers
{
    public class DocCompraController : ApiController
    {


        public class Filtro
        {
            public Login loginInfo;
            public string idDocument;
        }

        public class FiltroEncomendas: Filtro
        {

            public string descricaoFornecedor;
            public string idFornecedor;
            public string idArtigo;


        }

        public class FiltroConversao : Filtro
        {
            public string nrDocumentoExterno;
            public Dictionary<int, double> quantidades;
            public string armazem;
            public string idArmazem;
            public string tipoDestino;

        }


        public class FiltroEstorno : Filtro
        {

            public Dictionary<int, double> quantidades;
            public string motivo;


        }

        public Object FetchEncomendas(FiltroEncomendas filtro)
        {

            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.Login(filtro.loginInfo);

            if (erro.Erro == 0) return SINF_App.Lib_Primavera.Comercial.ECF_List(filtro.loginInfo,filtro.idDocument,filtro.descricaoFornecedor,filtro.idFornecedor,filtro.idArtigo);
            
            return erro;
        }

        public Object FetchEncomenda(FiltroEncomendas filtro)
        {
            RespostaErro erro = new RespostaErro();
            erro = SINF_App.Lib_Primavera.Comercial.Login(filtro.loginInfo);
            if (erro.Erro == 0)
                return SINF_App.Lib_Primavera.Comercial.ECF_Single(filtro.loginInfo, filtro.idDocument, filtro.descricaoFornecedor, filtro.idFornecedor, filtro.idArtigo);
            //um comentário aleatório
            return erro;

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

        
        public HttpResponseMessage PostAddDocument(Login login)
        {
            RespostaErro erro = new RespostaErro();

            /*
            
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

           */  

            return null;
        }


        public Object ConvertDocument(FiltroConversao f)
        {

            DocCompra d = new DocCompra();
            d.id = f.idDocument;


            return SINF_App.Lib_Primavera.Comercial.TransformaEncomenda(d, DocCompra.typeFromString(f.tipoDestino), f.loginInfo, f.quantidades,f.armazem,f.nrDocumentoExterno);




            

        }

      


        public List<string> GetMotivosEstorno([FromUri]Login loginInfo)
        {

            return SINF_App.Lib_Primavera.Comercial.MotivosEstorno(loginInfo);

        }


       [System.Web.Mvc.HttpGet]//this is a get method!!!!
        public Object Testx()
        {

            //List<EncomendaFornecedor> docs=Lib_Primavera.Comercial.ECF_List();

            return null;


        }
    }
}
