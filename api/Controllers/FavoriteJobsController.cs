
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteJobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteJob>>> GetFavorites()
        {
            return await _context.FavoriteJobs.ToListAsync();
        }
    }
}