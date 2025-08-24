using System;
using System.Collections.Generic;

namespace ProyGestionRRHH.Models;

public partial class HistorialPermiso
{
    public long IdHistorial { get; set; }

    public long IdEmpleado { get; set; }

    public long IdPermiso { get; set; }

    public DateOnly FechaSolicitud { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? Motivo { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Permiso IdPermisoNavigation { get; set; } = null!;
}
