namespace ProyGestionRRHH.DTO.DepartamentoDTO
{
    public class DepartamentoDTO
    {
        public long IdDepartamento { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
