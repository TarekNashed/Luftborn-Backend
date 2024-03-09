using Luftborn.Domain.Common;

namespace Luftborn.Domain.Entities
{
    public partial class Department:EntityBase
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool isActive { get; set; } = false;
    }
}
