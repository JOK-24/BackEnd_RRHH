using System;
using System.Collections.Generic;

namespace ProyGestionRRHH.Models;

public partial class Puesto
{
    public long IdPuesto { get; set; }

    public string Titulo { get; set; } = null!;

    public decimal? SalarioBase { get; set; }

    public long IdDepartamento { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
