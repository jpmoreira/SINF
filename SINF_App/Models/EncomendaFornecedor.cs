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


        public override GcpBEDocumentoCompra toERPType()
        {

           GcpBEDocumentoCompra doc = new GcpBEDocumentoCompra();
           doc.set_Tipodoc(DocCompra.typeString(TipoDoc.Encomenda_Fornecedor));
           return base.toERPType(doc);

          

        }

        public EncomendaFornecedor()
        {
            
        }
    }
}