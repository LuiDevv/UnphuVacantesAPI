using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using Microsoft.EntityFrameworkCore;
using UNPHU_Vacantes.DTOs;
using UNPHU_Vacantes.Models;
using api.Interfaces;

namespace UNPHU_Vacantes.Repositories
{
    public interface IVacantRepository
    {
        Task<List<Vacant>> GetVacants();
        Task<Vacant> GetVacantById(int id);
        Task<Vacant> CreateVacant(Vacant vacant);
        Task<Vacant> UpdateVacant(Vacant vacant);
        Task DeleteVacant(int id);
    }

    public class VacantRepository : IVacantRepository, IVacantsInterface
    {
        private readonly ApplicationDbContext _context;

        public VacantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vacant>> GetVacants()
        {
            return await _context.Vacants.ToListAsync();
        }

        public async Task<Vacant> GetVacantById(int id)
        {
            return await _context.Vacants.FindAsync(id);
        }

        public async Task<Vacant> CreateVacant(Vacant vacant)
        {
            _context.Vacants.Add(vacant);
            await _context.SaveChangesAsync();
            return vacant;
        }

        public async Task<Vacant> UpdateVacant(Vacant vacant)
        {
            _context.Vacants.Update(vacant);
            await _context.SaveChangesAsync();
            return vacant;
        }

        public async Task DeleteVacant(int id)
        {
            var vacant = await _context.Vacants.FindAsync(id);
            if (vacant != null)
            {
                _context.Vacants.Remove(vacant);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<VacantDTO>> GetVacanciesByCompanyIdAsync(int companyId)
        {
            return await _context.Vacants
                .Where(v => v.CompanyId == companyId)
                .Select(v => new VacantDTO
                {
                    Id = v.Id,
                    Title = v.Title,
                    Description = v.Description,
                    CompanyId = v.CompanyId,
                    Salary = v.Salary,
                    Area = v.Area,
                    Modality = v.Modality,
                    Status = v.Status
                })
                .ToListAsync();
        }
    }
}
