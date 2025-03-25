using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UNPHU_Vacantes.DTOs;
using UNPHU_Vacantes.Models;
using UNPHU_Vacantes.Repositories;

namespace UNPHU_Vacantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacantController : ControllerBase
    {
        private readonly IVacantRepository _vacantRepository;
        private readonly ApplicationDbContext _context; // Use the derived context

        public VacantController(IVacantRepository vacantRepository, ApplicationDbContext context) // Use ApplicationDbContext parameter
        {
            _vacantRepository = vacantRepository;
            _context = context; // Assign the injected context to the private field
        }
        private int GetAuthenticatedCompanyId()
{
    var companyIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CompanyId");
    return companyIdClaim != null ? int.Parse(companyIdClaim.Value) : 0;
}


    

        // POST: api/Vacant
        [HttpPost]
        public async Task<ActionResult<VacantDTO>> CreateVacant([FromBody] CreateVacantDto createVacantDto)
        {
            if (createVacantDto == null)
            {
                return BadRequest();
            }

            // Suponiendo que el CompanyId se obtiene desde el token JWT del usuario autenticado
            var companyId = GetAuthenticatedCompanyId();  // Método que extrae el CompanyId del token

            // Asignar valores del DTO a la entidad Vacant
            var vacant = new Vacant
            {
                Title = createVacantDto.Title,
                Area = createVacantDto.Area,
                Modality = createVacantDto.Modality,
                Salary = (int)createVacantDto.Salary,
                Description = string.IsNullOrEmpty(createVacantDto.Description) ? "" : createVacantDto.Description,
                Status = true, // Siempre es true
                CompanyId = companyId  // Asociar la vacante con el CompanyId
            };

            _context.Vacants.Add(vacant);
            await _context.SaveChangesAsync();

            // Crear el DTO con la vacante creada
            var vacantDto = new VacantDTO
            {
                Id = vacant.Id,
                Title = vacant.Title,
                Description = vacant.Description,
                Area = vacant.Area,
                Modality = vacant.Modality,
                Salary = vacant.Salary,
                Status = vacant.Status
            };

            return CreatedAtAction(nameof(GetVacant), new { id = vacantDto.Id }, vacantDto);
        }



        // GET: api/Vacant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacantDTO>>> GetVacants()
        {
            var vacants = await _vacantRepository.GetVacants();
            var vacantDtos = new List<VacantDTO>();

            foreach (var vacant in vacants)
            {
                vacantDtos.Add(new VacantDTO
                {
                    Id = vacant.Id,
                    Title = vacant.Title,
                    Description = vacant.Description,
                    Salary = vacant.Salary,
                    Area = vacant.Area,
                    Modality = vacant.Modality,
                    Status = vacant.Status
                });
            }

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
        public async Task<IActionResult> UpdateVacant(int id, [FromBody] VacantDTO vacantDto)
        {
            if (vacantDto == null || id != vacantDto.Id)
            {
                return BadRequest("El ID de la vacante no coincide.");
            }

            var vacant = new Vacant
            {
                Id = vacantDto.Id,
                Title = vacantDto.Title,
                Description = vacantDto.Description,
                Salary = vacantDto.Salary,
                Area = vacantDto.Area,
                Modality = vacantDto.Modality,
                Status = vacantDto.Status
            };

            var updatedVacant = await _vacantRepository.UpdateVacant(vacant);
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
        
    }
}
