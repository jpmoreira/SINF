using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_App.Models
{
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
    }
}