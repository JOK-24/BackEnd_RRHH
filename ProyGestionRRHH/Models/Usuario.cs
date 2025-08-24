using System;
using System.Collections.Generic;

namespace ProyGestionRRHH.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public long IdRol { get; set; }

    public bool Enabled { get; set; }

    public virtual Empleado? Empleado { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;
}
