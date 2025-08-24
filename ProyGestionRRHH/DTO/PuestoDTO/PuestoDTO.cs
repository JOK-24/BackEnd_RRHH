namespace ProyGestionRRHH.DTO.PuestoDTO
{
    public class PuestoDTO
    {
        public long IdPuesto { get; set; }
        public string Titulo { get; set; }
        public decimal? SalarioBase { get; set; }
        public long IdDepartamento { get; set; }
        public bool Enabled { get; set; }
    }
}
