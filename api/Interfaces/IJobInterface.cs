using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface IJobInterface
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job?> GetByIdAsync(Guid id);
        Task AddAsync(Job job);
        Task UpdateAsync(Job job);
        Task DeleteAsync(Guid id);
    }
}