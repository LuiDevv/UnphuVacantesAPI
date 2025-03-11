using api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Comment>> GetByJobIdAsync(Guid jobId);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Guid id);
    }
}
