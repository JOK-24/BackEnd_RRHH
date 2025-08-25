namespace ProyGestionRRHH.DTO.Login
{
    public class ResetPasswordRequest
    {
        public string NombreUsuario { get; set; } = null!;
        public string NuevaContrasena { get; set; } = null!;
    }
}
