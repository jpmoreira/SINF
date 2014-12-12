using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SINF_App.Models
{
    public class FiltroConversao
    {
        public string nrDocumentoExterno { get; set; }
        public Dictionary<int, double> quantidades { get; set; }
        public string idArmazem { get; set; }
        public string tipoDestino { get; set; }
        public Login loginInfo { get; set; }
        public string idDocument { get; set; }
    }
}