using Actividades_Semestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Actividades_Semestral.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ActividadesController : ControllerBase
    //{
    //    private readonly GestorActividadesContext _context;

    //    public ActividadesController(GestorActividadesContext context)
    //    {
    //        _context = context;
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Actividade>>> GetActividadesAsync()
    //    {
    //        var gestorActividadesContext = _context.Actividades.Include(a => a.IdEstadoNavigation).Include(a => a.IdSubcategoriaNavigation);
    //        return View(await gestorActividadesContext.ToListAsync());
    //    }

    //    private ActionResult<IEnumerable<Actividade>> View(List<Actividade> actividades)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
