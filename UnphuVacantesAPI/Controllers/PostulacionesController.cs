using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Models;
using UnphuVacantesAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class PostulacionesController : ControllerBase
{
    private readonly UnphuVacantesDbContext _context;

    public PostulacionesController(UnphuVacantesDbContext context)
    {
        _context = context;
    }

    // GET: api/Postulaciones
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Postulacion>>> GetPostulaciones()
    {
        return await _context.Postulaciones
            .Include(p => p.Postulante)
            .Include(p => p.Vacante)
            .ToListAsync();
    }

    // GET: api/Postulaciones/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Postulacion>> GetPostulacion(int id)
    {
        var postulacion = await _context.Postulaciones.FindAsync(id);
        if (postulacion == null)
        {
            return NotFound();
        }
        return postulacion;
    }

    // POST: api/Postulaciones
    [HttpPost]
    public async Task<ActionResult<Postulacion>> PostPostulacion(Postulacion postulacion)
    {
        _context.Postulaciones.Add(postulacion);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPostulacion), new { id = postulacion.Id }, postulacion);
    }

    // DELETE: api/Postulaciones/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostulacion(int id)
    {
        var postulacion = await _context.Postulaciones.FindAsync(id);
        if (postulacion == null)
        {
            return NotFound();
        }

        _context.Postulaciones.Remove(postulacion);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
