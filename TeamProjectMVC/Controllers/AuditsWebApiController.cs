using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Models;
using TeamProjectMVC.Services;

namespace TeamProjectMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsWebApiController : ControllerBase
    {

        private readonly AuditLogService _auditLogService;
        private readonly UserManager<User> _userManager;

        public AuditsWebApiController(AuditLogService auditLogService, UserManager<User> userManager)
        {
            _auditLogService = auditLogService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits(DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                bool step =  endDate.Value < startDate.Value;
                if (step)
                {
                    return BadRequest("EndDate must be after startDate");
                }
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();

            }
            var user = await _userManager.FindByIdAsync(userId);
            var rolesAsync = await _userManager.GetRolesAsync(user);
            if (rolesAsync[0] != "ADMIN")
            {
                return Unauthorized();

            }
            if (!startDate.HasValue && !endDate.HasValue)
            {
                return Ok(await _auditLogService.GetAuditLogs());
            }
            if (startDate.HasValue && !endDate.HasValue)
            {
                endDate = DateTime.MaxValue;
            }
            if (!startDate.HasValue && endDate.HasValue)
            {
                startDate = DateTime.MinValue;
            }

            var audits = await _auditLogService.Filter(startDate, endDate);
            return Ok(audits);
        }
    }
}
