using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;
using api.Dtos;
using FinShark.Dtos; // Asegúrate de que este using esté presente

namespace api.interfaces
{
    public interface ICompanyInterface
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetBySymbolAsync(string symbol);
        Task<Company?> AuthenticateAsync(string symbol, string password);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<IEnumerable<GetCompanyDTO>> GetFilteredCompaniesAsync(QueryObject query, int pageNumber, int pageSize, string sortBy, bool isDescending); // Modifica esta línea
    }
}
