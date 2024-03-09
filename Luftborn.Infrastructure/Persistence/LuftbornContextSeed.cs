using Luftborn.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Luftborn.Infrastructure.Persistence
{
    public class LuftbornContextSeed
    {
        public static async Task SeedAsync(LuftbornContext luftbornContext, ILogger<LuftbornContextSeed> logger)
        {
            if (!luftbornContext.Departments.Any())
            {
                luftbornContext.Departments.AddRange(GetPreconfiguredProducts());
                await luftbornContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associated with context {nameof(luftbornContext)}");
            }
        }
        public static List<Department> GetPreconfiguredProducts()
        {
            return new List<Department>
            {
                new Department {Code="001",Name="HR",isActive=true},
                new Department {Code="002",Name="IT",isActive=true}
            };
        }
    }
}
