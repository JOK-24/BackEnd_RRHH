using System;
using System.Collections.Generic;

namespace ProyGestionRRHH.Models;

public partial class Departamento
{
    public long IdDepartamento { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public bool Enabled { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Puesto> Puestos { get; set; } = new List<Puesto>();
}
