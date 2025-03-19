using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.interfaces
{
    public interface IJobApplicationInterface
    {
        Task<IEnumerable<JobApplication>> GetAllAsync();
        Task<JobApplication?> GetByIdAsync(Guid id);
        Task AddAsync(JobApplication jobApplication);
        Task UpdateAsync(JobApplication jobApplication);
        Task DeleteAsync(Guid id);
    }
}