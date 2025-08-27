using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.ReporteDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteContratoController : ControllerBase
    {

        //variable privada
        private readonly GestionRrhhContext ctx;
        public ReporteContratoController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<ReporteContratoController>
        [HttpGet("GetContratos")]
        public ActionResult<IEnumerable<ReporteContratoDTO>> GetContratos()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            var report = ctx.Empleados
                .Select(emp => new ReporteContratoDTO
                {
                    IdEmpleado = emp.IdEmpleado,
                    NombreCompleto = emp.NombreCompleto,
                    FechaContratacion = emp.FechaContratacion,
                    FechaVencimiento = emp.FechaContratacion.AddMonths(1),
                    DiasRestantes = Math.Max(
                (emp.FechaContratacion.AddMonths(1).ToDateTime(new TimeOnly())
                 - DateTime.Today).Days,
                 0),
                    EstadoEmpleado = emp.EstadoEmpleado
                })
                .ToList();

            return Ok(report);
        }

        [HttpGet("GetContratosFinalizados")]
        public ActionResult<IEnumerable<ReporteContratoDTO>> GetContratosFinalizados()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);

            var report = ctx.Empleados
                .Where(emp => emp.FechaContratacion.AddMonths(1) < hoy && emp.EstadoEmpleado == "Finalizado")
                .Select(emp => new ReporteContratoDTO
                {
                    IdEmpleado = emp.IdEmpleado,
                    NombreCompleto = emp.NombreCompleto,
                    FechaContratacion = emp.FechaContratacion,
                    FechaVencimiento = emp.FechaContratacion.AddMonths(1),
                    DiasRestantes = 0, // ya pasó
                    EstadoEmpleado = emp.EstadoEmpleado
                })
                .ToList();

            return Ok(report);
        }
    }
}
