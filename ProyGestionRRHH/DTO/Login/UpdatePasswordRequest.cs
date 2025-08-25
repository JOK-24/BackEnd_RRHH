namespace ProyGestionRRHH.DTO.Login
{
    public class UpdatePasswordRequest
    {
        public string NombreUsuario { get; set; } = null!;
        public string ContrasenaActual { get; set; } = null!;
        public string NuevaContrasena { get; set; } = null!;
    }
}
