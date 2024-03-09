namespace Luftborn.Domain.Common
{
    public abstract class EntityBase
    {
        public int ID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
