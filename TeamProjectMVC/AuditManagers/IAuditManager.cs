using TeamProjectMVC.Entity;

namespace TeamProjectMVC.AuditManagers
{
    public interface IAuditManager
    {
        Task WriteAuditLog(AuditLog log);
        Task<IEnumerable<AuditLog>> GetAuditLogs();
    }
}
