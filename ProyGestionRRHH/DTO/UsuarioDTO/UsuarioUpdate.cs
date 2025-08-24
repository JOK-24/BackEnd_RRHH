namespace ProyGestionRRHH.DTO.UsuarioDTO
{
    public class UsuarioUpdate
    {
        public string NombreUsuario { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public long IdRol { get; set; }
        public bool Enabled { get; set; }
    }
}
