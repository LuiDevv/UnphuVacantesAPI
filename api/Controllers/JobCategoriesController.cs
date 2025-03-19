using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
public class JobCategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobCategory>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }
}

}