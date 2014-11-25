using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.GcpBE800;
using Interop.IGcpBS800;


using SINF_App.Lib_Primavera;

namespace SINF_App.Models
{
    public class EncomendaFornecedor: DocCompra
    {


        public GcpBEDocumentoCompra toERPType()
        {

           GcpBEDocumentoCompra doc= base.toERPType();
           doc.set_Tipodoc("ECF");

           return doc;

        }
    }
}