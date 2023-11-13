using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Models;

namespace TeamProjectMVC.Data
{
    public class AppDbContext :  IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider services, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.Services = services;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Product> Products { get; set; }  
        public IServiceProvider Services { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }

        //********************************************************************
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration(Services));
            builder.Entity<Product>().HasData(
                
           new Product { Id="1", Name="HDD 1TB",  Quantity=55, Price=74.09, ToTalPrice=411569.95 },
           new Product { Id = "2", Name = "HDD SSD 512GB", Quantity = 102, Price = 190.99, ToTalPrice =1967578.98 },
           new Product { Id = "3", Name = "RAM DDR4 16GB", Quantity = 47, Price = 80.32, ToTalPrice =38127904 }
                );
        }







        //  /********************************** AUDIT********************************************


        public virtual async Task<int> SaveChangesAsync(string userId, string userName)
        {
            OnBeforeSaveChanges(userId, userName);
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSaveChanges(string userId, string userName)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntry.UserName = userName; ;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Entity.Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Entity.Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Entity.Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                if (property.CurrentValue != null)
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
        //  /********************************** AUDIT  ENDS ********************************************

    }

}
