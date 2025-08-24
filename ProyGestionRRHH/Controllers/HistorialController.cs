using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.HistorialDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {

        //variable privada
        private readonly GestionRrhhContext ctx;
        public HistorialController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<HistorialController>
        [HttpGet("GetSolicitudes")]
        public async Task<IActionResult> GetSolicitudes()
        {
            var solicitudes = await ctx.HistorialPermisos.ToListAsync();

            var solicitudesDto = solicitudes.Select(s => new HistorialPermisoDTO
            {
                IdHistorial = s.IdHistorial,
                IdEmpleado = s.IdEmpleado,
                IdPermiso = s.IdPermiso,
                FechaSolicitud = s.FechaSolicitud,
                FechaInicio = s.FechaInicio,
                FechaFin = s.FechaFin,
                Motivo = s.Motivo,
                Estado = s.Estado
            });

            return Ok(solicitudesDto);
        }

        // GET api/<HistorialController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HistorialController>
        [HttpPost("SolicitarPermisoPost")]
        public async Task<IActionResult> SolicitarPermisoPost([FromBody] HistorialPermisoDTO solicitudDto)
        {
            var solicitud = new HistorialPermiso
            {
                IdEmpleado = solicitudDto.IdEmpleado,
                IdPermiso = solicitudDto.IdPermiso,
                FechaSolicitud = DateOnly.FromDateTime(DateTime.Now),
                FechaInicio = solicitudDto.FechaInicio,
                FechaFin = solicitudDto.FechaFin,
                Motivo = solicitudDto.Motivo,
                Estado = "PENDIENTE"
            };

            ctx.HistorialPermisos.Add(solicitud);
            await ctx.SaveChangesAsync();

            return Ok("Solicitud registrada con éxito");
        }

        // PUT api/<HistorialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistorialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
