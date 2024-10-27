using System;
using System.Collections.Generic;

namespace Chubb.Prueba.Entities.Entities;

public partial class Seguro
{
    public int IdSeguro { get; set; }

    public string? NombreSeguro { get; set; }

    public string? CodigoSeguro { get; set; }

    public double? SumaAsegurada { get; set; }

    public double? Prima { get; set; }

    public int? IdEstado { get; set; }

    public virtual ICollection<AseguradoSeguro> AseguradoSeguros { get; set; } = new List<AseguradoSeguro>();

    public virtual Estado? IdEstadoNavigation { get; set; }
}
