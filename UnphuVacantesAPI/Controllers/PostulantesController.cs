using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Models;
using UnphuVacantesAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class PostulantesController : ControllerBase
{
    private readonly UnphuVacantesDbContext _context;

    public PostulantesController(UnphuVacantesDbContext context)
    {
        _context = context;
    }

    // GET: api/Postulantes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Postulante>>> GetPostulantes()
    {
        return await _context.Postulantes.ToListAsync();
    }

    // GET: api/Postulantes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Postulante>> GetPostulante(int id)
    {
        var postulante = await _context.Postulantes.FindAsync(id);
        if (postulante == null)
        {
            return NotFound();
        }
        return postulante;
    }

    // POST: api/Postulantes
    [HttpPost]
    public async Task<ActionResult<Postulante>> PostPostulante(Postulante postulante)
    {
        _context.Postulantes.Add(postulante);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPostulante), new { id = postulante.Id }, postulante);
    }

    // PUT: api/Postulantes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPostulante(int id, Postulante postulante)
    {
        if (id != postulante.Id)
        {
            return BadRequest();
        }

        _context.Entry(postulante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Postulantes.Any(p => p.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Postulantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostulante(int id)
    {
        var postulante = await _context.Postulantes.FindAsync(id);
        if (postulante == null)
        {
            return NotFound();
        }

        _context.Postulantes.Remove(postulante);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
