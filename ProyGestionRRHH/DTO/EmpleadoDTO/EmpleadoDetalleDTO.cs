namespace ProyGestionRRHH.DTO.EmpleadoDTO
{
    public class EmpleadoDetalleDTO
    {
        public long IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public DateOnly FechaContratacion { get; set; }
        public decimal Salario { get; set; }
        public string EstadoEmpleado { get; set; } = null!;
        public bool Enabled { get; set; }

        // Nombres “amigables”
        public string NombreUsuario { get; set; } = null!;
        public string NombreDepartamento { get; set; } = null!;
        public string TituloPuesto { get; set; } = null!;
    }
}
