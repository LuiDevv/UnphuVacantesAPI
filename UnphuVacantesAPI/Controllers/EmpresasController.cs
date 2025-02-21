using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Models;
using UnphuVacantesAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class EmpresasController : ControllerBase
{
    private readonly UnphuVacantesDbContext _context;

    public EmpresasController(UnphuVacantesDbContext context)
    {
        _context = context;
    }

    // GET: api/Empresas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
    {
        return await _context.Empresas.ToListAsync();
    }

    // GET: api/Empresas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Empresa>> GetEmpresa(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }
        return empresa;
    }

    // POST: api/Empresas
    [HttpPost]
    public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
    {
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
    }

    // PUT: api/Empresas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
    {
        if (id != empresa.Id)
        {
            return BadRequest();
        }

        _context.Entry(empresa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Empresas.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Empresas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpresa(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }

        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
