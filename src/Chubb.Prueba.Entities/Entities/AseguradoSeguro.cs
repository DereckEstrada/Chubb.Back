using System;
using System.Collections.Generic;

namespace Chubb.Prueba.Entities.Entities;

public partial class AseguradoSeguro
{
    public int IdAseguradoSeguro { get; set; }

    public int? IdAsegurado { get; set; }

    public int? IdSeguro { get; set; }

    public int? IdEstado { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual Asegurado? IdAseguradoNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Seguro? IdSeguroNavigation { get; set; }
}
