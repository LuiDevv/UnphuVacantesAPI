using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface ICompanyInterface
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(Guid id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Guid id);
    }
}