using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNPHU_Vacantes.DTOs;
using api.Repository;
using UNPHU_Vacantes.Models;

namespace api.Interfaces
{
    public interface IVacantsInterface
    {
        Task<List<VacantDTO>> GetVacanciesByCompanyIdAsync(int companyId);
        Task<IEnumerable<Vacant>> GetVacants();
        Task<Vacant> GetVacantById(int id);
        Task<Vacant> AddVacant(Vacant vacant);
        Task<Vacant> UpdateVacant(Vacant vacant);
        Task DeleteVacant(int id);

    }
}