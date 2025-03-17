using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Company>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(company => new Company
                {
                    Id = company.CompanyId,
                    Phone = company.Company.Phone,
                    Name = company.Company.Name,
                    Description = company.Company.Description,
                    ContactEmail = company.Company.ContactEmail,
                    Location = company.Company.Location,
                    
                })
                .ToListAsync();
        }
    }
}
