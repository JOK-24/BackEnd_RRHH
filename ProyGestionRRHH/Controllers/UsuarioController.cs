using Microsoft.AspNetCore.Mvc;
using ProyGestionRRHH.DTO;
using ProyGestionRRHH.Models;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.UsuarioDTO;
using ProyGestionRRHH.DTO.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //variable privada
        private readonly GestionRrhhContext ctx;
        public UsuarioController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }
        // Lista completa
        // GET: api/<EmpleadoController>
        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            // Obtener todos los usuarios de la base de datos
            var usuarios = await ctx.Usuarios
                .Include(u => u.IdRolNavigation) // Incluir el nombre del rol
                .ToListAsync();

            // Devolver la lista de usuarios en formato JSON
            return Ok(usuarios.Select(u => new
            {
                u.Id,
                u.NombreUsuario,
                u.NombreCompleto,
                u.Enabled,
                Rol = u.IdRolNavigation.Nombre // Nombre del rol
            }));
        }

        /*// GET: api/<UsuarioController> //Listar por usuario disponible
        [HttpGet("GetUsuariosDisponibles")]
        public ActionResult<List<object>> GetUsuariosDisponibles()
        {
            var usuariosDisponibles = ctx.Usuarios
                .Where(u => u.Enabled && u.Empleado == null)
                .Select(u => new
                {
                    u.Id,
                    u.NombreUsuario,
                    u.NombreCompleto
                })
                .ToList();

            return Ok(usuariosDisponibles);
        }*/
        // GET: api/<UsuarioController> //Listar por usuario disponible
        [HttpGet("GetUsuariosDisponibles")]
        public List<Usuario> GetUsuariosDisponibles()
        {
            var usuariosDisponibles = ctx.Usuarios
        .Where(u => u.Enabled && u.Empleado == null)
        .ToList();

            return usuariosDisponibles;
        }
        //

        // POST api/<UsuarioController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRequest request)
        {
            try
            {
                // Verificar si ya existe un usuario con ese nombre de usuario
                var existingUser = await ctx.Usuarios
                    .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

                if (existingUser != null)
                {
                    return BadRequest("El nombre de usuario ya está en uso.");
                }

                // Verificar si el rol existe
                var rol = await ctx.Roles
                    .FirstOrDefaultAsync(r => r.Id == request.IdRol);

                if (rol == null)
                {
                    return BadRequest("El rol seleccionado no existe.");
                }

                // Crear el nuevo usuario
                var nuevoUsuario = new Usuario
                {
                    NombreUsuario = request.NombreUsuario,
                    Contrasena = SecurityHelper.HashPassword(request.Contrasena), //Encriptamos la contra
                    NombreCompleto = request.NombreCompleto,
                    IdRol = request.IdRol, //Asignamos el rol
                    Enabled = true 
                };

                // Guardar el nuevo usuario en la base de datos
                ctx.Usuarios.Add(nuevoUsuario);
                await ctx.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Usuario registrado con éxito",
                    Usuario = nuevoUsuario.NombreUsuario,
                    Rol = rol.Nombre //Nombre del rol asignado
                });
            }
            catch (Exception ex)
            {
                // Si ocurre un error inesperado, devuelve un error general
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // POST api/<UsuarioController> login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await ctx.Usuarios
                .Include(u => u.IdRolNavigation) // Cargar los roles asociados
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

            if (usuario == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Verificar que la contraseña sea correcta
            var hashedPassword = SecurityHelper.HashPassword(request.Contrasena);
            if (usuario.Contrasena != hashedPassword)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Si las credenciales son correctas
            return Ok(new
            {
                Message = "Login exitoso",
                IdUsuario = usuario.Id,   // mandamos el id usuario
                IdEmpleado = usuario.Empleado?.IdEmpleado,
                Usuario = usuario.NombreUsuario,
                Rol = usuario.IdRolNavigation.Nombre // Nombre del rol asignado
            });
        }

        //
        // PUT api/<UsuarioController>/5
        [HttpPut("PutUsuario/{id}")]
        public ActionResult<UsuarioDTO> PutUsuario(long id, [FromBody] UsuarioUpdate dto)
        {
            var usuario = ctx.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Actualizamos campos
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.IdRol = dto.IdRol;
            usuario.Enabled = dto.Enabled;

            ctx.SaveChanges();

            // Retornamos el usuario actualizado como DTO
            var usuarioActualizado = new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Rol = ctx.Roles.FirstOrDefault(r => r.Id == usuario.IdRol)?.Nombre ?? "SIN ROL",
                Enabled = usuario.Enabled
            };

            return Ok(usuarioActualizado);

        }

        
        // GET api/<UsuarioController>/5
        [HttpGet("GetUsuario/{id}")]
        public ActionResult<UsuarioDTO> GetUsuario(long id)
        {
            var usuario = ctx.Usuarios
            .Include(u => u.IdRolNavigation)
            .Where(u => u.Id == id && u.Enabled) 
            .Select(u => new UsuarioDTO
        {
            Id = u.Id,
            NombreUsuario = u.NombreUsuario,
            NombreCompleto = u.NombreCompleto,
            Rol = u.IdRolNavigation.Nombre, // suponiendo que Role tiene "Nombre"
            Enabled = u.Enabled
        })
        .FirstOrDefault();

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }

        /* // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
