using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyGestionRRHH.DTO;
using ProyGestionRRHH.DTO.Login;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        //variable privada
        private readonly GestionRrhhContext ctx;
        public PasswordController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // PUT api/<PasswordController>/5 //Usuario
        [HttpPut("ResetPasswordPut/Admin")]
        public IActionResult ResetPasswordPut([FromBody] ResetPasswordRequest request)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.NombreUsuario == request.NombreUsuario && u.Enabled);

            if (usuario == null)
                return NotFound("Usuario no encontrado o inactivo.");

            // Asignar nueva contraseña en hash
            usuario.Contrasena = SecurityHelper.HashPassword(request.NuevaContrasena);
            ctx.SaveChanges();

            return Ok($"Contraseña de {usuario.NombreUsuario} restablecida correctamente por el administrador.");
        }

        // PUT api/<PasswordController>/5 //Usuario
        [HttpPut("UpdatePasswordPut")]
        public IActionResult UpdatePasswordPut([FromBody] UpdatePasswordRequest request)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.NombreUsuario == request.NombreUsuario && u.Enabled);

            if (usuario == null)
                return NotFound("Usuario no encontrado o inactivo.");

            // Validar contraseña actual
            var hashContrasenaActual = SecurityHelper.HashPassword(request.ContrasenaActual);
            if (usuario.Contrasena != hashContrasenaActual)
                return BadRequest("La contraseña actual no es correcta.");

            // Guardar nueva contraseña en hash
            usuario.Contrasena = SecurityHelper.HashPassword(request.NuevaContrasena);
            ctx.SaveChanges();

            return Ok("Contraseña actualizada correctamente.");
        }

        // DELETE api/<UpdatePasswordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
