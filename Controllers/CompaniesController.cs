using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Helpers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;


[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCompanies([FromQuery] QueryObject query, int pageNumber = 1, int pageSize = 10, string sortBy = "Name", bool isDescending = false)
    {
        var companyRepository = new CompanyRepository(_context);
        var companies = await companyRepository.GetFilteredCompaniesAsync(query, pageNumber, pageSize, sortBy, isDescending);
        return Ok(companies);
    }





    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(int id) // Cambiado a int
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null) return NotFound();
        return company;
    }

    [HttpPost]
    public async Task<ActionResult<Company>> CreateCompany(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, Company company)
    {
        if (id != company.Id) return BadRequest();
        _context.Entry(company).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id) // Cambiado a int
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null) return NotFound();
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
