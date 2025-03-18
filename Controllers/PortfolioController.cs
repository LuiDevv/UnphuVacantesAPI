using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Identity;
using api.interfaces;
using Microsoft.AspNetCore.Authorization;
using api.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyInterface _companyInterface;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, ICompanyInterface companyInterface, 
        IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _companyInterface = companyInterface;
            _portfolioRepo = portfolioRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var normalizedUsername = username.ToUpper();
            var appUser = await _userManager.FindByNameAsync(normalizedUsername); // Buscar con el nombre normalizado
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var company = await _companyInterface.GetBySymbolAsync(symbol);

            if (company == null)
            {
                return BadRequest("Company does not exist");
            }
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Company already in portfolio");
            }

            var portfolioModel = new Portfolio
            {
                CompanyId = company.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepo.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Error adding company to portfolio");
            }
            else
            {
                return Created();

            }
        }
        [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(int companyId)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        // Obtener el portafolio del usuario
        var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

        // Filtrar el portafolio por el ID de la compañía
        var filteredCompany = userPortfolio.FirstOrDefault(s => s.Id == companyId);

        if (filteredCompany == null)
        {
            return BadRequest("Company not in portfolio");
        }

        // Eliminar la compañía del portafolio
        await _portfolioRepo.DeletePortfolio(appUser, companyId);

        return Ok();
    }

        
            
    }

            
}


