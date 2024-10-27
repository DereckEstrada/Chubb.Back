using Chubb.Prueba.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chubb.Prueba.DTOs
{
    public class AseguradoDTO
    {
        public int? IdAsegurado { get; set; }
        public string? CedulaAsegurado { get; set; }
        public string? NombreAsegurado { get; set; }
        public string? ApellidoAsegurado { get; set; }
        public int? EdadAsegurado { get; set; }
        public string? TelefonoAsegurado { get; set; }
        public DateOnly? FechaRegistro { get; set; }
        public int? IdEstado { get; set; }
        public virtual ICollection<AseguradoSeguro> AseguradoSeguros { get; set; } = new List<AseguradoSeguro>();

    }
}
