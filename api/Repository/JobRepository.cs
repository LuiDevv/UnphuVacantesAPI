using api.Data;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository
{
    public class JobRepository : IJobInterface
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.Include(j => j.Company).ToListAsync();
        }

        public async Task<Job?> GetByIdAsync(Guid id)
        {
            var job = await _context.Jobs.Include(j => j.Company).FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
            {
                throw new InvalidOperationException("Job not found");
            }
            return job;
        }

        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IJobRepository
    {
    }
}
