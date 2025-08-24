using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProyGestionRRHH.Models;

public partial class Empleado
{
    public long IdEmpleado { get; set; }

    public long IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public long IdPuesto { get; set; }

    public long IdDepartamento { get; set; }

    public decimal Salario { get; set; }

    public string EstadoEmpleado { get; set; } = null!;

    public bool Enabled { get; set; }
    //[JsonIgnore]
    public virtual ICollection<HistorialPermiso> HistorialPermisos { get; set; } = new List<HistorialPermiso>();
    //[JsonIgnore]
    public virtual Departamento? IdDepartamentoNavigation { get; set; } = null!;
    //[JsonIgnore]
    public virtual Puesto? IdPuestoNavigation { get; set; } = null!;
    //[JsonInclude]
    public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
}
