using System;
using System.Collections.Generic;

namespace Chubb.Prueba.Entities.Entities;

public partial class Asegurado
{
    public int IdAsegurado { get; set; }
    public string? CedulaAsegurado { get; set; }
    public string? NombreAsegurado { get; set; }
    public string? ApellidoAsegurado { get; set; }
    public int? EdadAsegurado { get; set; }
    public string? TelefonoAsegurado { get; set; }
    public DateOnly? FechaRegistro { get; set; }
    public int? IdEstado { get; set; }
    public virtual ICollection<AseguradoSeguro> AseguradoSeguros { get; set; } = new List<AseguradoSeguro>();

    public virtual Estado? IdEstadoNavigation { get; set; }
}
