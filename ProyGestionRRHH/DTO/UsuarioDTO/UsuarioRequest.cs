namespace ProyGestionRRHH.DTO.UsuarioDTO
{
    public class UsuarioRequest
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string NombreCompleto { get; set; }
        public long IdRol { get; set; }
    }
}
