using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UNPHU_Vacantes.DTOs;
using UNPHU_Vacantes.Models;
using UNPHU_Vacantes.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace UNPHU_Vacantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacantController : ControllerBase
    {
        private readonly IVacantRepository _vacantRepository;
        private readonly ApplicationDbContext _context;

        public VacantController(IVacantRepository vacantRepository, ApplicationDbContext context)
        {
            _vacantRepository = vacantRepository;
            _context = context;
        }

        private int GetAuthenticatedCompanyId()
        {
            var companyIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            return companyIdClaim != null ? int.Parse(companyIdClaim.Value) : 0;
        }

        // POST: api/Vacant
        // Método CreateVacant en el controlador
        [HttpPost]
        public async Task<ActionResult<VacantDTO>> CreateVacant([FromBody] CreateVacantDto createVacantDto)
        {
            if (createVacantDto == null)
            {
                return BadRequest();
            }

            var companyId = GetAuthenticatedCompanyId();

            var vacant = new Vacant
            {
                Title = createVacantDto.Title,
                Area = createVacantDto.Area,
                Modality = createVacantDto.Modality,
                Salary = (int)createVacantDto.Salary,
                Description = string.IsNullOrEmpty(createVacantDto.Description) ? "" : createVacantDto.Description,
                Status = true,
                CompanyId = companyId
            };

            _context.Vacants.Add(vacant);
            await _context.SaveChangesAsync();

            // Cargar la compañía asociada usando Include
            var createdVacant = await _context.Vacants
                .Include(v => v.Company)
                .FirstOrDefaultAsync(v => v.Id == vacant.Id);

            var vacantDto = new VacantDTO
            {
                Id = createdVacant.Id,
                Title = createdVacant.Title,
                Description = createdVacant.Description,
                Area = createdVacant.Area,
                Modality = createdVacant.Modality,
                Salary = createdVacant.Salary,
                Status = createdVacant.Status,
                CompanyName = createdVacant.Company?.Name // Nombre de la compañía
            };

            return CreatedAtAction(nameof(GetVacant), new { id = vacantDto.Id }, vacantDto);
        }


        // POST: api/Vacant/{vacantId}/apply
        [HttpPost("{vacantId}/apply")]
        public async Task<IActionResult> ApplyToVacant(int vacantId, [FromBody] ApplyToVacantDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var vacant = await _context.Vacants
                .Include(v => v.Applications) // Incluimos las aplicaciones de la vacante
                .FirstOrDefaultAsync(v => v.Id == vacantId);

            if (vacant == null)
                return NotFound("Vacante no encontrada");

            var existingApplication = vacant.Applications.FirstOrDefault(a => a.AppUserId == userId);
            if (existingApplication != null)
            {
                return BadRequest("Ya has aplicado a esta vacante.");
            }

            var application = new Application
            {
                VacantId = vacantId,
                AppUserId = userId,
                CvUrl = model.CvUrl,
                ApplicationDate = DateTime.UtcNow
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return Ok("Aplicación enviada con éxito");
        }
        // POST: api/Vacant/{vacantId}/save
[HttpPost("{vacantId}/save")]
[Authorize]
public async Task<IActionResult> SaveVacant(int vacantId)
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId == null)
    {
        return Unauthorized();
    }

    var vacant = await _context.Vacants.FindAsync(vacantId);

    if (vacant == null)
    {
        return NotFound("Vacante no encontrada.");
    }

    // Verificar si la vacante ya está guardada por el usuario
    var existingSavedVacant = await _context.SavedVacants
        .FirstOrDefaultAsync(sv => sv.AppUserId == userId && sv.VacantId == vacantId);

    if (existingSavedVacant != null)
    {
        return BadRequest("Esta vacante ya está guardada.");
    }

    var savedVacant = new SavedVacant
    {
        AppUserId = userId,
        VacantId = vacantId
    };

    _context.SavedVacants.Add(savedVacant);
    await _context.SaveChangesAsync();

    return Ok("Vacante guardada con éxito.");
}


        // GET: api/Vacant
        // GET: api/Vacant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> GetVacants()
        {
            var vacants = await _context.Vacants
                .Include(v => v.Company) // Cargar datos relacionados con la compañía
                .ToListAsync();

            var vacantDtos = vacants.Select(v => new VacantDTO
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Area = v.Area,
                Modality = v.Modality,
                Salary = v.Salary,
                Status = v.Status,
                CompanyName = v.Company?.Name // Nombre de la compañía
            }).ToList();

            return Ok(vacantDtos);
        }




        // GET: api/Vacant/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VacantDTO>> GetVacant(int id)
        {
            var vacant = await _vacantRepository.GetVacantById(id);

            if (vacant == null)
            {
                return NotFound();
            }

            var vacantDto = new VacantDTO
            {
                Id = vacant.Id,
                Title = vacant.Title,
                Description = vacant.Description,
                Salary = vacant.Salary,
                Area = vacant.Area,
                Modality = vacant.Modality,
                Status = vacant.Status
            };

            return Ok(vacantDto);
        }

        // PUT: api/Vacant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacant(int id, [FromBody] VacantUpdateDTO vacantDto)
        {
            if (vacantDto == null || id != vacantDto.Id)
            {
                return BadRequest("El ID de la vacante no coincide.");
            }

            // 1. Obtener la vacante existente de la base de datos
            var existingVacant = await _vacantRepository.GetVacantById(id);

            if (existingVacant == null)
            {
                return NotFound();
            }

            // 2. Actualizar las propiedades de la vacante existente
            existingVacant.Title = vacantDto.Title;
            existingVacant.Description = vacantDto.Description;
            existingVacant.Salary = vacantDto.Salary;
            existingVacant.Area = vacantDto.Area;
            existingVacant.Modality = vacantDto.Modality;
            existingVacant.Status = vacantDto.Status;

            // 3. Actualizar la vacante en la base de datos
            var updatedVacant = await _vacantRepository.UpdateVacant(existingVacant);

            if (updatedVacant == null)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/Vacant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacant(int id)
        {
            var vacant = await _vacantRepository.GetVacantById(id);
            if (vacant == null)
            {
                return NotFound();
            }

            await _vacantRepository.DeleteVacant(id);
            return NoContent(); // 204 No Content
        }
        // DELETE: api/Vacant/{vacantId}/save
        [HttpDelete("{vacantId}/save")]
        [Authorize]
        public async Task<IActionResult> UnsaveVacant(int vacantId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var savedVacant = await _context.SavedVacants
                .FirstOrDefaultAsync(sv => sv.AppUserId == userId && sv.VacantId == vacantId);

            if (savedVacant == null)
            {
                return NotFound("Vacante guardada no encontrada.");
            }

            _context.SavedVacants.Remove(savedVacant);
            await _context.SaveChangesAsync();

            return Ok("Vacante eliminada de guardados.");
        }


        // GET: api/Vacant/{vacantId}/applications
        [HttpGet("{vacantId}/applications")]
        public async Task<IActionResult> GetApplicationsForVacant(int vacantId)
        {
            var vacant = await _context.Vacants
                .Include(v => v.Applications)  // Incluye las aplicaciones
                .ThenInclude(a => a.AppUser)  // Incluye el usuario asociado a la aplicación
                .FirstOrDefaultAsync(v => v.Id == vacantId);

            if (vacant == null)
                return NotFound(new { message = "Vacante no encontrada" });

            // Selecciona las aplicaciones y convierte a DTO
            var applications = vacant.Applications.Select(a => new ApplicationResponseDTO
            {
                Id = a.Id,
                ApplicantName = $"{a.AppUser.FirstName} {a.AppUser.LastName}",
                CvUrl = a.CvUrl,
                ApplicationDate = a.ApplicationDate
            }).ToList();

            if (applications.Count == 0)
            {
                return Ok(new { message = "No hay aplicaciones para esta vacante." });
            }

            return Ok(applications);
        }
        [HttpGet("applications")]
        public async Task<ActionResult<IEnumerable<ApplicationResponseDTO>>> GetAllApplications()
        {
            var applications = await _context.Applications
                .Include(a => a.AppUser)  // Incluye el usuario asociado a la aplicación
                .Select(a => new ApplicationResponseDTO
                {
                    Id = a.Id,
                    ApplicantName = $"{a.AppUser.FirstName} {a.AppUser.LastName}",
                    CvUrl = a.CvUrl,
                    ApplicationDate = a.ApplicationDate
                })
                .ToListAsync();

            if (applications.Count == 0)
            {
                return Ok(new { message = "No hay aplicaciones registradas." });
            }

            return Ok(applications);
        }

        // GET: api/Vacant/applied
        [HttpGet("applied")]
        [Authorize] // Requiere autenticación
        public async Task<ActionResult<IEnumerable<VacantDTO>>> GetAppliedVacants()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(); // Retorna 401 si el usuario no está autenticado
            }

            // Obtener las aplicaciones del usuario desde la base de datos
            var applications = await _context.Applications
                .Include(a => a.Vacant)  // Incluir la vacante
                .ThenInclude(v => v.Company) // Incluir la compañía de la vacante
                .Where(a => a.AppUserId == userId)
                .ToListAsync();

            // Mapear las aplicaciones a VacantDTO
            var appliedVacants = applications.Select(a => new VacantDTO
            {
                Id = a.Vacant.Id,
                Title = a.Vacant.Title,
                Description = a.Vacant.Description,
                Area = a.Vacant.Area,
                Modality = a.Vacant.Modality,
                Salary = a.Vacant.Salary,
                Status = a.Vacant.Status,
                CompanyName = a.Vacant.Company?.Name // Nombre de la compañía
            }).ToList();

            return Ok(appliedVacants);
        }
        // GET: api/Vacant/saved
        [HttpGet("saved")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> GetSavedVacants()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var savedVacants = await _context.SavedVacants
                .Include(sv => sv.Vacant)
                .ThenInclude(v => v.Company)
                .Where(sv => sv.AppUserId == userId)
                .ToListAsync();

            var vacantDtos = savedVacants.Select(sv => new VacantDTO
            {
                Id = sv.Vacant.Id,
                Title = sv.Vacant.Title,
                Description = sv.Vacant.Description,
                Area = sv.Vacant.Area,
                Modality = sv.Vacant.Modality,
                Salary = sv.Vacant.Salary,
                Status = sv.Vacant.Status,
                CompanyName = sv.Vacant.Company?.Name
            }).ToList();

            return Ok(vacantDtos);
        }
        

    }
}
