
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
    public class JobApplicationController
    {
        [Route("api/[controller]")]
[ApiController]
public class JobApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplication>>> GetApplications()
    {
        return await _context.JobApplications.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<JobApplication>> CreateApplication(JobApplication application)
    {
        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetApplications), new { id = application.Id }, application);
    }
}

    }
}