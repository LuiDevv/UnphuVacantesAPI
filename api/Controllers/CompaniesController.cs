using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Helpers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using api.Dtos.Account;
using api.Service;
using api.interfaces;
using api.Dtos;
using Microsoft.Extensions.Logging;
using FinShark.Dtos;
using api.Interfaces;
using UNPHU_Vacantes.DTOs; // Añade esta línea


[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly ICompanyInterface _companyRepository;
    private readonly ILogger<CompaniesController> _logger; // Añade esta línea

     private readonly IVacantsInterface _vacantRepository;

    public CompaniesController(ApplicationDbContext context, ITokenService tokenService, ICompanyInterface companyRepository,IVacantsInterface vacantRepository, ILogger<CompaniesController> logger) // Modifica el constructor
    {
        _context = context;
        _tokenService = tokenService;
        _companyRepository = companyRepository; // Usa la inyección de dependencias correctamente
        _logger = logger;
        _vacantRepository = vacantRepository; // Inicializa el logger
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCompanies([FromQuery] QueryObject query, int pageNumber = 1, int pageSize = 10, string sortBy = "Name", bool isDescending = false)
    {
        // Llama al método del repositorio inyectado correctamente
        var companies = await _companyRepository.GetFilteredCompaniesAsync(query, pageNumber, pageSize, sortBy, isDescending);
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(int id) // Cambiado a int
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null) return NotFound();
        return company;
    }

    [HttpPost] // Este es el método de registro
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            // Hashea la contraseña antes de guardar la compañía
            company.Password = PasswordHelper.HashPassword(company.Password);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }

        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Buscar la compañía por Symbol (el nombre de usuario en este caso)
    var company = await _context.Companies.FirstOrDefaultAsync(x => x.Symbol == loginRequestDTO.Symbol.ToLower());

    if (company == null)
        return Unauthorized("Invalid symbol");

    // Verificar la contraseña
    if (!PasswordHelper.VerifyPassword(loginRequestDTO.Password, company.Password))
    {
        return Unauthorized("Symbol or password is incorrect");
    }

    // Generar el token
    var token = _tokenService.GenerateToken(company);

    // Enviar el token en la respuesta JSON
    return Ok(new 
    {
        Symbol = company.Symbol,
        Email = company.ContactEmail,
        Token = token
    });
}

    [HttpGet("current-company")]
[Authorize]
public async Task<IActionResult> GetCurrentCompany()
{
    // Verificar autenticación primero
    if (User?.Identity?.IsAuthenticated != true)
    {
        return Unauthorized("Compañía no autenticada.");
    }

    // Obtener el ID de la empresa desde el token JWT
    var companyIdClaim = User.FindFirst("CompanyId")?.Value;

    if (string.IsNullOrEmpty(companyIdClaim) || !int.TryParse(companyIdClaim, out var companyId))
    {
        return BadRequest("ID de la compañía no válido o no proporcionado en el token.");
    }

    // Buscar la empresa en la base de datos
    var company = await _context.Companies.FindAsync(companyId);
    if (company == null)
    {
        return NotFound("Compañía no encontrada en la base de datos.");
    }

    // Devolver la información de la compañía
    return Ok(new CompanyDTO
    {
        Id = company.Id,
        Name = company.Name,
        Description = company.Description,
        ContactEmail = company.ContactEmail,
        Phone = company.Phone,
        Location = company.Location,
        RNC = company.RNC,
        Symbol = company.Symbol,
        ProfilePicture = company.ProfilePicture,
        IsApprovedByUNPHU = company.IsApprovedByUNPHU
    });
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, Company company)
    {
        if (id != company.Id) return BadRequest();
        _context.Entry(company).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    
    
    }
    [HttpPut("update-profile")]
    [Authorize]
    public async Task<IActionResult> UpdateCompanyProfile([FromBody] CompanyDTO updateCompanyProfileDto)
    {
        // Verificar autenticación
        if (User?.Identity?.IsAuthenticated != true)
        {
            return Unauthorized("Compañía no autenticada.");
        }

        // Obtener el claim "id" del token JWT
        var companyIdClaim = User.FindFirst("CompanyId")?.Value;

        if (string.IsNullOrEmpty(companyIdClaim) || !int.TryParse(companyIdClaim, out var companyId))
        {
            return BadRequest("ID de la compañía no válido o no proporcionado en el token.");
        }

        // Buscar la compañía en la base de datos
        var company = await _context.Companies.FindAsync(companyId);
        if (company == null)
        {
            return NotFound("Compañía no encontrada.");
        }

        // Actualizar los campos permitidos
        company.Name = updateCompanyProfileDto.Name ?? company.Name;
        company.Description = updateCompanyProfileDto.Description ?? company.Description;
        company.ContactEmail = updateCompanyProfileDto.ContactEmail ?? company.ContactEmail;
        company.Phone = updateCompanyProfileDto.Phone ?? company.Phone;
        company.Location = updateCompanyProfileDto.Location ?? company.Location;
        company.RNC = updateCompanyProfileDto.RNC ?? company.RNC;
        company.ProfilePicture = updateCompanyProfileDto.ProfilePicture ?? company.ProfilePicture;

        // Guardar los cambios
        try
        {
            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Devolver la información actualizada
            return Ok(new { message = "Perfil de la compañía actualizado correctamente.", Company = updateCompanyProfileDto });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al actualizar el perfil: {ex.Message}");
        }
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
        [HttpGet("company/{companyId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> GetCompanyVacancies(int companyId)
        {
        var company = await _context.Companies.FindAsync(companyId);

            if (company == null)
            {
                return NotFound("Company not found");
            }
            // Aquí usamos el repositorio de vacantes inyectado para obtener las vacantes
            var vacancies = await _vacantRepository.GetVacanciesByCompanyIdAsync(companyId);

            return Ok(vacancies);
        }
        
    }

