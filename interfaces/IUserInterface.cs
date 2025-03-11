using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<JobDTO>> GetAllJobs();
        Task<JobDTO> GetJobByIdAsync(int id);
        Task<JobDTO> CreateJob(CreateJobRequestDTO request);
        Task<bool> UpdateJob(int id, UpdateJobRequest request);
        Task<bool> DeleteJob(int id);
    }
}