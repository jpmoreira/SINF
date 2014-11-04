using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_App.Models
{
    public class Recepcao
    {
         DateTime DataDescarga
        {
            get;
            set;
        }

         string IDEncomenda
         {
             set;
             get;

         }

         Dictionary<int, float> QuantidadeRecebida
         {

             set;
             get;
         }

         string DocumentoACriar
         {
             set;
             get;

         }

         string Estado
         {

             set;
             get;
         }
         bool Anulado
         {
             set;
             get;
         }
         bool Fechado
         {
             set;
             get;
         }

         bool PodeAprovar
         {
             set;
             get;
         }

         string Matricula
         {
             set;
             get;
         }
         Armazem Armazem
         {
             set;
             get;

         }


    }
}