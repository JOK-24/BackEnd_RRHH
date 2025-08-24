namespace ProyGestionRRHH.DTO.UsuarioDTO
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
