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
    public class ActividController : ControllerBase  
    {
        private readonly GestorActividadesContext _context;

        public ActividController(GestorActividadesContext context)
        {
            _context = context;
        }

        // GET: api/Activid
        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var gestorActividades = await _context.Actividades
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdSubcategoriaNavigation)
                .ToListAsync();

            return Ok(gestorActividades);  
        }

        // GET: api/Activid/5
        [HttpGet("{id}")]  
        public async Task<IActionResult> GetDetails(int id)
        {
            var actividade = await _context.Actividades
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdSubcategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdActividad == id);

            if (actividade == null)
            {
                return NotFound();
            }

            return Ok(actividade);  
        }
        // GET: api/Activid/subcategoria/
        [HttpGet("subcategoria/{idSubcategoria}")]
        public async Task<IActionResult> GetBySubcategoria(int idSubcategoria)
        {
            var actividades = await _context.Actividades
                .Where(a => a.IdSubcategoria == idSubcategoria)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdSubcategoriaNavigation)
                .ToListAsync();

            if (actividades == null || !actividades.Any())
            {
                return NotFound();
            }

            return Ok(actividades);
        }

        // POST: api/Activid
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Actividade actividade)  
        {
            if (ModelState.IsValid)
            {
                _context.Add(actividade);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDetails), new { id = actividade.IdActividad }, actividade);
            }
            return BadRequest(ModelState);  
        }

        // PUT: api/Activid/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Actividade actividade)
        {
            if (id != actividade.IdActividad)
            {
                return BadRequest("Activity ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActividadeExists(id))
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

        // DELETE: api/Activid/
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var actividade = await _context.Actividades.FindAsync(id);
            if (actividade == null)
            {
                return NotFound();
            }

            _context.Actividades.Remove(actividade);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ActividadeExists(int id)
        {
            return _context.Actividades.Any(e => e.IdActividad == id);
        }
    }
}
