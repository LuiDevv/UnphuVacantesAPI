using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Company>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);

        // Eliminar la definición con el parámetro 'symbol' y mantener la de 'companyId'
        Task<Portfolio> DeletePortfolio(AppUser appUser, int companyId);
    }
}
