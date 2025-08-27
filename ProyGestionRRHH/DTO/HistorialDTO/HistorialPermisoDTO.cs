namespace ProyGestionRRHH.DTO.HistorialDTO
{
    public class HistorialPermisoDTO
    {
        public long IdHistorial { get; set; }
        public long IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public long IdPermiso { get; set; }
        public string NombrePermiso { get; set; } = null!;
        public DateOnly FechaSolicitud { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }
    }
}
