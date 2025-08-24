using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyGestionRRHH.DTO.RoleDTO;
using ProyGestionRRHH.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyGestionRRHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        //variable privada
        private readonly GestionRrhhContext ctx;
        public RoleController(GestionRrhhContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<RoleController>
        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            var roles = await ctx.Roles
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    Nombre = r.Nombre
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
