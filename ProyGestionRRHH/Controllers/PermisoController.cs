using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.PermisoDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {

        //variable privada
        private readonly GestionRrhhContext ctx;
        public PermisoController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<PermisoController>
        [HttpGet("GetPermisos")]
        public async Task<IActionResult> GetPermisos()
        {
            var permisos = await ctx.Permisos.ToListAsync();

            var permisosDto = permisos.Select(p => new PermisoDTO
            {
                IdPermiso = p.IdPermiso,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Enabled = p.Enabled
            });

            return Ok(permisosDto);
        }

        // GET api/<PermisoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PermisoController>
        [HttpPost("PermisoPost")]
        public async Task<IActionResult> PermisoPost([FromBody] PermisoDTO permisoDto)
        {
            var permiso = new Permiso
            {
                Nombre = permisoDto.Nombre,
                Descripcion = permisoDto.Descripcion,
                Enabled = permisoDto.Enabled
            };

            ctx.Permisos.Add(permiso);
            await ctx.SaveChangesAsync();

            return Ok("Permiso registrado con éxito");
        }

        // PUT api/<PermisoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PermisoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
