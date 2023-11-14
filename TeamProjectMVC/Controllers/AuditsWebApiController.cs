using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Models;

namespace TeamProjectMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuditsWebApiController(AppDbContext context) => _context = context;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits()
        {
            var audits = await _context.AuditLogs.ToListAsync();
            return Ok(audits);
        }
    }
}
