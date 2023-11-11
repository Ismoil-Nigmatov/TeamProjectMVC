using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;
using TeamProjectMVC.Data;
using TeamProjectMVC.Models;
using TeamProjectMVC.Services;

namespace TeamProjectMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsWebApiController : ControllerBase
    {


        private readonly AppDbContext _context;

        public AuditsWebApiController(AppDbContext context)
        {
             _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits()
        {
            var audits = await _context.AuditLogs.ToListAsync();

            return Ok(audits);
        }

    }
}
