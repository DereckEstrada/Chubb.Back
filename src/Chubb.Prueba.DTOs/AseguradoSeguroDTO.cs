using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.DTOs
{
    public class AseguradoSeguroDTO
    {
        public int? IdAseguradoSeguro { get; set; }
        public int? IdAsegurado { get; set; }
        public string? NombreAsegurado { get; set; }
        public string? ApellidoAsegurado { get; set; }
        public string? CedulaAsegurado { get; set; }
        public int? IdSeguro { get; set; }
        public string? CodigoSeguro { get; set; }
        public string? NombreSeguro { get; set; }
        public int? IdEstado { get; set; }
        public DateOnly? FechaRegistro { get; set; }
        public double? SumaAsegurada { get; set; }
        public double? Prima { get; set; }
    }
}
