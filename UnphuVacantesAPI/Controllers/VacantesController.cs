using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Data; 
using UnphuVacantesAPI.Models;


[Route("api/[controller]")]
[ApiController]
public class VacantesController : ControllerBase
{
    private readonly UnphuVacantesDbContext _context;

    public VacantesController(UnphuVacantesDbContext context)
    {
        _context = context;
    }

    // GET: api/Vacantes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vacante>>> GetVacantes()
    {
        return await _context.Vacantes.Include(v => v.Empresa).ToListAsync();
    }

    // GET: api/Vacantes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vacante>> GetVacante(int id)
    {
        var vacante = await _context.Vacantes.FindAsync(id);
        if (vacante == null)
        {
            return NotFound();
        }
        return vacante;
    }

    // POST: api/Vacantes
    [HttpPost]
    public async Task<ActionResult<Vacante>> PostVacante(Vacante vacante)
    {
        _context.Vacantes.Add(vacante);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVacante), new { id = vacante.Id }, vacante);
    }

    // PUT: api/Vacantes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVacante(int id, Vacante vacante)
    {
        if (id != vacante.Id)
        {
            return BadRequest();
        }

        _context.Entry(vacante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Vacantes.Any(v => v.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Vacantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacante(int id)
    {
        var vacante = await _context.Vacantes.FindAsync(id);
        if (vacante == null)
        {
            return NotFound();
        }

        _context.Vacantes.Remove(vacante);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
