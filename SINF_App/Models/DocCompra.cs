using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.GcpBE800;

namespace SINF_App.Models
{


    public enum TipoDoc { Tipo_Desconhecido, Encomenda_Fornecedor, Factura_Fornecedor, Guia_Transporte_Fornecedor, Guia_Remessa_Fornecedor };
    public class DocCompra
    {


        public string tipo
        {
            get;
            set;
        }

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

        public string Descricao { get; set; }

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


        public virtual GcpBEDocumentoCompra toERPType(GcpBEDocumentoCompra doc)
        {



            doc.set_ID(id);
            doc.set_NumDocExterno(NumDocExterno);
            doc.set_Entidade(Entidade);
            doc.set_NumDoc(NumDoc);
            doc.set_DataDoc(Data);
            doc.set_TotalMerc(TotalMerc);//Só é verdade se todas as linhs convertidas na totatilidade
            doc.set_Serie(Serie);
            doc.set_TipoEntidade("F");
            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();

            SINF_App.Lib_Primavera.PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(doc, rl);//dunno what this does!!!

            foreach (LinhaDocCompra linha in LinhasDoc)
            {
                SINF_App.Lib_Primavera.PriEngine.Engine.Comercial.Compras.AdicionaLinha(doc, linha.CodArtigo, linha.Quantidade, linha.Armazem, "", linha.PrecoUnitario, linha.Desconto, linha.Lote);

            }


            return doc;

        }


        public virtual GcpBEDocumentoCompra toERPType()
        {

            GcpBEDocumentoCompra doc = new GcpBEDocumentoCompra();


            doc.set_ID(id);
            doc.set_NumDocExterno(NumDocExterno);
            doc.set_Entidade(Entidade);
            doc.set_NumDoc(NumDoc);
            doc.set_DataDoc(Data);
            doc.set_TotalMerc(TotalMerc);//Só é verdade se todas as linhs convertidas na totatilidade
            doc.set_Serie(Serie);
            doc.set_TipoEntidade("F");
            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();

            SINF_App.Lib_Primavera.PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(doc, rl);//dunno what this does!!!

            foreach (LinhaDocCompra linha in LinhasDoc)
            {
                SINF_App.Lib_Primavera.PriEngine.Engine.Comercial.Compras.AdicionaLinha(doc, linha.CodArtigo, linha.Quantidade, linha.Armazem, "", linha.PrecoUnitario, linha.Desconto, linha.Lote);

            }


            return doc;

        }


        static public string typeString(TipoDoc type)
        {

            if (type == TipoDoc.Encomenda_Fornecedor) return "ECF";
            else if (type == TipoDoc.Factura_Fornecedor) return "VFA";
            else if (type == TipoDoc.Guia_Transporte_Fornecedor) return "VGT";
            else if (type == TipoDoc.Guia_Remessa_Fornecedor) return "VGR";

            return null;
        }

        static public TipoDoc typeFromString(string s)
        {
            if (s.Equals("ECF", StringComparison.Ordinal)) return TipoDoc.Encomenda_Fornecedor;
            if (s.Equals("VFA", StringComparison.Ordinal)) return TipoDoc.Factura_Fornecedor;
            if (s.Equals("VGT", StringComparison.Ordinal)) return TipoDoc.Guia_Transporte_Fornecedor;
            if (s.Equals("VGR", StringComparison.Ordinal)) return TipoDoc.Guia_Remessa_Fornecedor;

            return TipoDoc.Tipo_Desconhecido;
        }


        static public DocCompra fromERPType(GcpBEDocumentoCompra docOriginal)
        {

            DocCompra doc;
            if (docOriginal.get_Tipodoc().Equals(typeString(TipoDoc.Encomenda_Fornecedor))) doc = new EncomendaFornecedor();
            else if (docOriginal.get_Tipodoc().Equals(typeString(TipoDoc.Factura_Fornecedor))) doc = new Factura();
            else doc = new DocCompra();

            doc.id = docOriginal.get_ID();
            doc.NumDocExterno = docOriginal.get_NumDocExterno();
            doc.NumDoc = docOriginal.get_NumDoc();
            doc.Entidade = docOriginal.get_Entidade();
            doc.Data = docOriginal.get_DataDoc();
            doc.TotalMerc = docOriginal.get_TotalMerc();
            doc.Serie = docOriginal.get_Serie();
            doc.LinhasDoc = new List<LinhaDocCompra>();

            GcpBELinhasDocumentoCompra linhas = docOriginal.get_Linhas();

            int nritems = linhas.NumItens;
            int i = 0;
            // for(int i=0;i<linhas.NumItens;i++)
            foreach (GcpBELinhaDocumentoCompra linha in linhas)
            {
                i++;
                // GcpBELinhaDocumentoCompra linha=linhas[i];
                LinhaDocCompra l = new LinhaDocCompra();
                l.Armazem = linha.get_Armazem();
                l.CodArtigo = linha.get_Artigo();
                l.DescArtigo = linha.get_Descricao();
                l.Desconto = linha.get_Desconto1();
                l.Lote = linha.get_Lote();
                l.NumLinha = i;
                l.PrecoUnitario = linha.get_PrecUnit();
                l.Quantidade = linha.get_Quantidade();
                l.TotalILiquido = linha.get_TotalIliquido();
                l.TotalLiquido = linha.get_PrecoLiquido();
                l.Unidade = linha.get_Unidade();

                doc.LinhasDoc.Add(l);
            }



            return doc;

        }

    }
}