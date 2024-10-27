using System;
using System.Collections.Generic;

namespace Chubb.Prueba.Entities.Entities;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<AseguradoSeguro> AseguradoSeguros { get; set; } = new List<AseguradoSeguro>();

    public virtual ICollection<Asegurado> Asegurados { get; set; } = new List<Asegurado>();

    public virtual ICollection<Seguro> Seguros { get; set; } = new List<Seguro>();
}
