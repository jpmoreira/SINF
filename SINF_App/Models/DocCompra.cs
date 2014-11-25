using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.GcpBE800;

namespace SINF_App.Models
{


    public enum TipoDoc { ECF, Fact, GT };
    public class DocCompra
    {

        public string id
        {
            get;
            set;
        }

        public string NumDocExterno
        {
            get;
            set;
        }


        public string Entidade
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public List<LinhaDocCompra> LinhasDoc
        {
            get;
            set;
        }


        public virtual GcpBEDocumentoCompra toERPType()
        {

            GcpBEDocumentoCompra doc = new GcpBEDocumentoCompra();

            doc.set_ID(id);
            doc.set_NumDocExterno(NumDocExterno);
            doc.set_Entidade(Entidade);
            doc.set_NumDoc(NumDoc);
            doc.set_DataDoc(Data);
            doc.set_TotalMerc(TotalMerc);
            doc.set_Serie(Serie);


            foreach (LinhaDocCompra linha in LinhasDoc)
            {
                SINF_App.Lib_Primavera.PriEngine.Engine.Comercial.Compras.AdicionaLinha(doc, linha.CodArtigo, linha.Quantidade, linha.Armazem, "", linha.PrecoUnitario, linha.Desconto, linha.Lote);

            }


            return doc;

        }


        static public string typeString(TipoDoc type)
        {

            if (type == TipoDoc.ECF) return "ECF";
            else if (type == TipoDoc.Fact) return "VFA";
            else if (type == TipoDoc.GT) return "VGT";

            return "";
        }


    }
}