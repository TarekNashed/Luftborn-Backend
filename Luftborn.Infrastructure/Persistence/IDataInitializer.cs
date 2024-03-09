using Luftborn.Domain.IdentityEntities;

namespace Luftborn.Infrastructure.Persistence
{
    public interface IDataInitializer
    {
        List<ApplicationRole> GetInitialRoles();
    }
}
