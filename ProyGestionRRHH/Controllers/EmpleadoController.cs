using Microsoft.AspNetCore.Mvc;
using ProyGestionRRHH.DTO.EmpleadoDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        //variable privada
        private readonly GestionRrhhContext ctx;
        public EmpleadoController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<EmpleadoController>
        [HttpGet("GetEmpleados")]
        public List<Empleado> GetEmpleados()
        {
            var empleados = ctx.Empleados.ToList();
            var fechaHoy = DateOnly.FromDateTime(DateTime.Now);
            bool huboCambios = false;

            foreach (var emp in empleados)
            {
                var fechaFinContrato = emp.FechaContratacion.AddMonths(1);

                if (fechaHoy > fechaFinContrato && emp.Enabled)
                {
                    emp.Enabled = false;

                    // Solo cambiar si aún no estaba como "FINALIZADO"
                    if (emp.EstadoEmpleado != "FINALIZADO")
                    {
                        emp.EstadoEmpleado = "FINALIZADO";
                    }

                    huboCambios = true;
                }
            }

            if (huboCambios)
            {
                ctx.SaveChanges();
            }

            return empleados;
        }

        // GET api/<EmpleadoController>/5
        [HttpGet("GetEmpleadoDetalle/{id}")]
        public ActionResult<EmpleadoDetalleDTO> GetEmpleadoDetalle(long id)
        {
            var empleado = ctx.Empleados
                .Where(e => e.IdEmpleado == id && e.Enabled)
                .Select(e => new EmpleadoDetalleDTO
                {
                    IdEmpleado = e.IdEmpleado,
                    NombreCompleto = e.NombreCompleto,
                    Dni = e.Dni,
                    Correo = e.Correo,
                    Telefono = e.Telefono,
                    Direccion = e.Direccion,
                    FechaNacimiento = e.FechaNacimiento,
                    FechaContratacion = e.FechaContratacion,
                    Salario = e.Salario,
                    EstadoEmpleado = e.EstadoEmpleado,
                    Enabled = e.Enabled,
                    NombreUsuario = e.IdUsuarioNavigation.NombreUsuario,
                    NombreDepartamento = e.IdDepartamentoNavigation.Nombre,
                    TituloPuesto = e.IdPuestoNavigation.Titulo
                })
                .FirstOrDefault();

            if (empleado == null)
                return NotFound($"Empleado con ID {id} no encontrado.");

            return Ok(empleado);
        }

        // POST api/<EmpleadoController>
        [HttpPost("EmpleadoPost")]
        public string EmpleadoPost([FromBody] Empleado value)
        {
            ctx.Empleados.Add(value);
            ctx.SaveChanges();

            return $"Se registró al empleado: {value.NombreCompleto}";
        }

        // PUT api/<EmpleadoController>/5
        [HttpPut("EmpleadoPut/{id}")]
        public ActionResult EmpleadoPut(long id, [FromBody] Empleado value)
        {
            // Forzamos el id desde la URL, ignoramos el body.IdEmpleado
            value.IdEmpleado = id;

            ctx.Empleados.Update(value);
            ctx.SaveChanges();

            return Ok($"Se actualizó al empleado: {value.NombreCompleto}");
        }

        // GET: api/Empleado/GetEmpleadoByUsuario/1
        [HttpGet("GetEmpleadoByUsuario/{idUsuario}")]
        public ActionResult<Empleado> GetEmpleadoByUsuario(long idUsuario)
        {
            var empleado = ctx.Empleados
                .FirstOrDefault(e => e.IdUsuario == idUsuario && e.Enabled);

            if (empleado == null)
            {
                return NotFound(new { mensaje = "No se encontró empleado para este usuario" });
            }

            return Ok(empleado);
        }
    }
}
