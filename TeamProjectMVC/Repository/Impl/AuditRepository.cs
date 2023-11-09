using Microsoft.EntityFrameworkCore;
using TeamProjectMVC.Data;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository.Impl
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuditRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<List<AuditLog>> GetAll()
        {
           return await _appDbContext.AuditLogs.ToListAsync();
        }
    }
}
