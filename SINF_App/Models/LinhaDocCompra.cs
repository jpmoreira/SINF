using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.GcpBE800;

namespace SINF_App.Models
{

    public class LinhaDocCompra
    {

        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }

        public string IdCabecDoc
        {
            get;
            set;
        }

        public int NumLinha
        {
            get;
            set;
        }


        public double Quantidade
        {
            get;
            set;
        }

        public string Unidade
        {
            get;
            set;
        }

        public double Desconto
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public double TotalILiquido
        {
            get;
            set;
        }

        public double TotalLiquido
        {
            get;
            set;
        }

        public string Armazem
        {
            get;
            set;
        }

        public string Lote
        {
            get;
            set;
        }

        public double QuantidadeSatisfeita
        {
            get;
            set;
        }


        public GcpBELinhaDocumentoCompra toERPType()
        {


            GcpBELinhaDocumentoCompra linha = new GcpBELinhaDocumentoCompra();

            //idCabecDoc not set
            linha.set_Artigo(CodArtigo);
            linha.set_Descricao(DescArtigo);
            linha.set_NumLinDocOriginal(NumLinha);
            linha.set_Quantidade(Quantidade);
            linha.set_Unidade(Unidade);
            linha.set_Desconto1((float)Desconto);
            linha.set_PrecUnit(PrecoUnitario);
            linha.set_TotalIliquido(TotalILiquido);
            linha.set_PrecoLiquido(TotalLiquido);
            linha.set_Armazem(Armazem);
            linha.set_Lote(Lote);
            linha.set_QuantSatisfeita(QuantidadeSatisfeita);



            return linha;

        }
  
    }
}