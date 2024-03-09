using Luftborn.Domain.IdentityEntities;

namespace Luftborn.Infrastructure.Persistence
{
    public class DataInitializer : IDataInitializer
    {
        public List<ApplicationRole> GetInitialRoles()
        {
            return new List<ApplicationRole>
            {
                new ApplicationRole{Id="1",Name="admin",NormalizedName="ADMIN"},
                new ApplicationRole{Id="2",Name="user",NormalizedName="USER"}
            };
        }
    }
}
