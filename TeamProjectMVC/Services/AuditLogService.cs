
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TeamProjectMVC.Data;

namespace TeamProjectMVC.Services
{
    public class AuditLogService
    {
        private readonly AppDbContext _dbContext;

        public AuditLogService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetAuditLogsAsJsonAsync()
        {
            var auditLogs = await _dbContext.AuditLogs.ToListAsync();
            return JsonConvert.SerializeObject(auditLogs);
        }
    }
}
