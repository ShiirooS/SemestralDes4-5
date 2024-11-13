using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividades_Semestral.Models;

namespace Actividades_Semestral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestasActividadesController : ControllerBase  
    {
        private readonly GestorActividadesContext _context;

        public PropuestasActividadesController(GestorActividadesContext context)
        {
            _context = context;
        }

        // GET: api/PropuestasActividades
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var propuestasActividades = await _context.PropuestasActividades
                .Include(p => p.IdEstadoNavigation)
                .Include(p => p.IdSubcategoriaNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .ToListAsync();

            return Ok(propuestasActividades);  
        }

        // GET: api/PropuestasActividades/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var propuestasActividade = await _context.PropuestasActividades
                .Include(p => p.IdEstadoNavigation)
                .Include(p => p.IdSubcategoriaNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPropuesta == id);

            if (propuestasActividade == null)
            {
                return NotFound();
            }

            return Ok(propuestasActividade);  
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropuestasActividade propuestasActividade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.PropuestasActividades.Add(propuestasActividade);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDetails), new { id = propuestasActividade.IdPropuesta }, propuestasActividade);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "Error en el servidor: " + ex.Message });
            }
        }

        // PUT: api/PropuestasActividades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PropuestasActividade propuestasActividade)
        {
            if (id != propuestasActividade.IdPropuesta)
            {
                return BadRequest("ID de propuesta no coincide.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propuestasActividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropuestasActividadeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);  
        }

        // DELETE: api/PropuestasActividades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var propuestasActividade = await _context.PropuestasActividades.FindAsync(id);
            if (propuestasActividade == null)
            {
                return NotFound();
            }

            _context.PropuestasActividades.Remove(propuestasActividade);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PropuestasActividadeExists(int id)
        {
            return _context.PropuestasActividades.Any(e => e.IdPropuesta == id);
        }
    }
}
