using api.Data;
using api.Dtos;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository
{
    public class UserRepository : IUserInterface
    {
        public Task<IEnumerable<JobDTO>> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public Task<JobDTO> GetJobByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobDTO> CreateJob(CreateJobRequestDTO createJobRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateJob(int id, UpdateJobRequest updateJobRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteJob(int id)
        {
            throw new NotImplementedException();
        }
    
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }

    
}
