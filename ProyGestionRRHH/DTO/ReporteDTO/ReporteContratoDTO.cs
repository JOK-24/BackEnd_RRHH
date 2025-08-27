namespace ProyGestionRRHH.DTO.ReporteDTO
{
    public class ReporteContratoDTO
    {
        public long IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public DateOnly FechaContratacion { get; set; }
        public DateOnly FechaVencimiento { get; set; }  // se asigna en el controller
        public int DiasRestantes { get; set; }         // se asigna en el controller
        public string EstadoEmpleado { get; set; } = null!;
    }
}
