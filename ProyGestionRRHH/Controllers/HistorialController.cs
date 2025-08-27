using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.HistorialDTO;
using ProyGestionRRHH.Models;
using System.ComponentModel.DataAnnotations;

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
            var solicitudes = await ctx.HistorialPermisos
                .Include(h => h.IdEmpleadoNavigation)
                .Include(h => h.IdPermisoNavigation)
                .ToListAsync();

            var solicitudesDto = solicitudes.Select(s => new HistorialPermisoDTO
            {
                IdHistorial = s.IdHistorial,
                IdEmpleado = s.IdEmpleado,
                NombreEmpleado = s.IdEmpleadoNavigation.NombreCompleto,
                IdPermiso = s.IdPermiso,
                NombrePermiso = s.IdPermisoNavigation.Nombre,
                FechaSolicitud = s.FechaSolicitud,
                FechaInicio = s.FechaInicio,
                FechaFin = s.FechaFin,
                Motivo = s.Motivo,
                Estado = s.Estado
            });

            return Ok(solicitudesDto);
        }

        // GET api/Historial/GetSolicitudesByEmpleado/5
        [HttpGet("GetSolicitudesByEmpleado/{idEmpleado}")]
        public async Task<IActionResult> GetSolicitudesByEmpleado(int idEmpleado)
        {
            try
            {
                // Verificar que el empleado existe
                var empleadoExiste = await ctx.Empleados.AnyAsync(e => e.IdEmpleado == idEmpleado);
                if (!empleadoExiste)
                {
                    return NotFound("El empleado especificado no existe");
                }

                // Obtener solo las solicitudes del empleado e incluir Permiso
                var solicitudes = await ctx.HistorialPermisos
                    .Where(h => h.IdEmpleado == idEmpleado)
                    .Include(h => h.IdPermisoNavigation) // ✅ Incluir el permiso
                    .OrderByDescending(h => h.FechaSolicitud)
                    .ToListAsync();

                var solicitudesDto = solicitudes.Select(s => new HistorialPermisoDTO
                {
                    IdHistorial = s.IdHistorial,
                    IdEmpleado = s.IdEmpleado,
                    IdPermiso = s.IdPermiso,
                    NombrePermiso = s.IdPermisoNavigation.Nombre, // ✅ Asignar nombre
                    FechaSolicitud = s.FechaSolicitud,
                    FechaInicio = s.FechaInicio,
                    FechaFin = s.FechaFin,
                    Motivo = s.Motivo,
                    Estado = s.Estado
                });

                return Ok(solicitudesDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
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
            try
            {
                // DEBUG: Imprimir lo que llega
                Console.WriteLine("=== DATOS RECIBIDOS ===");
                Console.WriteLine($"solicitudDto: {solicitudDto}");

                if (solicitudDto != null)
                {
                    Console.WriteLine($"IdEmpleado: {solicitudDto.IdEmpleado}");
                    Console.WriteLine($"IdPermiso: {solicitudDto.IdPermiso}");
                    Console.WriteLine($"FechaInicio: {solicitudDto.FechaInicio}");
                    Console.WriteLine($"FechaFin: {solicitudDto.FechaFin}");
                    Console.WriteLine($"Motivo: '{solicitudDto.Motivo}'");
                }

                // Validaciones
                if (solicitudDto == null)
                {
                    Console.WriteLine("ERROR: solicitudDto es null");
                    return BadRequest("Los datos de la solicitud son requeridos");
                }

                if (solicitudDto.IdEmpleado <= 0)
                {
                    Console.WriteLine($"ERROR: IdEmpleado inválido: {solicitudDto.IdEmpleado}");
                    return BadRequest("ID de empleado inválido");
                }

                if (solicitudDto.IdPermiso <= 0)
                {
                    Console.WriteLine($"ERROR: IdPermiso inválido: {solicitudDto.IdPermiso}");
                    return BadRequest("ID de permiso inválido");
                }

                if (solicitudDto.FechaFin <= solicitudDto.FechaInicio)
                {
                    Console.WriteLine($"ERROR: Fechas inválidas - Inicio: {solicitudDto.FechaInicio}, Fin: {solicitudDto.FechaFin}");
                    return BadRequest("La fecha fin debe ser posterior a la fecha inicio");
                }

                // Verificar que el empleado existe
                var empleadoExiste = await ctx.Empleados.AnyAsync(e => e.IdEmpleado == solicitudDto.IdEmpleado);
                if (!empleadoExiste)
                {
                    Console.WriteLine($"ERROR: Empleado {solicitudDto.IdEmpleado} no existe");
                    return BadRequest("El empleado especificado no existe");
                }

                // Verificar que el permiso existe y está habilitado
                var permisoExiste = await ctx.Permisos.AnyAsync(p => p.IdPermiso == solicitudDto.IdPermiso && p.Enabled);
                if (!permisoExiste)
                {
                    Console.WriteLine($"ERROR: Permiso {solicitudDto.IdPermiso} no existe o no está habilitado");
                    return BadRequest("El permiso especificado no existe o no está habilitado");
                }

                var solicitud = new HistorialPermiso
                {
                    IdEmpleado = solicitudDto.IdEmpleado,
                    IdPermiso = solicitudDto.IdPermiso,
                    FechaSolicitud = DateOnly.FromDateTime(DateTime.Now),
                    FechaInicio = solicitudDto.FechaInicio,
                    FechaFin = solicitudDto.FechaFin,
                    Motivo = solicitudDto.Motivo ?? "", // Asegurar que no sea null
                    Estado = "PENDIENTE"
                };

                ctx.HistorialPermisos.Add(solicitud);
                await ctx.SaveChangesAsync();

                Console.WriteLine("SUCCESS: Solicitud registrada con éxito");
                return Ok("Solicitud registrada con éxito");
            }
            catch (Exception ex)
            {
                // Log del error (considera usar un logger como Serilog)
                Console.WriteLine($"ERROR EXCEPTION: {ex.Message}");
                Console.WriteLine($"STACK TRACE: {ex.StackTrace}");
                return StatusCode(500, "Error interno del servidor");
            }
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