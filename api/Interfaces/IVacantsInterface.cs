using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNPHU_Vacantes.DTOs;
using api.Repository;

namespace api.Interfaces
{
    public interface IVacantsInterface
    {
        Task<List<VacantDTO>> GetVacanciesByCompanyIdAsync(int companyId);

    }
}