using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Repository;

namespace TeamProjectMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditRepository _auditRepository;

        public AuditController(IAuditRepository auditRepository) => _auditRepository = auditRepository;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() => Ok(await _auditRepository.GetAll());
    }
}
