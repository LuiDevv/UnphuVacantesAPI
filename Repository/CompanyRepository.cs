using api.Data;
using api.Helpers;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository
{
    public class CompanyRepository : ICompanyInterface
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Company>> GetFilteredCompaniesAsync(QueryObject query, int pageNumber = 1, int pageSize = 10, string sortBy = "Name", bool isDescending = false)
        {
            var companyQuery = _context.Companies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                companyQuery = companyQuery.Where(c => c.Name.ToLower().Contains(query.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                companyQuery = companyQuery.Where(c => c.Location.ToLower().Contains(query.Location.ToLower()));
            }

            if (query.IsApprovedByUNPHU.HasValue)
            {
                companyQuery = companyQuery.Where(c => c.IsApprovedByUNPHU == query.IsApprovedByUNPHU);
            }

            // Ordenación dinámica
            if (isDescending)
            {
                companyQuery = companyQuery.OrderByDescending(c => EF.Property<object>(c, sortBy));
            }
            else
            {
                companyQuery = companyQuery.OrderBy(c => EF.Property<object>(c, sortBy));
            }

            // Paginación
            companyQuery = companyQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await companyQuery.ToListAsync();
        }



    }
}
