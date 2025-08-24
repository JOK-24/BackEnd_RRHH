using System;
using System.Collections.Generic;

namespace ProyGestionRRHH.Models;

public partial class Permiso
{
    public long IdPermiso { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<HistorialPermiso> HistorialPermisos { get; set; } = new List<HistorialPermiso>();
}
