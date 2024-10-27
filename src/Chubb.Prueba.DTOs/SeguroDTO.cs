using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.DTOs
{
    public class SeguroDTO
    {
        public int? IdSeguro { get; set; }
        public string? NombreSeguro { get; set; }
        public string? CodigoSeguro { get; set; }
        public double? SumaAsegurada { get; set; }
        public double? Prima { get; set; }
        public int? IdEstado { get; set; }
    }
}
