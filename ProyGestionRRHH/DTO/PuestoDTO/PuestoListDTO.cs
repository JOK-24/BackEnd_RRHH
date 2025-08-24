namespace ProyGestionRRHH.DTO.PuestoDTO
{
    public class PuestoListDTO
    {
        public long IdPuesto { get; set; }
        public string Titulo { get; set; }
        public decimal? SalarioBase { get; set; }
        
        public string NombreDepartamento { get; set; }
        public bool Enabled { get; set; }
    }
}
