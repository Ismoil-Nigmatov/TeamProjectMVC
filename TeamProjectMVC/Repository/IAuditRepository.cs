using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Repository
{
    public interface IAuditRepository
    {
        Task<List<AuditLog>> GetAll();
    }
}
