using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using TeamProjectMVC.Repository;
using TeamProjectMVC.Services;

namespace TeamProjectMVC.Controllers
{

   // [Authorize(Roles ="ADMIN")]
    public class AuditsController : Controller
    {

        private readonly AuditLogService _auditLogService;

        public AuditsController(AuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // Get the audit logs as JSON and return the response to the UI.
        public async Task<IActionResult> Index()
        {
            var auditLogsAsJson = await _auditLogService.GetAuditLogsAsJsonAsync();
            // return Content(auditLogsAsJson, "application/json", Encoding.UTF8);
            var auditLogs = JsonConvert.DeserializeObject<IEnumerable<TeamProjectMVC.Models.Audit>>(auditLogsAsJson);

            return View(auditLogs);

        }
    }
}
