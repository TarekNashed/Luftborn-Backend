using Luftborn.Domain.Entities;
using Luftborn.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Infrastructure.Persistence
{
    public partial class LuftbornContext:IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        private readonly IDataInitializer _dataInitializer;
        public LuftbornContext(DbContextOptions<LuftbornContext> options, IDataInitializer dataInitializer) : base(options) => _dataInitializer = dataInitializer;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(x => x.FirstName).HasMaxLength(50);
                b.Property(x => x.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.HasData(_dataInitializer.GetInitialRoles());
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .IsClustered(false);

                entity.ToTable("Departments");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
