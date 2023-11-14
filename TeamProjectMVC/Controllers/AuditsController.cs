using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeamProjectMVC.Services;

namespace TeamProjectMVC.Controllers
{
    public class AuditsController : Controller
    {

        private readonly AuditLogService _auditLogService;

        public AuditsController(AuditLogService auditLogService) => _auditLogService = auditLogService;

        [HttpGet]
        public IActionResult Index()
        {
            return View("AuditType");
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditTable()
        {
             var auditLogsAsJson = await _auditLogService.GetAuditLogsAsJsonAsync();
             var auditLogs = JsonConvert.DeserializeObject<IEnumerable<Models.Audit>>(auditLogsAsJson);
            return View("Index" , auditLogs);
        }
    }
}
