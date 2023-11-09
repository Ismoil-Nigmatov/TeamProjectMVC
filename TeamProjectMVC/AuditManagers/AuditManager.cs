using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.AuditManagers
{
    public class AuditManager : IAuditManager
    {
        private readonly AppDbContext _context;

        public AuditManager(AppDbContext context) => _context = context;

        public async Task WriteAuditLog(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogs()
        {
            return await _context.AuditLogs.ToListAsync();
        }
    }
}
