using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.PuestoDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        //variable privada
        private readonly GestionRrhhContext ctx;
        public PuestoController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<PuestoController>
        [HttpGet("GetPuestos")]
        public async Task<ActionResult<IEnumerable<PuestoListDTO>>> GetPuestos()
        {
            var puestos = await ctx.Puestos
        .Include(p => p.IdDepartamentoNavigation)
        .Select(p => new PuestoListDTO
        {
            IdPuesto = p.IdPuesto,
            Titulo = p.Titulo,
            SalarioBase = p.SalarioBase,
            Enabled = p.Enabled,
            NombreDepartamento = p.IdDepartamentoNavigation.Nombre // 👈 aquí traes el nombre
        })
        .ToListAsync();

            return Ok(puestos);
        }

        // GET api/<PuestoController>/5
        [HttpGet("GetPuesto/{id}")]
        public async Task<ActionResult<PuestoDTO>> GetPuesto(long id)
        {
            var puesto = await ctx.Puestos
                .Where(p => p.IdPuesto == id)
                .Select(p => new PuestoDTO
                {
                    IdPuesto = p.IdPuesto,
                    Titulo = p.Titulo,
                    SalarioBase = p.SalarioBase,
                    IdDepartamento = p.IdDepartamento,
                    Enabled = p.Enabled
                })
                .FirstOrDefaultAsync();

            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // POST api/<PuestoController>
        [HttpPost("PuestoPost")]
        public async Task<ActionResult<PuestoDTO>> PostPuesto(PuestoDTO puestoDto)
        {
            var puesto = new Puesto
            {
                Titulo = puestoDto.Titulo,
                SalarioBase = puestoDto.SalarioBase,
                IdDepartamento = puestoDto.IdDepartamento,
                Enabled = puestoDto.Enabled
            };

            ctx.Puestos.Add(puesto);
            await ctx.SaveChangesAsync();

            puestoDto.IdPuesto = puesto.IdPuesto;

            return CreatedAtAction(nameof(GetPuesto), new { id = puesto.IdPuesto }, puestoDto);
        }
        

        // PUT api/<PuestoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PuestoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
