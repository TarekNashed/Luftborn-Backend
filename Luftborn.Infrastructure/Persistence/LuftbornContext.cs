using Luftborn.Domain.Common;
using Luftborn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Infrastructure.Persistence
{
    public partial class LuftbornContext
    {
        public DbSet<Department> Departments { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
