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

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    // Método actualizado para eliminar por ID de la compañía
    public async Task<Portfolio> DeletePortfolio(AppUser appUser, int companyId)
    {
        // Buscar el portafolio del usuario con el ID de la compañía
        var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(p => p.AppUserId == appUser.Id && p.CompanyId == companyId);

        if (portfolioModel == null)
        {
            return null; // Si no se encuentra el portafolio, devolver null
        }

        _context.Portfolios.Remove(portfolioModel); // Eliminar el portafolio
        await _context.SaveChangesAsync(); // Guardar los cambios
        return portfolioModel; // Devolver el portafolio eliminado
    }

        public Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Company>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios
            .Where(u => u.AppUserId == user.Id)
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
