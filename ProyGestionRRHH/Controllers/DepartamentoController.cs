using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.DepartamentoDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        //variable privada
        private readonly GestionRrhhContext ctx;
        public DepartamentoController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<DepartamentoController>
        [HttpGet("GetDepartamentos")]
        public async Task<IActionResult> GetDepartamentos()
        {
            var departamentos = await ctx.Departamentos.ToListAsync();

            var departamentosDto = departamentos.Select(d => new DepartamentoDTO
            {
                IdDepartamento = d.IdDepartamento,
                Codigo = d.Codigo,
                Nombre = d.Nombre,
                Enabled = d.Enabled
            });

            return Ok(departamentosDto);
        }

        // GET api/<DepartamentoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DepartamentoController>
        [HttpPost("DepartamentoPost")]
        public async Task<IActionResult> DepartamentoPost([FromBody] DepartamentoDTO departamentoDto)
        {
            var departamento = new Departamento
            {
                Codigo = departamentoDto.Codigo,
                Nombre = departamentoDto.Nombre,
                Enabled = departamentoDto.Enabled
            };

            ctx.Departamentos.Add(departamento);
            await ctx.SaveChangesAsync();

            return Ok("Departamento registrado con éxito");
        }

        // PUT api/<DepartamentoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartamentoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
